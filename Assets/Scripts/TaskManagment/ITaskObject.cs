using System.Collections.Generic;
using TaskManagment.Tasks;

namespace TaskManagment
{
    public interface ITaskObject
    {
        void CreateTask(Dictionary<string, object> valuePairs);
        void DeleteTask(ITask task);
        List<ITask> GetAllTasks();
    }
}