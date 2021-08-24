using Game;

namespace Player.Input
{
    public interface IInpuntsManager: IManager
    {
        void Tap();
        void Select();
        void OpenBuildingList();
        void SetInputMode(EInputType inputType);
    }
}
