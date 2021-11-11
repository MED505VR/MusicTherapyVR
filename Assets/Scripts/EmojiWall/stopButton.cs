using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class stopButton : MonoBehaviour
{
    [SerializeField]
    public bool play = false;
    private bool prevPlay;
    public GameObject emoji0, emoji1, emoji2;

    private AudioSource source;

    private GameObject leftHand, rightHand;

    private void Start()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            if (emoji0 == true)
            {
                emoji0.GetComponent<MoodPlay>()._play = false;
                emoji0.GetComponent<MoodPlay>().color();
                emoji0.GetComponent<MoodPlay>().stopButtonSync();
                emoji0.GetComponent<MoodPlay>().Update();
            }

            if (emoji1 == true)
            {
                emoji1.GetComponent<MoodPlay>()._play = false;
                emoji1.GetComponent<MoodPlay>().color();
                emoji1.GetComponent<MoodPlay>().stopButtonSync();
                emoji1.GetComponent<MoodPlay>().Update();
            }

            if (emoji2 == true)
            {
                emoji2.GetComponent<MoodPlay>()._play = false;
                emoji2.GetComponent<MoodPlay>().color();
                emoji2.GetComponent<MoodPlay>().stopButtonSync();
                emoji2.GetComponent<MoodPlay>().Update();
            }

            
            
            

            
            
            

            
            
            

        }
    }
}
