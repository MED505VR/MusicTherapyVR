using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Sound.Models;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SynchronizedSound : RealtimeComponent<SynchronizedSoundModel>
    {
        [SerializeField] private List<AudioClip> soundAudioClips;
        private IEnumerator _playSoundCoroutine;
        
        protected AudioSource SoundAudioSource { get; set; }
        private List<AudioClip> SoundAudioClips
        {
            get => soundAudioClips;
            set => SetSoundAudioClips(value);
        }

        private void Start()
        {
            SoundAudioSource = GetComponent<AudioSource>();
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
            if (model.playSynchronizedSound)
                PlayLocalSound();
            else
                StopLocalSound();
        }


        private void SetSoundAudioClips(List<AudioClip> list)
        {
            SoundAudioClips.Clear();
            SoundAudioClips = list;
        }

        protected void PlaySynchronizedSound()
        {
            model.playSynchronizedSound = true;
        }

        protected void PlaySynchronizedSound(AudioClip clip)
        {
            SoundAudioClips.Clear();
            SoundAudioClips.Add(clip);
            model.playSynchronizedSound = true;
        }


        protected void StopSynchronizedSound()
        {
            model.playSynchronizedSound = false;
        }

        private void StopLocalSound()
        {
            if (_playSoundCoroutine != null) StopCoroutine(_playSoundCoroutine);
            SoundAudioSource.Stop();
        }

        private void PlayLocalSound()
        {
            if (SoundAudioClips.Count == 0) return;

            _playSoundCoroutine = PlayAudioClip();

            foreach (var audioClip in SoundAudioClips)
            {
                SoundAudioSource.clip = audioClip;
                StartCoroutine(_playSoundCoroutine);
            }

            model.playSynchronizedSound = false;
        }

        private IEnumerator PlayAudioClip()
        {
            SoundAudioSource.Play();

            while (SoundAudioSource.isPlaying) yield return new WaitForEndOfFrame();

            SoundAudioSource.Stop();
        }
    }
}