using Building;
using Game;
using HUD;
using Player.Input;
using System.Collections;
using System.Collections.Generic;
using TaskManagment;
using UnityEngine;


namespace Game
{
    public class DependencyConfiguration : MonoBehaviour
    {
        private List<IManager> managers = new List<IManager>();

        private bool isInit = false;
        void Awake()
        {
            CreateDependensies();

            InitManagers();
        }

        private void Start()
        {
            Pool.hudManager.ShowWidget(EWidgetType.MAIN);
        }

        private void Update()
        {
            foreach (IManager manager in managers)
            {
                manager.Update();
            }
        }

        private void OnDestroy()
        {
            DeactivationManagers();
        }

        private void CreateDependensies()
        {
            if (Pool.buildingManager == null)
            {
                Pool.SetBuildingManager(new BuildingManager());
                managers.Add(Pool.buildingManager);
            }

            if (Pool.hudManager == null)
            {
                Pool.SetHudManager(new HudManager());
                managers.Add(Pool.hudManager);
            }

            if (Pool.inpuntsManager == null)
            {
                Pool.SetInputsManager(new InputsManager());
                managers.Add(Pool.inpuntsManager);
            }

            if (Pool.taskManager == null)
            {
                Pool.SetTaskManager(new TaskManager());
                managers.Add(Pool.taskManager);
            }
        }

        private void InitManagers()
        {
            if (isInit)
                return;

            foreach (IManager manager in managers)
            {
                manager.Init();
            }
        }

        private void DeactivationManagers()
        {
            foreach (IManager manager in managers)
            {
                manager.Deactivation();
            }
        }
    }
}