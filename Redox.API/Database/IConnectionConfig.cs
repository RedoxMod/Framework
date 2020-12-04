namespace Redox.API.Database
{
    public interface IConnectionConfig
    { 
        /// <summary>
        /// The Database server.
        /// </summary>
        string Server { get; }
        
        /// <summary>
        /// The username of the database.
        /// </summary>
        string Username { get; }
        
        /// <summary>
        /// The password of the database.
        /// </summary>
        string Password { get; }
        
        /// <summary>
        /// The name of the database.
        /// </summary>
        string Database { get; }
        
        /// <summary>
        /// The port of the server.
        /// </summary>
        uint Port { get; }
    }
}