using System;
using Redox.API.Configuration;
using Redox.API.Plugins;

namespace Redox.Core.Configuration
{
    public class ConfigurationContext : IConfigurationContext
    {
        public IConfiguration Configuration { get; }
        
        public object DefaultConfiguration { get; }
        public ConfigInfo Info { get; }
        public IBasePlugin Plugin { get; }
        
        public ConfigurationContext(IConfiguration configuration, object defaultConfiguration, ConfigInfo info, IBasePlugin plugin)
        {
            Configuration = configuration;
            DefaultConfiguration = defaultConfiguration;
            Info = info;
            Plugin = plugin;
        }
    }
}