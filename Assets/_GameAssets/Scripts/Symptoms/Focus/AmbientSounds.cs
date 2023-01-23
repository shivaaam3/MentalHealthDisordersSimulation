using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            audioSource = GetComponentInChildren<AudioSource>();
            ambientSoundSO = soundSO as AmbientSoundSO;

            audioSource.clip = ambientSoundSO.clips[0];
            audioSource.loop = true;
        }


        protected override void Prepare()
        {
            base.Prepare();
            noOfCoroutinesRunning = 1;
        }

        protected override void CleanUp()
        {
            if (noOfCoroutinesRunning <= 0)
            {
                audioSource.enabled = false;
            }
        }

        public override void Simulate()
        {
            Prepare();
            audioSource.enabled = false;
            coroutine = StartCoroutine(MasterCoroutine());
        }


        private IEnumerator MasterCoroutine()
        {
            while (isSimulating)
            {
                float interval = Random.Range(ambientSoundSO.activeTime.min, ambientSoundSO.activeTime.max) + Random.value;
                if (ShouldSimulate(ambientSoundSO))
                    yield return PlaySimulation(interval);
                else
                    yield return new WaitForSeconds(interval);
            }

            --noOfCoroutinesRunning;
            CleanUp();
        }

        private IEnumerator PlaySimulation(float interval)
        {
            audioSource.volume = Mathf.Clamp(audioSource.volume + ambientSoundSO.volumeFactor, ambientSoundSO.volume.min, ambientSoundSO.volume.max);
            float speed = Mathf.Clamp(audioSource.pitch + ambientSoundSO.speedFactor, ambientSoundSO.volume.min, ambientSoundSO.volume.max);
            audioSource.pitch = speed;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);
            audioSource.enabled = true;
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
            audioSource.enabled = false;
        }

        public override void StopAbrupt()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            base.StopAbrupt();
        }
    }
}