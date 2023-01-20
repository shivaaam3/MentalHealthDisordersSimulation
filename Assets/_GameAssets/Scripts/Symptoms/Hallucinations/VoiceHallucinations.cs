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
            int moodIndex = Random.Range(0, selectedMoodsSoundsSO.Count);
            VoiceHallucinationSO voiceHallucinationSO = selectedMoodsSoundsSO[moodIndex] as VoiceHallucinationSO;

            float interval = Random.Range(voiceHallucinationSO.timeAfterClip.min, voiceHallucinationSO.timeAfterClip.max) + Random.value;
            if (ShouldSimulate(voiceHallucinationSO))
                yield return PlaySimulation(voiceHallucinationSO, audioSource);

            yield return new WaitForSeconds(interval);
        }


        private IEnumerator PlaySimulation(VoiceHallucinationSO voiceHallucinationSO, AudioSource audioSource)
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
            Vector3 pos = new Vector3(distance * Mathf.Cos(angle), CharacterData.CharacterHeadPos.y, distance * Mathf.Sin(angle));
            audioSource.transform.position = pos;

            int clipIndex = Random.Range(0, voiceHallucinationSO.clips.Count);
            AudioClip clip = voiceHallucinationSO.clips[clipIndex];

            audioSource.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length);
        }

    }
}