using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class HudManager : IHudManager
    {
        public static Action<EWidgetType> OnWidgetUpdate;

        public EWidgetType activeWidget = EWidgetType.NONE;

        public void Init()
        {

        }

        public void Deactivation()
        {

        }

        public void Update()
        {

        }

        public void ShowWidget(EWidgetType widgetType)
        {
            HideWidget();

            Pool.widgetReferenceStorage.GetWidgetRoot(widgetType)?.SetActive(true);
            activeWidget = widgetType;
            OnWidgetUpdate?.Invoke(widgetType);
        }

        public void HideWidget()
        {
            if (activeWidget != EWidgetType.NONE)
            {
                Pool.widgetReferenceStorage.GetWidgetRoot(activeWidget)?.SetActive(false);
            }
        }

        public void ShowPopup()
        {

        }

        public void HidePopup()
        {

        }
    }
}
