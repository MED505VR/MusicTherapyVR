using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ScaleChange : MonoBehaviour
{
    [SerializeField]
    private float _gain;
    private float _previousGain;


    // Synchronization
    private ScaleSync _scaleSync;

    // Hand info
    private GameObject leftHand, rightHand;
    private GameObject drumStick1Head, drumStick2Head, drumStick3Head, drumStick4Head;


    private GameObject myObject;
    private RealtimeView myView;

    private float volume = 0.1f;




    private void Awake()
    {
        _scaleSync = GetComponent<ScaleSync>();

        leftHand = GameObject.Find("LeftHandAnchor");

        rightHand = GameObject.Find("RightHandAnchor");

        drumStick1Head = GameObject.Find("Drumstick1Head");
        drumStick2Head = GameObject.Find("Drumstick2Head");
        drumStick3Head = GameObject.Find("Drumstick3Head");
        drumStick4Head = GameObject.Find("Drumstick4Head");

        myObject = GameObject.Find("Table");
        myView = myObject.GetComponent<RealtimeView>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1Head || other.gameObject == drumStick2Head || other.gameObject == drumStick3Head || other.gameObject == drumStick4Head)
        {
            myView.RequestOwnership();
            _gain = volume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1Head || other.gameObject == drumStick2Head || other.gameObject == drumStick3Head || other.gameObject == drumStick4Head)
        {
            _gain = 0;
            myView.ClearOwnership();
        }
    }

    private void Update()
    {

        if (_gain != _previousGain)
        {
            _scaleSync.SetGain(_gain);
            _previousGain = _gain;
        }
    }

}
