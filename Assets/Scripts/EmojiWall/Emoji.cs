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
            // Her skal farven på lys i rummet og emojien ændres til deres tilsvarende følelse (e.g. angry = red hue)

            // Emoji lyset
            

            // Emoji farven
            // Sætter alle emojis objects til deres originale farve
            emoji0.GetComponent<Renderer>().material.color = emoji0.GetComponent<Emoji>()._originalColor;
            emoji1.GetComponent<Renderer>().material.color = emoji1.GetComponent<Emoji>()._originalColor;
            emoji2.GetComponent<Renderer>().material.color = emoji2.GetComponent<Emoji>()._originalColor;

            // Sætter deres boolean til false som gør at de kan aktiveres igen når de trykkes på
            emoji0.GetComponent<Emoji>().color = false;
            emoji1.GetComponent<Emoji>().color = false;
            emoji2.GetComponent<Emoji>().color = false;

            // Sætter den emoji der er trykket på til pressed
            emojiRend.material.color = _pressedColor;
        }
    }

    public void Update()
    {
        if (color == true)
        {
            // Skifter farve på emoji feedback
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
