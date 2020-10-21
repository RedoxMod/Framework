using System;
using System.Reflection;

using Redox.API.Commands;
using Redox.API.Commands.Attributes;

namespace Redox.Core.Commands
{
    public sealed class CommandContext : ICommandContext
    {
        public ICommand Command { get; }
        
        public CommandInfoAttribute Info { get; }
        
        public CommandDetailsAttribute Details { get; }
        
        public CommandPermissionsAttribute Permissions { get; }
        
        public CommandContext(ICommand command)
        {
            Command = command;

            Type type = command.GetType();
            
            Info = type.GetCustomAttribute<CommandInfoAttribute>();
            Details = type.GetCustomAttribute<CommandDetailsAttribute>() ?? new CommandDetailsAttribute();
            Permissions = type.GetCustomAttribute<CommandPermissionsAttribute>() ?? new CommandPermissionsAttribute();

            if (Info == null)
            { 
                RedoxMod.GetMod().Logger.LogWarning("[RedoxMod.Commands] Command {0} is missing the \"CommandInfo\" Attribute, this command will not work without it!", type.FullName);
            }
        }
    }
}