using Normal.Realtime;
using UnityEngine;

public class MoodSync : RealtimeComponent<MoodSyncModel>
{
    
    //private MeshRenderer _meshRenderer;
    private MoodSound _play;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _play = GetComponent<MoodSound>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void OnRealtimeModelReplaced(MoodSyncModel previousModel, MoodSyncModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.playDidChange -= PlayDidChange;
            previousModel.colorDidChange -= ColorDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.play = _play.play;
                currentModel.color = _meshRenderer.material.color;

            UpdateLoopPlay();
            UpdateMeshRendererColor();

            currentModel.colorDidChange += ColorDidChange;
            currentModel.playDidChange += PlayDidChange;
        }
    }

    private void PlayDidChange(MoodSyncModel model, bool value)
    {
        UpdateLoopPlay();
    }

    private void ColorDidChange(MoodSyncModel model, Color value)
    {
        // Update the mesh renderer
        UpdateMeshRendererColor();
    }

    private void UpdateLoopPlay()
    {
        _play.play = model.play;
    }

    private void UpdateMeshRendererColor()
    {
        _meshRenderer.material.color = model.color;
    }

    public void SetPlay(bool play)
    {
        model.play = play;
    }

    public void SetColor(Color color)
    {
        model.color = color;
    }
}
