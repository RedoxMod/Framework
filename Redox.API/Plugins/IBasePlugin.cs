using System.IO;
using System.Threading.Tasks;
using Redox.API.Commands;
using Redox.API.Configuration;
using Redox.API.Eventing;
using Redox.API.Roles;

namespace Redox.API.Plugins
{
    public interface IBasePlugin
    {
        PluginInfo Info { get;  set; }

        PluginContact Contact { get; set; }
        
        PluginAnalytics Analytics { get; set; }
        
        IConfigurationProvider Configurations { get; }
        
        ICommandProvider Commands { get; }

        IRoleProvider Roles { get; }

        FileInfo FileInfo { get; }
        
        Task LoadAsync();

        Task UnloadAsync();
        
        Task LoadMethodsAsync();

        Task<object> CallAsync(string name, params object[] args);

        object Call(string name, params object[] args);

        T Call<T>(string name, params object[] args);
    }
}