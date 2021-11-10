using System;
using System.Collections;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangeLamp : RealtimeComponent<ColorChangeLampModel>
    {
        public Light PointLight { get; private set; }
        private Light SpotLight { get; set; }

        private readonly float _lightTransitionSpeed = 0.01f;
        private bool _isChanging;

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];
        }

        private void Update()
        {
            PointLight.color = model.color;
            SpotLight.color = model.color;
        }

        public void ChangeLightColor(Color color)
        {
            if (_isChanging) return;
            StartCoroutine(UpdateLightColor(color));
            StartCoroutine(UpdateLightColor(color));
        }

        private IEnumerator UpdateLightColor(Color targetColor)
        {
            _isChanging = true;

            while (model.color != targetColor)
            {
                model.color = Color.Lerp(model.color, targetColor, _lightTransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            _isChanging = false;
        }
    }
}