using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Make the object look at the xr rig's camera
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    private CharacterController characaterController = null;
    private Vector3 CharacterHeadPos;

    private void Awake()
    {
        characaterController = FindObjectOfType<CharacterController>();
        CharacterHeadPos = characaterController.transform.position + new Vector3(0, characaterController.height, 0);
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = CharacterHeadPos + characaterController.transform.forward * 2.25f;
    }
}
