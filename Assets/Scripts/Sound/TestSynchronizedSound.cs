using System;
using UnityEngine;

namespace Sound
{
    public class TestSynchronizedSound : SynchronizedSound
    {
        private void OnTriggerEnter(Collider other)
        {
            StartSynchronizedSound();
        }
    }
}
