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

        PlayerPrefsManager.TurnToggleState = toggleSide;
    }


    //Left : TELEPORT
    //Right : CONTINUOUS
    private void OnMovementToggleValueChanged(ToggleSide toggleSide) {
        teleportationProvider.enabled = toggleSide == ToggleSide.Left;

        continuousMoveProvider.enabled = toggleSide == ToggleSide.Right;
        movementSpeedSlider.transform.parent.gameObject.SetActive(toggleSide == ToggleSide.Right);

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
