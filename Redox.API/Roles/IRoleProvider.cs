using System.Threading.Tasks;
using Redox.API.Components;

namespace Redox.API.Roles
{
    public interface IRoleProvider : IBaseComponent
    {
        Task RegisterRoleAsync(IRole role);

        Task UnregisterRoleAsync(string name);

        Task<IRole> GetRoleAsync(string name);

        Task<bool> HasRoleAsync(string name);
    }
}