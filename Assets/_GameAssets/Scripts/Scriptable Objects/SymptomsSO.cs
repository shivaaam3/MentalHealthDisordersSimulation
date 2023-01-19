using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class SymptomsSO : ScriptableObject
    {
        [Range(0.0f, 1.0f)]
        public float probability = 0;
    }
}