using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public interface ISymptoms
    {
        public bool IsSimulating { get; }
        public void Simulate();
        public void Stop();
    }
}