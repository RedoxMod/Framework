using Redox.API.Player;

namespace Redox.Core.Events.Player
{
    public delegate void PlayerChatDelegate(PlayerChatEvent @event);
    
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlayerChatEvent
    {
        public IRedoxPlayer Sender { get; }
        
        public string Message { get; set; }
        
        public bool Cancel { get; set; }
        
        public PlayerChatEvent(in IRedoxPlayer sender, string message)
        {
            Sender = sender;
            Message = message;
        }
    }
}