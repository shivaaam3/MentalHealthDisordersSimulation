using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sounds/Conscience")]
    public class ConscienceSO : SoundsSO
    {
        public MinMax<int> timeAfterClip;
    }
}