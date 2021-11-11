using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MovementInstrumentSync : RealtimeComponent<MovModel>
{
    private MovementInstrumentSound movement;

    private void Start()
    {
        movement = GetComponent<MovementInstrumentSound>();
    }
    protected override void OnRealtimeModelReplaced(MovModel previousModel, MovModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.movPlayDidChange -= movPlayDidChange;

        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.movPlay = movement.MovPlay;
            UpdatePlay();
            currentModel.movPlayDidChange += movPlayDidChange;
        }
    }
    private void movPlayDidChange(MovModel model, bool value) {
        UpdatePlay();
    }
    private void UpdatePlay() {
        movement.MovPlay = MovModel.movPlay;
    }
}
