using UnityEngine;

namespace HUD
{
    public interface IWidgetReferenceStorage
    {
        bool AddWidget(EWidgetType widgetType, GameObject root);
        GameObject GetWidgetRoot(EWidgetType widgetType);
        bool IsContainsWidget(EWidgetType widgetType);
    }
}