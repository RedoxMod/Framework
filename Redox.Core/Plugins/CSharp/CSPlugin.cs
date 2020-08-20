using System;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Commands;
using Redox.API.Configuration;
using Redox.API.Plugins;
using Redox.API.Roles;

namespace Redox.Core.Plugins.CSharp
{
    /// <summary>
    /// Generic presentation for a CSharp Plugin.
    /// </summary>
    public abstract class CSPlugin : IBasePlugin
    {
        public PluginInfo Info { get; set; }
        public PluginContact Contact { get; set; }
        
        public PluginAnalytics Analytics { get; set; }

        public IConfigurationProvider Configurations { get; }
        public ICommandProvider Commands { get; }
        
        public IRoleProvider Roles { get; }
        
        public string CurrentPath { get; }


        protected abstract Task OnEnableAsync();
        protected abstract Task OnDisableAsync();
        
        protected virtual Task LoadTranslationsAsync()
        {
            return Task.CompletedTask;
        }
        public Task OnLoadAsync()
        {
            Type type = this.GetType();
            this.Info = type.GetCustomAttribute<PluginInfo>() ?? new PluginInfo();
            this.Contact = type.GetCustomAttribute<PluginContact>() ?? new PluginContact();
            
            //TODO: Insert command loader, config loader and translations loader.

            return Task.CompletedTask;
        }

        public Task OnUnloadAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}