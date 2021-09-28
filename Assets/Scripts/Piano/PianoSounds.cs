using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSounds : MonoBehaviour
{
    public int hit = 0;
    public string note;
    public string NOTESTRING;

    private void Update()
    {
        if (hit == 1)
        {
            print("HIIIIIIT");
            print(hit);
            FindObjectOfType<AudioManager>().Play("C3");
        }
    }
}