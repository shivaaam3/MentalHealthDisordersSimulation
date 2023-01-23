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
            coroutines = new Coroutine[audioSources.Length];
        }

        protected override void Prepare()
        {
            base.Prepare();
            noOfCoroutinesRunning = audioSources.Length;
        }

        protected override void CleanUp()
        {
            if (noOfCoroutinesRunning <= 0)
            {
                for (int i = 0; i < audioSources.Length; i++)
                    audioSources[i].enabled = false;
            }
        }

        public override void Simulate()
        {
            Prepare();
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].enabled = false;
                coroutines[i] = StartCoroutine(MasterCoroutine(audioSources[i]));
            }
        }

        private IEnumerator MasterCoroutine(AudioSource audioSource)
        {
            while (isSimulating)
            {
                int moodIndex = Random.Range(0, selectedConscienceSoundsSO.Count);
                ConscienceSO conscienceSO = selectedConscienceSoundsSO[moodIndex] as ConscienceSO;

                float interval = Random.Range(conscienceSO.timeAfterClip.min, conscienceSO.timeAfterClip.max) + Random.value;
                if (ShouldSimulate(conscienceSO))
                    yield return PlaySimulation(conscienceSO, audioSource);

                yield return new WaitForSeconds(interval);
            }

            --noOfCoroutinesRunning;
            CleanUp();
        }

        private IEnumerator PlaySimulation(ConscienceSO conscienceSO, AudioSource audioSource)
        {
            float speed = Random.Range(conscienceSO.speed.min, conscienceSO.speed.max) + Random.value;
            float volume = Random.Range(conscienceSO.volume.min, conscienceSO.volume.max) + Random.value;

            audioSource.pitch = speed;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
            audioSource.volume = volume;

            int clipIndex = Random.Range(0, conscienceSO.clips.Count);
            AudioClip clip = conscienceSO.clips[clipIndex];
            audioSource.enabled = true;
            audioSource.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length);
            audioSource.enabled = false;
        }


        public override void StopAbrupt()
        {
            for (int i = 0; i < coroutines.Length; i++)
            {
                if (coroutines[i] != null)
                    StopCoroutine(coroutines[i]);
            }

            base.StopAbrupt();
        }

    }
}