using EmojiWall;
using UnityEngine;

namespace Materials
{
    [RequireComponent(typeof(EmojiSynchronizedSound))]
    public class EmojiMaterialChange : SynchronizedMaterial
    {
        private static readonly int EmissionColor1 = Shader.PropertyToID("_EmissionColor");

        [Header("Color Settings")] [SerializeField]
        private Color _color;

        [SerializeField] private Color _emissionColor;

        private Color _baseColor, _baseEmissionColor;
        private EmojiSynchronizedSound _emojiSynchronizedSound;

        protected override void Start()
        {
            base.Start();

            _emojiSynchronizedSound = GetComponent<EmojiSynchronizedSound>();
            _baseColor = GetComponent<MeshRenderer>().material.color;
            _baseEmissionColor = GetComponent<MeshRenderer>().material.GetColor(EmissionColor1);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_emojiSynchronizedSound.SoundAudioSource.isPlaying)
            {
                SetColor(_color);
                SetEmissionColor(_emissionColor);
                SetIsEmissive(true);
            }
            else
            {
                SetColor(_baseColor);
                SetEmissionColor(_baseEmissionColor);
                SetIsEmissive(false);
            }
        }
    }
}