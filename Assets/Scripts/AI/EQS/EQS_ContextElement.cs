using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public class EQS_ContextElement : MonoBehaviour, IEQS_ContextElement
    {
        public EEQS_ContextType contextType = EEQS_ContextType.NONE;
        public string path = "";
        public Dictionary<string, object> parameters = new Dictionary<string, object>();
        public List<IEQS_ContextElement> child = new List<IEQS_ContextElement>();

        public EEQS_ContextType ContextType { get { return contextType; } set { contextType = value; } }
        public string Path { get { return path; } set { path = value; } }
        public Dictionary<string, object> Parameters { get { return parameters; } set { parameters = value; } }
        public List<IEQS_ContextElement> Child { get { return child; } set { child = value; } }
        public Vector3 Position => transform.localPosition;

        public List<IEQS_ContextElement> GetChildrenByPath(string path)
        {
            List<IEQS_ContextElement> res = new List<IEQS_ContextElement>();
            foreach (IEQS_ContextElement element in child)
                if (element.Path.Equals(path))
                    res.Add(element);
            return res;
        }
    }
}