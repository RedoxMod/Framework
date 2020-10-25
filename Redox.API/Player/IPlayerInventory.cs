using System.Collections.Generic;

namespace Redox.API.Player
{
    /// <summary>
    /// Representation for the universal player inventory.
    /// </summary>
    public interface IPlayerInventory
    {
        /// <summary>
        /// List of items that are in the inventory.
        /// </summary>
        IEnumerable<IPlayerItem> Items { get; }
        
        /// <summary>
        /// Adds a new item to the inventory.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="amount">The amount you want to add.</param>
        void AddItem(string name, int amount = 1);

        /// <summary>
        /// Removes a item from the inventory.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="amount">The amount you want to remove.</param>
        void RemoveItem(string name, int amount = 1);


        /// <summary>
        /// Checks if the inventory has a item.
        /// </summary>
        /// <param name="name">The name of the item you want to check for.</param>
        /// <returns>True if exists, otherwise False.</returns>
        bool HasItem(string name);
        
        
        /// <summary>
        /// Clears the whole inventory.
        /// </summary>
        void ClearAll();
    }
}