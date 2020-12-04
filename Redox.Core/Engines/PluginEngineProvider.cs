using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Engines;

namespace Redox.Core.Engines
{
    [ComponentInfo("PluginEngineProvider", LoadPriority.Medium)]
    public sealed class PluginEngineProvider : IPluginEngineProvider
    {
        private readonly IDictionary<string, IPluginEngine> _engines = new Dictionary<string, IPluginEngine>();

        public Task RegisterAsync<TEngine>() where TEngine : IPluginEngine
        {
            PluginEngine engine = (PluginEngine)Activator.CreateInstance(typeof(TEngine));
            if (!_engines.ContainsKey(engine.Name))
            {
                _engines.Add(engine.Name, engine);
            }
            return Task.CompletedTask;
        }

        public IPluginEngine Resolve<TEngine>() where TEngine : IPluginEngine
        {
            return _engines.Values.FirstOrDefault(x => x is TEngine);
        }

        public Task UnregisterAsync<TEngine>() where TEngine : IPluginEngine
        {
            
            return Task.CompletedTask;
        }

        public async Task StartAllAsync()
        {
            foreach (PluginEngine engine in _engines.Values)
            {
                await engine.StartAsync();
            }
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }
    }
}