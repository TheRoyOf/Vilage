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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponentInChildren<Collider>().enabled = false;
        }

        public void DropItem()
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponentInChildren<Collider>().enabled = true;
        }

        public int GetMaxStackCount()
        {
            throw new System.NotImplementedException();
        }
    }
}