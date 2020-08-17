using System.Threading.Tasks;

namespace Redox.API.Engines
{
    public interface IPluginEngineProvider
    {
        Task RegisterAsync<TEngine>() where TEngine : IPluginEngine;

        Task<IPluginEngine> ResolveAsync<TEngine>(string name = "") where TEngine : IPluginEngine;

        Task UnregisterAsync<TEngine>(string name = "") where TEngine : IPluginEngine;
    }
}