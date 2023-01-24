using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace com.sharmas4.MentalHealthDisorder
{

    public class ParticlesHallucinations : Symptoms
    {
        // Provided by the preset
        public ParticlesSO particlesSO;

        private Transform particlesParentT;
        private ParticleSystem[] particles;
        private Coroutine coroutine;


        protected override void Awake()
        {
            base.Awake();
            particlesParentT = transform.GetChild(0);
            particles = GetComponentsInChildren<ParticleSystem>();
            EnableDisableParticles(false);
        }

        protected override void Prepare()
        {
            base.Prepare();
            noOfCoroutinesRunning = 1;
            particlesParentT.parent = CharacterData.CharacterCamera.transform;
            particlesParentT.localPosition = particlesSO.offset;
            particlesParentT.localRotation = Quaternion.identity;
        }

        protected override void CleanUp()
        {
            if (noOfCoroutinesRunning <= 0)
            {
                particlesParentT.parent = transform;
                particlesParentT.localPosition = particlesSO.offset;
                particlesParentT.localRotation = Quaternion.identity;
                EnableDisableParticles(false);
            }
        }

        public override void Simulate()
        {
            Prepare();
            coroutine = StartCoroutine(MasterCoroutine());
        }

        private IEnumerator MasterCoroutine()
        {
            while (isSimulating)
            {
                float interval = Random.Range(particlesSO.activeTime.min, particlesSO.activeTime.max) + Random.value;
                if (ShouldSimulate(particlesSO))
                    yield return PlaySimulation(interval);
                else
                    yield return new WaitForSeconds(interval);
            }

            --noOfCoroutinesRunning;
            CleanUp();
        }

        private IEnumerator PlaySimulation(float interval)
        {
            EnableDisableParticles(true);
            yield return new WaitForSeconds(interval);
            EnableDisableParticles(false);
        }

        private void EnableDisableParticles(bool state)
        {
            foreach (ParticleSystem particle in particles)
            {
                var em = particle.emission;
                em.enabled = state;
            }
        }

        public override void StopAbrupt()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            base.StopAbrupt();
        }

    }
}