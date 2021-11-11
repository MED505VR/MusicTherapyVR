using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangingLamp : RealtimeComponent<ColorChangingLampModel>
    {
        private List<Color> _colors;
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
            _colors = _controller.Colors;
            model.colorIndex = _colors.Count - 1;
        }

        protected override void OnRealtimeModelReplaced(ColorChangingLampModel previousModel,
            ColorChangingLampModel currentModel)
        {
            if (previousModel != null) previousModel.colorIndexDidChange -= UpdateColorToMatchModel;

            if (currentModel != null) currentModel.colorIndexDidChange += UpdateColorToMatchModel;
        }

        public void SetModelColorIndex(int colorIndex)
        {
            model.colorIndex = colorIndex;
        }

        private void UpdateColorToMatchModel(ColorChangingLampModel pModel, int value)
        {
            ChangeLightColor(model.colorIndex);
        }

        private void ChangeLightColor(int colorIndex)
        {
            if (_isChanging) return;
            StartCoroutine(UpdateLightColor(PointLight, _colors[colorIndex], _colorTransitionSpeed));
            StartCoroutine(UpdateLightColor(SpotLight, _colors[colorIndex], _colorTransitionSpeed));
        }

        private IEnumerator UpdateLightColor(Light pLight, Color targetColor, float transitionSpeed)
        {
            _isChanging = true;

            while (pLight.color != targetColor)
            {
                pLight.color = Color.Lerp(pLight.color, targetColor, transitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            _isChanging = false;
        }
    }
}