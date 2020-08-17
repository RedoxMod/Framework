using System;
using System.IO;
using System.Threading.Tasks;
using Redox.API;
using Redox.API.Components;

namespace Redox.Core
{
    [ComponentInfo("logger", LoadPriority.High)]
    public sealed class Logger : ILogger
    {
        private TextWriter writer;
        private string datetime;
        
        public void Log(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public void LogWarning(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public void LogError(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public void LogException(Exception exception, bool verbose = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(verbose ? exception.ToString() : exception.Message);
            Console.ResetColor();
        }

        internal Logger() {}

        public Task RunAsync()
        {
            datetime = DateTime.Now.ToString("DD-mm-YYYY");
            writer = new StreamWriter(File.Open("somePath", FileMode.OpenOrCreate));
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            writer?.Close();
            return Task.CompletedTask;
        }
    }
}