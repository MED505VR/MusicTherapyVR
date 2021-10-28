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


    private GameObject myObject;
    private RealtimeView myView;

    private float volume = 0.1f;




    private void Awake()
    {
        _scaleSync = GetComponent<ScaleSync>();

        myObject = GameObject.Find("Table");
        myView = myObject.GetComponent<RealtimeView>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrumstickLeft") || other.CompareTag("DrumstickRight"))
        {
            myView.RequestOwnership();
            _gain = volume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DrumstickLeft") || other.CompareTag("DrumstickRight"))
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
