using System;
using UnityEngine;

namespace Networking
{
    public class InitializeNormcore : MonoBehaviour
    {
        private Normal.Realtime.Realtime _realtime;
        public string currentRoomName;

        private void Awake()
        {
            currentRoomName = Application.isEditor ? "Development Room" : "Experiment Room";
        }

        private void Start()
        {
            _realtime = GetComponent<Normal.Realtime.Realtime>();
            _realtime.Connect(currentRoomName);
        }
    }
}