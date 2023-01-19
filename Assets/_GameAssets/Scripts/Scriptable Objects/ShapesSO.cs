using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.sharmas4.MentalHealthDisorder
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Visuals/Shape Hallucination")]
    public class ShapesSO : SymptomsSO
    {
        public List<Sprite> sprites;
        public Material[] _materials;
        public MinMax<float> scale = new MinMax<float>(0.3f, 0.8f);
        public MinMax<int> distance = new MinMax<int>(20, 50);
        public MinMax<float> transparency = new MinMax<float>(0.3f, 0.8f);
        public MinMax<int> activeTime = new MinMax<int>(5, 15);
    }
}
