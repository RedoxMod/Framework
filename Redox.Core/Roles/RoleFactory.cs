using System.Threading.Tasks;
using Redox.API.Roles;

namespace Redox.Core.Roles
{
    public sealed class RoleFactory : IRoleFactory
    {
        public Task<RoleSettings> CreateRoleSettingsAsync(bool defaultrole, bool masterrole)
        {
            throw new System.NotImplementedException();
        }

        public Task<IRole> CreateRoleAsync(string name, string summary, RoleSettings settings)
        {
            throw new System.NotImplementedException();
        }
    }
}