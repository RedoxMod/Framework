using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Redox.API.Database
{
    public interface IConnection
    {
     
        IConnectionConfig Settings { get; }
        
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
        /// </param>
        /// <returns>The affected rows by this query.</returns>
        Task<int> QueryAsync(string statement);

        /// <summary>
        /// Executes asynchronous query that returns data.
        /// </summary>
        /// <param name="statement">The statement you want to execute.
        /// </param>
        /// <returns>Returns a list of dictionaries that contains all the affected data.</returns>
        Task<List<Dictionary<string, object>>> GetResultsAsync(string statement);
        
        /// <summary>
        ///  Executes asynchronous query that returns data.
        /// </summary>
        /// <param name="statement">The statement you want to execute.
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