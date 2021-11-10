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
        
        [field: SerializeField] public List<AudioClip> SoundAudioClips { get; set; }

        private void Start()
        {
            SoundAudioSource = GetComponent<AudioSource>();
        }

        protected void PlaySynchronizedSound()
        {
            if (SoundAudioClips.Count == 0) return;
            
            foreach (var audioClip in SoundAudioClips)
            {
                SoundAudioSource.clip = audioClip;
                StartCoroutine(PlayAudioClip());
            }
        }

        private IEnumerator PlayAudioClip()
        {
            SoundAudioSource.Play();

            while (SoundAudioSource.isPlaying) yield return new WaitForEndOfFrame();

            SoundAudioSource.Stop();
        }
    }
}