using UnityEngine;

namespace Ai.EQS
{
    public static class EQS
    {
        public static void Init()
        {
            EQS_WorldContextsContainer.RegisterContext(EEQS_ContextType.ITEMS, new EQS_Context(EEQS_ContextType.ITEMS));
            EQS_WorldContextsContainer.RegisterContext(EEQS_ContextType.BUILDING, new EQS_Context(EEQS_ContextType.BUILDING));
        }

        public static IEQS_Context GetContext(EEQS_ContextType contextType)
        {
            return EQS_WorldContextsContainer.GetContext(contextType);
        }
    }
}