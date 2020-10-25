using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Data;

namespace Redox.API.Roles
{
    public interface IPermissionsProvider : IBaseComponent, ISaveable
    {
        Task GiveAsync(ulong playerId, string permission);
        
        Task RemoveAsync(ulong playerId, string permission);

        Task<IEnumerable<string>> GetAsync(ulong playerId);
    }
}