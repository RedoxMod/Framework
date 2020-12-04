using System.Collections.Generic;
using Redox.API.Localization;

namespace Redox.Core.Localization
{
    public sealed class Translation : ITranslation
    {
        public string Language { get; }

        public IReadOnlyDictionary<string, string> Messages { get; }

        public Translation(string language, IReadOnlyDictionary<string, string> messages)
        {
            Language = language;
            Messages = messages;
        }
        
    }
}