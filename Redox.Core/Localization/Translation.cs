using System.Collections.Generic;
using Redox.API.Localization;

namespace Redox.Core.Localization
{
    public sealed class Translation : ITranslation
    {
        public string Language { get; }

        public IDictionary<string, string> Messages { get; }

        public Translation(string language, IDictionary<string, string> messages)
        {
            Language = language;
            Messages = messages;
        }
        
    }
}