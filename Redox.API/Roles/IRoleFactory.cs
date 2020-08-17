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
        /// <param name="defaultrole">Should people be automatically assigned to this role?</param>
        /// <param name="masterrole">Is this the master administration role?</param>
        /// <returns><see cref="RoleSettings"/></returns>
        Task<RoleSettings> CreateRoleSettingsAsync(bool defaultrole, bool masterrole);
        
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