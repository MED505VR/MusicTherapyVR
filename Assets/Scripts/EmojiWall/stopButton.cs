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
            emoji0.GetComponent<MoodPlay>()._play = false;
            emoji1.GetComponent<MoodPlay>()._play = false;
            emoji2.GetComponent<MoodPlay>()._play = false;

            emoji0.GetComponent<MoodPlay>().color();
            emoji1.GetComponent<MoodPlay>().color();
            emoji2.GetComponent<MoodPlay>().color();

        }
    }
}
