using Normal.Realtime;
using UnityEngine;

namespace LoopScripts
{
    public class BeatSync : RealtimeComponent<BeatModel>
    {
        private MeshRenderer _meshRenderer;
        private BeatTrigger _play;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _play = GetComponent<BeatTrigger>();
        }

        private void UpdateMeshRendererColor()
        {
            // Get the color from the model and set it on the mesh renderer.
            _meshRenderer.material.color = model.color;
        }

        private void UpdateLoopPlay()
        {
            _play.play = model.play;
        }

        protected override void OnRealtimeModelReplaced(BeatModel previousModel, BeatModel currentModel)
        {
            if (previousModel != null)
            {
                // Unregister from events
                previousModel.colorDidChange -= ColorDidChange;
                previousModel.playDidChange -= PlayDidChange;
            }

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.color = _meshRenderer.material.color;
                currentModel.play = _play.play;
            }

            UpdateMeshRendererColor();
            UpdateLoopPlay();

            currentModel.colorDidChange += ColorDidChange;
            currentModel.playDidChange += PlayDidChange;
        }

        private void ColorDidChange(BeatModel model, Color value)
        {
            // Update the mesh renderer
            UpdateMeshRendererColor();
        }

        private void PlayDidChange(BeatModel model, bool value)
        {
            // Update the mesh renderer
            UpdateLoopPlay();
        }

        public void SetColor(Color color)
        {
            // Set the color on the model

            model.color = color;
        }


        public void Setplay(bool play)
        {
            model.play = play;
        }
    }
}