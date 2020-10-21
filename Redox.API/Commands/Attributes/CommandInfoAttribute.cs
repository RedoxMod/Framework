using System;

namespace Redox.API.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandInfoAttribute : Attribute
    {
        
        /// <summary>
        /// The name of the command
        /// <para>Example: /help - "help" is the name of the command</para>
        /// </summary>
        public string Name { get;  }
        
        /// <summary>
        /// The user that can execute this command.
        /// <para>This can either be the Player, Console or Both</para>
        /// </summary>
        public CommandCaller Caller { get; }
        
        public CommandInfoAttribute(string name, CommandCaller caller)
        {
            Name = name;
            Caller = caller;
        }
    }
}