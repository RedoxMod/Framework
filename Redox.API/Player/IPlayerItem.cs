namespace Redox.API.Player
{
    public interface IPlayerItem
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The inventory slot of the item.
        /// </summary>
        int Slot { get; }
    }
}