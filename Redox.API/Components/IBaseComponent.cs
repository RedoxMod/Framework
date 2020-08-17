using System.Threading.Tasks;

namespace Redox.API.Components
{
    public interface IBaseComponent
    {
        Task RunAsync();

        Task ShutdownAsync();
    }
}