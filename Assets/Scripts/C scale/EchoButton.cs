using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class EchoButton : MonoBehaviour
{
    private GameObject myObject;
    private RealtimeView myView;

    private bool _echo = false;
    private bool _previousEcho;

    private EchoSync _echoSync;


    private GameObject leftHand, rightHand;
    //private AudioEchoFilter echo;
    private void Awake()
    {
        myObject = GameObject.Find("Table");
        myView = myObject.GetComponent<RealtimeView>();

        _echoSync = GetComponent<EchoSync>();
    }

    private void OnTriggerEnter(Collider other)
    {
        myView.RequestOwnership();
        if (other.CompareTag("DrumstickLeft") || other.CompareTag("DrumstickRight"))
        {
            _echo = !_echo;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    // Update is called once per frame
    void Update()
    {
        if (_echo != _previousEcho)
        {
            _echoSync.SetEcho(_echo);
            _previousEcho = _echo;
        }
    }
}
