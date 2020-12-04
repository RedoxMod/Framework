using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Redox.API.Engines;
using Redox.API.Logging;
using Redox.API.Plugins;
using Redox.Core.Logging;

namespace Redox.Core.Engines
{
    public abstract class PluginEngine : IPluginEngine
    {
        /// <summary>
        /// The name of the plugin engine.
        /// <para>Example: CSharpEngine</para>
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// The extension of the plugin.
        /// <para>Example: *.dll</para>
        /// </summary>
        public abstract string Extension { get; }

        
        protected readonly IDictionary<string, IBasePlugin> Plugins = new Dictionary<string, IBasePlugin>();
        protected ILogger Logger => RedoxMod.GetMod().Logger;
        
        
        /// <summary>
        /// Loads a plugin.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public abstract Task LoadAsync(FileInfo info);
        
        /// <summary>
        /// Starts the plugin engine.
        /// </summary>
        /// <returns></returns>
        public virtual async Task StartAsync()
        {
            Logger.Info("[{0}] Loading plugins....", Name);
            int count = 0;
            foreach (string file in Directory.GetFiles(RedoxMod.GetMod().PluginDirectory, Extension))
            {
                await LoadAsync(new FileInfo(file));
                count++;
            }
            Logger.Info("[{0}]  Found {1} plugins, loaded {2}", Name, count, Plugins.Count);
        }

        /// <summary>
        /// Unloads a plugin by name.
        /// </summary>
        /// <param name="name">The name/title of the plugin.</param>
        /// <returns></returns>
        public virtual async Task UnloadAsync(string name)
        {
            if(Plugins.TryGetValue(name, out IBasePlugin plugin) && plugin.State == PluginState.Loaded)
            {
                try
                {
                    await plugin.UnloadAsync();
                }
                catch (Exception e)
                {
                    Logger.Error("[CSharp-Error] Failed to unload plugin {0} due to error: {1}", name,
                        e.Message);
                }
                finally
                {
                    Logger.Info("[CSharp] Unloaded plugin {0}", name);
                }
            }
            else
            {
                Logger.Warning("[CSharp] Couldn't find plugin by name {0}", name);
            }
        }
        
        /// <summary>
        /// Reloads a plugin by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task ReloadAsync(string name)
        {
            if (Plugins.TryGetValue(name, out IBasePlugin plugin))
            {
                if (plugin.State == PluginState.Loaded)
                    await plugin.UnloadAsync();
                await plugin.LoadAsync();
            }
        }
        
        /// <summary>
        /// Reloads all plugins associated with this engine.
        /// </summary>
        /// <returns></returns>
        public virtual async Task ReloadAllAsync()
        {
            foreach (IBasePlugin plugin in Plugins.Values)
            {
                if (plugin.State == PluginState.Loaded)
                    await plugin.UnloadAsync();
                await plugin.LoadAsync();
            }
        }
        
        /// <summary>
        /// Unloads all plugins associated with this engine.
        /// </summary>
        /// <returns></returns>
        public virtual async Task UnloadAllAsync()
        {
            foreach (IBasePlugin plugin in Plugins.Values)
            {
                if (plugin.State == PluginState.Loaded)
                    await plugin.UnloadAsync();
            }
        }

        public async Task RunAsync()
        {
            await this.StartAsync();
        }

        public async Task ShutdownAsync()
        {
            await this.UnloadAllAsync();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}