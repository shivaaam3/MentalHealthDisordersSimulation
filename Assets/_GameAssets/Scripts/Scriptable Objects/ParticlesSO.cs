using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Visuals/Particles Hallucination")]
    public class ParticlesSO : SymptomsSO
    {
        public MinMax<int> activeTime = new MinMax<int>(5, 15);
    }
}
