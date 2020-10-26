using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Redox.API;
using Redox.API.Engines;

namespace Redox.Core.Plugins.CSharp
{
    public sealed class CSharpEngine : IPluginEngine
    {
        private readonly Dictionary<string, CSPlugin> _plugins = new Dictionary<string, CSPlugin>();
        
        private readonly Regex _fileNameRegex = new Regex(@"^[a-zA-Z0-9-_]+$");
        
        public string Name => "CSharp";
        public string Extension => "*.dll";


        private ILogger _logger => RedoxMod.GetMod().Logger;
        
        public async Task StartAsync()
        {
            foreach (string file in Directory.GetDirectories(RedoxMod.GetMod().PluginDirectory, Extension))
            {
                await LoadAsync(new FileInfo(file));
            }
        }

        public async Task LoadAsync(FileInfo info)
        {
            string name = Path.GetFileNameWithoutExtension(info.Name);

            if (!_fileNameRegex.IsMatch(name))
            {
                _logger.Warning("[CSharp-Warning] Plugin {0} has illegal characters in its name. Allowed: \"{1}\"", name, "a-zA-Z0-9-_");
                return;
            }
            _logger.Debug("[CSharpEngine] Loading plugin {0}", name);
            if (_plugins.ContainsKey(name))
            {
                //TODO: Add some code for already loaded plugins.
            }
            else
            {
                try
                {
                    //Reads the assembly (.dll)
                    byte[] data = File.ReadAllBytes(info.FullName);

                    //Lets check if file isn't empty.
                    if (data.Length != 0)
                    {
                        Assembly assembly = Assembly.Load(data);

                        Type type = assembly.GetExportedTypes()
                            .FirstOrDefault(x => x.IsSubclassOf(typeof(CSPlugin)) && !x.IsAbstract);
                        if (type != null)
                        {
                            //TODO: Create new instance of the type and Load the plugin into the plugin manager.

                            //Lets create an instance of the class so we can access its members
                            CSPlugin plugin = (CSPlugin) Activator.CreateInstance(type);
                            if (plugin != null)
                            {
                                plugin.FileInfo = info;
                                await plugin.LoadAsync();
                                _plugins.Add(name, plugin);
                                RedoxMod.GetMod().PluginManager.AddPlugin(plugin);
                                _logger.Info("[CSharp] Successfully loaded plugin {0}, Authors: {1}, V({2}) ({3})",
                                        name, plugin.Info.Title, plugin.Info.Authors, plugin.Info.Version,
                                        plugin.Info.Description);
                            }
                        }
                        else
                        {
                            
                        }
                    }

                }
                catch (Exception e)
                {
                    _logger.Error("[CSharp-Error] Failed to load plugin {0} due to error: {1}", name, e.Message);
                   // _logger.LogWarning("[CSharp-Warning] Consider contacting the devs at: https://redoxmodding.org");
                }
            }
        }

        public async Task UnloadAsync(string name)
        {
            if(_plugins.TryGetValue(name, out CSPlugin plugin) && plugin.Loaded)
            {
                try
                {
                    await plugin.UnloadAsync();
                }
                catch (Exception e)
                {
                    RedoxMod.GetMod().Logger.Error("[CSharp-Error] Failed to unload plugin {0} due to error: {1}", name,
                        e.Message);
                }
                finally
                {
                    RedoxMod.GetMod().Logger.Info("[CSharp] Unloaded plugin {0}", name);
                }
            }
            else
            {
                RedoxMod.GetMod().Logger.Warning("[CSharp] Couldn't find plugin by name {0}", name);
            }
        }

        public async Task ReloadAsync(string name)
        {
        }

        public async Task ReloadAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task UnloadAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}