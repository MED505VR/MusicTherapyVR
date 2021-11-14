using Normal.Realtime;
using Sound.Models;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SynchronizedSound : RealtimeComponent<SynchronizedSoundModel>
    {
        protected AudioSource SoundAudioSource { get; set; }

        [field: SerializeField] private AudioClip SoundAudioClip { get; set; }

        protected virtual void Start()
        {
            SoundAudioSource = GetComponent<AudioSource>();
            SoundAudioSource.clip = SoundAudioClip;
        }

        protected override void OnRealtimeModelReplaced(SynchronizedSoundModel previousModel,
            SynchronizedSoundModel currentModel)
        {
            if (previousModel != null) // Unregister from events
                previousModel.playSynchronizedSoundDidChange -= PlaySynchronizedSoundDidChange;

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.playSynchronizedSound = false;

                // Register for events so we'll know if the color changes later
                currentModel.playSynchronizedSoundDidChange += PlaySynchronizedSoundDidChange;
            }
        }

        private void PlaySynchronizedSoundDidChange(SynchronizedSoundModel pModel, bool value)
        {
            if (SoundAudioSource.isPlaying) SoundAudioSource.Stop();
            if (model.playSynchronizedSound)
                SoundAudioSource.Play();
            else
                SoundAudioSource.Stop();
        }

        protected void PlaySynchronizedSound()
        {
            model.playSynchronizedSound = true;
        }

        protected void PlaySynchronizedSound(AudioClip clip)
        {
            SoundAudioSource.clip = clip;
            model.playSynchronizedSound = true;
        }

        public void StopSynchronizedSound()
        {
            model.playSynchronizedSound = false;
        }
    }
}