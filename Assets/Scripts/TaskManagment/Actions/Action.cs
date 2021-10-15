using Ai.Brain;
using Building.Interactive;
using Items;
using System.Collections;
using System.Collections.Generic;
using TaskManagment.Tasks;
using UnityEngine;

namespace TaskManagment
{
    public class Action : IAction
    {
        public ITask task = null;
        public IBrain performer = null;

        public EActionType actionType = EActionType.NONE;
        public string path = "";
        public IItem item = null;
        public IInteractive interactive = null;
        public IStorage storageFrom = null;
        public IStorage storageTo = null;
        public int count = 1;
        public float time = 0;
        public float progress = 0;
        public float targetProgress = 0;

        public ITask Task { get => task; set => task = value; }
        public EActionType ActionType { get => actionType; set => actionType = value; }
        IBrain IAction.Performer { get => performer; set => performer = value; }

        public event System.Action OnActionComplete;
        public event System.Action OnActionFailed;

        public void BreakAction()
        {
            throw new System.NotImplementedException();
        }

        public void SetCarryAction_Item(ITask task, IBrain performer, IItem item, IStorage storageTo)
        {
            actionType = EActionType.CARRY;
            this.task = task;
            this.performer = performer;
            this.item = item;
            this.storageTo = storageTo;
        }

        public void SetCarryAction_Storage(ITask task, IBrain performer, string path, int count, IStorage storageFrom, IStorage storageTo)
        {
            actionType = EActionType.CARRY;
            this.task = task;
            this.performer = performer;
            this.path = path;
            this.count = count;
            this.storageFrom = storageFrom;
            this.storageTo = storageTo;
        }

        public void SetInteractionAction(ITask task, IBrain performer, float progressStep, float time, IInteractive interactive)
        {
            actionType = EActionType.INTERACTION;
            this.task = task;
            this.performer = performer;
            this.progress = progressStep;
            this.time = time;
            this.interactive = interactive;
        }

        public void SetParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool StartAction()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IAction.Action()
        {
            throw new System.NotImplementedException();
        }
    }
}