using System.Threading.Tasks;

namespace Redox.API.Components
{
    /// <summary>
    /// Represents a generic base component.
    /// </summary>
    public interface IBaseComponent
    {
        Task RunAsync();

        Task ShutdownAsync();
    }
}