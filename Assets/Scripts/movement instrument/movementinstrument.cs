using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class movementinstrument : MonoBehaviour
{
    private GameObject lefthand, righthand;
    private RealtimeView myView;
    public bool _movPlay;
    public bool _prevMovPlay;


    private void Awake()
    {
        lefthand = GameObject.Find("LeftHandAnchor");
        righthand = GameObject.Find("RightHandAnchor");
        _movPlay = false;
        myView = GetComponent<RealtimeView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lefthand || other.gameObject == righthand)
        {
            myView.RequestOwnership();
            _movPlay = !_movPlay;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }
    private void Update()
    {
        if (_movPlay != _prevMovPlay)
        {

            _prevMovPlay = _movPlay;
        }
    }
}