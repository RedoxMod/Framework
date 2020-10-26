using System.Threading.Tasks;
using Redox.API.Components;

namespace Redox.API.Plugins
{
    public interface IPluginManager : IBaseComponent
    {
        void AddPlugin(IBasePlugin plugin);
        
        void RemovePlugin(IBasePlugin plugin);
        
        Task LoadPlugin(string name);

        Task ReloadPlugin(string name);
        
        Task UnloadPlugin(string name);
    }
}