using log4net.Core;
using log4net.Util;
using System;
using System.IO;

namespace GSCBase.Application.Exceptions.Logs
{
    public sealed class CallerActionPatternConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            if (state == null)
            {
                writer.Write(SystemInfo.NullText);
                return;
            }

            var loggingEvent = state as LoggingEvent;
            var actionInfo = loggingEvent == null ? null : loggingEvent.MessageObject as CallerLoggerInfo;

            if (actionInfo == null)
            {
                writer.Write(loggingEvent.MessageObject);
            }
            else
            {
                writer.Write(string.Format($"Action: {actionInfo.Action}\r\nCaminho: {actionInfo.Path}\r\nMetodo: {actionInfo.Metodo}\r\nQuery string: {actionInfo.QueryString}\r\nBody:{actionInfo.Body}\r\nMensagem: {actionInfo.Message}"));
            }
        }
    }
}
