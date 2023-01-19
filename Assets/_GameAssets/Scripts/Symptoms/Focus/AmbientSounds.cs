using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace com.sharmas4.MentalHealthDisorder
{
    public class AmbientSounds : Symptoms
    {
        // Provided by the preset
        public SoundsSO soundSO;

        private AudioSource audioSource;
        private Coroutine coroutine;
        private AmbientSoundSO ambientSoundSO;


        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            ambientSoundSO = soundSO as AmbientSoundSO;

            audioSource.clip = ambientSoundSO.clips[0];
            audioSource.loop = true;
        }


        private IEnumerator SimulateCoroutine()
        {
            while (IsSimulating)
            {
                Reset();
                float interval = Random.Range(ambientSoundSO.activeTime.min, ambientSoundSO.activeTime.max) + Random.value;
                if (ShouldSimulate(ambientSoundSO))
                    yield return PlaySimulation(interval);
                else
                    yield return new WaitForSeconds(interval);
            }

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator PlaySimulation(float interval)
        {
            audioSource.volume = Mathf.Clamp(audioSource.volume + ambientSoundSO.volumeFactor, ambientSoundSO.volume.min, ambientSoundSO.volume.max);
            float speed = Mathf.Clamp(audioSource.pitch + ambientSoundSO.speedFactor, ambientSoundSO.volume.min, ambientSoundSO.volume.max);
            audioSource.pitch = speed;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
            audioSource.Play();

            float time = 0;
            while (time < 1)
            {
                time += 2f * Time.deltaTime / interval;
                float currentVolume = Tweens.Linear(time);
                audioSource.volume = currentVolume;
                yield return null;
            }

            time = 0;
            while (time < 1)
            {
                time += 2f * Time.deltaTime / interval;
                float currentVolume = 1 - Tweens.Linear(time);
                audioSource.volume = currentVolume;
                yield return null;
            }
        }


        private void Reset()
        {
            audioSource.pitch = 1;
            audioSource.volume = 0;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1);
        }

        public override void Simulate()
        {
            IsSimulating = true;
            coroutine = StartCoroutine(SimulateCoroutine());
        }

        public override void Stop()
        {
            StopCoroutine(coroutine);
            IsSimulating = false;
        }
    }
}