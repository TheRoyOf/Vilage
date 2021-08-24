using Items;
using UnityEngine;

namespace Building.Interactive
{
    public interface IStorage
    {
        void AddItem(IItem item);
        IItem GetItem(string path, int count = 1);
        IItem PoolItem(string path, int count = 1);
        bool IsContain(string path, int count = 1);
        Transform GetTransform();
    }
}