using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Localization;
using Redox.API.Plugins;

namespace Redox.Core.Localization
{
    public class TranslationsProvider : ITranslationsProvider
    {
        private IBasePlugin _plugin;

        public bool Exists => File.Exists(Path.Combine(_plugin.FileInfo.DirectoryName!, "Translations.json"));
        
        private IList<ITranslation> _translations = new List<ITranslation>();
        
        public  Task RegisterAsync(ITranslation translation)
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

        
        public async Task SaveAsync()
        {
            //TODO
        }

        public async Task LoadAsync()
        {
            //TODO
        }
    }
}