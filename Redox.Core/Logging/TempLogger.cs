using System;
using System.Collections.Generic;

namespace Redox.Core.Logging
{
    public sealed class TempLogger
    {
        private static TempLogger instance;
        
        public readonly Queue<LogMessage> Messages = new Queue<LogMessage>();
        
        public void Log(string message, params object[] args)
        {
           this.Write(message, LogType.Message, args);
        }

        public void Info(string message, params object[] args)
        {
            this.Write(message, LogType.Info, args);
        }

        public void Warning(string message, params object[] args)
        {
            this.Write(message, LogType.Warning, args);
        }

        public void Error(string message, params object[] args)
        {
            this.Write(message, LogType.Error, args);
        }

        public void Exception(Exception exception, bool verbose = false)
        {
            if (exception == null) return;
            string msg = verbose ? exception.ToString() : exception.Message;
            this.Write(msg, LogType.Exception, new object());
        }

        public void Debug(string message, params object[] args)
        {
            this.Write(message, LogType.Debug, args);
        }

        private void Write(string message, LogType type, params object[] args)
        {
            if (string.IsNullOrEmpty(message)) return;
            LogMessage logMessage;
            logMessage.Type = type;
            logMessage.content = string.Format(message, args);
            Messages.Enqueue(logMessage);
        }



        public static TempLogger Get()
        {
            return instance ??= new TempLogger();
        }
    }

    public struct LogMessage
    {
        public LogType Type;
        public string content;
    }

    public enum LogType
    {
        Message, Info, Warning, Error, Exception, Debug
    }
}