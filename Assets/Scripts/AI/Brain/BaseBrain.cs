using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ai.EQS;
using Game;
using TaskManagment;
using UnityEngine.AI;
using Ai.Inventory;
using Building.Interactive;
using TaskManagment.Tasks;

namespace Ai.Brain
{
    public class BaseBrain : MonoBehaviour, IBrain
    {
        [SerializeField]
        private NavMeshAgent agent = null;

        private IInventory inventory = null;

        TaskManagment.Action activeAction = null;

        private void Awake()
        {
            if(inventory == null)
            {
                inventory = GetComponent<IInventory>();
            }
            else
            {
                Debug.LogError("Inventory not found");
            }
        }

        private void Start()
        {
            UpdateActiveTask();
        }

        public void BreakeActiveTask()
        {
            if(activeAction != null)
            {
                StopCoroutine(CarryAction());
                StopCoroutine(InteractionAction());

                switch(activeAction.actionType)
                {
                    case EActionType.CARRY:
                        if(inventory.GetItemInHand() != null)
                        {
                            inventory.DropFromHand();
                        }
                        agent.SetDestination(gameObject.transform.localPosition);
                        break;

                    case EActionType.INTERACTION:
                        break;
                }

                activeAction = null;
            }
        }

        public void UpdateActiveTask()
        {
            ITask task = Pool.taskManager.GetNextTask(ETaskType.ANY);

            if (task == null)
                return;

            activeAction = task.GetAction(this);

            if(activeAction == null)
            {
                Debug.LogWarning("Next task invalide");

                return;
            }

            StartAction();
        }

        private void StartAction()
        {
            if (activeAction == null)
                return;

            switch(activeAction.actionType)
            {
                case EActionType.CARRY:
                    StartCoroutine(CarryAction());
                    break;

                case EActionType.INTERACTION:
                    StartCoroutine(InteractionAction());
                    break;
            }
        }

        private IEnumerator CarryAction()
        {
            if (activeAction.storageFrom != null)
                agent.SetDestination(activeAction.storageFrom.GetTransform().localPosition);
            else
                agent.SetDestination(activeAction.item.gameObject.transform.localPosition);

            bool waitReachDestination = true;
            while (waitReachDestination)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            waitReachDestination = false;
                        }
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }

            if (activeAction.storageFrom != null)
            {
                inventory.TakeToHand(activeAction.storageFrom.GetItem(activeAction.path, activeAction.count));
            }
            else
            {
                inventory.TakeToHand(activeAction.item);
            }

            agent.SetDestination(activeAction.storageTo.GetTransform().localPosition);

            waitReachDestination = true;
            while (waitReachDestination)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            waitReachDestination = false;
                        }
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }

            activeAction.storageTo.AddItem(inventory.DropFromHand());
            BreakeActiveTask();
        }

        private IEnumerator InteractionAction()
        {
            IInteractive interactive = activeAction.interactive;

            //подходим к целе

            while (interactive.GetCurrentProgress() < interactive.GetMaxProgress())
            {
                yield return new WaitForSeconds(activeAction.time);
                interactive.AddProgress(activeAction.progress);
            }

            BreakeActiveTask();
        }

        public IInventory GetInventory()
        {
            return inventory;
        }
    }
}
