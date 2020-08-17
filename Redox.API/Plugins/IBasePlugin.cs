using Redox.API.Commands;
using Redox.API.Configuration;
using Redox.API.Eventing;
using Redox.API.Roles;

namespace Redox.API.Plugins
{
    public interface IBasePlugin
    {
        PluginInfo Info { get; }
        PluginContact Contact { get; }
        
        IConfigurationProvider Configurations { get; }
        
        ICommandProvider Commands { get; }
        IRoleProvider Roles { get; }
        
        string CurrentPath { get; }
        
        
        void OnLoad();
        void OnUnload();
    }
}