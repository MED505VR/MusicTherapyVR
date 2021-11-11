using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangingLamp : RealtimeComponent<ColorChangingLampModel>
    {
        private float _colorTransitionSpeed;
        private ColorChangingLampController _controller;
        private bool _isChanging;

        public Light PointLight { get; private set; }
        private Light SpotLight { get; set; }

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];

            _controller = FindObjectOfType<ColorChangingLampController>();
            _colorTransitionSpeed = _controller.lightTransitionSpeed;
        }

        protected override void OnRealtimeModelReplaced(ColorChangingLampModel previousModel,
            ColorChangingLampModel currentModel)
        {
            if (previousModel != null) previousModel.colorDidChange -= UpdateColorToMatchModel;
            if (currentModel != null) currentModel.colorDidChange += UpdateColorToMatchModel;
        }

        public void SetModelColor(Color color)
        {
            model.color = color;
        }

        private void UpdateColorToMatchModel(ColorChangingLampModel pModel, Color value)
        {
            ChangeLightColor(value);
        }

        private void ChangeLightColor(Color color)
        {
            if (_isChanging) return;
            StartCoroutine(UpdateLightColor(PointLight, color, _colorTransitionSpeed));
            StartCoroutine(UpdateLightColor(SpotLight, color, _colorTransitionSpeed));
        }

        private IEnumerator UpdateLightColor(Light targetLight, Color targetColor, float transitionSpeed)
        {
            _isChanging = true;

            while (targetLight.color != targetColor)
            {
                targetLight.color = Color.Lerp(targetLight.color, targetColor, transitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            _isChanging = false;
        }
    }
}