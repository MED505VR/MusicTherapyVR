﻿using System.Collections.Generic;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangingLampController : MonoBehaviour
    {
        public float lightTransitionSpeed = 0.01f;

        private int CurrentColorIndex { get; set; }
        [field: SerializeField] private List<Color> Colors { get; set; }
        private ColorChangingLamp[] Lamps { get; set; }


        private void Start()
        {
            Lamps = FindObjectsOfType<ColorChangingLamp>();
            Colors.Add(Lamps[0].PointLight.color);
            CurrentColorIndex = Colors.Count - 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerInteractor")) ChangeLampColor();
        }

        private void ChangeLampColor()
        {
            foreach (var lamp in Lamps) lamp.SetModelColor(Colors[CurrentColorIndex]);

            CurrentColorIndex = CurrentColorIndex == Colors.Count - 1 ? 0 : CurrentColorIndex + 1;
        }
    }
}