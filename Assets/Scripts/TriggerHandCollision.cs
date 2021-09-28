using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TriggerHandCollision : MonoBehaviour
{
    private string _note = default;
    private string _previousNote = default;


    private pianoSoundSync _pianoSoundSync;
    private GameObject leftHand, rightHand;
    private Transform leftHandPosition, rightHandPosition;


    private GameObject myObject;
    private RealtimeView myView;

    public string note;
    private GameObject mynote;


    public int hit = 0;


    private int _hit = 0;
    private int _previousHit = default;

    private void Awake()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        leftHandPosition = leftHand.transform;
        rightHand = GameObject.Find("RightHandAnchor");
        rightHandPosition = rightHand.transform;
        _pianoSoundSync = GetComponent<pianoSoundSync>();
        myObject = GameObject.Find("CScaleCylinder");
        myView = myObject.GetComponent<RealtimeView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        myView.RequestOwnership();
        mynote = gameObject;


        if (other.gameObject == leftHand || other.gameObject == rightHand)
            switch (mynote.name)
            {
                case "C3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("C3");
                    _note = "C3";
                    break;
                case "D3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("D3");
                    _note = "D3";
                    break;
                case "E3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("E3");
                    _note = "E3";
                    break;
                case "F3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("F3");
                    _note = "F3";
                    break;
                case "G3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("G3");
                    _note = "G3";
                    break;
                case "A3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("A3");
                    _note = "A3";
                    break;
                case "B3":
                    _hit = 1;
                    //FindObjectOfType<AudioManager>().Play("B3");
                    _note = "B3";
                    break;
            }
    }


    private void onTriggerExit(Collider other)
    {
        _note = null;
        //_hit = 0;
        myView.ClearOwnership();
    }

    private void Update()
    {
        //print("note: " + note);

        if (hit == 1 && note != null)
        {
            FindObjectOfType<AudioManager>().Play(note);

            _hit = 0;
        }

        if (_hit != _previousHit)
        {
            print("hit: " + hit);
            _pianoSoundSync.SetHit(_hit);
            _previousHit = _hit;
        }

        if (_note != _previousNote)
        {
            _pianoSoundSync.SetNote(_note);
            _previousNote = _note;
        }
    }
}