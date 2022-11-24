using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sounds/Voice Hallucination")]
    public class VoiceHallucinationSO : SoundsSO
    {
        public MinMax<int> pitch;
        public MinMax<int> distance;
        public MinMax<int> timeAfterClip;
    }
}