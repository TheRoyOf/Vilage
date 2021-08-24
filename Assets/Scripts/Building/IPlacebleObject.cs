using UnityEngine;

namespace Building
{
    public interface IPlacebleObject
    {
        Transform GetTransform();
        void UpdateGhostMaterial();
    }
}