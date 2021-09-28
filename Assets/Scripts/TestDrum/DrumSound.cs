using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSound : MonoBehaviour
{
    public bool hit = false;
    private bool prevHit;

    public float volume = default;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        hit = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit != prevHit)
            if (hit)
            {
                audioSource.pitch = volume;
                audioSource.Play();
                prevHit = hit;
            }
    }
}