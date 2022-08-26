using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    //Left : TELEPORT
    //Right : CONTINUOUS
    private void OnMovementToggleValueChanged(ToggleSide toggleSide) {
        continuousMoveProvider.enabled = toggleSide == ToggleSide.Right;
        movementSpeedSlider.transform.parent.gameObject.SetActive(toggleSide == ToggleSide.Right);
        teleportationProvider.enabled = toggleSide == ToggleSide.Left;
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


    // Start is called before the first frame update
    void Start() {
        //PlayerPrefsManager.ClearData();
        InitSettings();
    }

    void InitSettings() {

        continuousMoveProvider.enabled = PlayerPrefsManager.MoveToggleState == ToggleSide.Right;
        movementSpeedSlider.transform.parent.gameObject.SetActive(PlayerPrefsManager.MoveToggleState == ToggleSide.Right);
        teleportationProvider.enabled = PlayerPrefsManager.MoveToggleState == ToggleSide.Left;

        movementToggle.currentSide = PlayerPrefsManager.MoveToggleState;

        snapTurnProvider.enabled = PlayerPrefsManager.TurnToggleState == ToggleSide.Left;
        continuousTurnProvider.enabled = PlayerPrefsManager.TurnToggleState == ToggleSide.Right;

        snapAngleSlider.transform.parent.gameObject.SetActive(PlayerPrefsManager.TurnToggleState == ToggleSide.Left);
        turnSpeedSlider.transform.parent.gameObject.SetActive(PlayerPrefsManager.TurnToggleState == ToggleSide.Right);

        turnToggle.currentSide = PlayerPrefsManager.TurnToggleState;

        snapTurnProvider.turnAmount = PlayerPrefsManager.SnapTurnAmount;
        snapAngleSlider.ApplyNewData(PlayerPrefsManager.SnapTurnAmount);

        continuousTurnProvider.turnSpeed = PlayerPrefsManager.TurnSpeed;
        turnSpeedSlider.ApplyNewData(PlayerPrefsManager.TurnSpeed);

        continuousMoveProvider.moveSpeed = PlayerPrefsManager.MoveSpeed;
        movementSpeedSlider.ApplyNewData(PlayerPrefsManager.MoveSpeed);
    }

}
