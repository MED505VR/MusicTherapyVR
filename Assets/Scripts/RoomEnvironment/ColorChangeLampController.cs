using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangeLampController : MonoBehaviour
    {
        private int _currentColorIndex;
        [field: SerializeField] private List<Color> LampColors { get; set; }
        private ColorChangeLamp[] Lamps { get; set; }

        private void Start()
        {
            Lamps = FindObjectsOfType<ColorChangeLamp>();
            LampColors.Add(Lamps[0].PointLight.color);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("PlayerInteractor")) ChangeLampColor();
        }

        private void ChangeLampColor()
        {
            foreach (var lamp in Lamps) lamp.ChangeLightColor(LampColors[_currentColorIndex]);

            _currentColorIndex = _currentColorIndex == LampColors.Count - 1 ? 0 : _currentColorIndex + 1;
        }
    }
}