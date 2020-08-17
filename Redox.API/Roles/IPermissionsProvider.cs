using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Components;

namespace Redox.API.Roles
{
    public interface IPermissionsProvider : IBaseComponent
    {
        Task GivePermissionToPlayerAsync(ulong playerid, string permission);
        
        Task RemovePermissionFromPlayerAsync(ulong playerid, string permission);

        Task<IEnumerable<string>> GetPlayerPermissionsAsync(ulong playerid);
        
    }
}