using System.Collections.Generic;
using EmojiWall;
using Normal.Realtime;
using UnityEngine;

namespace DataRecording
{
    public class DataRecorder : MonoBehaviour
    {
        public List<RealtimeAvatar> CurrentConnectedPlayers { get; private set; }

        private void Awake()
        {
            CurrentConnectedPlayers = new List<RealtimeAvatar>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckForConnectedPlayers), 5f, 10f);
            if (EmojiSynchronizedSound.emojiActivated != null)
                EmojiSynchronizedSound.emojiActivated.AddListener(OnEmojiActivated);
        }

        private void CheckForConnectedPlayers()
        {
            Debug.Log("Checking for connected players...");

            var players = FindObjectsOfType<RealtimeAvatar>();

            if (players.Equals(CurrentConnectedPlayers)) return;

            CurrentConnectedPlayers.Clear();
            CurrentConnectedPlayers.AddRange(players);
        }

        private void OnEmojiActivated(string objectName)
        {
            // TODO: Add csvwriter code here
        }
    }
}