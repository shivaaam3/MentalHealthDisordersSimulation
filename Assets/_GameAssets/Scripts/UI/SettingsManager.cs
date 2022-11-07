using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingsManager : MonoBehaviour {

    [SerializeField] private ButtonSlider snapAngleSlider;
    [SerializeField] private ButtonSlider turnSpeedSlider;
    [SerializeField] private ButtonSlider movementSpeedSlider;
    [SerializeField] private Toggle movementToggle;
    [SerializeField] private Toggle turnToggle;


    [SerializeField] private ActionBasedContinuousMoveProvider continuousMoveProvider;
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurnProvider;
    [SerializeField] private ActionBasedSnapTurnProvider snapTurnProvider;

    public static Action<ToggleSide> OnMovementChanged;

    private void OnEnable() {
        snapAngleSlider.OnValueChanged += OnSnapAngleValueChanged;
        turnSpeedSlider.OnValueChanged += OnTurnSpeedValueChanged;
        movementSpeedSlider.OnValueChanged += OnMovementSpeedValueChanged;
        movementToggle.OnValueChanged += OnMovementToggleValueChanged;
        turnToggle.OnValueChanged += OnTurnToggleValueChanged;
    }

    private void OnDisable() {
        snapAngleSlider.OnValueChanged -= OnSnapAngleValueChanged;
        turnSpeedSlider.OnValueChanged -= OnTurnSpeedValueChanged;
        movementSpeedSlider.OnValueChanged -= OnMovementSpeedValueChanged;
        movementToggle.OnValueChanged -= OnMovementToggleValueChanged;
        turnToggle.OnValueChanged -= OnTurnToggleValueChanged;
    }

    // Left : SNAP
    // Right : CONTINUOUS
    private void OnTurnToggleValueChanged(ToggleSide toggleSide) {
        snapTurnProvider.enabled = toggleSide == ToggleSide.Left;
        snapAngleSlider.transform.parent.gameObject.SetActive(toggleSide == ToggleSide.Left);

        continuousTurnProvider.enabled = toggleSide == ToggleSide.Right;
        turnSpeedSlider.transform.parent.gameObject.SetActive(toggleSide == ToggleSide.Right);

        PlayerPrefsManager.TurnToggleState = toggleSide;
    }


    //Left : TELEPORT
    //Right : CONTINUOUS
    private void OnMovementToggleValueChanged(ToggleSide toggleSide) {

        if (toggleSide == ToggleSide.Left){
            snapTurnProvider.leftHandSnapTurnAction.action.Enable();
            continuousTurnProvider.leftHandTurnAction.action.Enable();
            teleportationProvider.enabled = true;
            continuousMoveProvider.enabled = false;
            movementSpeedSlider.transform.parent.gameObject.SetActive(false);
        }
        else {
            snapTurnProvider.leftHandSnapTurnAction.action.Disable();
            continuousTurnProvider.leftHandTurnAction.action.Disable();
            teleportationProvider.enabled = false;
            continuousMoveProvider.enabled = true;
            movementSpeedSlider.transform.parent.gameObject.SetActive(true);
        }
        OnMovementChanged?.Invoke(toggleSide);
        PlayerPrefsManager.MoveToggleState = toggleSide;
    }

    void OnSnapAngleValueChanged(float value) {
        snapTurnProvider.turnAmount = value;
        PlayerPrefsManager.SnapTurnAmount = value;
    }

    void OnTurnSpeedValueChanged(float value) {
        continuousTurnProvider.turnSpeed = value;
        PlayerPrefsManager.TurnSpeed = value;
    }

    void OnMovementSpeedValueChanged(float value) {
        continuousMoveProvider.moveSpeed = value;
        PlayerPrefsManager.MoveSpeed = value;
    }


    public void Exit() {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start() {
        InitSettings();
    }




    void InitSettings() {
        //PlayerPrefsManager.ClearData();
        //PlayerPrefsManager.DebugPrefs();

        movementToggle.SwitchToggleTo(PlayerPrefsManager.MoveToggleState);
        turnToggle.SwitchToggleTo(PlayerPrefsManager.TurnToggleState);
        snapAngleSlider.ApplyNewData(PlayerPrefsManager.SnapTurnAmount);
        turnSpeedSlider.ApplyNewData(PlayerPrefsManager.TurnSpeed);
        movementSpeedSlider.ApplyNewData(PlayerPrefsManager.MoveSpeed);
    }

}
