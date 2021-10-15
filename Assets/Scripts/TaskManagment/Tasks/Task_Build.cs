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
using UnityEngine.Profiling;

namespace TaskManagment.Tasks
{
    public class Task_Build : ITask
    {
        public event System.Action OnTaskComplete;
        public event System.Action OnTaskFailed;

        private const string BUILDPATH = "buildPath";
        private const string TARGET = "target";


        private ETaskType taskType = ETaskType.BUILD;
        private float priority = 0;

        private bool avable = false;

        private TaskManagment.IAction lastAction = null;


        private string buildPath = "";
        private GameObject target = null;
        private float progressStep = 0;
        private float timeForStep = 0;

        private IStorage buildingStorage = null;
        private IInteractive interactive = null;

        public ETaskType TaskType { get => taskType; set => taskType = value; }
        public float Priority { get => priority; set => priority = value; }

        public Task_Build()
        {
            Pool.taskManager.RegisterTask(this);
        }

        public void AbortExecution(TaskManagment.IAction action = null)
        {
            if(lastAction != null)
            {
                lastAction = null;
            }

            avable = true;
        }

        public IAction GetAction(IBrain performer)
        {
            Profiler.BeginSample("GetAction");
            Construction construction = Pool.buildingConfig.GetByPath(buildPath);

            List<Construction.BuildCost> needItems = new List<Construction.BuildCost>();

            //все ли ресурсы на складе? (Нести ресурсы)
            if (!IsContainAllNeedItems(construction, needItems))
            {
                IAction action = new Carry_Action();

                if (IsAvableAllRequiredItems(needItems))
                {
                    //request for find nearest requried item
                    IEQS_ContextElement item = EQS.GetContext(EEQS_ContextType.ITEMS).GetNearest(needItems[0].itemPath, performer.gameObject.transform.localPosition);
                    //request for find nearest storage which contain required item
                    IEQS_ContextElement storage = EQS.GetContext(EEQS_ContextType.BUILDING)
                        .GetFilter().AddElementsByChildrenPath(needItems[0].itemPath, "Storage").SortByDistance(performer.gameObject.transform.localPosition).GetContextElement();

                    if((item != null && storage == null) 
                        || Vector3.Distance(performer.gameObject.transform.localPosition, item.Position) 
                        < Vector3.Distance(performer.gameObject.transform.localPosition, storage.Position))
                    {
                        //item
                        action.SetParameters(new Dictionary<string, object> 
                        { 
                            { "task", this }, 
                            { "performer", performer }, 
                            { "path", "" },
                            { "item", item.gameObject.GetComponent<IItem>() },
                            { "storageFrom", null },
                            { "storageTo", buildingStorage },
                            { "count", needItems[0].count }
                        });
                    }
                    else if (storage != null)
                    {
                        //storage
                        action.SetParameters(new Dictionary<string, object>
                        {
                            { "task", this },
                            { "performer", performer },
                            { "path", "" },
                            { "item", null },
                            { "storageFrom", storage.gameObject.GetComponent<IStorage>() },
                            { "storageTo", buildingStorage },
                            { "count", needItems[0].count }
                        });
                    }
                    else
                    {
                        Debug.LogError("Invalide parameters");
                        lastAction = null;
                        OnTaskFailed?.Invoke();
                        Profiler.EndSample();
                        return null;
                    }

                    lastAction = action;
                    Profiler.EndSample();
                    return action;
                }
            }
            
            //Не максимальный прогресс сборки? (Строить здание)
            if(interactive.GetCurrentProgress() < interactive.GetMaxProgress() && IsContainAllNeedItems(construction))
            {
                IAction action = new Interaction_Action();
                action.SetParameters(new Dictionary<string, object>
                {
                    { "task", this },
                    { "performer", performer },
                    { "interactive", interactive },
                    { "time", timeForStep },
                    { "progress", progressStep }
                });

                lastAction = action;
                Profiler.EndSample();
                return action;
            }

            lastAction = null;
            OnTaskComplete?.Invoke();
            Profiler.EndSample();
            return null;
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

        public IAction GetLastAction(string key = "")
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