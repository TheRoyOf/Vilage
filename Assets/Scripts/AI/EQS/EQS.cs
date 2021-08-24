using UnityEngine;

namespace Ai.EQS
{
    public static class EQS
    {
        public static IEQS_Context GetContext(EEQS_ContextType contextType)
        {
            return EQS_WorldContextsContainer.GetContext(contextType);
        }
    }
}