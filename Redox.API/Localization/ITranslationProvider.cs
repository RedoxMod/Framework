using System.Globalization;
using System.Threading.Tasks;

namespace Redox.API.Localization
{
    public interface ITranslationProvider
    {
        Task RegisterAsync(ITranslation translation);
        
        Task<string> TranslateAsync(CultureInfo culture, string key);
        
        Task<string> TranslateAsync(string lang, string key);
    }
}