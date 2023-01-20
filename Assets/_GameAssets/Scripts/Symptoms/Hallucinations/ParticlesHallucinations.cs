using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace com.sharmas4.MentalHealthDisorder
{

    public class ParticlesHallucinations : Symptoms
    {
        public ParticlesSO particlesSO;
        private Transform particlesParentT;
        private ParticleSystem[] particles;

        protected override void Awake()
        {
            base.Awake();
            particlesParentT = transform.GetChild(0);
            particles = GetComponentsInChildren<ParticleSystem>();
            EnableDisableParticles(false);
        }

        private void Start()
        {
            Simulate();
        }


        protected override void Prepare()
        {
            base.Prepare();
            particlesParentT.parent = CharacterData.CharacterCamera.transform;
            particlesParentT.localPosition = particlesSO.offset;
            particlesParentT.localRotation = Quaternion.identity;
        }

        // Called from within the SimulateCoroutine
        protected override void CleanUp()
        {
            particlesParentT.parent = transform;
            particlesParentT.localPosition = particlesSO.offset;
            particlesParentT.localRotation = Quaternion.identity;
            base.CleanUp();
        }

        protected override IEnumerator MasterCoroutine()
        {
            Prepare();
            while (isMasterCoroutineRunning)
            {
                StartCoroutine(RunProbabilityTest());
            }
            yield return new WaitForEndOfFrame();
            CleanUp();
        }

        private IEnumerator RunProbabilityTest()
        {
            float interval = Random.Range(particlesSO.activeTime.min, particlesSO.activeTime.max) + Random.value;

            if (ShouldSimulate(particlesSO))
                yield return PlaySimulation(interval);
            else
                yield return new WaitForSeconds(interval);
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

    }
}