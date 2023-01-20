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

        protected override void Awake()
        {
            base.Awake();
            audioSources = GetComponentsInChildren<AudioSource>();
        }

        protected override void Prepare()
        {
            base.Prepare();
            for (int i = 0; i < audioSources.Length; i++)
                audioSources[i].enabled = true;
        }

        protected override void CleanUp()
        {
            for (int i = 0; i < audioSources.Length; i++)
                audioSources[i].enabled = false;
            base.CleanUp();
        }

        protected override IEnumerator MasterCoroutine()
        {
            Prepare();
            while (isMasterCoroutineRunning)
            {
                for (int i = 0; i < audioSources.Length; i++)
                    StartCoroutine(RunProbabilityTest(audioSources[i]));
            }
            yield return new WaitForEndOfFrame();
            CleanUp();
        }

        private IEnumerator RunProbabilityTest(AudioSource audioSource)
        {
            int moodIndex = Random.Range(0, selectedConscienceSoundsSO.Count);
            ConscienceSO conscienceSO = selectedConscienceSoundsSO[moodIndex] as ConscienceSO;

            float interval = Random.Range(conscienceSO.timeAfterClip.min, conscienceSO.timeAfterClip.max) + Random.value;
            if (ShouldSimulate(conscienceSO))
                yield return PlaySimulation(conscienceSO, audioSource);

            yield return new WaitForSeconds(interval);

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
            audioSource.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length);
        }

    }
}