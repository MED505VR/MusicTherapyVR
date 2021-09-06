using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synth : MonoBehaviour
{
    public double frequency = 440;
    private double increment;
    private double phase;
    private double samplingFrequency = 48000.0;

    public float gain = 0.0f;
    private float prevGain;

    public Material material;
    public Material oldMat;
    public GameObject gameObject;

    private MeshRenderer meshrenderer;

    private void Awake()
    {
        meshrenderer = gameObject.GetComponent<MeshRenderer>();
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

    private void Update()
    {
        if(gain != prevGain)
        {
            if(gain > 0)
            {
                meshrenderer.material = material;
                prevGain = gain;
            }
            if(gain == 0)
            {
                meshrenderer.material = oldMat;
                prevGain = gain;
            }
        }
    }

}
