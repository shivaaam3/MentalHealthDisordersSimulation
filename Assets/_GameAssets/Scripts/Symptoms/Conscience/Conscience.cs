using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class Conscience : MonoBehaviour, ISymptoms
    {

        public AudioSource[] audioSources;

        private Coroutine[] coroutines;
        private List<SoundsSO> selectedMoods;

        public bool IsSimulating { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Random.InitState(Constants.RANDOM_SEED);
            audioSources = GetComponentsInChildren<AudioSource>();
        }

        private IEnumerator SimulateCoroutine(AudioSource audioSource)
        {
            while (IsSimulating)
            {
                audioSource.pitch = 1;
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1);

                int moodIndex = Random.Range(0, selectedMoods.Count);
                ConscienceSO mood = selectedMoods[moodIndex] as ConscienceSO;

                float speed = Random.Range(mood.speed.min, mood.speed.max) + Random.value;
                float volume = Random.Range(mood.volume.min, mood.volume.max) + Random.value;

                int clipIndex = Random.Range(0, mood.clips.Count);
                AudioClip clip = mood.clips[clipIndex];

                audioSource.pitch = speed;
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
                audioSource.volume = volume;
                audioSource.PlayOneShot(clip);

                float interval = Random.Range(mood.timeAfterClip.min, mood.timeAfterClip.max) + Random.value;
                yield return new WaitForSeconds(audioSource.clip.length + interval);
            }
            yield return new WaitForEndOfFrame();
        }

        public void Simulate()
        {
            IsSimulating = true;
            for (int i = 0; i < audioSources.Length; i++)
            {
                Coroutine coroutine = StartCoroutine(SimulateCoroutine(audioSources[i]));
                coroutines[i] = coroutine;
            }
        }

        public void Stop()
        {
            foreach (Coroutine coroutine in coroutines)
            {
                StopCoroutine(coroutine);
            }
            IsSimulating = false;
        }
    }
}