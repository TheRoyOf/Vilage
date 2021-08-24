using Game;
using TaskManagment.Tasks;

namespace TaskManagment
{
    public interface ITaskManager: IManager
    {
        ITask GetNextTask(ETaskType taskType);
        void RegisterTask(ITask task);
        bool RemoveTask(ITask task);
    }
}