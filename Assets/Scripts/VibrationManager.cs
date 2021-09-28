using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager singleton;

    // Start is called before the first frame update
    private void Start()
    {
        if (singleton && singleton != this)

            Destroy(this);
        else
            singleton = this;
    }

    public void TriggerVibration(int vibrationduration, int freq, int amp, OVRInput.Controller controller)
    {
        var clip = new OVRHapticsClip(vibrationduration);

        for (var i = 0; i < vibrationduration; i++) clip.WriteSample(i % freq == 0 ? (byte)amp : (byte)0);

        if (controller == OVRInput.Controller.LTouch)
            OVRHaptics.LeftChannel.Preempt(clip);
        else if (controller == OVRInput.Controller.RTouch) OVRHaptics.RightChannel.Preempt(clip);
    }
}