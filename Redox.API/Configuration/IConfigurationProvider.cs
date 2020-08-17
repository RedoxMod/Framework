using System;
using System.Threading.Tasks;
using Redox.API.Plugins;

namespace Redox.API.Configuration
{
    public interface IConfigurationProvider
    {
        Task RegisterAsync(IBasePlugin plugin);
        Task<IConfiguration> ResolveAsync(IBasePlugin plugin, string name);
    }
}