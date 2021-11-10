using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInstrumentSound : MonoBehaviour
{

    public bool MovPlay = false;
    private bool prevMovplay;
    private AudioSource AudioSource;

    private void Start()
    {
        MovPlay = false;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovPlay != prevMovplay) {
            if (MovPlay) {
                AudioSource.Play();
                prevMovplay = MovPlay;
            }

            if (!MovPlay) {
                AudioSource.Stop();
                prevMovplay = MovPlay;
            }
        }
    }
}
