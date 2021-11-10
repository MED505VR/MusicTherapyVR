using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
/*
public class stopButton : MonoBehaviour
{
    [SerializeField]
    private MoodSound _moodSound;
    private MoodPlay _moodPlay;

    private MeshRenderer _meshRenderer;
    private RealtimeView myView;

    private GameObject leftHand, rightHand, myObject;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myView = GetComponent<RealtimeView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            //_moodSound.play = false;
            _moodPlay._play = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }
}
*/