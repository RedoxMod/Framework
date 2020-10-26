using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Redox.API.Database
{
    public interface IConnection
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
        
        /// <summary>
        /// The connection of the database.
        /// </summary>
        DbConnection DbConnection { get; }
        
        /// <summary>
        /// Checks if there is a connection or not.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Executes asynchronous query statement.
        /// </summary>
        /// <param name="statement">The statement you want to execute.
        /// <para>Example: "INSERT INTO awesomeTable (columns) VALUES(values)"</para>
        /// </param>
        /// <returns>The affected rows by this query.</returns>
        Task<int> QueryAsync(string statement);

        /// <summary>
        /// Executes asynchronous query that returns data.
        /// </summary>
        /// <param name="statement">The statement you want to execute.
        /// <para>Example: "SELECT * WHERE awesome_column = 'AwesomeValue'"</para>
        /// </param>
        /// <returns>Returns a list of dictionaries that contains all the affected data.</returns>
        Task<List<Dictionary<string, object>>> GetResultsAsync(string statement);
        
        /// <summary>
        ///  Executes asynchronous query that returns data.
        /// </summary>
        /// <param name="statement">The statement you want to execute.
        /// <para>Example: "SELECT * WHERE awesome_column = 'AwesomeValue'"</para></param>
        /// <typeparam name="T">The type you want to put the data into.</typeparam>
        /// <returns>A list of the type that contains the data.</returns>
        Task<List<T>> GetResultsAsync<T>(string statement) where T : class;

        /// <summary>
        /// Opens a new connection asynchronous
        /// </summary>
        /// <returns>Returns if the connection was opened.</returns>
        Task<bool> OpenAsync();

        /// <summary>
        /// Closes a new connection asynchronous
        /// </summary>
        /// <returns>Returns if the connection got closed..</returns>
        Task<bool> CloseAsync();
    }
}