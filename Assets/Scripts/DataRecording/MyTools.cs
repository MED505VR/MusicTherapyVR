using System;
using UnityEditor;
using UnityEngine;

namespace DataRecording
{
    public class MyTools : MonoBehaviour
    {
        private DataRecorder dataRecorder;

        private void Start()
        {
            dataRecorder = FindObjectOfType<DataRecorder>();
        }

        void FixedUpdate()
        {
            print(dataRecorder.CurrentConnectedPlayers.Count);
            if (dataRecorder.CurrentConnectedPlayers.Count > 0)
            {
                CsvManager.AppendToReport(1,
                    new string[18]
                    {
                        dataRecorder.CurrentConnectedPlayers[0].head.position.x.ToString(), //float
                        dataRecorder.CurrentConnectedPlayers[0].head.position.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].head.position.z.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].head.rotation.x.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].head.rotation.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].head.rotation.z.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.position.x.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.position.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.position.z.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.rotation.x.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.rotation.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].leftHand.rotation.z.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.position.x.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.position.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.position.z.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.rotation.x.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.rotation.y.ToString(), //float 
                        dataRecorder.CurrentConnectedPlayers[0].rightHand.rotation.z.ToString(), //float 
                    }
                );

                if (dataRecorder.CurrentConnectedPlayers.Count > 1)
                {
                    CsvManager.AppendToReport(2,
                        new string[18]
                        {
                            dataRecorder.CurrentConnectedPlayers[1].head.position.x.ToString(), //float
                            dataRecorder.CurrentConnectedPlayers[1].head.position.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].head.position.z.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].head.rotation.x.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].head.rotation.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].head.rotation.z.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.position.x.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.position.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.position.z.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.rotation.x.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.rotation.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].leftHand.rotation.z.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.position.x.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.position.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.position.z.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.rotation.x.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.rotation.y.ToString(), //float 
                            dataRecorder.CurrentConnectedPlayers[1].rightHand.rotation.z.ToString(), //float 
                        }
                    );
                }

                Debug.Log("Report updated succesfully!");
            }
        }
    }
}