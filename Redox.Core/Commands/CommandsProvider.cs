using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Redox.API.Commands;
using Redox.API.Components;
using Redox.API.Player;
using Redox.API.Plugins;
using Redox.API.Users;
using Redox.Core.Components;

namespace Redox.Core.Commands
{
    [ComponentInfo("Commands", LoadPriority.Medium)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CommandsProvider : ICommandsProvider
    {
        private readonly IDictionary<string, IList<ICommandContext>> _commands = new Dictionary<string, IList<ICommandContext>>();

        public async Task RegisterAsync(IBasePlugin plugin)
        {
            string pluginTitle = plugin.Info.Title;
            IEnumerable<Type> types = plugin.GetType().Assembly.GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Command)));
            foreach (Type type in types)
            {
                ICommand command = (ICommand)Activator.CreateInstance(type);
                ICommandContext context = new CommandContext(command);
                bool exists = await HasAsync(context.Info.Name);

                if (exists)
                {
                    RedoxMod.GetMod().TempLogger.Warning("[{0}] The command {1} is already in use and can cause conflicts.",
                        pluginTitle, context.Info.Name);
                }
                if (!_commands.ContainsKey(pluginTitle))
                    _commands.Add(pluginTitle, new List<ICommandContext>());
                _commands[pluginTitle].Add(context);
            }
        }

        public Task UnregisterAsync<TCommand>(string pluginTitle) where TCommand : ICommand
        {
            if (!_commands.ContainsKey(pluginTitle))
                return Task.CompletedTask;
            _commands[pluginTitle].Clear();
            return Task.CompletedTask;
        }

        public Task<bool> HasAsync(string name)
        {
            return Task.FromResult(_commands.Values.Any(commands => commands.Any(x => x.Info.Name == name)));
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
                        bool hasPerm = await this.HasPerm(player, context.Permissions.Collection);
                        if (!hasPerm)
                        {
                            player.SendMessage("RedoxMod.Messages.Commands.NoPermission");
                            continue;
                        }
                        ICommandExecutionContext executionContext = new CommandExecutionContext(sender, args);
                        bool b = await context.Command.ExecuteAsync(executionContext);
                        if (!b)
                        {
                            player.SendMessage(context.Help.Help ?? "No Help provided for this plugin");
                        }
                    }
                    else
                    {
                        player.SendMessage("RedoxMod.Messages.Commands.NotAllowed");
                    }
                }
                else
                {
                    
                    //TODO: Add console command execution code.
                }
            }
        }

        public Task ClearAsync(string pluginTitle)
        {
            _commands[pluginTitle]?.Clear();
            return Task.CompletedTask;
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            return Task.CompletedTask;
        }

        private async Task<bool> HasPerm(IRedoxPlayer player, IEnumerable<string> commandPermissions)
        {
            foreach (string perm in commandPermissions)
            {
                if (!await player.HasPermissionAsync(perm))
                    continue;
                return true;
            }
            return false;
        }

        public static CommandsProvider Get()
        {
            return (CommandsProvider)ComponentsProvider.Get().ResolveComponent<ICommandsProvider>();
        }
    }
}