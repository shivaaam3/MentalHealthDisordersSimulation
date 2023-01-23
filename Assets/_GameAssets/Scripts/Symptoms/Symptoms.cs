
using System.Collections;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class Symptoms : MonoBehaviour
    {
        protected int noOfCoroutinesRunning = -1;

        // For the other scripts to know that the simulation is entirely stopped
        public bool IsSimulationRunning => noOfCoroutinesRunning <= 0;

        // Calculates if the simulation would be simulated or skipped for every iteration while the coroutine runs
        protected bool ShouldSimulate(SymptomsSO so)
        {
            return Random.value <= so.probability;
        }

        protected CharacterData CharacterData { get; private set; }

        // Guides the coroutine if it should run
        protected bool isSimulating = false;

        //protected abstract IEnumerator MasterCoroutine();

        protected virtual void Awake()
        {
            Random.InitState((int)Time.time);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            CharacterData = CharacterData.Instance;
            Simulate();
        }

        // Called immediately before the instances of the MasterCoroutine start running
        // Called from within the Simulate method
        protected virtual void Prepare()
        {
            isSimulating = true;
        }

        // The method to start the symptom simulation
        public abstract void Simulate();


        // Called by the instance of the MasterCoroutine that terminates last
        // Used to reset the components/ values to their default states
        protected abstract void CleanUp();


        // Let's the final iteration of the while loops run and then ends the corourine naturally
        public void Stop()
        {
            isSimulating = false;
        }

        // Stops the coroutine immediately 
        public virtual void StopAbrupt()
        {
            noOfCoroutinesRunning = -1;
            CleanUp();
        }
    }
}