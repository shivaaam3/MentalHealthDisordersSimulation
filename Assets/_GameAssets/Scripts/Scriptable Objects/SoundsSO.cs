using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class SoundsSO : SymptomsSO
    {
        public List<AudioClip> clips;
        public MinMax<float> speed;
        public MinMax<float> volume;
    }
}