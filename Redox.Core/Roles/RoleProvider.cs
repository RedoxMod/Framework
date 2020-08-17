using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Roles;

namespace Redox.Core.Roles
{
    [ComponentInfo("RoleProvider", LoadPriority.Medium)]
    public sealed class RoleProvider : IRoleProvider
    {
        public async Task RegisterRoleAsync(IRole role)
        {
            throw new System.NotImplementedException();
        }

        public async Task UnregisterRoleAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IRole> GetRoleAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> HasRoleAsync(string name)
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