using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class audioColider : MonoBehaviour
{
    private GameObject leftHand, rightHand;
    Color white = new Color(0, 0, 0, 1);
    Color black = new Color(1, 1, 1, 1);
    private GameObject myObject;
    private RealtimeView myView;
    private GameObject beat;
    private object color;

    private void Awake()
    {
        // Get a reference to the color sync component
        beat = GameObject.Find("beat");
        color = beat.GetComponent<Color>();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myObject = GameObject.Find("beat");

        color = white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand & beat.GetComponent<Color>() == white)
        {
            myView.RequestOwnership();
            color = black;

            // play clip


            myView.ClearOwnership();
        }

        else if (other.gameObject == leftHand || other.gameObject == rightHand & beat.GetComponent<Color>() == black){

            myView.RequestOwnership();

            color = white;

            //play clip

            myView.ClearOwnership();


        }
    }
}
