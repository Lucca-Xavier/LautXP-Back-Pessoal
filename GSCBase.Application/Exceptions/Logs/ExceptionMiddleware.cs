using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace GSCBase.Application.Exceptions.Logs
{
    public class ExceptionMiddleware
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ExceptionMiddleware));
        private readonly RequestDelegate next;

        static ExceptionMiddleware()
        {
            string filePath = Directory.GetCurrentDirectory() + "/wwwroot/log4net.config";

            if (!File.Exists(filePath))
            {
                CreateLogFile(filePath, "GSCBase");
            }

            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(filePath));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            // Podemos criar uma exceção de negócios para mandar a mensagem para a interface 
            // catch (SGCException ex)
            // {
            //     context.Response.StatusCode = 422;
            //     context.Response.ContentType = "application/json";
            //     var mensagem = JsonConvert.SerializeObject(new { mensagem = ex.Message }, Newtonsoft.Json.Formatting.Indented);
            //     await context.Response.WriteAsync(mensagem).ConfigureAwait(false);
            // }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var logInfo = await CreateLogInformation(context, ex);
                log.Error(logInfo, ex);
            }
        }

        private async Task<CallerLoggerInfo> CreateLogInformation(HttpContext context, Exception ex)
        {
            var request = context.Request;
            var response = context.Response;
            var body = await new StreamReader(request.Body).ReadToEndAsync();

            var mensagem = JsonConvert.SerializeObject(new { message = ex.Message }, Newtonsoft.Json.Formatting.Indented);
            await response.WriteAsync(mensagem).ConfigureAwait(false);
            var url = $"{request.Scheme}://{request.Host.Value}{request.Path.Value}";
            var logInfo = new CallerLoggerInfo(url, request.Method, request.Path.Value, request.QueryString.ToString(), body, ex.Message);

            return logInfo;
        }

        public static void CreateLogFile(string filePath, string appName)
        {
            string text =
                            $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                                <log4net>
                                  <appender name=""FileAppender"" type=""log4net.Appender.RollingFileAppender"">
                                    <file value=""log/""/>
                                    <datePattern value=""'log'yyyyMMdd'.txt'""/>
                                    <staticLogFileName value=""false""/>
                                    <appendToFile value=""true""/>
                                    <rollingStyle value=""Date""/>
                                    <lockingModel type=""log4net.Appender.FileAppender+MinimalLock""/>
                                    <layout type=""log4net.Layout.PatternLayout""/>
                                    <maxSizeRollBackups value=""10""/>
                                    <layout type=""{appName}.Application.Exceptions.Logs.CallerActionPatternLayout, {appName}.Application"">
                                      <conversionPattern value=""----------------------------------------------------------------%newlineExceção logada em %date%newlineLevel: %level%newline%callerinfo{"{action}"}%newlineStack trace:%newline""/>
                                    </layout>
                                    <filter type=""log4net.Filter.LevelRangeFilter"">
                                      <levelMin value=""DEBUG""/>
                                    </filter>    
                                  </appender>
                                  <root>
                                    <level value=""DEBUG""/>
                                    <appender-ref ref=""FileAppender""/>
                                  </root>
                                </log4net>";

            using (File.Create(filePath)) ;
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(text);
            }
        }
    }
}
