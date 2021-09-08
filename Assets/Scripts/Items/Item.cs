using Ai.EQS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField]
        private string path = "";

        [SerializeField]
        private int count = 1;

        public IEQS_ContextElement EqsElement { get; private set; } = null;

        public int GetCount()
        {
            return count;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public string GetPath()
        {
            return path;
        }

        public void PickupItem()
        {
            throw new System.NotImplementedException();
        }

        public void DropItem()
        {
            throw new System.NotImplementedException();
        }

        public int GetMaxStackCount()
        {
            throw new System.NotImplementedException();
        }
    }
}