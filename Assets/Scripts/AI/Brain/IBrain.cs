using Ai.Inventory;
using UnityEngine;

namespace Ai.Brain
{
    public interface IBrain
    {
        GameObject gameObject { get; }
        IInventory GetInventory();
        void BreakeActiveTask();
        void UpdateActiveTask();
    }
}