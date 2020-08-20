
using Redox.API.Commands.Attributes;

namespace Redox.API.Commands
{
    public interface ICommandContext
    {
        ICommand Command { get; }
     //   ICommandExecutionContext ExecutionContext { get; }
        
        CommandInfoAttribute Info { get; }
        CommandDetailsAttribute Details { get; }
        CommandPermissionsAttribute Permissions { get; }
    }
}