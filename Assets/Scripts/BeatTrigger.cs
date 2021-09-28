using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTrigger : MonoBehaviour
{
    public bool play = false;
    private bool prevPlay;
    private AudioSource audioSource;

    private void Awake()
    {
        play = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (play != prevPlay)
        {
            if (play)
            {
                audioSource.Play();
                prevPlay = play;
            }

            if (!play)
            {
                audioSource.Stop();
                prevPlay = play;
            }
        }
    }
}