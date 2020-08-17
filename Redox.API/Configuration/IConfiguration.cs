using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Data;

namespace Redox.API.Configuration
{
    public interface IConfiguration  : ISaveable
    {
        void Load();
        
        /*
        object this[string ob] { get; set; }
            
        IReadOnlyCollection<string> Keys { get; }

        Task AppendAsync(string key, object value);
        Task SetAsync(string key, object newvalue);
        Task RemoveAsync(string key);
        
        Task<object> GetAsync(string key);

        Task<bool> HasAsync(string key);
        */
    }
}