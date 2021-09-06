using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class pianoLoop : MonoBehaviour
{
    [SerializeField]

    private PianoLoopSync _pianoLoopSync;
    private GameObject leftHand, rightHand;
    Color white = new Color(1, 1, 1, 1);
    Color red = new Color(1, 0, 0, 1);
    private GameObject myObject;
    private RealtimeView myView;


    [SerializeField]
    private Color _color;
    private Color _previousColor;
    public bool _pianoPlay;
    private bool _prevPlay;


    private void Awake()
    {
        _pianoLoopSync = GetComponent<PianoLoopSync>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myObject = GameObject.Find("pianoLoop");
        myView = myObject.GetComponent<RealtimeView>();
        _color = white;
        _pianoPlay = false;
    }

    private void color()
    {
        if (_pianoPlay == true)
        {

            _color = red;
        }
        else if (_pianoPlay == false)
        {

            _color = white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            _pianoPlay = !_pianoPlay;
            color();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    private void Update()
    {

        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (_color != _previousColor)
        {
            _pianoLoopSync.SetColor(_color);
            _previousColor = _color;
        }

        if (_pianoPlay != _prevPlay)
        {
            _pianoLoopSync.setPianoPlay(_pianoPlay);
            _prevPlay = _pianoPlay;
        }

    }
}
