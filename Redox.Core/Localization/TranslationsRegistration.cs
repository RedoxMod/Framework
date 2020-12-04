using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Localization;
using Redox.API.Plugins;
using Redox.Core.Serialization;

namespace Redox.Core.Localization
{
    public class TranslationsRegistration : ITranslationsRegistration
    {
        private string _filePath => Path.Combine(Plugin.Directory.FullName, "Translations.json");
        public bool Exists => File.Exists(_filePath);
        
        private IList<ITranslation> _translations = new List<ITranslation>();

        public IBasePlugin Plugin { get; }

        public Task RegisterAsync(ITranslation translation)
        {
            if (!_translations.Any((x => x.Language == translation.Language)))
            {
                _translations.Add(translation);
            }
            return Task.CompletedTask;
        }

        public async Task<string> TranslateAsync(CultureInfo culture, string key)
        {
            string language = culture.Parent.ToString();
            return await TranslateAsync(language, key);
        }

        public Task<string> TranslateAsync(string lang, string key)
        {
            ITranslation translation = _translations.FirstOrDefault(x => x.Language == lang);
            if (translation != null)
            {
                string message;
                if (translation.Messages.TryGetValue(key, out message))
                {
                    return Task.FromResult(message);
                }
                return null;
            }

            return null;
        }

        
        public Task SaveAsync()
        {
            JsonSerializer.ToFile(_filePath, _translations);
            return Task.CompletedTask;
        }

        public Task LoadAsync()
        {
            if (Exists)
            {
                _translations = JsonSerializer.FromFile<IList<ITranslation>>(_filePath);
            }

            return Task.CompletedTask;
        }
        
        public TranslationsRegistration(in IBasePlugin plugin)
        {
            Plugin = plugin;
        }
    }
}