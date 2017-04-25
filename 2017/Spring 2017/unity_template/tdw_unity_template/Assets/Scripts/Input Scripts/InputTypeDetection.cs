///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputTypeDetection.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum ActiveInputType
{
    KEYBOARD,
    MOUSE,
    GAMEPAD
}
#endregion

#region EVENTS
public class EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE : GameEvent
{
    public EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE() { }
}
public class EVENT_TOGGLE_MOUSE_CURSOR : GameEvent
{
    public bool showMouseCursor;
    public EVENT_TOGGLE_MOUSE_CURSOR(bool _showMouseCursor)
    {
        showMouseCursor = _showMouseCursor;
    }
}
public class EVENT_UPDATE_UI_TO_GAMEPAD : GameEvent
{
    public EVENT_UPDATE_UI_TO_GAMEPAD() { }
}
#endregion

public class InputTypeDetection : MonoBehaviour
{
    #region FIELDS
    [HideInInspector]
    public static ActiveInputType currentlyActiveInputType;
    [SerializeField]
    bool utilizeKeyboard;
    public static bool useKeyboardInput = false;
    [SerializeField]
    bool utilizeMouse;
    public static bool useMouseInput = false;

    [SerializeField]
    bool utilizeGamePad;
    public static bool useGamePadInput = false;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //listeners
        if (utilizeKeyboard)
        {
            useKeyboardInput = true;
            Events.instance.AddListener<EVENT_KEYBOARD_ACTIVITY_DETECTED>(KeyboardActivityDetected);
        }
        if (utilizeMouse)
        {
            useMouseInput = true;
            Events.instance.AddListener<EVENT_MOUSE_ACTIVITY_DETECTED>(MouseActivityDetected);
        }
        if (utilizeGamePad)
        {
            useGamePadInput = true;
            Events.instance.AddListener<EVENT_GAMEPAD_ACTIVITY_DETECTED>(GamePadActivityDetected);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
	
	}
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void KeyboardActivityDetected(EVENT_KEYBOARD_ACTIVITY_DETECTED _event)
    {
        //Debug.Log("switching to keyboard input");
        Events.instance.Raise(new EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE());
        Events.instance.Raise(new EVENT_TOGGLE_MOUSE_CURSOR(true));
        currentlyActiveInputType = ActiveInputType.KEYBOARD;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void MouseActivityDetected(EVENT_MOUSE_ACTIVITY_DETECTED _event)
    {
        //Debug.Log("switching to mouse input");
        Events.instance.Raise(new EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE());
        Events.instance.Raise(new EVENT_TOGGLE_MOUSE_CURSOR(true));
        currentlyActiveInputType = ActiveInputType.MOUSE;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void GamePadActivityDetected(EVENT_GAMEPAD_ACTIVITY_DETECTED _event)
    {
        //Debug.Log("switching to GamePad input");
        Events.instance.Raise(new EVENT_UPDATE_UI_TO_GAMEPAD());
        Events.instance.Raise(new EVENT_TOGGLE_MOUSE_CURSOR(false));
        currentlyActiveInputType = ActiveInputType.GAMEPAD;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        if (useKeyboardInput)
            Events.instance.RemoveListener<EVENT_KEYBOARD_ACTIVITY_DETECTED>(KeyboardActivityDetected);
        if (useMouseInput)
            Events.instance.RemoveListener<EVENT_MOUSE_ACTIVITY_DETECTED>(MouseActivityDetected);
        if (useGamePadInput)
            Events.instance.RemoveListener<EVENT_GAMEPAD_ACTIVITY_DETECTED>(GamePadActivityDetected);
    }
    #endregion
}
