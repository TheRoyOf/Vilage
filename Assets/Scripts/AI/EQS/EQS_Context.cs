using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public class EQS_Context : IEQS_Context
    {
        public EEQS_ContextType contextType = EEQS_ContextType.NONE;
        public List<IEQS_ContextElement> contextElements = new List<IEQS_ContextElement>();

        public EQS_Context() { }

        public EQS_Context(EEQS_ContextType contextType)
        {
            this.contextType = contextType;
        }

        public List<IEQS_ContextElement> GetContextElements()
        {
            return contextElements;
        }

        public IEQS_ContextFilter GetFilter()
        {
            IEQS_ContextFilter filter = new EQS_ContextFilter();
            filter.SetContext(this);
            return filter;
        }

        public IEQS_ContextElement GetNearest(string path, Vector3 position)
        {
            return GetFilter().AddElementsOfPath(path).SortByDistance(position).GetContextElement();
        }

        public List<IEQS_ContextElement> GetAllByPath(string path)
        {
            return GetFilter().AddElementsOfPath(path).GetContextElements();
        }

        public void AddContextElement(IEQS_ContextElement element)
        {
            contextElements.Add(element);
        }

        public bool RemoveContextElement(IEQS_ContextElement element)
        {
            if(!contextElements.Contains(element))
                return contextElements.Remove(element);
            return false;
        }
    }
}