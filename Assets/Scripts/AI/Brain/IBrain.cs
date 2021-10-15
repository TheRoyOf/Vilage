using Ai.Inventory;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Ai.Brain
{
    public interface IBrain
    {
        event Action OnDestinationReached;
        event Action OnDestinationInvald;

        GameObject gameObject { get; }
        IInventory inventory { get; set; }
        bool MoveTo(Vector3 destination);
        void UpdateActiveTask();
        void BreakeActiveTask();
    }
}