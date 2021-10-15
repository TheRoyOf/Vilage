using Items;
using UnityEngine;
using Ai.EQS;

namespace Building.Interactive
{
    public interface IStorage
    {
        void AddItem(IItem item);
        IItem GetItem(string path, int count = 1);
        IItem PoolItem(string path, int count = 1);
        bool IsContain(string path, int count = 1);
        Vector3 GetInteractionPosition();
        IEQS_ContextElement EqsElement { get; }
    }
}