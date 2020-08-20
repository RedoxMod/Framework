using System;
using System.Collections.Generic;

namespace Redox.API.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandPermissionsAttribute : Attribute
    {
        /// <summary>
        /// Set of permissions of this command.
        /// </summary>
        public IReadOnlyCollection<string> Collection { get; }
        
        public CommandPermissionsAttribute(params string[] permissions)
        {
            Collection = permissions;
        }
    }
}