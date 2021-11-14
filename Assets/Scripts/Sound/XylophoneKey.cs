using System.Collections;
using UnityEngine;

namespace Sound
{
    public class XylophoneKey : SynchronizedSound
    {
        [Header("Haptic Settings")] [SerializeField]
        private float freq;

        [SerializeField] private float amp;
        [SerializeField] private float dura;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DrumstickHeadR") || other.CompareTag("DrumstickHeadL"))
            {
                PlaySynchronizedSound();

                if (other.CompareTag("DrumstickHeadR"))
                    StartCoroutine(Haptic(freq, amp, dura, true, false));
                else if (other.CompareTag("DrumstickHeadL")) StartCoroutine(Haptic(freq, amp, dura, false, true));
            }
        }

        private IEnumerator Haptic(float frequency, float amplitude, float duration, bool rightHand, bool leftHand)
        {
            if (rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
            if (leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

            yield return new WaitForSeconds(duration);
            if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}