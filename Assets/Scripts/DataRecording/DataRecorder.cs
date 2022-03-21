using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

namespace DataRecording
{
    public class DataRecorder : MonoBehaviour
    {
        [SerializeField] private string[] reportObjects;
        
        public List<RealtimeAvatar> CurrentConnectedPlayers { get; private set; }

        private void Awake()
        {
            CurrentConnectedPlayers = new List<RealtimeAvatar>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckForConnectedPlayers), 5f, 10f);
        }

        private void CheckForConnectedPlayers()
        {
            Debug.Log("Checking for connected players...");

            var players = FindObjectsOfType<RealtimeAvatar>();

            if (players.Equals(CurrentConnectedPlayers)) return;

            CurrentConnectedPlayers.Clear();
            CurrentConnectedPlayers.AddRange(players); 
        }
    }
}