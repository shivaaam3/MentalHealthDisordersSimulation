using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Visuals/Particles Hallucination")]
    public class ParticlesSO : SymptomsSO
    {
        public Vector3 offset = new Vector3(0, 0, 2.25f);
        public MinMax<int> activeTime = new MinMax<int>(5, 15);
    }
}
