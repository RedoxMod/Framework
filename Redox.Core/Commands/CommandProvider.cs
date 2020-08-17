using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Commands;
using Redox.API.Plugins;

namespace Redox.Core.Commands
{
    public sealed class CommandProvider : ICommandProvider
    {
        private readonly IBasePlugin _plugin;
        
        
        private readonly List<ICommand> _commands = new List<ICommand>();
        
        
        public async Task RegisterAsync<TCommand>() where TCommand : ICommand
        {
            ICommand command = Activator.CreateInstance<TCommand>();
            bool exists = await this.HasAsync(command.Name);

            if (exists)
            {
                //Redox.Mod.Logger.LogWarning("[Commands] Failed to register {0} in {1} because another command with the same name is already loaded.", command.Name, _plugin.Info.Title);
                return;
            }
            _commands.Add(command);
        }

        public async Task UnregisterAsync<TCommand>() where TCommand : ICommand
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> HasAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICommand> GetAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task ClearAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}