using UnityEngine;

namespace Items
{
    public interface IItem
    {
        string GetPath();
        int GetCount();
        int GetMaxStackCount();
        GameObject GetGameObject();
        void PickupItem();
        void DropItem();
    }
}