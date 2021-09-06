using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class HandOwnership : MonoBehaviour
{
    private RealtimeView _RealtimeView;
    private RealtimeTransform _RealtimeTransform;

    void Awake()
    {
        _RealtimeView = GetComponent<RealtimeView>();
        _RealtimeTransform = GetComponent <RealtimeTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_RealtimeView.isOwnedLocallyInHierarchy)
        {
            _RealtimeTransform.RequestOwnership();
            _RealtimeView.RequestOwnership();
        }
    }
}
