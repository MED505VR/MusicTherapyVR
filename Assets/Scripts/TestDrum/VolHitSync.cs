using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class VolHitSync : RealtimeComponent<DrumModel>
{
    private DrumSound _drum;

    private void Awake()
    {
        _drum = GetComponent<DrumSound>();
    }


    private void UpdateVolume()
    {
        _drum.volume = model.volume;
    }

    private void UpdateHit()
    {
        _drum.hit = model.hit;
    }


    protected override void OnRealtimeModelReplaced(DrumModel previousModel, DrumModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.volumeDidChange -= VolumeDidChange;
            previousModel.hitDidChange -= HitDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.volume = _drum.volume;
            currentModel.hit = _drum.hit;

            // Update the mesh render to match the new model
            UpdateVolume();
            UpdateHit();

            // Register for events so we'll know if the color changes later
            currentModel.volumeDidChange += VolumeDidChange;
            currentModel.hitDidChange += HitDidChange;
        }
    }


    private void VolumeDidChange(DrumModel model, float value)
    {
        UpdateVolume();
    }

    private void HitDidChange(DrumModel model, bool value)
    {
        UpdateHit();
    }


    public void SetVolume(float volume)
    {
        model.volume = volume;
    }

    public void SetHit(bool hit)
    {
        model.hit = hit;
    }
}