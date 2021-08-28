using UnityEngine;
using Ai.EQS;

namespace Items
{
    public interface IItem
    {
        string GetPath();
        int GetCount();
        int GetMaxStackCount();
        void PickupItem();
        void DropItem();
        GameObject gameObject { get; }
        IEQS_ContextElement EqsElement { get; }
    }
}