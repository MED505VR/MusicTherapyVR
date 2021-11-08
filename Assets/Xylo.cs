using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Xylo : MonoBehaviour
{
    private AudioSource _source;
    private GameObject myObject;
    private RealtimeView myView;
    [SerializeField]
    public string key;
    public bool _play;
    private bool _prevPlay;

    private DrumSync _drumSync;


    // Start is called before the first frame update
    private void Awake()
    {
        _drumSync = GetComponent<DrumSync>();
        myObject = GameObject.Find(key);
        myView = myObject.GetComponent<RealtimeView>();
        _source = GetComponent<AudioSource>();
        _play = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        myView.RequestOwnership();
        if (other.CompareTag("DrumstickLeft") || other.CompareTag("DrumstickRight"))
        {
            _play = !_play;
            _source.volume = other.gameObject.GetComponent<TrackSpeed>().speed; //will set the volume to speed but max 1
            //color();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myView.ClearOwnership();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_play != _prevPlay)
        {
            _drumSync.SetPlay(_play);
            _prevPlay = _play;
        }
    }
}
