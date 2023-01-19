using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sounds/Ambient Sounds")]
    public class AmbientSoundSO : SoundsSO
    {
        public float volumeFactor;
        public float speedFactor;
        public MinMax<int> activeTime = new MinMax<int>(5, 15);
    }
}