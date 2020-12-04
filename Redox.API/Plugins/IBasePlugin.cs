using System.IO;
using System.Threading.Tasks;
using Redox.API.Localization;
using Redox.API.Roles;

namespace Redox.API.Plugins
{
    public interface IBasePlugin
    {
        IPluginInfo Info { get; }

        IPluginSupport Support { get; }
        
        IPluginAnalytics Analytics { get; }
        
        PluginState State { get; }
        
        IRolesProvider Roles { get; }
        
        ITranslationsRegistration Translations { get; }

        FileInfo FileInfo { get; }
        
        DirectoryInfo Directory { get; }
        
        Task LoadAsync();

        Task UnloadAsync();
        
        Task LoadMethodsAsync();

        Task<object> CallAsync(string name, params object[] args);

        object Call(string name, params object[] args);

        T Call<T>(string name, params object[] args);
    }
}