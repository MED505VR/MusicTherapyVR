using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MoodPlay : MonoBehaviour
{
    public AudioSource[] audioSources;
    public Material materialBase, materialPressed;

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
        materialBase = meshRenderer.material;
    }

    private void Start()
    {
        audioSources = gameObject.transform.parent.GetComponentsInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (AudioSource audioSource in audioSources) {
            audioSource.Stop();
        } 

        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            _play = !_play;
            meshRenderer.material = materialPressed;
        }
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
        meshRenderer.material = materialBase;
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
