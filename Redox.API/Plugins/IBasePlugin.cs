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
        
        string CurrentPath { get; }
        
        
        Task OnLoadAsync();
        Task OnUnloadAsync();
    }
}