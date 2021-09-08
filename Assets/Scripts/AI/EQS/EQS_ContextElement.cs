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

        [SerializeField]
        private List<Int_Parameter> int_Parameters = new List<Int_Parameter>();
        [SerializeField]
        private List<String_Parameter> string_Parameters = new List<String_Parameter>();

        private void Awake()
        {
            PrepareParameters();

            EQS.GetContext(contextType).AddContextElement(this);
        }

        public List<IEQS_ContextElement> GetChildrenByPath(string path)
        {
            List<IEQS_ContextElement> res = new List<IEQS_ContextElement>();
            foreach (IEQS_ContextElement element in child)
                if (element.Path.Equals(path))
                    res.Add(element);
            return res;
        }


        private void PrepareParameters()
        {
            foreach(Int_Parameter p in int_Parameters)
            {
                parameters.Add(p.key, p.value);
            }
            foreach(String_Parameter p in string_Parameters)
            {
                parameters.Add(p.key, p.value);
            }
        }


        [System.Serializable]
        private class Int_Parameter
        {
            public string key = "";
            public int value = 0;
        }

        [System.Serializable]
        private class String_Parameter
        {
            public string key = "";
            public string value = "";
        }
    }
}