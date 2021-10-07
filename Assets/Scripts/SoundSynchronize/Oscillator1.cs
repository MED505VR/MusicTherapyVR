using System;
using UnityEngine;

namespace SoundSynchronize
{
    public class Oscillator1 : MonoBehaviour
    {
        public double frequency = 440;
        private double increment;
        private double phase;
        private double samplingFrequency = 48000.0;

        public float gain = 0.0f;

        public Material trailMaterial;


        private GameObject leftHand, rightHand;
        private Transform leftHandPosition, rightHandPosition;

        private void Awake()
        {

            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
        

        }


        /* private void OnTriggerStay(Collider other)
    {
        Vector3 closestToLeftHand = other.ClosestPoint(leftHandPosition.position);
        if (Vector3.Distance(closestToLeftHand, leftHandPosition.position) < .01f)
        {
            LeaveTrail(closestToLeftHand, .01f, trailMaterial);
        }

        Vector3 closestToRightHand = other.ClosestPoint(rightHandPosition.position);
        if (Vector3.Distance(closestToLeftHand, rightHandPosition.position) < .01f)
        {
            LeaveTrail(closestToLeftHand, .01f, trailMaterial);
        }
    }*/

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TrailRenderer>())
                other.GetComponent<TrailRenderer>().enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<TrailRenderer>())
                other.GetComponent<TrailRenderer>().enabled = false;
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            increment = frequency * 2.0 * Mathf.PI / samplingFrequency;

            for(int i = 0; i < data.Length; i+= channels)
            {
                phase += increment;
                data[i] = (float)(gain * Mathf.Sin((float)phase));

                if(channels == 2)
                {
                    data[i + 1] = data[i];
                }
                if(phase > (Mathf.PI * 2))
                {
                    phase = 0.0;
                }
            }
        }
    }
}
