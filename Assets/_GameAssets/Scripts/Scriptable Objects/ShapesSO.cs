using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Visuals/Shape Hallucination")]
    public class ShapesSO : ScriptableObject
    {
        public List<Sprite> sprites;
        public float scale;
        public Material _material;
        public MinMax<int> distance;
        public MinMax<int> timeAfterClip;
    }
}
