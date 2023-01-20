
using System.Collections;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class Symptoms : MonoBehaviour
    {
        // For the other scripts to know that the simulation is entirely stopped
        public bool IsSimulating { get; protected set; }

        // Calculates if the simulation would be simulated or skipped for every iteration while the coroutine runs
        protected bool ShouldSimulate(SymptomsSO so)
        {
            return Random.value <= so.probability;
        }
        protected CharacterData CharacterData { get; private set; }

        protected bool isMasterCoroutineRunning = false;
        protected Coroutine masterCoroutine;

        protected abstract IEnumerator MasterCoroutine();

        protected virtual void Awake()
        {
            Random.InitState((int)Time.time);
            CharacterData = CharacterData.Instance; ;
        }


        // Call from within the master coroutine preferrably in the beginning 
        protected virtual void Prepare()
        {
            IsSimulating = true;
        }

        // Call from within the master coroutine preferrably in the end 
        protected virtual void CleanUp()
        {
            IsSimulating = false;
        }

        public void Simulate()
        {
            isMasterCoroutineRunning = true;
            masterCoroutine = StartCoroutine(MasterCoroutine());
        }

        public void Stop()
        {
            isMasterCoroutineRunning = false;
        }

        public void StopAbrupt()
        {
            CleanUp();
            if (masterCoroutine != null)
                StopCoroutine(masterCoroutine);
        }
    }
}