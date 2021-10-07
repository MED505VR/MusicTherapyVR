using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MoodPlay : MonoBehaviour
{
    [SerializeField]
    private MoodSync _moodSync;
    private GameObject leftHand, rightHand;
    private GameObject myObject;
    private RealtimeView myView;
    private MeshRenderer meshRenderer;

    [SerializeField]
    public bool _play;
    private bool _prevPlay;
    private Color _color, _prevColor;

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();
        meshRenderer = GetComponent<MeshRenderer>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        //myObject = GameObject.Find("AngryEmoji");   // Change name for another object
        myView = GetComponent<RealtimeView>();
        _play = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            _play = !_play;
        }

        //color();
    }

    /*private void color()
    {
        if (_play == true)
        {
            _color = ;
        }

        else if (_play == false)
        {
            _color = ;
        }
    } */

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    private void Update()
    {
        /*if (_color != _prevColor)
        {
            _moodSync.SetColor(_color);
            _prevColor = _color;
        }*/

        if (_play != _prevPlay)
        {
            _moodSync.SetPlay(_play);
            _prevPlay = _play;
        }
    }
}
