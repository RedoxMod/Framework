using System.Threading.Tasks;

namespace Redox.API.Commands
{
    public interface ICommandProvider
    {
        Task RegisterAsync<TCommand>() where TCommand : ICommand;
        
        Task UnregisterAsync<TCommand>() where TCommand : ICommand;

        Task<bool> HasAsync(string name);

        Task<ICommand> GetAsync(string name);

        Task ClearAsync();
    }
}