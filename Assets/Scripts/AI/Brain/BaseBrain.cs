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
        public event System.Action OnDestinationReached;
        public event System.Action OnDestinationInvald;

        private const int agentStopRate = 100;

        [SerializeField]
        private NavMeshAgent agent = null;

        private IInventory inventory = null;

        private ITask activeTask = null;
        private TaskManagment.IAction activeAction = null;

        private Coroutine action_Coroutine = null;
        private Coroutine moveTo_Coroutine = null;

        IInventory IBrain.inventory { get => inventory; set => inventory = value; }

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
            if (activeTask == null)
            {
                return;
            }

            if (action_Coroutine != null)
                StopCoroutine(action_Coroutine);

            switch (activeAction.ActionType)
            {
                case EActionType.CARRY:
                    if (inventory.GetItemInHand() != null)
                    {
                        inventory.DropFromHand();
                    }
                    agent.SetDestination(gameObject.transform.localPosition);
                    break;

                case EActionType.INTERACTION:
                    break;
            }

            activeTask.OnTaskComplete -= TaskComplete;
            activeTask.OnTaskFailed -= TaskFailed;

            activeAction.OnActionComplete -= ActionComplete;
            activeAction.OnActionFailed -= ActionFailed;

            activeTask.AbortExecution();
            activeAction.BreakAction();

            activeTask = null;
            activeAction = null;
        }

        public void UpdateActiveTask()
        {
            Debug.Log("Update active task");
            if (activeTask != null)
            {
                BreakeActiveTask();
            }

            activeTask = Pool.taskManager.GetNextTask(ETaskType.ANY);

            if(activeTask == null)
            {
                Debug.LogWarning("Next task invalide");
                return;
            }

            activeTask.OnTaskComplete += TaskComplete;
            activeTask.OnTaskFailed += TaskFailed;

            UpdateActiveAction();
        }

        public bool MoveTo(Vector3 destination)
        {
            if (moveTo_Coroutine != null)
            {
                StopCoroutine(moveTo_Coroutine);
            }

            agent.SetDestination(destination);

            if (agent.path.status == NavMeshPathStatus.PathInvalid)
            {
                OnDestinationInvald?.Invoke();
                return false;
            }

            moveTo_Coroutine = StartCoroutine(MoveTo_Coroutine(destination));
            return true;
        }


        private void UpdateActiveAction()
        {
            Debug.Log("Update active action");
            if (activeAction != null)
            {
                activeAction.OnActionComplete -= ActionComplete;
                activeAction.OnActionFailed -= ActionFailed;
                UpdateActiveTask();
                return;
            }

            if (activeTask == null)
            {
                UpdateActiveTask();
            }

            activeAction = activeTask.GetAction(this);

            if(activeAction == null)
            {
                Debug.LogWarning("Next action invalide");
                return;
            }

            activeAction.OnActionComplete += ActionComplete;
            activeAction.OnActionFailed += ActionFailed;

            action_Coroutine = StartCoroutine(activeAction.Action());
        }

        private void ActionComplete()
        {
            Debug.Log("Action complete");
            activeAction = null;
            UpdateActiveAction();
        }

        private void ActionFailed()
        {
            Debug.LogWarning("Action failed");
            UpdateActiveTask();
        }
        

        private void TaskComplete()
        {
            Debug.Log("Task complete");
            UpdateActiveTask();
        }

        private void TaskFailed()
        {
            Debug.LogWarning("Task failed");
            UpdateActiveTask();
        }


        private IEnumerator MoveTo_Coroutine(Vector3 destination)
        {
            if (agent.path.status == NavMeshPathStatus.PathInvalid)
            {
                OnDestinationInvald?.Invoke();
                yield return null;
            }


            while (true)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude <= agent.speed / agentStopRate))
                    {
                        OnDestinationReached?.Invoke();
                        break;
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
