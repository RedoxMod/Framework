using System.Collections.Generic;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Plugins;

namespace Redox.Core.Plugins
{
    [ComponentInfo("PluginManager", LoadPriority.Low)]
    public sealed class PluginManager : IPluginManager
    {
        private readonly IDictionary<string, IBasePlugin> _plugins = new Dictionary<string, IBasePlugin>();
        
        public void AddPlugin(in IBasePlugin plugin)
        {
            if (plugin == null)
                return;
            
            string title = plugin.Info.Title;
            if (!_plugins.ContainsKey(title))
            {
                _plugins.Add(title, plugin);
            }
        }
        
        public void RemovePlugin(in IBasePlugin plugin)
        {
            if (plugin == null)
                return;
            string title = plugin.Info.Title;
            if (_plugins.ContainsKey(title))
            {
                _plugins.Remove(title);
            }
        }

        public async Task LoadPlugin(string name)
        {
            
        }

        public async Task ReloadPlugin(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task UnloadPlugin(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task RunAsync()
        {
        }

        public async Task ShutdownAsync()
        {
        }
    }
}