using System.Collections.Generic;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangeLampController : MonoBehaviour
    {
        public float LightTransitionSpeed { get; set; }
        public int CurrentColorIndex { get; set; }
        [field: SerializeField] public List<Color> LampColors { get; set; }
        private ColorChangeLamp[] Lamps { get; set; }


        private void Start()
        {
            Lamps = FindObjectsOfType<ColorChangeLamp>();
            LampColors.Add(Lamps[0].PointLight.color);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerInteractor")) ChangeLampColor();
        }

        private void ChangeLampColor()
        {
            foreach (var lamp in Lamps) lamp.SetModelColorIndex(CurrentColorIndex);

            CurrentColorIndex = CurrentColorIndex == LampColors.Count - 1 ? 0 : CurrentColorIndex + 1;
        }
    }
}