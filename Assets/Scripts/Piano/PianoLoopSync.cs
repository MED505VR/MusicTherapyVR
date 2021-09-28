using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PianoLoopSync : RealtimeComponent<pianoLoopModel>
{
    private MeshRenderer _meshRenderer;

    private pianoBool _pianoBool;

    // Start is called before the first frame update
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _pianoBool = GetComponent<pianoBool>();
    }

    private void UpdateColor()
    {
        _meshRenderer.material.color = model.color;
    }

    private void UpdatePianoPlay()
    {
        _pianoBool.pianoPlay = model.pianoPlay;
    }

    protected override void OnRealtimeModelReplaced(pianoLoopModel previousModel, pianoLoopModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.colorDidChange -= ColorDidChange;
            previousModel.pianoPlayDidChange -= pianoPlayDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.color = _meshRenderer.material.color;
            currentModel.pianoPlay = _pianoBool.pianoPlay;
        }

        UpdateColor();
        UpdatePianoPlay();

        currentModel.colorDidChange += ColorDidChange;
        currentModel.pianoPlayDidChange += pianoPlayDidChange;
    }

    private void ColorDidChange(pianoLoopModel model, Color value)
    {
        UpdateColor();
    }

    private void pianoPlayDidChange(pianoLoopModel model, bool value)
    {
        UpdatePianoPlay();
    }

    public void SetColor(Color color)
    {
        model.color = color;
    }

    public void setPianoPlay(bool pianoPlay)
    {
        model.pianoPlay = pianoPlay;
    }
}