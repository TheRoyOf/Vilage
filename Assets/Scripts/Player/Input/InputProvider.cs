using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Input
{
    public class InputProvider : MonoBehaviour
    {
        public void OnTapProvide()
        {
            Pool.inpuntsManager.Tap();
        }
    }
}