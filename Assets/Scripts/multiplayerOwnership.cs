using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Tilia;
using OculusIntegrationForVRTK;

  public class multiplayerOwnership : MonoBehaviour
{
    public GameObject Cube;  
    

    public void RequestOwnership()
    {
        Cube = GameObject.Find("Cube");
        var realtimeView = GetComponent<RealtimeView>();
        var realtimeTransform = GetComponent<RealtimeTransform>();

        if (realtimeView != null)
        {
            realtimeView.RequestOwnership();
        }

        if (realtimeTransform != null) {

            realtimeTransform.RequestOwnership();
        }
    }

}
