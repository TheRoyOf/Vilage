using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_Context
    {
        List<IEQS_ContextElement> GetContextElements();
        void AddContextElement(IEQS_ContextElement element);
        bool RemoveContextElement(IEQS_ContextElement element);
        IEQS_ContextFilter GetFilter();
        //Fast filtered
        IEQS_ContextElement GetNearest(string path, Vector3 position);
        List<IEQS_ContextElement> GetAllByPath(string path);
    }
}