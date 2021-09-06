using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringScript : MonoBehaviour
{
    public double frequency = 440;
    private double increment;
    private double phase;
    private double samplingFrequency = 48000.0;
    public float gain = 0.0f;

    private GameObject leftHand, rightHand;


    private void Awake()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            gain = 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            gain = 0f;
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / samplingFrequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0;
            }
        }
    }
}
