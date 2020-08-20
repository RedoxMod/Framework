using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Redox.API.Commands;
using Redox.API.Components;
using Redox.API.Player;
using Redox.API.Plugins;
using Redox.API.Users;

namespace Redox.Core.Commands
{
    [ComponentInfo("CommandProvider", LoadPriority.Medium)]
    public sealed class CommandProvider : ICommandProvider
    {
        private IDictionary<IBasePlugin, IList<ICommandContext>> _commands;

        public async Task RegisterAsync<TCommand>(IBasePlugin plugin) where TCommand : ICommand
        {
            ICommand command = Activator.CreateInstance<TCommand>();
            ICommandContext context = new CommandContext(command);
            
            bool exists = await HasAsync(context.Info.Name);

            if (exists)
            {
                Redox.GetMod().Logger.LogWarning("[{0}] The command {1} is already in use and can cause conflicts.",
                    plugin.Info.Title, context.Info.Name);
            }
            if (!_commands.ContainsKey(plugin))
                _commands.Add(plugin, new List<ICommandContext>());
            _commands[plugin].Add(context);
        }

        public Task UnregisterAsync<TCommand>(IBasePlugin plugin) where TCommand : ICommand
        {
            if (!_commands.ContainsKey(plugin))
                return Task.CompletedTask;
            _commands[plugin].Clear();
            return Task.CompletedTask;
        }

        public Task<bool> HasAsync(string name)
        {
            if (_commands.Values.Any(commands => commands.Any(x => x.Info.Name == name)))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<ICommand> GetAsync(string name)
        {
            return (from commands in _commands.Values where commands.Any(x => x.Info.Name == name) select Task.FromResult(commands.FirstOrDefault(x => x.Info.Name == name).Command)).FirstOrDefault();
        }

        public async Task CallAsync(IRedoxUser sender, string name, string[] args)
        {

            foreach (var list in _commands.Values)
            {
                ICommandContext context =
                    list.FirstOrDefault(x => x.Info.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

                if (context == null)
                    return;

                if (sender.IsPlayer)
                {
                    IRedoxPlayer player = (IRedoxPlayer) sender;
                    if (context.Info.Caller == CommandCaller.Player || context.Info.Caller == CommandCaller.Both)
                    {
                        bool hasperm = await this.HasPerm(player, context.Permissions.Collection);
                        if (!hasperm)
                        {
                            player.SendMessage("Redox.Messages.Commands.NoPermission");
                            continue;
                        }
                        ICommandExecutionContext executionContext = new CommandExecutionContext(sender, args);
                        bool b = await context.Command.ExecuteAsync(executionContext);
                        if (!b)
                        {
                            player.SendMessage(context.Details.Help ?? "No Help provided for this plugin");
                        }
                    }
                    else
                    {
                        player.SendMessage("Redox.Messages.Commands.NotAllowed");
                    }
                }
                else
                {
                    //TODO: Add console command execution code.
                }
            }
        }

        public Task ClearAsync(IBasePlugin plugin)
        {
            _commands[plugin]?.Clear();
            return Task.CompletedTask;
        }

        public Task RunAsync()
        {
            _commands = new Dictionary<IBasePlugin, IList<ICommandContext>>();
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }

        private async Task<bool> HasPerm(IRedoxPlayer player, IReadOnlyCollection<string> commandPermissions)
        {
            foreach (string perm in commandPermissions)
            {
                if (!await player.HasPermissionAsync(perm))
                    continue;
                return true;
            }

            return false;
        }
    }
}