using System;
using Redox.API.Components;

namespace Redox.API
{
    public interface ILogger : IBaseComponent
    {
        void Log(string message, params object[] args);
        
        void LogInfo(string message, params object[] args);
        
        void LogWarning(string message, params object[] args);
        
        void LogError(string message, params object[] args);
        
        void LogException(Exception exception, bool verbose = false);
    }
}