using System.IO;
using System.Threading.Tasks;
using Redox.API.Plugins;

namespace Redox.API.Engines
{
    public delegate void PluginLoaded(IBasePlugin plugin);
    /// <summary>
    /// Base representation of a plugin engine.
    /// </summary>
    public interface IPluginEngine
    {
        /// <summary>
        /// The name of the plugin engine.
        /// <para>Example: CSharpEngine</para>
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The extension of the plugin.
        /// <para>Example: .dll</para>
        /// </summary>
        string Extension { get; }
        
        /// <summary>
        /// Starts the plugin engine.
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        
        /// <summary>
        /// Loads a plugin.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task LoadAsync(FileInfo info);

        /// <summary>
        /// Unloads a plugin by name.
        /// </summary>
        /// <param name="name">The name/title of the plugin.</param>
        /// <returns></returns>
        Task UnloadAsync(string name);

        /// <summary>
        /// Reloads a plugin by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task ReloadAsync(string name);

        /// <summary>
        /// Reloads all plugins associated with this engine.
        /// </summary>
        /// <returns></returns>
        Task ReloadAllAsync();

        /// <summary>
        /// Unloads all plugins associated with this engine.
        /// </summary>
        /// <returns></returns>
        Task UnloadAllAsync();
    }
}