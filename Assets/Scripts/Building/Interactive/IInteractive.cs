namespace Building.Interactive
{
    public interface IInteractive
    {
        bool AddProgress(float progress);
        void ResetProgress();
        float GetCurrentProgress();
        void SetMaxProgress(float progress);
        float GetMaxProgress();
        void SetInteractionResult(IInteractionResult interaction);
    }
}