using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class Symptoms : MonoBehaviour
    {
        public bool IsSimulating { get; protected set; }

        public abstract void Simulate();
        public abstract void Stop();

        protected bool ShouldSimulate(SymptomsSO so)
        {
            return Random.value <= so.probability;
        }

        protected CharacterData characterData;

        protected virtual void Awake()
        {
            Random.InitState((int)Time.time);
            characterData = CharacterData.Instance; ;
        }
    }
}