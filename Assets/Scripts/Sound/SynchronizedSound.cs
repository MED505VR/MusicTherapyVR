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
        protected AudioSource SoundAudioSource { get; set; }

        [field: SerializeField] private List<AudioClip> SoundAudioClips { get; set; }

        private void Start()
        {
            SoundAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (model.playSynchronizedSound && !SoundAudioSource.isPlaying) PlayLocalSound();
        }

        protected void StartSynchronizedSound()
        {
            model.playSynchronizedSound = true;
        }

        private void PlayLocalSound()
        {
            if (SoundAudioClips.Count == 0) return;

            foreach (var audioClip in SoundAudioClips)
            {
                SoundAudioSource.clip = audioClip;
                StartCoroutine(PlayAudioClip());
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