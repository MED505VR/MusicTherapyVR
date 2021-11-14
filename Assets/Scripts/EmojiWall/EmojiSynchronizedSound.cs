using Sound;
using UnityEngine;

namespace EmojiWall
{
    public class EmojiSynchronizedSound : SynchronizedSound
    {
        private StopEmojiSounds _stopEmojiSounds;

        protected override void Start()
        {
            base.Start();

            _stopEmojiSounds = FindObjectOfType<StopEmojiSounds>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var isPlaying = SoundAudioSource.isPlaying;

            _stopEmojiSounds.StopAllEmojiSoundsRightNow();

            if (isPlaying) return;
            PlaySynchronizedSound();
        }
    }
}