using System.Threading.Tasks;

namespace Redox.API.Roles
{
    /// <summary>
    /// Base representation of a role factory.
    /// </summary>
    public interface IRoleFactory
    {
        /// <summary>
        ///  Creates a new setting for a role.
        /// </summary>
        /// <param name="defaultRole">Should people be automatically assigned to this role?</param>
        /// <param name="masterRole">Is this the master administration role?</param>
        /// <returns><see cref="RoleSettings"/></returns>
        Task<RoleSettings> CreateSettingsAsync(bool defaultRole, bool masterRole);
        
        /// <summary>
        ///  Creates a new role.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <param name="summary">Brief description about this role. <para>Example: This role is for moderators.</para></param>
        /// <param name="settings">The settings for this role.</param>
        /// <returns><see cref="IRole"/></returns>
        Task<IRole> CreateRoleAsync(string name, string summary, RoleSettings settings);
    }
}