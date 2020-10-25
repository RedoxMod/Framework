using System.Collections.Generic;
using Redox.API.Roles;

namespace Redox.Core.Roles
{
    public sealed class Role : IRole
    {
        public string Name { get; }
        
        public string Summary { get; }
        
        public RoleSettings Settings { get; }
        
        public HashSet<string> Permissions { get; }
        
        public HashSet<string> Members { get; }
        
        public Role(string name, string summary, RoleSettings settings)
        {
            Name = name;
            Summary = summary;
            Settings = settings;
            Permissions = new HashSet<string>();
            Members = new HashSet<string>();
        }
    }
}