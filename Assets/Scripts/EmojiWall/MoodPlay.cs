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
    private GameObject stopButton;
    private RealtimeView myView;
    private MeshRenderer meshRenderer;
    private MeshRenderer lightRenderer;

    private AudioSource audioSource;

    [SerializeField]
    public bool _play;
    private bool _prevPlay;

    private Color _color = default;
    private Color _previousColor = default;

    private Color _original;

    public Light _light;

    private Color _originalLight;
    public Color _lightColor;
    private Color _previousLight;
    private Color _testLight;

    // #FFF4D6

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();
        meshRenderer = GetComponent<MeshRenderer>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        stopButton = GameObject.Find("stopButton");

        myView = GetComponent<RealtimeView>();
        _play = false;

        _original = GetComponent<Renderer>().material.color;
        _originalLight = _light.GetComponent<Light>().color;

        audioSource = GetComponent<AudioSource>();
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


    public void color()
    {
        if (_play == true)
        {
            // Farven når den er aktiveret
            _color = Color.Lerp(_original, Color.white, .5f);
            _light.color = Color.Lerp(_originalLight, _lightColor, .5f);
        }

        else if (_play == false)
        {
            // Farven når den ikke er aktiveret
            _color = _original;
            _light.color = _originalLight;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    private void Update()
    {
        if (_play != _prevPlay)
        {
            _moodSync.SetPlay(_play);
            _prevPlay = _play;
        }

        if (_color != _previousColor)
        {
            _moodSync.SetColor(_color);
            _previousColor = _color;
        }

        if (_lightColor != _previousLight)
        {
            _moodSync.SetColor(_lightColor);
            _previousLight = _lightColor;
        }
    }

    public void stopButtonSync()
    {
        Update();
    }
}
