using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Data;

namespace Redox.API.Roles
{
    public interface IRolesProvider : IBaseComponent, ISaveable
    {
        Task AddAsync(IRole role);

        Task RemoveAsync(string name);

        Task<IRole> GetAsync(string name);

        Task<bool> HasAsync(string name);
    }
}