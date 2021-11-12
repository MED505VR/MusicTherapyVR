using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Sound;
using System;

public class buttonStop : SynchronizedSound
{
    [SerializeField]
    public GameObject emoji0, emoji1, emoji2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrumstickHeadR") || other.CompareTag("DrumstickHeadL"))
        {
            //emoji0.GetComponent<Emoji>()._stop();
            //emoji1.GetComponent<Emoji>()._stop();
            //emoji2.GetComponent<Emoji>()._stop();

            Debug.Log("button is registered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exitted from collider");
    }
}