using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class beatSync : RealtimeComponent<beatModel>
{
    private MeshRenderer _meshRenderer;
    private beatBool _play;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _play = GetComponent<beatBool>();
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
    protected override void OnRealtimeModelReplaced(beatModel previousModel, beatModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.colorDidChange -= ColorDidChange;
            previousModel.playDidChange -= playDidChange;
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
        currentModel.playDidChange += playDidChange;



    }
    private void ColorDidChange(beatModel model, Color value)
    {
        // Update the mesh renderer
        UpdateMeshRendererColor();
    }

    private void playDidChange(beatModel model, bool value)
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
