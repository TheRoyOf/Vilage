using Building.Configuration.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Building.Configuration
{
    public interface IBuildingConfig
    {
        Construction GetByName(string name);
        Construction GetByPath(string path);
        List<Construction> GetConstructions();
        Transform GetRoot();
    }
}