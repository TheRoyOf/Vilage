using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building.Interactive
{
    public class BaseInteractive : MonoBehaviour, IInteractive
    {
        private float progress = 0;
        private float maxProgress = 0;

        private IInteractionResult interactionResult = null;

        public bool AddProgress(float progress)
        {
            progress = Mathf.Clamp(this.progress + progress, 0, maxProgress);

            if (interactionResult != null && progress == maxProgress)
                interactionResult.ReachMaxProgress();
            else if (interactionResult == null && progress == maxProgress)
                UnityEngine.Debug.LogWarning("ReachMaxProgress is doesn't call! InteractionResult don't set!");

            return progress == maxProgress;
        }

        public void ResetProgress()
        {
            progress = 0;
        }

        public float GetCurrentProgress()
        {
            return progress;
        }

        public void SetMaxProgress(float progress)
        {
            ResetProgress();
            maxProgress = progress;
        }

        public float GetMaxProgress()
        {
            return maxProgress;
        }

        public void SetInteractionResult(IInteractionResult interaction)
        {
            interactionResult = interaction;
        }
    }
}