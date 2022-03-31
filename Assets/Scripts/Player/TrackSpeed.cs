using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpeed : MonoBehaviour
{

    private Vector3 lastPosition; //Here we will store the last position
    public float speed; //The speed is what we will set the volume with a max of 1 (Volume cant be more than 1)


    private void Start()
    {
        lastPosition = transform.position; //We take the position from where we spawn and put it to lastPosition 
    }
    private void FixedUpdate()  //Instaed of Update() that runs on every frame FixedUpdate runs every 0.2 sec
    {
        speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime); //How much have we moved in pr sec
        lastPosition = transform.position; //set the current position to the lastPosition
    }
}