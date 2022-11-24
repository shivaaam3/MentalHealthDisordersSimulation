using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sounds/Ambient Sounds")]
    public class AmbientSoundSO : SoundsSO
    {
        public float volumeFactor;
        public float speedFactor;
        public float step;
    }
}