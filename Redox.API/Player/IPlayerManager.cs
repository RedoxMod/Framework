using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Engines;

namespace Redox.API.Player
{
    public interface IPlayerManager
    {
        IEnumerable<IRedoxPlayer> GetPlayers();

        IRedoxPlayer FindPlayer(string name);

        IRedoxPlayer FindPlayerById(ulong id);
    }
}