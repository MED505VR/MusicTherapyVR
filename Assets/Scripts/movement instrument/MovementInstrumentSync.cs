using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MovementInstrumentSync : MonoBehaviour
{
    private MovementInstrumentSound _movPlay;

    private void Start()
    {
        _movPlay = GetComponent<MovementInstrumentSound>();
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
                currentModel._MovPlay = _movPlay.play;

            currentModel.movPlayDidChange += movPlayDidChange;
        }
    }
}
