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

        private void Update()
        {
            if (model.playSynchronizedSound && !SoundAudioSource.isPlaying) PlayLocalSound();
            if (!model.playSynchronizedSound && SoundAudioSource.isPlaying) StopLocalSound();
        }

        private void SetSoundAudioClips(List<AudioClip> list)
        {
            SoundAudioClips.Clear();
            SoundAudioClips = list;
        }

        protected void StartSynchronizedSound()
        {
            model.playSynchronizedSound = true;
        }

        protected void StartSynchronizedSound(AudioClip clip)
        {
            model.playSynchronizedSound = true;
            SoundAudioClips.Clear();
            SoundAudioClips.Add(clip);
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