using Redox.API.Player;

namespace Redox.Core.Events.Player
{
    public static class PlayerEvents
    {
        /// <summary>
        /// Called when a player joins the server.
        /// </summary>
        public static event PlayerJoinedDelegate U_OnPlayerConnected;
        
        /// <summary>
        /// Called when a player left the server.
        /// </summary>
        public static event PlayerLeftDelegate U_OnPlayerDisconnected;
        
        /// <summary>
        /// Called when a player gets banned from the server.
        /// </summary>
        public static event PlayerBannedDelegate U_OnPlayerBanned;
        /// <summary>
        /// Called when a player gets kicked from the server.
        /// </summary>
        public static event PlayerKickedDelegate U_OnPlayerKicked;
        
        /// <summary>
        /// Called when a player sends a chat message.
        /// </summary>
        public static event PlayerChatDelegate U_OnPlayerChat;

        internal static void Init()
        {
            U_OnPlayerConnected = delegate(IRedoxPlayer player) { };
            U_OnPlayerDisconnected = delegate(IRedoxPlayer player) { };
            U_OnPlayerBanned = delegate(IRedoxPlayer target, string reason) { };
            U_OnPlayerKicked = delegate(IRedoxPlayer player, string reason) { };
            U_OnPlayerChat = delegate(PlayerChatEvent @event) { };
        }


        public static void PlayerConnected(IRedoxPlayer player)
        {
            if (player != null)
            {
                U_OnPlayerConnected?.Invoke(player);
            }
        }
        
        public static void PlayerDisconnected(IRedoxPlayer player)
        {
            if (player != null)
            {
                U_OnPlayerDisconnected?.Invoke(player);
            }
        }

        public static void PlayerKicked(IRedoxPlayer player, string reason)
        {
            if (player != null)
            {
                U_OnPlayerKicked?.Invoke(player, reason);
            }
        }
        public static void PlayerBanned(IRedoxPlayer player, string reason)
        {
            if (player != null)
            {
                U_OnPlayerBanned?.Invoke(player, reason);
            }
        }

        public static bool PlayerChat(IRedoxPlayer player, string message)
        {
            if (player != null)
            {
                PlayerChatEvent @event = new PlayerChatEvent(player, message);
                U_OnPlayerChat?.Invoke(@event);
                return @event.Cancel;
            }
            return false;
        }
    }
}