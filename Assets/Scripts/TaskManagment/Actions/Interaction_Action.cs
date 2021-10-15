using Ai.Brain;
using Building.Interactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskManagment.Tasks
{
    public class Interaction_Action : IAction
    {
        public event System.Action OnActionComplete;
        public event System.Action OnActionFailed;

        public ITask Task { get; set; }
        public IBrain Performer { get; set; }
        public EActionType ActionType { get; set; } = EActionType.INTERACTION;

        private IInteractive interactive = null;
        private float time = 0;
        private float progress = 0;

        bool waitReachDestination = false;

        public void SetParameters(Dictionary<string, object> parameters)
        {
            object bufer = null;

            if (parameters.TryGetValue("task", out bufer) && bufer is ITask)
            {
                Task = (ITask)bufer;
            }

            if (parameters.TryGetValue("performer", out bufer) && bufer is IBrain)
            {
                Performer = (IBrain)bufer;
            }

            if (parameters.TryGetValue("interactive", out bufer) && bufer is IInteractive)
            {
                interactive = (IInteractive)bufer;
            }

            if (parameters.TryGetValue("time", out bufer) && bufer is float)
            {
                time = (float)bufer;
            }

            if (parameters.TryGetValue("progress", out bufer) && bufer is float)
            {
                progress = (float)bufer;
            }
        }

        public IEnumerator Action()
        {
            if(ActionType != EActionType.INTERACTION)
            {
                OnActionFailed?.Invoke();
                yield return null;
            }

            //Setup error reaction
            Performer.OnDestinationInvald -= BreakReachDestination;
            Performer.OnDestinationInvald += BreakReachDestination;

            if (interactive == null)
            {
                OnActionFailed?.Invoke();
                yield return null;
            }

            Performer.MoveTo(interactive.GetInteractionPosition());

            //Wait reach destiation
            waitReachDestination = false;
            Performer.OnDestinationReached -= ReachDestinationReaction;
            Performer.OnDestinationReached += ReachDestinationReaction;
            while (!waitReachDestination)
            {
                yield return new WaitForSeconds(0.1f);
            }

            while (interactive.GetCurrentProgress() < interactive.GetMaxProgress())
            {
                yield return new WaitForSeconds(time);
                interactive.AddProgress(progress);
                Debug.Log("Corrent interaction porogress: " + interactive.GetCurrentProgress());
            }

            OnActionComplete?.Invoke();
        }

        public void BreakAction()
        {

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