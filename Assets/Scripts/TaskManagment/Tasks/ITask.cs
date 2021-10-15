using Ai.Brain;
using System.Collections.Generic;

namespace TaskManagment.Tasks
{
    public interface ITask
    {
        event System.Action OnTaskComplete;
        event System.Action OnTaskFailed;

        ETaskType TaskType { get; set; }
        float Priority { get; set; }

        TaskManagment.IAction GetAction(IBrain performer);
        TaskManagment.IAction GetLastAction(string key = "");
        void SetParameters(Dictionary<string, object> valuePairs);
        bool AvableToGet();
        void AbortExecution(TaskManagment.IAction action = null);
    }
}