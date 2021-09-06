using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumColor : MonoBehaviour
{
    public Color DrumSkinOriginal = new Color(204,204,169,255);
    public Color DrumSkinHit = new Color(89, 89, 89, 255);
    private GameObject leftHand, rightHand, myObject;

    private void Awake()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        myObject = GameObject.Find("Skin");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            myObject.GetComponent<Renderer>().material.color = DrumSkinHit;
        }
        else if (other.gameObject != leftHand || other.gameObject != rightHand)
        {
            myObject.GetComponent<Renderer>().material.color = DrumSkinOriginal;
        }
    }
}
