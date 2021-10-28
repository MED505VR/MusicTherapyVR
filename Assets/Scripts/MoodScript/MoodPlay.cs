using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MoodPlay : MonoBehaviour
{
    [SerializeField]
    private MoodSync _moodSync;

    private MoodSound moodSound;
    private GameObject leftHand, rightHand;
    private GameObject myObject;
    private RealtimeView myView;
    private MeshRenderer meshRenderer;

    [SerializeField]
    public bool _play;
    private bool _prevPlay;

    private Color _color = default;
    private Color _previousColor = default;

    private Color _original;

    Color gray = Color.gray;

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();
        meshRenderer = GetComponent<MeshRenderer>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myView = GetComponent<RealtimeView>();
        _play = false;

        _original = GetComponent<Renderer>().material.color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            _play = !_play;
            color();
        }
    }

    private void color()
    {
        if (_play == true)
        {
            // Farven når den er aktiveret
            _color = Color.Lerp(_original, Color.white, .5f);
        }

        else if (_play == false)
        {
            // Farven når den ikke er aktiveret
            _color = _original;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    private void Update()
    {
        if (_color != _previousColor)
        {
            _moodSync.SetColor(_color);
            _previousColor = _color;
        }

        if (_play != _prevPlay)
        {
            _moodSync.SetPlay(_play);
            _prevPlay = _play;
        }
    }
}
