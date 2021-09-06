using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ScaleSync : RealtimeComponent<ScaleModel>
{
    private Synth _synth;

    private void Awake()
    {
        _synth = GetComponent<Synth>();
    }


    private void UpdateGain()
    {
        _synth.gain = model.gain;
    }


    protected override void OnRealtimeModelReplaced(ScaleModel previousModel, ScaleModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.gainDidChange -= GainDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            currentModel.gain = _synth.gain;

            // Update the mesh render to match the new model
            UpdateGain();

            // Register for events so we'll know if the color changes later
            currentModel.gainDidChange += GainDidChange;
        }
    }


    private void GainDidChange(ScaleModel model, float value)
    {
        UpdateGain();
    }


    public void SetGain(float gain)
    {
        model.gain = gain;
    }
}
