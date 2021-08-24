using Building.Configuration.Models;
using Game;
using HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building.UI
{
    public class ConstructionSlot : MonoBehaviour
    {
        Construction construction = null;

        public void SetData(Construction construction)
        {
            this.construction = construction;
        }

        public void ClickAction()
        {
            Pool.buildingManager.SetBuilding(construction.structurePrefab);
        }
    }
}