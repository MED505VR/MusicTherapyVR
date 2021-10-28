using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class VibrationScripts : MonoBehaviour
{
    [SerializeField]
    private float freq;
    [SerializeField]
    private float amp;
    [SerializeField]
    private float dura;


    private GameObject leftHand, rightHand;
    private GameObject drumStick1Head, drumStick2Head, drumStick3Head, drumStick4Head;
    private GameObject myObject;
    private RealtimeView myView;


    IEnumerator Haptic(float frequency, float amplitude, float duration, bool rightHand, bool leftHand)
    {
        if (rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(duration);
        if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    private void Awake()
    {
        myObject = GameObject.Find("Table");
        myView = myObject.GetComponent<RealtimeView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrumstickLeft"))
        {
            myView.RequestOwnership();
            StartCoroutine(Haptic(freq, amp, dura, false, true));
        }

        if (other.CompareTag("DrumstickRight"))
        {
            myView.RequestOwnership();
            StartCoroutine(Haptic(freq, amp, dura, true, false));

        }
    }
}
