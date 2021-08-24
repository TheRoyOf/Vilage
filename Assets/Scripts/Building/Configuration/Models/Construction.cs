using UnityEngine;
using System;
using System.Collections.Generic;

namespace Building.Configuration.Models
{
    [Serializable]
    public class Construction
    {
        public string name = "";
        public string path = "";
        public string fullPath { get { return path + "/" + name; } }
        public GameObject structurePrefab = null;
        public float timeValue = 1;
        public float progressStepValue = 1;
        public float maxBuildProgressValue = 1;
        public List<BuildCost> buildCost = new List<BuildCost>();

        [Serializable]
        public class BuildCost
        {
            public string itemPath = "";
            public int count = 1;

            public BuildCost() { }

            public BuildCost(string itemPath, int count)
            {
                this.itemPath = itemPath;
                this.count = count;
            }
        }
    }
}