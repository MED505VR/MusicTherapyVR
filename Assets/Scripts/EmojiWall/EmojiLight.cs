using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class EmojiLight : MonoBehaviour
    {
        private ColorChangingLamp[] Lamps { get; set; }
        private Color moodColor;

        private void Awake()
        {
            
        }

        private void ChangeLampColor()
        {
            foreach (var lamp in Lamps) lamp.SetModelColor(Color.blue);
        }

        public void Update()
        {
            if (this.GetComponent<Emoji>().color == true)
            {
                ChangeLampColor();
            }
        }
    }
}