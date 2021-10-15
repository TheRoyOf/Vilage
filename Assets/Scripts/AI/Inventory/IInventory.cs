using Ai.Brain;
using Building.Interactive;
using Items;
using System;

namespace Ai.Inventory
{
    public interface IInventory: IStorage
    {
        bool TakeToHand(IItem item);
        IItem DropFromHand();
        IItem GetItemInHand();
        Action<IItem> GetUpdateItemDelegate();
    }
}