using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Input
{
    public class InputsManager : IInpuntsManager
    {
        public static Action<EInputType> OnInputTypeChanged;

        private EInputType inputType = EInputType.NONE;

        public void Init()
        {

        }

        public void Deactivation()
        {

        }

        public void Update()
        {

        }

        public void OpenBuildingList()
        {
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void SetInputMode(EInputType inputType)
        {
            this.inputType = inputType;
            OnInputTypeChanged?.Invoke(inputType);
        }

        public void Tap()
        {
            switch(inputType)
            {
                case EInputType.BUILDING:
                    Pool.buildingManager.PlaceBuilding();
                    break;
            }
        }

    }
}