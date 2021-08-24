namespace Building.Interactive
{
    public interface IInteractionResult
    {
        void ReachMaxProgress();
        void ProgressFail();
    }
}
