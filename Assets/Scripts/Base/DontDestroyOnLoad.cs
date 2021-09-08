using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}