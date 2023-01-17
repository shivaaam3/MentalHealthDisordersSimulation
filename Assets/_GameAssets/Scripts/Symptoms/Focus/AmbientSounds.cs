using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class AmbientSounds : Symptoms
    {
        // Provided by the preset
        public SoundsSO sound;

        private AudioSource audioSource;
        private Coroutine coroutine;
        private AmbientSoundSO ambientSound;


        // Start is called before the first frame update
        void Start()
        {
            Random.InitState(Constants.RANDOM_SEED);
            audioSource = GetComponent<AudioSource>();
            ambientSound = sound as AmbientSoundSO;

            audioSource.clip = ambientSound.clips[0];
            audioSource.loop = true;
        }


        private IEnumerator SimulateCoroutine()
        {
            audioSource.pitch = 1;
            audioSource.volume = 0;
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1);
            audioSource.Play();

            while (IsSimulating)
            {
                audioSource.volume = Mathf.Clamp(audioSource.volume + ambientSound.volumeFactor, ambientSound.volume.min, ambientSound.volume.max);
                float speed = Mathf.Clamp(audioSource.pitch + ambientSound.speedFactor, ambientSound.volume.min, ambientSound.volume.max);
                audioSource.pitch = speed;
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchBend", 1f / speed);

                yield return new WaitForSeconds(ambientSound.step);
            }

            yield return new WaitForEndOfFrame();
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