using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace com.sharmas4.MentalHealthDisorder
{

    public class ParticlesHallucinations : Symptoms
    {
        public ParticlesSO particlesSO;
        private Transform particlesParentT;
        private Coroutine coroutine;

        protected override void Awake()
        {
            base.Awake();
            particlesParentT = transform.GetChild(0);
            EnableDisableParticles(false);
        }


        private IEnumerator SimulateCoroutine()
        {
            while (IsSimulating)
            {
                float interval = Random.Range(particlesSO.activeTime.min, particlesSO.activeTime.max) + Random.value;

                if (ShouldSimulate(particlesSO))
                    yield return PlaySimulation();

                yield return new WaitForSeconds(interval);
            }
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator PlaySimulation()
        {
            EnableDisableParticles(true);
            yield return null;
        }

        void EnableDisableParticles(bool state)
        {
            particlesParentT.gameObject.SetActive(state);
        }

        public override void Simulate()
        {
            IsSimulating = true;
            particlesParentT.parent = characterData.CharacterCamera.transform;
            coroutine = StartCoroutine(SimulateCoroutine());
        }

        public override void Stop()
        {
            StopCoroutine(coroutine);
            particlesParentT.parent = transform;
            IsSimulating = false;
        }

    }
}