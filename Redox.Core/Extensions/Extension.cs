using System;
using System.Reflection;
using System.Threading.Tasks;
using Redox.API.Extensions;
using Redox.API.Plugins;
using Redox.Core.Plugins.CSharp;

namespace Redox.Core.Extensions
{
    public abstract class Extension : IExtension
    {
        public IPluginInfo Info { get; private set; }
        public IPluginSupport Support { get; private set; }

        protected abstract Task OnEnableAsync();
        protected abstract Task OnDisableAsync();
        
        public async Task LoadAsync()
        {
            Type type = this.GetType();
            
            this.Info = type.GetCustomAttribute<PluginInfoAttribute>() ?? new PluginInfoAttribute();
            this.Support = type.GetCustomAttribute<PluginSupportAttribute>() ?? new PluginSupportAttribute();
            await OnEnableAsync();
        }

        public async Task UnloadAsync()
        {
            await OnDisableAsync();
        }
    }
}