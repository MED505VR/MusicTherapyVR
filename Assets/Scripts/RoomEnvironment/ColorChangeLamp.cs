using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangeLamp : RealtimeComponent<ColorChangeLampModel>
    {
        private List<Color> _colors;
        private float _colorTransitionSpeed;
        private ColorChangeLampController _controller;
        private bool _isChanging;

        public Light PointLight { get; private set; }
        private Light SpotLight { get; set; }

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];

            _controller = FindObjectOfType<ColorChangeLampController>();
            _colorTransitionSpeed = _controller.LightTransitionSpeed;
            _colors = _controller.LampColors;
        }

        protected override void OnRealtimeModelReplaced(ColorChangeLampModel previousModel,
            ColorChangeLampModel currentModel)
        {
            if (previousModel != null) previousModel.colorIndexDidChange -= UpdateColorToMatchModel;

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel) currentModel.colorIndex = _controller.CurrentColorIndex;

                ChangeLightColor(currentModel.colorIndex);

                // Register for events so we'll know if the color changes later
                currentModel.colorIndexDidChange += UpdateColorToMatchModel;
            }
        }

        public void SetModelColorIndex(int colorIndex)
        {
            model.colorIndex = colorIndex;
        }

        private void UpdateColorToMatchModel(ColorChangeLampModel pModel, int value)
        {
            ChangeLightColor(model.colorIndex);
        }

        public void ChangeLightColor(int colorIndex)
        {
            if (_isChanging) return;
            StartCoroutine(UpdateLightColor(PointLight, _colors[colorIndex], _colorTransitionSpeed));
            StartCoroutine(UpdateLightColor(SpotLight, _colors[colorIndex], _colorTransitionSpeed));
        }

        private IEnumerator UpdateLightColor(Light light, Color targetColor, float transitionSpeed)
        {
            _isChanging = true;

            while (light.color != targetColor)
            {
                light.color = Color.Lerp(light.color, targetColor, transitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            _isChanging = false;
        }
    }
}