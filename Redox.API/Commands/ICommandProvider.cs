using System.Threading.Tasks;
using Redox.API.Components;
using Redox.API.Plugins;
using Redox.API.Users;

namespace Redox.API.Commands
{
    public interface ICommandProvider : IBaseComponent
    {
        Task RegisterAsync<TCommand>(IBasePlugin plugin) where TCommand : ICommand;
        
        Task UnregisterAsync<TCommand>(IBasePlugin plugin) where TCommand : ICommand;

        Task<bool> HasAsync(string name);

        Task<ICommand> GetAsync(string name);


        Task CallAsync(IRedoxUser sender, string name, string[] args);

        Task ClearAsync(IBasePlugin plugin);
    }
}