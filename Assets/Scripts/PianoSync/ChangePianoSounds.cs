using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ChangePianoSounds : MonoBehaviour
{
    [SerializeField]
    private int _hit = default;
    private int _previousHit = default;

    private int isHit = 1;
    private int isNotHit = 0;

    [SerializeField]
    private string _note =  default;
    private string _previousNote = default;


    private GameObject leftHand, rightHand;

    private GameObject myObject;
    private RealtimeView myView;

    private pianoSoundSync _pianoSync;

    private GameObject noteObject;


    private void Awake()
    {
        _pianoSync = GetComponent<pianoSoundSync>();

        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myObject = GameObject.Find("C3");

        myView = myObject.GetComponent<RealtimeView>();
    }


    private void OnTriggerEnter(Collider other)
    {
        myView.RequestOwnership();
        noteObject = gameObject;
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            print("trigger enter");
            _hit = isHit;
            _note = noteObject.name;
            print(_note);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _hit = isNotHit;
        _note = "no note";
        myView.ClearOwnership();
    }


    private void Update()
    {
        if (_hit != _previousHit)
        {
            _pianoSync.SetHit(_hit);
            _previousHit = _hit;
        }
        if (_note != _previousNote)
        {
            _pianoSync.SetNote(_note);
            _previousNote = _note;
        }
    }
}

