using System;
using Redox.API.Components;

namespace Redox.API.Logging
{
    public interface ILogger
    {
        void Log(string message, params object[] args);
        
        void Info(string message, params object[] args);
        
        void Warning(string message, params object[] args);
        
        void Error(string message, params object[] args);
        
        void Exception(Exception exception, bool verbose = false);

        void Debug(string message,  params object[] args);
    }
}