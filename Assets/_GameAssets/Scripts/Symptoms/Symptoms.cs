using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.VersionControl;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public abstract class Symptoms : MonoBehaviour
    {
        public bool IsSimulating { get; protected set; }
        private GameObject character;
        public GameObject Character => character != null ? character : (character = FindObjectOfType<XROrigin>() ? FindObjectOfType<XROrigin>().Camera.gameObject : FindObjectOfType<Camera>().gameObject);

        public abstract void Simulate();
        public abstract void Stop();
    }
}