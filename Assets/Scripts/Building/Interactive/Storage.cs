using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ai.EQS;

namespace Building.Interactive
{
    public class Storage : MonoBehaviour, IStorage
    {
        List<IItem> items = new List<IItem>();

        public IEQS_ContextElement EqsElement { get; private set; } = null;

        private void Awake()
        {
            EqsElement = GetComponent<IEQS_ContextElement>();
        }

        public void AddItem(IItem item)
        {
            if(IsContain(item.GetPath()))

            items.Add(item);

            if(EqsElement != null && item.EqsElement != null)
            {
                EqsElement.Child.Add(item.EqsElement);
            }
        }

        public IItem GetItem(string path, int count = 1)
        {
            return items.Find((i)=> { return i.GetPath().Equals(path); });
        }

        public IItem PoolItem(string path, int count = 1)
        {
            throw new System.NotImplementedException();
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
    }
}