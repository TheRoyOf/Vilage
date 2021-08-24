using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public static class EQS_WorldContextsContainer
    {
        private static Dictionary<EEQS_ContextType, IEQS_Context> contexts = new Dictionary<EEQS_ContextType, IEQS_Context>();

        public static IEQS_Context GetContext(EEQS_ContextType contextType)
        {
            IEQS_Context context;

            if (contexts.TryGetValue(contextType, out context))
                return context;
            else
                Debug.LogError("Context " + contextType.ToString() + " don't register!");

            return null;
        }

        public static bool RegisterContext(EEQS_ContextType contextType, IEQS_Context context)
        {
            if(contexts.ContainsKey(contextType))
                return false;

            contexts.Add(contextType, context);

            return true;
        }
    }
}
