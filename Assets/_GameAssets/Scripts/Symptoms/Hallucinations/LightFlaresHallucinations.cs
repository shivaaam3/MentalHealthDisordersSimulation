using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{

    public class LightFlaresHallucinations : Symptoms
    {

        public override void Simulate()
        {
            IsSimulating = true;
        }

        public override void Stop()
        {
            IsSimulating = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }
    }
}