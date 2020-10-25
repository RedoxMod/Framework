using System.Threading.Tasks;
using Redox.API.Plugins;

namespace Redox.API.Extensions
{
    /// <summary>
    /// A extension is a plugin that expands the functionality of the core.
    /// </summary>
    public interface IExtension
    {
        PluginInfo Info { get;  set; }

        PluginContact Contact { get; set; }
        
        Task LoadAsync();

        Task UnloadAsync();
    }
}