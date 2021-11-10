using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Xylo : MonoBehaviour
{
    private AudioSource _source;
    private RealtimeView myView;
    [SerializeField]
    public bool _play;
    private bool _prevPlay;

    private DrumSync _drumSync;


    private void Start()
    {
        _drumSync = GetComponent<DrumSync>();
        myView = GetComponent<RealtimeView>();
        _source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _play = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        myView.RequestOwnership();
        if (other.CompareTag("DrumStickHead") || other.CompareTag("DrumstickRight"))
        {
            _play = !_play;
            //_source.volume = other.gameObject.GetComponent<TrackSpeed>().speed; //will set the volume to speed but max 1
            _source.Play();
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
