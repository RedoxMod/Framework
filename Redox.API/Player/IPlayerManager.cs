using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Engines;

namespace Redox.API.Player
{
    public interface IPlayerManager
    {
        IEnumerable<IPluginEngine> GetPlayers();

        Task<IRedoxPlayer> FindPlayerAsync(string name);

        Task<IRedoxPlayer> FindPlayerByIdAsync(ulong id);
    }
}