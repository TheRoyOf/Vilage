using Game;
using HUD;
using Player.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BuildingManager : IBuildingManager
    {
        public static Action<GameObject> OnActiveBuildingChange;

        private GameObject buildingPrefab = null;
        private Material greenGhostMaterial = null;
        private Material redGhostMaterial = null;


        private bool ghostActive = false;
        private IPlacebleObject ghostObject = null;

        private float raycastDistance = 500f;
        private int layserMask = 11 << 12;
        private Vector3 prevPosition = Vector3.zero;


        public void Init()
        {
            InputsManager.OnInputTypeChanged -= CheckShowGhost;
            InputsManager.OnInputTypeChanged += CheckShowGhost;
        }

        public void Update()
        {
            UpdateGhostPosition();
        }

        public void Deactivation()
        {
            InputsManager.OnInputTypeChanged -= CheckShowGhost;
        }

        public void UpdateGhostPosition()
        {
            if (ghostActive)
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, layserMask);
                ghostObject.GetTransform().position = Vector3.Lerp(prevPosition, hit.point, 2f);
                prevPosition = ghostObject.GetTransform().position;
                // Add using camera rotation
                ghostObject.UpdateGhostMaterial();
            }
        }

        public void SetBuilding(GameObject building)
        {
            if (building != null)
            {
                buildingPrefab = building;

                ShowGhost();

                Pool.inpuntsManager.SetInputMode(EInputType.BUILDING);
                Pool.hudManager.ShowWidget(EWidgetType.MAIN);

                OnActiveBuildingChange?.Invoke(building);
            }
        }

        public void PlaceBuilding()
        {
            if (!ghostActive)
                return;

            GameObject building = UnityEngine.Object.Instantiate(buildingPrefab, ghostObject.GetTransform().localPosition, ghostObject.GetTransform().localRotation);
            
            if (Pool.buildingConfig.GetRoot() != null)
                building.transform.parent = Pool.buildingConfig.GetRoot();
            HideGhost();
        }

        public void ShowGhost()
        {
            if (ghostActive)
                return;

            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, layserMask);
            ghostObject = UnityEngine.Object.Instantiate(buildingPrefab, hit.point, Quaternion.identity).GetComponent<BasePlacebleObject>();

            if(Pool.buildingConfig.GetRoot() != null)
                ghostObject.GetTransform().parent = Pool.buildingConfig.GetRoot();

            if(ghostObject == null)
            {
                UnityEngine.Debug.LogError("Component 'PlacebleObject' not find!");
                return;
            }

            prevPosition = ghostObject.GetTransform().position;

            ghostActive = true;
        }

        public void HideGhost()
        {
            if (ghostObject != null)
            {
                ghostActive = false;
                UnityEngine.Object.Destroy(ghostObject.GetTransform().gameObject);
                ghostObject = null;
            }
        }

        public Material GetGhostMaterial(bool canPlace)
        {
            return canPlace ? greenGhostMaterial : redGhostMaterial;
        }


        private void CheckShowGhost(EInputType inputType)
        {
            if(inputType.Equals(EInputType.BUILDING))
            {
                ShowGhost();
            }
            else
            {
                HideGhost();
            }
        }
    }
}
