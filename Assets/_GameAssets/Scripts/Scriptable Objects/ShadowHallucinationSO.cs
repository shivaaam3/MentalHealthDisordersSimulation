using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class ShadowHallucinationSO : ImageSO
    {
        public float scale;
        public Sprite _sprite;
        public Material _material;
        public MinMax<int> distance;
        public MinMax<int> timeAfterClip;
    }
}
