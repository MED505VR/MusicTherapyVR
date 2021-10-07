using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeNormcore : MonoBehaviour
{
    private Normal.Realtime.Realtime realtime;

    private void Awake()
    {
        realtime = GetComponent<Normal.Realtime.Realtime>();
    }

    private void Start()
    {
        if (Application.isEditor)
            realtime.Connect("Dev Room", null);

        else realtime.Connect("Test Room", null);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}