using System.Collections.Generic;

namespace Redox.API.Localization
{
    public interface ITranslation : IDictionary<string, string>
    {
        /// <summary>
        /// The language of the translation.
        /// </summary>
        string Language { get; }
    }
}