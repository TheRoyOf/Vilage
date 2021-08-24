using Game;
using System.Collections;
using System.Collections.Generic;
using TaskManagment.Tasks;
using UnityEngine;

namespace TaskManagment
{
    public class TaskManager : ITaskManager
    {
        private List<ITask> taskList = new List<ITask>();
        //private Dictionary<ETaskType, Queue<ITask>> taskDictionary = new Dictionary<ETaskType, Queue<ITask>>();

        public void Deactivation()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterTask(ITask task)
        {
            taskList.Add(task);
        }

        public bool RemoveTask(ITask task)
        {
            return taskList.Remove(task);
        }

        public ITask GetNextTask(ETaskType taskType)
        {
            switch (taskType)
            {
                case ETaskType.ANY:
                    return taskList.Find((t) => { return t.AvableToGet(); });
            }

            return taskList.Find((t) => { return t.AvableToGet(); });
        }

        //храним задачи и ссылки на объекты-задачи
        //определяем какие задачи и экшены давать на запрос
        //провайдим события

    }
}