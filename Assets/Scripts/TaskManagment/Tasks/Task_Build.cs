using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Building;
using Building.Interactive;
using Ai.Brain;
using Game;
using Building.Configuration.Models;
using Items;
using Ai.EQS;

namespace TaskManagment.Tasks
{
    public class Task_Build : ITask
    {
        private const string BUILDPATH = "buildPath";
        private const string TARGET = "target";


        private ETaskType taskType = ETaskType.BUILD;
        private float priority = 0;

        private bool avable = false;

        private TaskManagment.Action lastAction = null;


        private string buildPath = "";
        private GameObject target = null;
        private float progressStep = 0;
        private float timeForStep = 0;

        private IStorage buildingStorage = null;
        private IInteractive interactive = null;


        public Task_Build()
        {
            Pool.taskManager.RegisterTask(this);
        }

        public void AbortExecution(TaskManagment.Action action = null)
        {
            if(lastAction != null)
            {
                lastAction.performer.BreakeActiveTask();
                lastAction = null;
            }

            if(action != null)
            {
                action.performer.BreakeActiveTask();
            }

            avable = true;
        }

        public Action GetAction(IBrain performer)
        {
            Construction construction = Pool.buildingConfig.GetByPath(buildPath);

            List<Construction.BuildCost> needItems = new List<Construction.BuildCost>();

            //все ли ресурсы на складе? (Нести ресурсы)
            if (!IsContainAllNeedItems(construction, needItems))
            {
                Action action = new Action();

                if (IsAvableAllRequiredItems(needItems))
                {
                    IEQS_ContextElement item = EQS.GetContext(EEQS_ContextType.ITEMS).GetNearest(needItems[0].itemPath, performer.gameObject.transform.localPosition);
                    IEQS_ContextElement storage = EQS.GetContext(EEQS_ContextType.BUILDING)
                        .GetFilter().AddElementsByChildrenPath(needItems[0].itemPath, "Storage").SortByDistance(performer.gameObject.transform.localPosition).GetContextElement();
                    if((item != null && storage == null) || Vector3.Distance(performer.gameObject.transform.localPosition, item.Position) < 
                        Vector3.Distance(performer.gameObject.transform.localPosition, storage.Position))
                    {
                        //item
                        action.SetCarryAction_Item(this, performer, item.gameObject.GetComponent<IItem>(), buildingStorage);
                    }
                    else if (storage != null)
                    {
                        //storage
                        action.SetCarryAction_Storage(this, performer, needItems[0].itemPath, needItems[0].count, storage.gameObject.GetComponent<IStorage>(), buildingStorage);
                    }
                    else
                    {
                        Debug.LogError("Invalide parameters");
                        lastAction = null;
                        return null;
                    }

                    lastAction = action;
                    return action;
                }
            }
            
            //Не максимальный прогресс сборки? (Строить здание)
            if(interactive.GetCurrentProgress() < interactive.GetMaxProgress() && IsContainAllNeedItems(construction))
            {
                Action action = new Action();
                action.SetInteractionAction(this, performer, progressStep, timeForStep, interactive);

                lastAction = action;
                return action;
            }

            lastAction = null;
            return null;
        }

        public void SetTaskPriority(float priority)
        {
            this.priority = priority;
        }

        public float GetTaskPriority()
        {
            return priority;
        }

        public ETaskType GetTaskType()
        {
            return taskType;
        }

        public bool AvableToGet()
        {
            if (!avable)
                return false;

            List<Construction.BuildCost> needItems = new List<Construction.BuildCost>();

            if (!IsContainAllNeedItems(Pool.buildingConfig.GetByPath(buildPath), needItems))
            {
                return IsAvableAllRequiredItems(needItems);
            }

            return false;
        }

        public void SetParameters(Dictionary<string, object> valuePairs)
        {
            object parameter = null;

            if (valuePairs.TryGetValue(BUILDPATH, out parameter))
            {
                buildPath = parameter as string;
            }
            else
            {
                Debug.LogError("Invalide buildPath parameter: " + BUILDPATH);
            }

            if (valuePairs.TryGetValue(TARGET, out parameter))
            {
                target = parameter as GameObject;
                buildingStorage = target.GetComponent<IStorage>();
                interactive = target.GetComponent<IInteractive>();

                if (interactive != null)
                {
                    interactive.SetInteractionResult(target.GetComponent<IInteractionResult>());
                    Construction construction = Pool.buildingConfig.GetByPath(buildPath);
                    if (construction != null)
                    {
                        interactive.SetMaxProgress(construction.maxBuildProgressValue);
                        progressStep = construction.progressStepValue;
                        timeForStep = construction.timeValue;
                    }
                    else
                    {
                        Debug.LogError("Don't find in buildConfig for " + buildPath);
                    }
                }
                else
                    Debug.LogError("Interactive component doesn't found!");
            }
            else
            {
                Debug.LogError("Invalide target parameter: " + TARGET);
            }

            avable = true;
        }

        public Action GetLastAction(string key = "")
        {
            return lastAction;
        }

        private bool IsContainAllNeedItems(Construction construction, List<Construction.BuildCost> needItems = null)
        {
            bool result = true;

            foreach(Construction.BuildCost item in construction.buildCost)
            {
                if(!buildingStorage.IsContain(item.itemPath, item.count))
                {
                    result = false;
                    if(needItems != null)
                    {
                        IItem fromStorage = buildingStorage.GetItem(item.itemPath);
                        if(fromStorage != null)
                        {
                            needItems.Add(new Construction.BuildCost(item.itemPath, item.count - fromStorage.GetCount()));
                        }
                        else
                        {
                            needItems.Add(item);
                        }
                    }
                }
            }

            return result;
        }


        private bool IsAvableAllRequiredItems(List<Construction.BuildCost> needItems)
        {
            foreach (Construction.BuildCost cost in needItems)
            {
                int avableItemCount = 0;

                foreach (IEQS_ContextElement item in EQS.GetContext(EEQS_ContextType.ITEMS).GetAllByPath(cost.itemPath))
                {
                    object parameter;
                    if (item.Parameters.TryGetValue("Count", out parameter))
                        avableItemCount += (int)parameter;

                    if (avableItemCount >= cost.count)
                        break;
                }

                if (avableItemCount >= cost.count)
                    break;


                foreach (IEQS_ContextElement storage in EQS.GetContext(EEQS_ContextType.BUILDING).GetFilter().AddElementsByChildrenPath(cost.itemPath, "Storage").GetContextElements())
                {
                    foreach (IEQS_ContextElement item in storage.GetChildrenByPath(cost.itemPath))
                    {
                        object parameter;
                        if (item.Parameters.TryGetValue("Count", out parameter))
                            avableItemCount += (int)parameter;

                        if (avableItemCount >= cost.count)
                            break;
                    }
                }

                if (avableItemCount < cost.count)
                    return false;
            }

            return true;
        }
    }
}