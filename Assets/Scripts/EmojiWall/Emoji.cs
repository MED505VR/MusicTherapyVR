using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Sound;
using System;

public class Emoji : SynchronizedSound
{
    [SerializeField]
    public GameObject emoji0, emoji1, emoji2;
    public bool play = false;
    public bool color = false;

    private Color _originalColor, _pressedColor;
    Renderer emojiRend;

    private void Awake()
    {
        _originalColor = GetComponent<Renderer>().material.color;
        _pressedColor = Color.Lerp(_originalColor, Color.white, .5f);
        emojiRend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrumstickHeadR") || other.CompareTag("DrumstickHeadL"))
        {
            emoji0.GetComponent<Emoji>().StopSynchronizedSound();
            emoji1.GetComponent<Emoji>().StopSynchronizedSound();
            emoji2.GetComponent<Emoji>().StopSynchronizedSound();

            StopSynchronizedSound();
            PlaySynchronizedSound();

            play = true;
            color = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void colorChange()
    {
        if (color == true)
        {
            // Her skal farven p� lys i rummet og emojien �ndres til deres tilsvarende f�lelse (e.g. angry = red hue)

            // Emoji lyset
            

            // Emoji farven
            // S�tter alle emojis objects til deres originale farve
            emoji0.GetComponent<Renderer>().material.color = emoji0.GetComponent<Emoji>()._originalColor;
            emoji1.GetComponent<Renderer>().material.color = emoji1.GetComponent<Emoji>()._originalColor;
            emoji2.GetComponent<Renderer>().material.color = emoji2.GetComponent<Emoji>()._originalColor;

            // S�tter deres boolean til false som g�r at de kan aktiveres igen n�r de trykkes p�
            emoji0.GetComponent<Emoji>().color = false;
            emoji1.GetComponent<Emoji>().color = false;
            emoji2.GetComponent<Emoji>().color = false;

            // S�tter den emoji der er trykket p� til pressed
            emojiRend.material.color = _pressedColor;
        }
    }

    public void Update()
    {
        if (color == true)
        {
            // Skifter farve p� emoji feedback
            colorChange();
        }
    }

    public void _stop()
    {
        // Den skal bruges til knappen, men knappen registrere ikke collider event.
        Debug.Log("emoji stop method entered");

        StopSynchronizedSound();

        Debug.Log("emoji stop method exitted");
    }
}
