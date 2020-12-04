using Redox.API.Plugins;

namespace Redox.Core.Events.Plugin
{
    public static class PluginEvents
    {
        public static event PluginLoadedDelegate OnPluginLoaded;
        public static event PluginUnloadedDelegate OnPluginUnloaded;
        public static event PluginReloadedDelegate OnPluginReloaded;
        
        internal static void Init()
        {
            OnPluginLoaded = delegate(IBasePlugin plugin) {  };
            OnPluginUnloaded = delegate(IBasePlugin plugin) {  };
            OnPluginUnloaded = delegate(IBasePlugin plugin) {  };
        }
    }
}