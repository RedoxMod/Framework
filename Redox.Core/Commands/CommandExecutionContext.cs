using System.Collections.Generic;
using Redox.API.Commands;
using Redox.API.Users;

namespace Redox.Core.Commands
{
    public sealed class CommandExecutionContext : ICommandExecutionContext
    {
        public IRedoxUser Sender { get; }
        public IReadOnlyCollection<string> Args { get; }
        
        public CommandExecutionContext(IRedoxUser sender,  string[] args)
        {
            Sender = sender;
            Args = args;
        }
    }
}