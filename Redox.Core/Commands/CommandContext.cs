using System;
using System.Reflection;

using Redox.API.Commands;

namespace Redox.Core.Commands
{
    public sealed class CommandContext : ICommandContext
    {
        public ICommand Command { get; }
        
        public CommandInfoAttribute Info { get; }
        
        public CommandHelpAttribute Help { get; }
        
        public CommandPermissionsAttribute Permissions { get; }
        
        public CommandContext(ICommand command)
        {
            Command = command;

            Type type = command.GetType();
            
            Info = type.GetCustomAttribute<CommandInfoAttribute>();
            Help = type.GetCustomAttribute<CommandHelpAttribute>() ?? new CommandHelpAttribute();
            Permissions = type.GetCustomAttribute<CommandPermissionsAttribute>() ?? new CommandPermissionsAttribute();

            if (Info == null)
            { 
                RedoxMod.GetMod().TempLogger.Warning("[RedoxMod.Commands] Command {0} is missing the \"CommandInfo\" Attribute, this command will not work without it!", type.FullName);
            }
        }
    }
}