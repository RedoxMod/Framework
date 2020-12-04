using System.IO;
using System.Threading.Tasks;
using Redox.API.Plugins;

namespace Redox.API.Engines
{
    /// <summary>
    /// Base representation of a plugin engine.
    /// </summary>
    public interface IPluginEngine
    {
        Task RunAsync();
        Task ShutdownAsync();
    }
}