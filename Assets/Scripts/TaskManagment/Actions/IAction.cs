using Ai.Brain;
using Building.Interactive;
using System.Collections;
using System.Collections.Generic;
using TaskManagment.Tasks;

namespace TaskManagment
{
    public interface IAction
    {
        event System.Action OnActionComplete;
        event System.Action OnActionFailed;

        ITask Task { get; set; }
        IBrain Performer { get; set; }
        EActionType ActionType { get; set; }

        IEnumerator Action();
        void SetParameters(Dictionary<string, object> parameters);
        void BreakAction();
    }
}