using System.Threading.Tasks;
using Redox.API.Components;

namespace Redox.API.Engines
{
    public interface IPluginEngineProvider : IBaseComponent
    {
        Task RegisterAsync<TEngine>() where TEngine : IPluginEngine;

        Task<IPluginEngine> ResolveAsync<TEngine>() where TEngine : IPluginEngine;
        
        Task UnregisterAsync<TEngine>() where TEngine : IPluginEngine;
    }
}