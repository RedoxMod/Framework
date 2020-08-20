using System;

namespace Redox.API.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandDetailsAttribute : Attribute
    {

        /// <summary>
        /// A brief description about this command.
        /// </summary>
        public string Summary { get; private set; } = string.Empty;

        /// <summary>
        /// A piece of help text about this command.
        /// <para>Example: Use /help to see information about all commands</para>
        /// </summary>
        public string Help { get; private set; } = string.Empty;

        public CommandDetailsAttribute() {}
        
        public CommandDetailsAttribute(string summary, string help)
        {
            Summary = summary;
            Help = help;
        }
    }
}