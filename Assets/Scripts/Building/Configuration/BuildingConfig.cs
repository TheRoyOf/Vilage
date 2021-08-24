using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Building.Configuration.Models;
using System;
using Game;

namespace Building.Configuration
{
    public class BuildingConfig : MonoBehaviour, IBuildingConfig
    {
        [SerializeField]
        private Transform root = null;

        [SerializeField]
        private List<Construction> constructions = new List<Construction>();

        private void Awake()
        {
            Pool.SetBuildingConfig(this);
        }

        public List<Construction> GetConstructions()
        {
            return constructions;
        }

        public Construction GetByName(string name)
        {
            return Array.Find(constructions.ToArray(), (c) => { return c.name.Equals(name); });
        }

        public Construction GetByPath(string path)
        {
            return Array.Find(constructions.ToArray(), (c) => { return c.fullPath.Equals(path); });
        }

        public Transform GetRoot()
        {
            return root;
        }
    }
}