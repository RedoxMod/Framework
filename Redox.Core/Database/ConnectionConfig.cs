using Redox.API.Database;

namespace Redox.Core.Database
{
    public sealed class ConnectionConfig : IConnectionConfig
    {
        public string Server { get; }
        public string Username { get; }
        public string Password { get; }
        public string Database { get; }
        public uint Port { get; }
        
        public ConnectionConfig(string server, string username, string password, string database, uint port)
        {
            Server = server;
            Username = username;
            Password = password;
            Database = database;
            Port = port;
        }
    }
}