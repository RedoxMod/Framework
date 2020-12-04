using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Redox.Core.Extensions;

namespace Redox.Core.Engines
{
    public sealed class ExtensionsEngine : PluginEngine
    {
        
        public override string Name => "Extensions";

        public override string Extension => "*.dll";
        
        private const string GAME_EXTENSION = "Redox.Game.*.dll";
        
        private readonly IDictionary<string, Extension> _extensions = new Dictionary<string, Extension>();
        
        public override async Task StartAsync()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string gameFile = Directory.GetFiles(assemblyPath, GAME_EXTENSION).FirstOrDefault();
            List<string> extensions = Directory.GetFiles(RedoxMod.GetMod().ExtensionsDirectory, Extension).ToList();
            
            //Let's make sure the game extension always get's loaded first.
            if(!string.IsNullOrEmpty(gameFile)) extensions.Insert(0, gameFile);
            foreach (string file in extensions)
            {
                await LoadAsync(new FileInfo(file));
            }
        }

        public override async Task LoadAsync(FileInfo info)
        {
            if (info != null)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(info.Name);
                    if (!_extensions.ContainsKey(name))
                    {
                        byte[] data = File.ReadAllBytes(info.FullName);
                        if (data.Length > 0)
                        {
                            Assembly assembly = Assembly.Load(data);
                            Type type = assembly.GetExportedTypes()
                                .FirstOrDefault(x => x.IsSubclassOf(typeof(Extension)) && !x.IsAbstract);
                            if (type != null)
                            {
                                Extension extension = (Extension) Activator.CreateInstance(type);
                                if (extension != null)
                                {
                                    await extension.LoadAsync();
                                    _extensions.Add(name, extension);
                                    RedoxMod.GetMod().TempLogger.Info("[Extensions] Loaded extension {0} ({1})",
                                        extension.Info.Title, extension.Info.Authors);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public override Task UnloadAsync(string name)
        {
            return Task.CompletedTask;
        }

        public override Task ReloadAsync(string name)
        {
            return Task.CompletedTask;
        }

        public override Task ReloadAllAsync()
        {
            return Task.CompletedTask;
        }

        public override Task UnloadAllAsync()
        {
            return Task.CompletedTask;
        }
    }
}