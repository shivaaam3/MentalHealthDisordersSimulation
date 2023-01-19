using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class VoiceHallucinations : Symptoms
    {
        // Provided by the preset
        public List<SoundsSO> selectedMoodsSoundsSO;

        private AudioSource[] audioSources;
        private Coroutine[] coroutines;


        protected override void Awake()
        {
            base.Awake();
            audioSources = GetComponentsInChildren<AudioSource>();
            coroutines = new Coroutine[audioSources.Length];
        }

        private IEnumerator SimulateCoroutine(AudioSource audioSource)
        {
            while (IsSimulating)
            {
                Reset(audioSource);

                int moodIndex = Random.Range(0, selectedMoodsSoundsSO.Count);
                VoiceHallucinationSO voiceHallucinationSO = selectedMoodsSoundsSO[moodIndex] as VoiceHallucinationSO;

                int clipIndex = Random.Range(0, voiceHallucinationSO.clips.Count);
                AudioClip clip = voiceHallucinationSO.clips[clipIndex];

                float interval = Random.Range(voiceHallucinationSO.timeAfterClip.min, voiceHallucinationSO.timeAfterClip.max) + Random.value;
                if (ShouldSimulate(voiceHallucinationSO))
                    yield return PlaySimulation(voiceHallucinationSO, audioSource, clip);

                yield return new WaitForSeconds(clip.length + interval);
            }
            yield return new WaitForEndOfFrame();
        }


        private IEnumerator PlaySimulation(VoiceHallucinationSO voiceHallucinationSO, AudioSource audioSource, AudioClip clip)
        {

            float pitch = Random.Range(voiceHallucinationSO.pitch.min, voiceHallucinationSO.pitch.max) + Random.value;
            float speed = Random.Range(voiceHallucinationSO.speed.min, voiceHallucinationSO.speed.max) + Random.value;
            float volume = Random.Range(voiceHallucinationSO.volume.min, voiceHallucinationSO.volume.max) + Random.value;

            audioSource.pitch = speed;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
            audioSource.pitch += pitch;
            audioSource.volume = volume;

            float angle = Random.Range(0, 3.14f);
            float distance = Random.Range(voiceHallucinationSO.distance.min, voiceHallucinationSO.distance.max) + Random.value;
            Vector3 pos = new Vector3(distance * Mathf.Cos(angle), characterData.CharacterHeadPos.y, distance * Mathf.Sin(angle));
            audioSource.transform.position = pos;

            audioSource.PlayOneShot(clip);

            yield return null;
        }


        private void Reset(AudioSource audioSource)
        {
            audioSource.pitch = 1;
            audioSource.volume = 0;
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