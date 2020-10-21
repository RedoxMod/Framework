using System.Reflection;
using System.Threading.Tasks;

namespace Redox.API.Components
{
    /// <summary>
    /// The generic component provider interface.
    /// </summary>
    public interface IComponentProvider
    {
      //  Task RegisterAssemblyAsync(Assembly assembly);

        void RegisterType<TService, TImplementation>() where TImplementation : IBaseComponent;

        TService ResolveComponent<TService>();

        Task StartAllAsync();
    }
}