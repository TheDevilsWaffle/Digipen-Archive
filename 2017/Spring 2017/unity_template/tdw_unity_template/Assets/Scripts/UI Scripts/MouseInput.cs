///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MouseInput.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum MouseInputStatus
{
    INACTIVE,
    PRESSED,
    HELD,
    RELEASED
}
public enum MouseDirectionStatus
{
    INACTIVE,
    LEFT,
    LEFT_UP,
    UP,
    RIGHT_UP,
    RIGHT,
    RIGHT_DOWN,
    DOWN,
    LEFT_DOWN
}
#endregion

#region EVENTS
public class EVENT_MOUSE_ACTIVITY_DETECTED : GameEvent
{
    public EVENT_MOUSE_ACTIVITY_DETECTED() { }
}
#endregion

public class MouseInput : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    bool overrideDefaultCursor;
    [Header("CUSTOM MOUSE CURSOR")]
    [SerializeField]
    Texture2D defaultCursorTexture2D;
    [SerializeField]
    Vector2 defaultCursorOffset = new Vector2(0f, 0f);

    [Header("ATTRIBUTES")]
    [SerializeField]
    [Range(0f, 0.5f)]
    float mouseDirectionSensitivity = 0.2f;
    [SerializeField]
    float delayBeforeDeclaredInactive = 3f;
    [SerializeField]
    bool hideMouseCursor = false;

    static Vector3 pixelPosition;
    public static Vector3 PixelPosition
    {
        get { return pixelPosition; }
        set { pixelPosition = value; }
    }
    static Vector3 percentagePosition;
    public static Vector3 PercentagePosition
    {
        get { return percentagePosition; }
        set { percentagePosition = value; }
    }
    static Vector3 percentagePositionCorrected;
    public static Vector3 PercentagePositionCorrected
    {
        get { return percentagePositionCorrected; }
        set { percentagePositionCorrected = value; }
    }
    static MouseDirectionStatus mouseCurrentDirection;
    public static MouseDirectionStatus MouseCurrentDirection
    {
        get { return mouseCurrentDirection; }
        private set { mouseCurrentDirection = value; }
    }
    
    static bool isMouseActive;
    public static bool IsMouseActive
    {
        get { return isMouseActive; }
        private set { isMouseActive = value; }
    }
    float inactivityTimer = 0f;
    static MouseInputStatus mouse1;
    public static MouseInputStatus Mouse1
    {
        get { return mouse1; }
    }
    static MouseInputStatus mouse2;
    public static MouseInputStatus Mouse2
    {
        get { return mouse2; }
    }
    static Vector2 mouseCurrentDirectionValue;
    public static Vector2 MouseCurrentDirectionValue
    {
        get { return mouseCurrentDirectionValue; }
        private set { mouseCurrentDirectionValue = value; }
    }
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        inactivityTimer = 0f;
        SetSubscriptions();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (overrideDefaultCursor)
            SetDefaultMouseCursorSprite();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_TOGGLE_MOUSE_CURSOR>(ToggleMouseCursor);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (CheckMouseActivity())
        {
            //Debug.Log("is mouse active? = " + IsMouseActive);
            mouse1 = UpdateMouse1Status();
            //Debug.Log("mouse1 = " + Mouse1);
            mouse2 = UpdateMouse2Status();
            //Debug.Log("mouse2= " + Mouse2);
            UpdateMousePosition();
            UpdateMouseDirection();
            //Debug.Log("mouseCurrentDirectionValue = " + MouseCurrentDirectionValue);
            //Debug.Log("mouseCurrentDirection = " + MouseCurrentDirection);
        }

        //DEBUG
        //if (Input.GetKeyDown(KeyCode.Keypad0))
        //    HideMouseCursor();
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //    ShowMouseCursor();
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        //    SetCustomMouseCursorSprite();
        //if (Input.GetKeyDown(KeyCode.Keypad3))
        //    SetDefaultMouseCursorSprite();
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///  updates the staus of the left-click mouse button
    /// </summary>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    MouseInputStatus UpdateMouse1Status()
    {
        //released
        if (Input.GetMouseButtonUp(0))
        {
            IsMouseActive = true;
            return MouseInputStatus.RELEASED;
        }
        //pressed
        else if (Input.GetMouseButtonDown(0))
        {
            IsMouseActive = true;
            return MouseInputStatus.PRESSED;
        }
        //held
        else if (Input.GetMouseButton(0))
        {
            IsMouseActive = true;
            return MouseInputStatus.HELD;
        }
        //inactive
        else
        {
            return MouseInputStatus.INACTIVE;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Updates the status of the right-click mouse button
    /// </summary>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    MouseInputStatus UpdateMouse2Status()
    {
        //released
        if (Input.GetMouseButtonUp(1))
        {
            IsMouseActive = true;
            return MouseInputStatus.RELEASED;
        }
        //pressed
        else if (Input.GetMouseButtonDown(1))
        {
            IsMouseActive = true;
            return MouseInputStatus.PRESSED;
        }
        //held
        else if (Input.GetMouseButton(1))
        {
            IsMouseActive = true;
            return MouseInputStatus.HELD;
        }
        //inactive
        else
        {
            return MouseInputStatus.INACTIVE;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMousePosition()
    {
        PixelPosition = Input.mousePosition;
        PercentagePosition = new Vector3((PixelPosition.x / Screen.width),
                                         (PixelPosition.y / Screen.height), 
                                         PixelPosition.z);
        PercentagePositionCorrected = new Vector3(Mathf.Clamp(((PercentagePosition.x * 2) - 1), -1f, 1f),
                                                  Mathf.Clamp(((PercentagePosition.y * 2) - 1), -1f, 1f),
                                                  PixelPosition.z);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Updates the value/enum of the mouse's current direction
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMouseDirection()
    {
        MouseCurrentDirectionValue = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //mouse is going left
        if (MouseCurrentDirectionValue.x < -mouseDirectionSensitivity)
        {
            IsMouseActive = true;

            if (MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.LEFT_UP;
            else if (MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.LEFT_DOWN;
            else
                MouseCurrentDirection = MouseDirectionStatus.LEFT;
        }
        //mouse is going right
        else if (MouseCurrentDirectionValue.x > mouseDirectionSensitivity)
        {
            IsMouseActive = true;

            if (MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.RIGHT_UP;
            else if (MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.RIGHT_DOWN;
            else
                MouseCurrentDirection = MouseDirectionStatus.RIGHT;
        }
        //mouse is going up
        else if (MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
        {
            IsMouseActive = true;

            if (MouseCurrentDirectionValue.x < mouseDirectionSensitivity && MouseCurrentDirectionValue.x > -mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.UP;
        }
        //mouse is going down
        else if (MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
        {
            IsMouseActive = true;

            if (MouseCurrentDirectionValue.x < mouseDirectionSensitivity && MouseCurrentDirectionValue.x > -mouseDirectionSensitivity)
                MouseCurrentDirection = MouseDirectionStatus.DOWN;
        }
        else
        {
            MouseCurrentDirection = MouseDirectionStatus.INACTIVE;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Checks to see if the mouse is active at all
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool CheckMouseActivity()
    {
        if(InputTypeDetection.useMouseInput)
        {
            if (MouseCurrentDirection == MouseDirectionStatus.INACTIVE && Mouse1 == MouseInputStatus.INACTIVE && Mouse2 == MouseInputStatus.INACTIVE)
            {
                if (inactivityTimer < delayBeforeDeclaredInactive)
                {
                    inactivityTimer += Time.unscaledDeltaTime;
                    //Debug.Log("MOUSE INACTIVE TIMER STARTED: " + inactivityTimer +" out of " + delayBeforeDeclaredInactive);
                }
                else if (inactivityTimer >= delayBeforeDeclaredInactive)
                {
                    IsMouseActive = false;
                    //Debug.Log("MOUSE DECLARED INACTIVE!");
                }
            }
            else
            {
                if (inactivityTimer >= delayBeforeDeclaredInactive)
                {
                    inactivityTimer = 0f;
                    Events.instance.Raise(new EVENT_MOUSE_ACTIVITY_DETECTED());
                    InputTypeDetection.currentlyActiveInputType = ActiveInputType.MOUSE;
                    //Debug.Log("MOUSE ACTIVE AFTER BEING INACTIVE!");
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleMouseCursor(EVENT_TOGGLE_MOUSE_CURSOR _event)
    {
        if(_event.showMouseCursor)
        {
            ShowMouseCursor();
        }
        else
        {
            HideMouseCursor();
        }
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void HideMouseCursor()
    {
        Cursor.visible = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_texture2D"></param>
    /// <param name="_offset"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCustomMouseCursorSprite(Texture2D _texture2D, Vector2 _offset)
    {
        ShowMouseCursor();
        Cursor.SetCursor(_texture2D, _offset, CursorMode.Auto);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetDefaultMouseCursorSprite()
    {
        ShowMouseCursor();
        if (defaultCursorTexture2D != null)
        {
            Cursor.SetCursor(defaultCursorTexture2D, defaultCursorOffset, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    #endregion

    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_TOGGLE_MOUSE_CURSOR>(ToggleMouseCursor);
    }
    #endregion
}
