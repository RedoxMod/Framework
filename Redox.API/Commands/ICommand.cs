using System.Threading.Tasks;

namespace Redox.API.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// This method gets called when the command gets executed.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True if successful, False to broadcast the help text to the player.</returns>
        Task<bool> ExecuteAsync(ICommandExecutionContext context);
    }
}