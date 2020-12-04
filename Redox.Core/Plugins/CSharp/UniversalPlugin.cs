using Redox.API;
using Redox.API.Logging;

namespace Redox.Core.Plugins.CSharp
{
    public abstract class UniversalPlugin : CSPlugin
    {
        protected IServer Server  => RedoxMod.GetMod().Components.ResolveComponent<IServer>();
        protected ILogger Logger => RedoxMod.GetMod().Components.ResolveComponent<ILogger>();
    }
}