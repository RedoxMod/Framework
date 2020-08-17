using System.Threading.Tasks;

namespace Redox.API.Data
{
    public interface ISaveable
    {
        bool Exists { get; }
        
        Task SaveAsync();

        Task LoadAsync();

        Task WriteObjectAsync(object ob);

        Task<T> ReadObjectAsync<T>();
    }
}