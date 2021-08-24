using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_ContextFilter
    {
        IEQS_ContextElement GetContextElement();
        IEQS_ContextFilter AddElementsOfPath(string path);
        IEQS_ContextFilter SortByDistance(Vector3 targetPosition, int multiply = 1);
        void SetContext(IEQS_Context context);
    }
}