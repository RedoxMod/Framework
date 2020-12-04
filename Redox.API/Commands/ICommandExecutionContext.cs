using System.Collections.Generic;
using Redox.API.Users;

namespace Redox.API.Commands
{
    /// <summary>
    /// The context of the command executor.
    /// </summary>
    public interface ICommandExecutionContext
    {
        /// <summary>
        /// The sender of the command.
        /// <para>This can either be a Player, Console or Both</para>
        /// </summary>
        IRedoxUser Sender { get; }
        
        /// <summary>
        /// The arguments given.
        /// <para>
        /// Example: /help 1 - the number 1 will be the first argument.
        /// </para>
        /// </summary>
        IReadOnlyCollection<string> Args { get; }
    }
}