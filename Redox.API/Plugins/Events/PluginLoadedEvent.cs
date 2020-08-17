using Redox.API.Eventing;

namespace Redox.API.Plugins.Events
{
    public class PluginLoadedEvent  : IEvent
    {
        public string Name => "OnPluginLoaded";
        /// <summary>
        /// The plugin that gets loaded.
        /// </summary>
        public IBasePlugin Plugin { get; }
        
        internal PluginLoadedEvent(IBasePlugin plugin)
        {
            this.Plugin = plugin;
        }
    }
}