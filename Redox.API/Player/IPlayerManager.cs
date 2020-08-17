using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Engines;

namespace Redox.API.Player
{
    public interface IPlayerManager
    {
        IEnumerable<IPluginEngine> GetPlayers();

        Task<IPlayer> FindPlayerAsync(string name);

        Task<IPlayer> FindPlayerByIdAsync(ulong id);
    }
}