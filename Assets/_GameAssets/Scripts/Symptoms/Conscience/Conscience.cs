using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class Conscience : Symptoms
    {
        // Provided by the preset
        public List<SoundsSO> selectedConscienceSoundsSO;

        private AudioSource[] audioSources;
        private Coroutine[] coroutines;

        protected override void Awake()
        {
            base.Awake();
            audioSources = GetComponentsInChildren<AudioSource>();
        }

        private IEnumerator SimulateCoroutine(AudioSource audioSource)
        {
            while (IsSimulating)
            {
                Reset(audioSource);

                int moodIndex = Random.Range(0, selectedConscienceSoundsSO.Count);
                ConscienceSO conscienceSO = selectedConscienceSoundsSO[moodIndex] as ConscienceSO;

                int clipIndex = Random.Range(0, conscienceSO.clips.Count);
                AudioClip clip = conscienceSO.clips[clipIndex];

                float interval = Random.Range(conscienceSO.timeAfterClip.min, conscienceSO.timeAfterClip.max) + Random.value;
                if (ShouldSimulate(conscienceSO))
                    yield return PlaySimulation(conscienceSO, audioSource, clip);

                yield return new WaitForSeconds(clip.length + interval);
            }
            yield return new WaitForEndOfFrame();
        }


        private IEnumerator PlaySimulation(ConscienceSO conscienceSO, AudioSource audioSource, AudioClip clip)
        {
            float speed = Random.Range(conscienceSO.speed.min, conscienceSO.speed.max) + Random.value;
            float volume = Random.Range(conscienceSO.volume.min, conscienceSO.volume.max) + Random.value;

            audioSource.pitch = speed;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);

            yield return null;
        }


        private void Reset(AudioSource audioSource)
        {
            audioSource.pitch = 1;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1);
        }

        public override void Simulate()
        {
            IsSimulating = true;
            for (int i = 0; i < audioSources.Length; i++)
            {
                Coroutine coroutine = StartCoroutine(SimulateCoroutine(audioSources[i]));
                coroutines[i] = coroutine;
            }
        }

        public override void Stop()
        {
            foreach (Coroutine coroutine in coroutines)
            {
                StopCoroutine(coroutine);
            }
            IsSimulating = false;
        }
    }
}