namespace Redox.API.Roles
{
    public class RoleSettings
    {
        public bool DefaultRole { get; set; }
        public bool MasterRole { get; set; }

        public RoleSettings(bool defaultRole, bool masterRole)
        {
            this.DefaultRole = defaultRole;
            this.MasterRole = masterRole;
        }
    }
}