using System.Threading.Tasks;
using Redox.API.Plugins;

namespace Redox.API.Extensions
{
    /// <summary>
    /// A extension is a plugin that expands the functionality of the core.
    /// </summary>
    public interface IExtension
    {
        IPluginInfo Info { get; }

        IPluginSupport Support { get;}
        
        Task LoadAsync();

        Task UnloadAsync();
    }
}