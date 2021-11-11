using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MoodPlay : MonoBehaviour
{
    [SerializeField]
    private MoodSync _moodSync; // For normcore syncing 
    private GameObject leftHand, rightHand; // for objects
    public GameObject emoji0, emoji1, emoji2; // For public objects
    private RealtimeView myView; // For normcore object ownership

    // Varaibles for playing the sound
    public bool _play;
    private bool _prevPlay;

    // Variables for colour on the emojis
    private Color _color = default;
    private Color _previousColor = default;
    private Color _original;

    // Variables for the light caused by the emojis
    public Light _light;
    private Color _originalLight;
    public Color _lightColor;
    private Color _previousLight;

    private void Awake() 
    {
        _moodSync = GetComponent<MoodSync>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myView = GetComponent<RealtimeView>();
        _play = false;
        _original = GetComponent<Renderer>().material.color;
        _originalLight = _light.GetComponent<Light>().color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            this.GetComponent<MoodSync>().SetPlay(false);
            //emoji1.GetComponent<MoodSound>().play = false;
            //emoji2.GetComponent<MoodSound>().play = false;

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

    public void Update()
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
            _moodSync.SetLight(_lightColor);
            _previousLight = _lightColor;
        }
    }

    public void stopButtonSync()
    {
        this.Update();
        
    }
}
