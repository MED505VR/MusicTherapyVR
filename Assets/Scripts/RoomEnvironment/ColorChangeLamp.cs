using System.Collections;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangeLamp : MonoBehaviour
    {
        public Light PointLight { get; private set; }
        public Light SpotLight { get; private set; }

        private readonly float _lightTransitionSpeed = 0.01f;
        private bool _isChanging;

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];
        }

        public void ChangeLightColor(Color color)
        {
            if (_isChanging) return;
            StartCoroutine(UpdateLightColor(color, PointLight));
            StartCoroutine(UpdateLightColor(color, SpotLight));
        }

        private IEnumerator UpdateLightColor(Color targetColor, Light targetLight)
        {
            _isChanging = true;

            while (targetLight.color != targetColor)
            {
                targetLight.color = Color.Lerp(targetLight.color, targetColor, _lightTransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            _isChanging = false;
        }
    }
}