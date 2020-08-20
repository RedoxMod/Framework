using System;

namespace Redox.Core.Configuration.Redox
{
    [Serializable]
    public class RedoxMessages
    {
        public string CommandNotFound { get; private set; }

        public RedoxMessages Init()
        {
            this.CommandNotFound = "Unknown command \"{0}\"";
            
            return this;
        }
    }
}