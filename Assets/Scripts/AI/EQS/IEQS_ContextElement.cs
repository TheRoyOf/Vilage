using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_ContextElement
    {
        EEQS_ContextType ContextType { get; set; }
        string Path { get; set; }
        Dictionary<string, object> Parameters { get; set; }
        List<IEQS_ContextElement> Child { get; set; }
        Vector3 Position { get; }
        GameObject gameObject { get; }

        List<IEQS_ContextElement> GetChildrenByPath(string path);
    }
}
