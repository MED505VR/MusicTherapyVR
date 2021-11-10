using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class movementinstrument : MonoBehaviour
{
    private GameObject lefthand, righthand;
    private RealtimeView myView;
    public bool _play;
    public bool _prevPlay;


    private void Awake()
    {
        lefthand = GameObject.Find("LeftHandAnchor");
        righthand = GameObject.Find("RightHandAnchor");
        _play = false;
        myView = GetComponent<RealtimeView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lefthand || other.gameObject == righthand)
        {
            myView.RequestOwnership();
            _play = !_play;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }
    private void Update()
    {
        if (_play != _prevPlay)
        {
            _moodSync.SetPlay(_play);
            _prevPlay = _play;
        }
    }
}