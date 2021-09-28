using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class pianoSoundSync : RealtimeComponent<PianoModel>
{
    private PianoSounds _piano;

    private void Awake()
    {
        _piano = GetComponent<PianoSounds>();
    }

    private void UpdateNote()
    {
        _piano.note = model.note;
    }

    private void UpdateHit()
    {
        _piano.hit = model.hit;
    }

    protected override void OnRealtimeModelReplaced(PianoModel previousModel, PianoModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.noteDidChange -= NoteDidChange;
            previousModel.hitDidChange -= HitDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.note = _piano.note;
            currentModel.hit = _piano.hit;


            // Update the mesh render to match the new model
            UpdateNote();
            UpdateHit();

            // Register for events so we'll know if the color changes later
            currentModel.noteDidChange += NoteDidChange;
            currentModel.hitDidChange += HitDidChange;
        }
    }


    private void NoteDidChange(PianoModel model, string value)
    {
        UpdateNote();
    }

    private void HitDidChange(PianoModel model, int value)
    {
        UpdateHit();
    }


    public void SetNote(string note)
    {
        model.note = note;
    }

    public void SetHit(int hit)
    {
        model.hit = hit;
    }
}