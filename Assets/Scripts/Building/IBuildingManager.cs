using Game;
using UnityEngine;

namespace Building
{
    public interface IBuildingManager: IManager
    {
        Material GetGhostMaterial(bool canPlace);
        void HideGhost();
        void PlaceBuilding();
        void SetBuilding(GameObject building);
        void ShowGhost();
        void UpdateGhostPosition();
    }
}