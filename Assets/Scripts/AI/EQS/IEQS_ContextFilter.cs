using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_ContextFilter
    {
        //Get
        IEQS_ContextElement GetContextElement();
        List<IEQS_ContextElement> GetContextElements();
        //Add
        IEQS_ContextFilter AddElementsOfPath(string path);
        IEQS_ContextFilter AddElementsByChildrenPath(string childPath, string path = "");
        //Sort
        IEQS_ContextFilter SortByDistance(Vector3 targetPosition, int multiply = 1);
        //Setup
        void SetContext(IEQS_Context context);
    }
}