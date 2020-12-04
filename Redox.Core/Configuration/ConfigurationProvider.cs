using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Configuration;
using Redox.API.Plugins;
using Redox.Core.Helpers;

namespace Redox.Core.Configuration
{
    [ComponentInfo("ConfigurationProvider", LoadPriority.Low)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IDictionary<string, IList<IConfigurationContext>> _configurations = new Dictionary<string, IList<IConfigurationContext>>();

        public Task RegisterAsync(IBasePlugin plugin)
        {
            try
            {
                string title = plugin.Info.Title;
                if (_configurations.ContainsKey(title))
                    return Task.CompletedTask;
                
                Assembly assembly = plugin.GetType().Assembly;
            
                //Let's collect all the configuration classes.
                IEnumerable<Type> configs = (from x in assembly.GetExportedTypes()
                    where x.IsSubclassOf(typeof(PluginConfiguration)) && x.GetCustomAttribute<ConfigInfo>() != null
                    select x);
                //Looping through the collected configurations and load/save them.
                foreach (Type type in configs)
                {
                    try
                    {
                        IConfiguration configuration = (IConfiguration) Activator.CreateInstance(type);
                        ConfigInfo info = type.GetCustomAttribute<ConfigInfo>();
                        IConfigurationContext context = new ConfigurationContext(configuration, info, plugin);
                        if(!_configurations.ContainsKey(title))
                            _configurations.Add(title, new List<IConfigurationContext>());
                        _configurations[title].Add(context);
                    }
                    catch (Exception e)
                    {
                        RedoxMod.GetMod().Logger.Error(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         
            return Task.CompletedTask;
        }

        public Task<IConfiguration> ResolveAsync(IBasePlugin plugin, string name)
        {
            if (!_configurations.ContainsKey(plugin.Info.Title))
                return null;
            var context = _configurations[plugin.Info.Title].FirstOrDefault(x => x.Info.Name == name);
            if (context == null) return null;
            
            ConfigHelper.SaveOrLoadConfig(plugin, context);
            return Task.FromResult(context.Configuration);
        }

        public static IConfigurationProvider Get()
        {
            return RedoxMod.GetMod().Components.ResolveComponent<IConfigurationProvider>();
        }
        public Task RunAsync()
        {
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            _configurations.Clear();
            return Task.CompletedTask;
        }
    }
}