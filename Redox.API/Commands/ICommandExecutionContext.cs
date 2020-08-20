using System.Collections.Generic;
using Redox.API.Users;

namespace Redox.API.Commands
{
    public interface ICommandExecutionContext
    {
        /// <summary>
        /// The sender of the command.
        /// <para>This can either be a Player, Console or Both</para>
        /// </summary>
        IRedoxUser Sender { get; }
        
        IReadOnlyCollection<string> Args { get; }
    }
}