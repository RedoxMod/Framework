using System.Threading.Tasks;

namespace Redox.API.Data
{
    public interface ISaveable
    {
        bool Exists { get; }
        
        Task SaveAsync();

        Task LoadAsync();
    }
}