using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SoundChange : MonoBehaviour
{
    [SerializeField]
    private float _gain;
    private float _previousGain;

    [SerializeField]
    private double _frequency;
    private double _previousFrequency;

    private double freqL;
    private double freqR;
    private bool left = false;
    private bool right = false;

    // Synchronization
    private SoundSync _soundSync;

    // Hand info
    private GameObject leftHand, rightHand, drumStick1, drumStick2, drumStick3, drumStick4;
    private Transform leftHandPosition, rightHandPosition, drumStick1Pos, drumStick2Pos, drumStick3Pos, drumStick4Pos;
    private float leftHandY;
    private float rightHandY;


    private GameObject myObject;
    private RealtimeView myView;

    private float volume = 0.1f;




    private void Awake()
    {
        _soundSync = GetComponent<SoundSync>();

        //find hands
        leftHand = GameObject.Find("LeftHandAnchor");
        leftHandPosition = leftHand.transform;
        rightHand = GameObject.Find("RightHandAnchor");
        rightHandPosition = rightHand.transform;

        //Find drumsticks
        drumStick1 = GameObject.Find("Drumstick1Head");
        drumStick1Pos = drumStick1.transform;
        drumStick2 = GameObject.Find("Drumstick2Head");
        drumStick2Pos = drumStick2.transform;
        drumStick3 = GameObject.Find("Drumstick3Head");
        drumStick3Pos = drumStick3.transform;
        drumStick4 = GameObject.Find("Drumstick4Head");
        drumStick4Pos = drumStick4.transform;

        myObject = GameObject.Find("SynthCube");
        myView = myObject.GetComponent<RealtimeView>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1 || other.gameObject == drumStick2 || other.gameObject == drumStick3 || other.gameObject == drumStick4)
        {
            if (other.gameObject == leftHand || other.gameObject == drumStick1 || other.gameObject == drumStick3)
            {
                left = true;
            }
            if (other.gameObject == rightHand || other.gameObject == drumStick2 || other.gameObject == drumStick4)
            {
                right = true;
            }
            myView.RequestOwnership();
            _gain = volume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1 || other.gameObject == drumStick2 || other.gameObject == drumStick3 || other.gameObject == drumStick4)
        {
            _gain = 0;
            if (other.gameObject == leftHand || other.gameObject == drumStick1 || other.gameObject == drumStick3)
            {
                left = false;
            }
            if (other.gameObject == rightHand || other.gameObject == drumStick2 || other.gameObject == drumStick4)
            {
                right = false;
            }
            myView.ClearOwnership();
        }
    }

    private void Update()
    {
        leftHandY = leftHandPosition.position[1];
        rightHandY = rightHandPosition.position[1];
        freqL = 440 + (leftHandY) * 100;
        freqR = 440 + (rightHandY) * 100;
        if (_gain > 0 && left == true)
        {
            _frequency = freqL;
        }
        if (_gain > 0 && right == true)
        {
            _frequency = freqR;
        }


        if (_frequency != _previousFrequency)
        {
            _soundSync.SetFrequency(_frequency);
            _previousFrequency = _frequency;
        }

        if (_gain != _previousGain)
        {
            _soundSync.SetGain(_gain);
            _previousGain = _gain;
        }
    }

}
