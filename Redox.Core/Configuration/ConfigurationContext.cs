using System;
using System.Threading.Tasks;

using Redox.API.Configuration;
using Redox.API.Plugins;

namespace Redox.Core.Configuration
{
    public class ConfigurationContext : IConfigurationContext
    {
        public IConfiguration Configuration { get; }
        
        public ConfigInfo Info { get; }
        public IBasePlugin Plugin { get; }
        
        
        public bool Exists { get; }
        public async Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task LoadAsync()
        {
            throw new NotImplementedException();
        }
        
        public ConfigurationContext(IConfiguration configuration, ConfigInfo info, IBasePlugin plugin)
        {
            Configuration = configuration;
            Info = info;
            Plugin = plugin;
        }
    }
}