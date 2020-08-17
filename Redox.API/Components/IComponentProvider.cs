using System.Reflection;
using System.Threading.Tasks;

namespace Redox.API.Components
{
    public interface IComponentProvider
    {
      //  Task RegisterAssemblyAsync(Assembly assembly);

        Task<IBaseComponent> RegisterTypeAsync<TComponent>() where TComponent : IBaseComponent;

        Task<IBaseComponent> ResolveComponentAsync<TComponent>(string name = "") where TComponent : IBaseComponent;

        Task StartAllAsync();
    }
}