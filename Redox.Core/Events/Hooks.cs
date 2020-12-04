using Redox.Core.Events.Player;
using Redox.Core.Events.Plugin;

namespace Redox.Core.Events
{
    internal static class Hooks
    {
        internal static void InitHooks()
        {
            PlayerEvents.Init();
            PluginEvents.Init();
        }
    }
}