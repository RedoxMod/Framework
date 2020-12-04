
namespace Redox.API.Commands
{
    /// <summary>
    /// Represents the context of a registered command.
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// The implementation of the command interface.
        /// </summary>
        ICommand Command { get; }
        
        /// <summary>
        /// Holds information about the command.
        /// </summary>
        CommandInfoAttribute Info { get; }
        
        /// <summary>
        /// Holds a help message about this command.
        /// </summary>
        CommandHelpAttribute Help { get; }
        CommandPermissionsAttribute Permissions { get; }
    }
}