using Ai.Brain;
using Building.Interactive;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskManagment.Tasks
{
    public class Carry_Action : IAction //Добавить механизм сохранения состояния для логики сохранения\загрузки
    {
        public event System.Action OnActionComplete;
        public event System.Action OnActionFailed;

        public ITask Task { get; set; }
        public IBrain Performer { get; set; }
        public EActionType ActionType { get; set; } = EActionType.CARRY;

        private string path = "";
        private IItem item = null;
        private IStorage storageFrom = null;
        private IStorage storageTo = null;
        private int count = 1;

        bool waitReachDestination = false;

        public void SetParameters(Dictionary<string, object> parameters)
        {
            object bufer = null;

            if(parameters.TryGetValue("task", out bufer) && bufer is ITask)
            {
                Task = (ITask) bufer;
            }

            if(parameters.TryGetValue("performer", out bufer) && bufer is IBrain)
            {
                Performer = (IBrain) bufer;
            }

            if(parameters.TryGetValue("path", out bufer) && bufer is string)
            {
                path = (string) bufer;
            }

            if(parameters.TryGetValue("item", out bufer) && bufer is IItem)
            {
                item = (IItem) bufer;
            }

            if(parameters.TryGetValue("storageFrom", out bufer) && bufer is IStorage)
            {
                storageFrom = (IStorage) bufer;
            }

            if(parameters.TryGetValue("storageTo", out bufer) && bufer is IStorage)
            {
                storageTo = (IStorage) bufer;
            }

            if(parameters.TryGetValue("count", out bufer) && bufer is int)
            {
                count = (int) bufer;
            }
        }

        public IEnumerator Action()
        {
            if (ActionType != EActionType.CARRY)
                yield return null;

            //Setup error reaction
            Performer.OnDestinationInvald -= BreakReachDestination;
            Performer.OnDestinationInvald += BreakReachDestination;

            //Move to first target
            if (storageFrom != null)
                Performer.MoveTo(storageFrom.GetInteractionPosition());
            else
                Performer.MoveTo(item.gameObject.transform.localPosition);

            //Wait reach destiation
            waitReachDestination = false;
            Performer.OnDestinationReached -= ReachDestinationReaction;
            Performer.OnDestinationReached += ReachDestinationReaction;
            while (!waitReachDestination)
            {
                yield return new WaitForSeconds(0.1f);
            }

            //Get item
            if (storageFrom != null)
            {
                Performer.inventory.TakeToHand(storageFrom.GetItem(path, count));
            }
            else
            {
                Performer.inventory.TakeToHand(item);
            }

            //Move to second target
            Performer.MoveTo(storageTo.GetInteractionPosition());

            //Wait reach destiation
            waitReachDestination = false;
            Performer.OnDestinationReached -= ReachDestinationReaction;
            Performer.OnDestinationReached += ReachDestinationReaction;
            while (!waitReachDestination)
            {
                yield return new WaitForSeconds(0.1f);
            }

            //Pull item
            storageTo.AddItem(Performer.inventory.DropFromHand());

            OnActionComplete?.Invoke();
        }

        public void BreakAction()
        {
            OnActionFailed?.Invoke();
        }


        private void ReachDestinationReaction()
        {
            waitReachDestination = true;
        }

        private void BreakReachDestination()
        {
            Debug.LogError("Break reach destination!!!");

        }
    }
}