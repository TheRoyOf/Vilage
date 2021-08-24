using System.Collections;
using System.Collections.Generic;
using TaskManagment.Tasks;
using UnityEngine;

namespace TaskManagment
{
    public class Build_TaskObject : MonoBehaviour, ITaskObject
    {
        private const string BUILDPATH = "buildPath";
        private const string TARGET = "target";

        [SerializeField]
        private string buildPath = "";

        private ITask task = null;

        private void Start()
        {
            CreateTask(new Dictionary<string, object>
            {
                { BUILDPATH, buildPath },
                { TARGET, gameObject }
            });
        }

        public void CreateTask(Dictionary<string, object> valuePairs)
        {
            task = new Task_Build();
            task.SetParameters(valuePairs);
        }

        public void DeleteTask(ITask task)
        {
            if (task == null)
                return;

            task.AbortExecution(task.GetLastAction());
        }

        public List<ITask> GetAllTasks()
        {
            List<ITask> tasks = new List<ITask>
            {
                task
            };
            return tasks;
        }
    }
}