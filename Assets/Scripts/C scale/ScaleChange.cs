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
    private GameObject drumStick1, drumStick2;


    private GameObject myObject;
    private RealtimeView myView;

    private float volume = 0.1f;




    private void Awake()
    {
        _scaleSync = GetComponent<ScaleSync>();

        leftHand = GameObject.Find("LeftHandAnchor");

        rightHand = GameObject.Find("RightHandAnchor");

        drumStick1 = GameObject.Find("Drumstick1");
        drumStick2 = GameObject.Find("Drumstick2");

        myObject = GameObject.Find("Table");
        myView = myObject.GetComponent<RealtimeView>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1 || other.gameObject == drumStick2)
        {
            myView.RequestOwnership();
            _gain = volume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand || other.gameObject == drumStick1 || other.gameObject == drumStick2)
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
