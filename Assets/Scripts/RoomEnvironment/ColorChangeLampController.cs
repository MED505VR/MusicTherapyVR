using UnityEngine;

namespace Assets.Scripts.RoomEnvironment
{
    public class ColorChangeLampController : MonoBehaviour
    {
        public ColorChangeLamp[] Lamps { get; private set; }

        [SerializeField]
        public Color[] Colors { get; set; }

        private int _currentColorIndex;

        void Start()
        {
            Lamps = FindObjectsOfType<ColorChangeLamp>();
        }

        public void ChangeLampColor()
        {
            foreach (var lamp in Lamps)
            {
                lamp.ChangeLightColor(Colors[_currentColorIndex]);
            }

            _currentColorIndex = _currentColorIndex == Colors.Length ? 0 : _currentColorIndex + 1;
        }
    }
}