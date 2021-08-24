using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public class EQS_ContextElement : MonoBehaviour, IEQS_ContextElement
    {
        public EEQS_ContextType contextType = EEQS_ContextType.NONE;
        public string path = "";

        public EEQS_ContextType ContextType { get { return contextType; } set { contextType = value; } }
        public string Path { get { return path; } set { path = value; } }
        public Vector3 Position => transform.localPosition;
    }
}