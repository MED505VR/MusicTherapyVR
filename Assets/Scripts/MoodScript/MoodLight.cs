using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MoodLight : MonoBehaviour
{
    [SerializeField]
    private MoodSync _moodSync;

    private MoodPlay _moodPlay;
    private MeshRenderer lightRenderer;

    private Light _light;
    private Color _originalLight;
    private Color _previousLight;

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();
    }
}

