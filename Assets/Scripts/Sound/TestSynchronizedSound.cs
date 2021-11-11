using System;
using UnityEngine;

namespace Sound
{
    public class TestSynchronizedSound : SynchronizedSound
    {
        private void OnTriggerEnter(Collider other)
        {
            PlaySynchronizedSound();
        }

        private void OnTriggerExit(Collider other)
        {
            StopSynchronizedSound();
        }
    }
}
