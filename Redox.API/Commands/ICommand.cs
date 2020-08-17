using System.Threading.Tasks;
using Redox.API.User;

namespace Redox.API.Commands
{
    public interface ICommand
    {
        string Name { get; }
        
        string Summary { get; }
        
        string Permission { get; }
        
        string[] Aliases { get; }
        
        CommandCaller Caller { get; }

        Task<bool> ExecuteAsync(IUser sender, string[] args);
    }
}