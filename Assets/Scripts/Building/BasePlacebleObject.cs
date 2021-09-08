using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BasePlacebleObject : MonoBehaviour, IPlacebleObject
    {
        public Transform GetTransform()
        {
            return gameObject.transform;
        }

        public void UpdateGhostMaterial()
        {

        }
    }
}