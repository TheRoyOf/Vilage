using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class WidgetReferenceStorage : MonoBehaviour, IWidgetReferenceStorage
    {
        [SerializeField]
        private List<WidgetReference> widgetReferences = new List<WidgetReference>();

        private void Awake()
        {
            Pool.SetWidgetReferenceStorage(this);
        }

        public GameObject GetWidgetRoot(EWidgetType widgetType)
        {
            return Array.Find(widgetReferences.ToArray(), (w) => { return w.widgetType.Equals(widgetType); }).root;
        }

        public bool AddWidget(EWidgetType widgetType, GameObject root)
        {
            if (IsContainsWidget(widgetType))
                return false;

            widgetReferences.Add(new WidgetReference(widgetType, root));
            return true;
        }

        public bool IsContainsWidget(EWidgetType widgetType)
        {
            return Array.Find(widgetReferences.ToArray(), (w) => { return w.widgetType.Equals(widgetType); }) != null;
        }


        [Serializable]
        private class WidgetReference
        {
            public EWidgetType widgetType = EWidgetType.NONE;
            public GameObject root = null;

            public WidgetReference() { }

            public WidgetReference(EWidgetType widgetType, GameObject root)
            {
                this.widgetType = widgetType;
                this.root = root;
            }
        }
    }
}