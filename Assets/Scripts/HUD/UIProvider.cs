using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class UIProvider : MonoBehaviour
    {
        public void ShowMain()
        {
            Pool.hudManager.ShowWidget(EWidgetType.MAIN);
        }

        public void ShowBuildingList()
        {
            Pool.hudManager.ShowWidget(EWidgetType.BUILDING_LIST);
        }

        public void HideWidget()
        {
            Pool.hudManager.HideWidget();
        }
    }
}
