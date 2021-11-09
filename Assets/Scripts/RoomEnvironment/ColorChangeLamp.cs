using System.Collections;
using UnityEngine;

namespace Assets.Scripts.RoomEnvironment
{
    public class ColorChangeLamp : MonoBehaviour
    {
        public Light PointLight { get; private set; }
        public Light SpotLight { get; private set; }

        private readonly float lightTransitionSpeed = 0.1f;
        private bool isChanging;

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];
        }

        public void ChangeLightColor(Color color)
        {
            if (isChanging) return;
            StartCoroutine(UpdateLightColor(color, PointLight));
            StartCoroutine(UpdateLightColor(color, SpotLight));
        }

        private IEnumerator UpdateLightColor(Color targetColor, Light light)
        {
            isChanging = true;

            while (light.color != targetColor)
            {
                light.color = Color.Lerp(light.color, targetColor, lightTransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            isChanging = false;
        }
    }
}