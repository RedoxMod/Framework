using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Redox.Core.Logging;
using Redox.Core.Plugins.CSharp;

namespace Redox.Core.Engines
{
    public sealed class CSharpEngine : PluginEngine
    {
        private readonly Regex _fileNameRegex = new Regex(@"^[a-zA-Z0-9-_]+$");
        
        public override string Name => "CSharp";
        public override string Extension => "*.dll";
        
        public override async Task LoadAsync(FileInfo info)
        {
            string name = Path.GetFileNameWithoutExtension(info.Name);

            if (!_fileNameRegex.IsMatch(name))
            {
                Logger.Warning("[CSharp-Warning] Plugin {0} has illegal characters in its name. Allowed: \"{1}\"", name, "a-zA-Z0-9-_");
                return;
            }
            Logger.Debug("[CSharpEngine] Loading plugin {0}", name);
            if (Plugins.ContainsKey(name))
            {
                //TODO: Add some code for already loaded plugins.
            }
            else
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
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
                            CSPlugin plugin = Activator.CreateInstance(type) as CSPlugin;
                            if (plugin != null)
                            {
                                plugin.FileInfo = info;
                                await plugin.LoadAsync();
                                Plugins.Add(name, plugin);
                                RedoxMod.GetMod().PluginManager.AddPlugin(plugin);
                                Logger.Info("[CSharp] Successfully loaded plugin {0}, Authors: {1}, V({2}) ({3})",
                                    plugin.Info.Title, plugin.Info.Authors, plugin.Info.Version,
                                    plugin.Info.Description);
                            }
                            else
                            {
                                Logger.Warning("[CSharp] Plugin {0} seems to be null.", name);
                            }
                        }
                        //else
                           // _logger.Log();
                    }

                }
                catch (Exception e)
                {
                    Logger.Error("[CSharp-Error] Failed to load plugin {0} due to error: {1}", name, e.ToString());
                    // _logger.LogWarning("[CSharp-Warning] Consider contacting the devs at: https://redoxmodding.org");
                }
                finally
                {
                    stopwatch.Stop();
                    double elapsed = stopwatch.Elapsed.TotalMilliseconds;

                    if (elapsed > 200)
                    {
                        Logger.Log("Loading {0} took approximately {1} milliseconds", name, Math.Round(elapsed));
                    }
                }
            }
        }
    }
}