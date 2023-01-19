using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class CharacterData : MonoBehaviour
    {
        private static CharacterData _instance;

        public static CharacterData Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private CharacterController characterController;
        private GameObject characterCamera;
        public CharacterController CharacterController => characterController != null ? characterController : characterController = FindObjectOfType<CharacterController>();

        public GameObject CharacterCamera => characterCamera != null ? characterCamera :
            characterCamera = FindObjectOfType<XROrigin>() ?
            FindObjectOfType<XROrigin>().Camera.gameObject : FindObjectOfType<Camera>().gameObject;

        public Vector3 CharacterCenter => CharacterController.transform.position + CharacterController.center;
        public Vector3 CharacterHeadPos => characterCamera.transform.position;
    }
}
