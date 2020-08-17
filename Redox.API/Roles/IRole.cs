using System.Collections.Generic;

namespace Redox.API.Roles
{
    public interface IRole
    {
        /// <summary>
        /// The name of the role.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Brief description about this role.
        /// </summary>
        string Summary { get; }
        
        /// <summary>
        /// Role settings.
        /// </summary>
        RoleSettings Settings { get; }
        
        /// <summary>
        /// List of permissions.
        /// </summary>
        HashSet<string> Permissions { get; }
        
        /// <summary>
        /// List of members.
        /// </summary>
        HashSet<string> Members { get; }
    }
}