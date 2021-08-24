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
    }
}