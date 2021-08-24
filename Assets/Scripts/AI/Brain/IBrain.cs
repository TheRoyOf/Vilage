using Ai.Inventory;

namespace Ai.Brain
{
    public interface IBrain
    {
        IInventory GetInventory();
        void BreakeActiveTask();
        void UpdateActiveTask();
    }
}