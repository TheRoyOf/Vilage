using Building.Interactive;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.Inventory
{
    public class BaseInventory : MonoBehaviour, IInventory
    {
        private Action<IItem> OnUpdateItemInHand;

        List<IItem> items = new List<IItem>();

        IItem handItem = null;

        public void AddItem(IItem item)
        {
            if (IsContain(item.GetPath()))

                items.Add(item);
        }

        public IItem GetItem(string path, int count = 1)
        {
            return items.Find((i) => { return i.GetPath().Equals(path); });
        }

        public IItem PoolItem(string path, int count = 1)
        {
            throw new NotImplementedException();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public bool IsContain(string path, int count = 1)
        {
            IItem item = items.Find((i) => { return i.GetPath().Equals(path); });

            return item != null && item.GetCount() >= count;
        }

        public bool TakeToHand(IItem item)
        {
            if(handItem == null)
            {
                return false;
            }

            handItem = item;
            item.PickupItem();

            OnUpdateItemInHand?.Invoke(handItem);

            return true;
        }

        public IItem GetItemInHand()
        {
            return handItem;
        }

        public IItem DropFromHand()
        {
            if (handItem == null)
            {
                return null;
            }

            IItem item = handItem;
            handItem = null;
            item.DropItem();
            OnUpdateItemInHand?.Invoke(item);

            return item;
        }

        public Action<IItem> GetUpdateItemDelegate()
        {
            return OnUpdateItemInHand;
        }
    }
}