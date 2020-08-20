using System.Threading.Tasks;

namespace Redox.API.Commands
{
    public interface ICommand
    {
        Task<bool> ExecuteAsync(ICommandExecutionContext context);
    }
}