using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Configuration;
using Redox.API.Plugins;

namespace Redox.Core.Configuration
{
    [ComponentInfo("ConfigurationProvider", LoadPriority.Medium)]
    public sealed class ConfigurationProvider  : IBaseComponent, IConfigurationProvider
    {
        private IDictionary<IBasePlugin, IList<IConfigurationContext>> _configurations;

        public Task RegisterAsync(IBasePlugin plugin)
        {
            if(_configurations.ContainsKey(plugin))
                _configurations[plugin].Clear();
            
            Assembly assembly = plugin.GetType().Assembly;
            
            IEnumerable<Type> configs = (from x in assembly.GetExportedTypes()
                where x.IsSubclassOf(typeof(IConfiguration)) && x.GetCustomAttribute<ConfigInfo>() != null
                select x);

            foreach (Type type in configs)
            {
                object defaultconfiguration = Activator.CreateInstance(type);
                IConfiguration configuration = (IConfiguration) defaultconfiguration;
                ConfigInfo info = type.GetCustomAttribute<ConfigInfo>();
                IConfigurationContext context = new ConfigurationContext(configuration, defaultconfiguration, info, plugin);
                
                if(!_configurations.ContainsKey(plugin))
                    _configurations.Add(plugin, new List<IConfigurationContext>());
                _configurations[plugin].Add(context);
            }

            return Task.CompletedTask;
        }

        public Task<IConfiguration> ResolveAsync(IBasePlugin plugin, string name)
        {
            if (!_configurations.ContainsKey(plugin))
                return null;
            return Task.FromResult(_configurations[plugin].FirstOrDefault(x => x.Info.Name == name)?.Configuration);
        }
        public Task RunAsync()
        {
            _configurations = new Dictionary<IBasePlugin, IList<IConfigurationContext>>();
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            _configurations.Clear();
            return Task.CompletedTask;
        }
    }
}