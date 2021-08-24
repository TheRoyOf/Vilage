using Ai.Brain;
using System.Collections.Generic;

namespace TaskManagment.Tasks
{
    public interface ITask
    {
        TaskManagment.Action GetAction(IBrain performer);
        TaskManagment.Action GetLastAction(string key = "");
        void SetParameters(Dictionary<string, object> valuePairs);
        bool AvableToGet();
        ETaskType GetTaskType();
        void SetTaskPriority(float priority);
        float GetTaskPriority();
        void AbortExecution(TaskManagment.Action action = null);
    }
}