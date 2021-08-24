using Game;

namespace HUD
{
    public interface IHudManager: IManager
    {
        void HidePopup();
        void HideWidget();
        void ShowPopup();
        void ShowWidget(EWidgetType widgetType);
    }
}