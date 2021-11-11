using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : SynchronizedSound
{

    private GameObject leftHand, rightHand;
    private Transform leftHandPos, rightHandPos;
    private float lefthandY, rightHandY;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        leftHandPos = leftHand.transform;
        rightHand = GameObject.Find("RightHandAnchor");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
