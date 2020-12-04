using System.Globalization;
using System.Threading.Tasks;
using Redox.API.Data;
using Redox.API.Plugins;

namespace Redox.API.Localization
{
    public interface ITranslationsRegistration : ISaveable
    {
        
        IBasePlugin Plugin { get; }
        
        Task RegisterAsync(ITranslation translation);
        
        Task<string> TranslateAsync(CultureInfo culture, string key);
        
        Task<string> TranslateAsync(string lang, string key);
    }
}