using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Building;
using HUD;
using Building.Configuration;
using Player.Input;
using TaskManagment;

namespace Game
{
    public class Pool
    {
        //Managers
        public static IBuildingManager buildingManager { get; private set; } = null;
        public static IHudManager hudManager { get; private set; } = null;
        public static IInpuntsManager inpuntsManager { get; private set; } = null;
        public static ITaskManager taskManager { get; private set; } = null;

        //Other
        public static IWidgetReferenceStorage widgetReferenceStorage { get; private set; } = null;
        public static IBuildingConfig buildingConfig { get; private set; } = null;


        public static void SetBuildingManager(IBuildingManager manager)
        {
            buildingManager = manager;
        }

        public static void SetInputsManager(IInpuntsManager manager)
        {
            inpuntsManager = manager;
        }

        public static void SetHudManager(IHudManager manager)
        {
            hudManager = manager;
        }

        public static void SetTaskManager(ITaskManager manager)
        {
            taskManager = manager;
        }

        public static void SetWidgetReferenceStorage(IWidgetReferenceStorage storage)
        {
            widgetReferenceStorage = storage;
        }

        public static void SetBuildingConfig(IBuildingConfig config)
        {
            buildingConfig = config;
        }
    }
}
