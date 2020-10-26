using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Engines;

namespace Redox.Core.Engines
{
    [ComponentInfo("PluginEngineProvider", LoadPriority.Medium)]
    public sealed class PluginEngineProvider : IPluginEngineProvider
    {
        private IDictionary<string, IPluginEngine> _engines;

        public Task RegisterAsync<TEngine>() where TEngine : IPluginEngine
        {
            IPluginEngine engine = Activator.CreateInstance<TEngine>();
            if (!_engines.ContainsKey(engine.Name))
            {
                _engines.Add(engine.Name, engine);
            }

            return Task.CompletedTask;
        }

        public Task<IPluginEngine> ResolveAsync<TEngine>() where TEngine : IPluginEngine
        {
            return null;
        }

        public Task UnregisterAsync<TEngine>() where TEngine : IPluginEngine
        {
            return Task.CompletedTask;
        }

        public Task RunAsync()
        {
            _engines = new Dictionary<string, IPluginEngine>();
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }
    }
}