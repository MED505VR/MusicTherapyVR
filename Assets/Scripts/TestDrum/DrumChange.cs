using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class DrumChange : MonoBehaviour
{

    [SerializeField]
    private float maxCollisionVelocity = 5f;

    private GameObject leftHand, rightHand;
    private Transform leftHandPosition, rightHandPosition;
    private AudioSource audioSource;

    private GameObject myObject;
    private RealtimeView myView;

    private float _volume;
    private float _prevVolume;

    private bool _hit;
    private bool _prevHit;

    private VolHitSync _sync;

    private void Awake()
    {
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        leftHandPosition = leftHand.transform;
        rightHandPosition = rightHand.transform;
        audioSource = GetComponent<AudioSource>();
        myObject = GameObject.Find("DrumCylinder");
        myView = myObject.GetComponent<RealtimeView>();
        _sync = GetComponent<VolHitSync>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand)
        {
            myView.RequestOwnership();

            float volumeLeft = CalculateVolume(LeftControllerSpeed);
            print(volumeLeft);
            _volume = volumeLeft;
            _hit = true;
            //audioSource.pitch = volumeLeft;
            //audioSource.Play();


        }

        else if (other.gameObject == rightHand)
        {
            myView.RequestOwnership();
            float volumeRight = CalculateVolume(RightControllerSpeed);
            print(volumeRight);
            _volume = volumeRight;
            _hit = true;
            //audioSource.pitch = volumeRight;
            //audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _hit = false;
        myView.ClearOwnership();
    }


    private float CalculateVolume(float velocity)
    {
        float changeInValue = 1;
        float startingValue = 0;
        return changeInValue * ((velocity = velocity / maxCollisionVelocity - 1) * velocity * velocity + 1) + startingValue;
    }


    private float RightControllerSpeed
    {
        get
        {
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude;
        }
    }

    private float LeftControllerSpeed
    {
        get
        {
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude;
        }
    }


    private void Update()
    {
        if (_volume != _prevVolume)
        {
            _sync.SetVolume(_volume);
            _prevVolume = _volume;
        }
        if (_hit != _prevHit)
        {
            _sync.SetHit(_hit);
            _prevHit = _hit;
        }
    }

}
