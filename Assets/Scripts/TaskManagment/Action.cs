using Ai.Brain;
using Building.Interactive;
using Items;
using System.Collections;
using System.Collections.Generic;
using TaskManagment.Tasks;
using UnityEngine;

namespace TaskManagment
{
    public class Action
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

        public void SetCarryAction_Item(ITask task, IBrain performer, IItem item, IStorage storageTo)
        {
            this.task = task;
            this.performer = performer;
            this.item = item;
            this.storageTo = storageTo;
        }
        
        public void SetCarryAction_Storage(ITask task, IBrain performer, string path, int count, IStorage storageFrom, IStorage storageTo)
        {
            this.task = task;
            this.performer = performer;
            this.path = path;
            this.count = count;
            this.storageFrom = storageFrom;
            this.storageTo = storageTo;
        }
        
        public void SetInteractionAction(ITask task, IBrain performer, float progressStep, float time, IInteractive interactive)
        {
            this.task = task;
            this.performer = performer;
            this.progress = progressStep;
            this.time = time;
            this.interactive = interactive;
        }
    }
}