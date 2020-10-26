using System;
using System.Xml.Serialization;
using Redox.API.Database;

namespace Redox.Core.Configuration.Redox
{
    [Serializable]
    [XmlRoot("Configuration")]
    public sealed class RedoxConfig
    {
        public string CorePrefix { get; set; }
        public DatabaseConfig PermissionsDb { get; set; }
        public DatabaseConfig RolesDb { get; set; }

        public RedoxConfig Init()
        {
            CorePrefix = "Redox";
            PermissionsDb = new DatabaseConfig();
            RolesDb = new DatabaseConfig();
            return this;
        }

        [Serializable]
        public class DatabaseConfig : IConnectionConfig
        {
            [XmlAttribute("enabled")]
            public bool Enabled { get; set; }
            
            public string Server { get; set; }

            public string Username { get; set; } = "root";
            
            public string Password { get; set; }
            
            public string Database { get; set; }

            public uint Port { get; set; } = 3306;
        }
    }
}