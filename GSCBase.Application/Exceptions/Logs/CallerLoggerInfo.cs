using System;

namespace GSCBase.Application.Exceptions.Logs
{
    public sealed class CallerLoggerInfo
    {
        public CallerLoggerInfo(string actionUrl, string metodo, string path, string queryString, string body, string message)
        {
            this.Action = actionUrl;
            this.Metodo = metodo;
            this.Path = path;
            this.QueryString = queryString;
            this.Body = body.Replace("{", "{{").Replace("}", "}}");
            this.Message = message;
        }
        public string Action { get; }
        public string Metodo { get; }
        public string Path { get; }
        public string QueryString { get; }
        public string Body { get; }
        public string Message { get; }
    }
}
