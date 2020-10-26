using System.Globalization;
using System.Threading.Tasks;
using Redox.API.Data;

namespace Redox.API.Localization
{
    public interface ITranslationsProvider : ISaveable
    {
        Task RegisterAsync(ITranslation translation);
        
        Task<string> TranslateAsync(CultureInfo culture, string key);
        
        Task<string> TranslateAsync(string lang, string key);
    }
}