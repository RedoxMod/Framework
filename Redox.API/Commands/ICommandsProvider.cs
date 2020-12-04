using System.Threading.Tasks;

using Redox.API.Components;
using Redox.API.Plugins;
using Redox.API.Users;

namespace Redox.API.Commands
{
    public interface ICommandsProvider : IBaseComponent
    {
        /// <summary>
        /// Registers a command asynchronously.
        /// </summary>
        /// <param name="plugin">The plugin associated with this command.</param>
        /// <typeparam name="TCommand">The type that inherits the Command interface.</typeparam>
        /// <returns></returns>
        Task RegisterAsync(IBasePlugin plugin);
        
        /// <summary>
        /// Unregisters a command asynchronously.
        /// </summary>
        /// <param name="plugin">The plugin associated with this command.</param>
        /// <typeparam name="TCommand">The type that inherits the Command interface.</typeparam>
        /// <returns></returns>
        Task UnregisterAsync<TCommand>(string pluginTitle) where TCommand : ICommand;

        
        /// <summary>
        /// Checks if a command by name exists.
        /// </summary>
        /// <param name="name">The name of the command. Example: help</param>
        /// <returns>True of False</returns>
        Task<bool> HasAsync(string name);

        /// <summary>
        /// Gets a command by name asynchronously.
        /// </summary>
        /// <param name="name">The name of the command. Example: help</param>
        /// <returns>The command if found and null if otherwise</returns>
        Task<ICommand> GetAsync(string name);
        
        /// <summary>
        /// Executes the given command.
        /// </summary>
        /// <param name="sender">The sender of the command. <see cref="CommandCaller"/></param>
        /// <param name="name">The name of the command. Example: help</param>
        /// <param name="args">The provided arguments</param>
        /// <returns></returns>
        Task CallAsync(IRedoxUser sender, string name, string[] args);
        
        
        /// <summary>
        /// Unregisters all commands from a plugin.
        /// </summary>
        /// <param name="pluginTitle">The name of the plugin.</param>
        /// <returns></returns>
        Task ClearAsync(string pluginTitle);
    }
}