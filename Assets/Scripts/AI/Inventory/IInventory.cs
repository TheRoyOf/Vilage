using Building.Interactive;
using Items;
using System;

namespace Ai.Inventory
{
    public interface IInventory: IStorage
    {
        IItem DropFromHand();
        IItem GetItemInHand();
        bool TakeToHand(IItem item);
        Action<IItem> GetUpdateItemDelegate();
    }
}