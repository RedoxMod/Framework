using System.Linq;
using System.Threading.Tasks;
using Redox.API.Commands;
using Redox.API.Users;

namespace Redox.Core.Commands
{
    public abstract class Command : ICommand
    {

        protected abstract Task<bool> Execute(IRedoxUser sender, string[] args);
        
        public async Task<bool> ExecuteAsync(ICommandExecutionContext context)
        {
            return await this.Execute(context.Sender, context.Args.ToArray());
        }
    }
}