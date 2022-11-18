using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    public static float SnapTurnAmount
    {
        get { return PlayerPrefs.GetFloat(PlayerPrefsConstants.SNAP_TURN_AMOUNT, 45f); }
        set { PlayerPrefs.SetFloat(PlayerPrefsConstants.SNAP_TURN_AMOUNT, value); }
    }

    public static float TurnSpeed
    {
        get { return PlayerPrefs.GetFloat(PlayerPrefsConstants.TURN_SPEED, 60f); }
        set { PlayerPrefs.SetFloat(PlayerPrefsConstants.TURN_SPEED, value); }
    }

    public static float MoveSpeed
    {
        get { return PlayerPrefs.GetFloat(PlayerPrefsConstants.MOVE_SPEED, 1f); }
        set { PlayerPrefs.SetFloat(PlayerPrefsConstants.MOVE_SPEED, value); }
    }


    public static ToggleSide TurnToggleState
    {
        get { return (ToggleSide)System.Enum.Parse(typeof(ToggleSide), PlayerPrefs.GetString(PlayerPrefsConstants.TURN_TOGGLE_STATE, ToggleSide.Left.ToString())); }
        set { PlayerPrefs.SetString(PlayerPrefsConstants.TURN_TOGGLE_STATE, value.ToString()); }
    }


    public static ToggleSide MoveToggleState
    {
        get { return (ToggleSide)System.Enum.Parse(typeof(ToggleSide), PlayerPrefs.GetString(PlayerPrefsConstants.MOVE_TOGGLE_STATE, ToggleSide.Left.ToString())); }
        set { PlayerPrefs.SetString(PlayerPrefsConstants.MOVE_TOGGLE_STATE, value.ToString()); }
    }


    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }


    public static void DebugPrefs()
    {
        Debug.Log("MoveToggleState: " + MoveToggleState);
        Debug.Log("TurnToggleState: " + TurnToggleState);
        Debug.Log("MoveSpeed: " + MoveSpeed.ToString());
        Debug.Log("TurnSpeed: " + TurnSpeed.ToString());
        Debug.Log("SnapTurnAmount: " + SnapTurnAmount.ToString());
    }
}


public static class PlayerPrefsConstants
{
    public const string SNAP_TURN_AMOUNT = "SnapAngleValue";
    public const string TURN_SPEED = "TurnSpeed";
    public const string MOVE_SPEED = "MoveSpeed";
    public const string TURN_TOGGLE_STATE = "TurnToggleState";
    public const string MOVE_TOGGLE_STATE = "MoveToggleState";
}