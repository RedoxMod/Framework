using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Components;

namespace Redox.API.Data
{
    /// <summary>
    /// Representation of a datastore object.
    /// </summary>
    public interface IDataStore : IBaseComponent, ISaveable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task AppendAsync(string tableName, object key, object value);

        Task RemoveAsync(string tableName, object key);
        Task RemoveTableAsync(string table);

        Task<bool> HasAsync(string tableName, object key);
        Task<bool> HasTableAsync(string tableName);

        Task<object> GetAsync(string tableName, object key);
        Task<IReadOnlyDictionary<object, object>> GetTableAsync(string tableName);

        Task FlushAsync(string tableName);
        Task FlushAllAsync();
    }
}