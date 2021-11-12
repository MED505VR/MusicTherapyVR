using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class EmojiLight : MonoBehaviour
    {
        private int CurrentColorIndex { get; set; }
        [field: SerializeField] private List<Color> Colors { get; set; }
        private ColorChangingLamp[] Lamps { get; set; }

        private void Start()
        {
            //this.GetComponent<Emoji>().color == true;
        }

        public void setLight()
        {
            // Herinde skal der være koden for at ændre lyset
        }

        public void Update()
        {
            if (this.GetComponent<Emoji>().color == true)
            {
                setLight();
            }
        }
    }
}