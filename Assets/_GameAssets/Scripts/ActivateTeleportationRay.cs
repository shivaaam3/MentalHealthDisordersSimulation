using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportationRay;
    public GameObject rightTeleportationRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;


    // Update is called once per frame
    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNum, out bool leftIsValid);
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNum, out bool rightIsValid);
        leftTeleportationRay.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1);
        rightTeleportationRay.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1);
    }
}
