using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Roles;

namespace Redox.Core.Roles
{
    [ComponentInfo("PermissionsProvider", LoadPriority.Medium)]
    public sealed class PermissionsProvider : IPermissionsProvider
    {
        public async Task GivePermissionToPlayerAsync(ulong playerid, string permission)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemovePermissionFromPlayerAsync(ulong playerid, string permission)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetPlayerPermissionsAsync(ulong playerid)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task RunAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task ShutdownAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}