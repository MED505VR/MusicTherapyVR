using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pianoBool : MonoBehaviour
{
    public bool pianoPlay = false;
    private bool prevPlay;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        pianoPlay = false;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (pianoPlay != prevPlay) {
            if (pianoPlay) {
                audioSource.Play();
                prevPlay = pianoPlay;
            }
            if (!pianoPlay) {
                audioSource.Stop();
                prevPlay = pianoPlay;
            }
        }
        
    }
}
