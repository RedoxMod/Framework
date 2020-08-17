using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redox.API.Data
{
    /// <summary>
    /// Representation of a datastore object.
    /// </summary>
    public interface IDataStore : ISaveable
    {
        Task AppendAsync(string tablename, object key, object value);

        Task RemoveAsync(string tablename, object key);
        Task RemoveTableAsync(string table);

        Task<bool> HasAsync(string tablename, object key);
        Task<bool> HasTableAsync(string tablename);

        Task<object> GetAsync(string tablename, object key);
        Task<IReadOnlyDictionary<object, object>> GetTableAsync(string tablename);

        Task FlushAsync(string tablename);
        Task FlushAllAsync();
    }
}