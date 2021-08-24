using Building.Configuration.Models;
using Game;
using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building.UI
{
    public class BuildingList : MonoBehaviour
    {
        [SerializeField]
        private ConstructionSlot slotPrefab = null;

        [SerializeField]
        private Transform root = null;

        private void Awake()
        {
            HudManager.OnWidgetUpdate -= CheckUpdate;
            HudManager.OnWidgetUpdate += CheckUpdate;
        }

        private void OnDestroy()
        {
            HudManager.OnWidgetUpdate -= CheckUpdate;
        }

        public void FillList()
        {
            ClearList();

            foreach(Construction construction in Pool.buildingConfig.GetConstructions())
            {
                Instantiate(slotPrefab, root).GetComponent<ConstructionSlot>().SetData(construction);
            }
        }


        private void CheckUpdate(EWidgetType type)
        {
            FillList();
        }

        private void ClearList()
        {
            while(root.transform.childCount > 0)
            {
                DestroyImmediate(root.transform.GetChild(0).gameObject);
            }
        }
    }
}