using System;
using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Plugins;

namespace Redox.API.Configuration
{
    public interface IConfigurationProvider : IBaseComponent
    {
        Task RegisterAsync(IBasePlugin plugin);
        Task<IConfiguration> ResolveAsync(IBasePlugin plugin, string name);
    }
}