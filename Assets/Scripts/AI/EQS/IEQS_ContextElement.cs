using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_ContextElement
    {
        EEQS_ContextType ContextType { get; set; }
        string Path { get; set; }
        Vector3 Position { get; }
        GameObject gameObject { get; }
    }
}
