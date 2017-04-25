///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamePadInputData.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public enum GamePadInputButtons
{
    Y, B, A, X,
    Select, Start,
    LS, RS,
    LT, RT,
    DPad_Up, DPad_Right, DPad_Down, DPad_Left,
    LeftAnalogStick, RightAnalogStick, L3, R3
};

public class GamePadInputData
{
    #region Y
    GamePadButtonState y;
    public GamePadButtonState Y
    {
        get { return y; }
        set { y = value; }
    }
    float y_heldTimer = 0f;
    public float Y_HeldTimer
    {
        get { return y_heldTimer; }
        set { y_heldTimer = value; }
    }
    float y_releasedTimer = 0f;
    public float Y_ReleasedTimer
    {
        get { return y_releasedTimer; }
        set { y_releasedTimer = value; }
    }
    #endregion
    #region B
    GamePadButtonState b;
    public GamePadButtonState B
    {
        get { return b; }
        set { b = value; }
    }
    float b_heldTimer = 0f;
    public float B_HeldTimer
    {
        get { return b_heldTimer; }
        set { b_heldTimer = value; }
    }
    float b_releasedTimer = 0f;
    public float B_ReleasedTimer
    {
        get { return b_releasedTimer; }
        set { b_releasedTimer = value; }
    }
    #endregion
    #region A
    GamePadButtonState a;
    public GamePadButtonState A
    {
        get { return a; }
        set { a = value; }
    }
    float a_heldTimer = 0f;
    public float A_HeldTimer
    {
        get { return a_heldTimer; }
        set { a_heldTimer = value; }
    }
    float a_releasedTimer = 0f;
    public float A_ReleasedTimer
    {
        get { return a_releasedTimer; }
        set { a_releasedTimer = value; }
    }
    #endregion
    #region X
    GamePadButtonState x;
    public GamePadButtonState X
    {
        get { return x; }
        set { x = value; }
    }
    float x_heldTimer = 0f;
    public float X_HeldTimer
    {
        get { return x_heldTimer; }
        set { x_heldTimer = value; }
    }
    float x_releasedTimer = 0f;
    public float X_ReleasedTimer
    {
        get { return x_releasedTimer; }
        set { x_releasedTimer = value; }
    }
    #endregion

    #region SELECT
    GamePadButtonState select;
    public GamePadButtonState Select
    {
        get { return select; }
        set { select = value; }
    }
    float select_heldTimer = 0f;
    public float Select_HeldTimer
    {
        get { return select_heldTimer; }
        set { select_heldTimer = value; }
    }
    float select_releasedTimer = 0f;
    public float Select_ReleasedTimer
    {
        get { return select_releasedTimer; }
        set { select_releasedTimer = value; }
    }
    #endregion
    #region START
    GamePadButtonState start;
    public GamePadButtonState Start
    {
        get { return start; }
        set { start = value; }
    }
    float start_heldTimer = 0f;
    public float Start_HeldTimer
    {
        get { return start_heldTimer; }
        set { start_heldTimer = value; }
    }
    float start_releasedTimer = 0f;
    public float Start_ReleasedTimer
    {
        get { return start_releasedTimer; }
        set { start_releasedTimer = value; }
    }
    #endregion

    #region LEFT SHOULDER
    GamePadButtonState lb;
    public GamePadButtonState LB
    {
        get { return lb; }
        set { lb = value; }
    }
    float lb_heldTimer = 0f;
    public float LB_HeldTimer
    {
        get { return lb_heldTimer; }
        set { lb_heldTimer = value; }
    }
    float lb_releasedTimer = 0f;
    public float LB_ReleasedTimer
    {
        get { return lb_releasedTimer; }
        set { lb_releasedTimer = value; }
    }
    #endregion
    #region RIGHT SHOULDER
    GamePadButtonState rb;
    public GamePadButtonState RB
    {
        get { return rb; }
        set { rb = value; }
    }
    float rb_heldTimer = 0f;
    public float RB_HeldTimer
    {
        get { return rb_heldTimer; }
        set { rb_heldTimer = value; }
    }
    float rb_releasedTimer = 0f;
    public float RB_ReleasedTimer
    {
        get { return rb_releasedTimer; }
        set { rb_releasedTimer = value; }
    }
    #endregion

    #region LEFT TRIGGER
    GamePadButtonState lt;
    public GamePadButtonState LT
    {
        get { return lt; }
        set { lt = value; }
    }
    float ltValue;
    public float LTValue
    {
        get { return ltValue; }
        set { ltValue = value; }
    }
    float lt_heldTimer = 0f;
    public float LT_HeldTimer
    {
        get { return lt_heldTimer; }
        set { lt_heldTimer = value; }
    }
    float lt_releasedTimer = 0f;
    public float LT_ReleasedTimer
    {
        get { return lt_releasedTimer; }
        set { lt_releasedTimer = value; }
    }
    #endregion
    #region RIGHT TRIGGER
    GamePadButtonState rt;
    public GamePadButtonState RT
    {
        get { return rt; }
        set { rt = value; }
    }
    float rtValue;
    public float RTValue
    {
        get { return rtValue; }
        set { rtValue = value; }
    }
    float rt_heldTimer = 0f;
    public float RT_HeldTimer
    {
        get { return rt_heldTimer; }
        set { rt_heldTimer = value; }
    }
    float rt_releasedTimer = 0f;
    public float RT_ReleasedTimer
    {
        get { return rt_releasedTimer; }
        set { rt_releasedTimer = value; }
    }
    #endregion

    #region DPAD UP
    GamePadButtonState dpad_up;
    public GamePadButtonState DPadUp
    {
        get { return dpad_up; }
        set { dpad_up = value; }
    }
    float dpad_up_heldTimer = 0f;
    public float DPAD_UP_HeldTimer
    {
        get { return dpad_up_heldTimer; }
        set { dpad_up_heldTimer = value; }
    }
    float dpad_up_releasedTimer = 0f;
    public float DPAD_UP_ReleasedTimer
    {
        get { return dpad_up_releasedTimer; }
        set { dpad_up_releasedTimer = value; }
    }
    #endregion
    #region DPAD RIGHT
    GamePadButtonState dpad_right;
    public GamePadButtonState DPadRight
    {
        get { return dpad_right; }
        set { dpad_right = value; }
    }
    float dpad_right_heldTimer = 0f;
    public float DPAD_RIGHT_HeldTimer
    {
        get { return dpad_right_heldTimer; }
        set { dpad_right_heldTimer = value; }
    }
    float dpad_right_releasedTimer = 0f;
    public float DPAD_RIGHT_ReleasedTimer
    {
        get { return dpad_right_releasedTimer; }
        set { dpad_right_releasedTimer = value; }
    }
    #endregion
    #region DPAD DOWN
    GamePadButtonState dpad_down;
    public GamePadButtonState DPadDown
    {
        get { return dpad_down; }
        set { dpad_down = value; }
    }
    float dpad_down_heldTimer = 0f;
    public float DPAD_DOWN_HeldTimer
    {
        get { return dpad_down_heldTimer; }
        set { dpad_down_heldTimer = value; }
    }
    float dpad_down_releasedTimer = 0f;
    public float DPAD_DOWN_ReleasedTimer
    {
        get { return dpad_down_releasedTimer; }
        set { dpad_down_releasedTimer = value; }
    }
    #endregion
    #region DPAD LEFT
    GamePadButtonState dpad_left;
    public GamePadButtonState DPadLeft
    {
        get { return dpad_left; }
        set { dpad_left = value; }
    }
    float dpad_left_heldTimer = 0f;
    public float DPAD_LEFT_HeldTimer
    {
        get { return dpad_left_heldTimer; }
        set { dpad_left_heldTimer = value; }
    }
    float dpad_left_releasedTimer = 0f;
    public float DPAD_LEFT_ReleasedTimer
    {
        get { return dpad_left_releasedTimer; }
        set { dpad_left_releasedTimer = value; }
    }
    #endregion

    #region LEFT ANALOG STICK
    Vector3 leftAnalogStick;
    public Vector3 LeftAnalogStick
    {
        get { return leftAnalogStick; }
        set { leftAnalogStick = value; }
    }
    Vector3 leftAnalogStickRaw;
    public Vector3 LeftAnalogStickRaw
    {
        get { return leftAnalogStickRaw; }
        set { leftAnalogStickRaw = value; }
    }
    float leftAnalogStick_angle;
    public float LeftAnalogStickAngle
    {
        get { return leftAnalogStick_angle; }
        set { leftAnalogStick_angle = value; }
    }
    GamePadButtonState leftAnalogStick_Status;
    public GamePadButtonState LeftAnalogStick_Status
    {
        get { return leftAnalogStick_Status; }
        set { leftAnalogStick_Status = value; }
    }
    GamePadButtonState l3;
    public GamePadButtonState L3
    {
        get { return l3; }
        set { l3 = value; }
    }
    float l3_heldTimer = 0f;
    public float L3_HeldTimer
    {
        get { return l3_heldTimer; }
        set { l3_heldTimer = value; }
    }
    float l3_releasedTimer = 0f;
    public float L3_ReleasedTimer
    {
        get { return l3_releasedTimer; }
        set { l3_releasedTimer = value; }
    }
    #endregion
    #region RIGHT ANALOG STICK
    Vector3 rightAnalogStick;
    public Vector3 RightAnalogStick
    {
        get { return rightAnalogStick; }
        set { rightAnalogStick = value; }
    }
    Vector3 rightAnalogStickRaw;
    public Vector3 RightAnalogStickRaw
    {
        get { return rightAnalogStickRaw; }
        set { rightAnalogStickRaw = value; }
    }
    float rightAnalogStickAngle;
    public float RightAnalogStickAngle
    {
        get { return rightAnalogStickAngle; }
        set { rightAnalogStickAngle = value; }
    }
    GamePadButtonState rightAnalogStick_Status;
    public GamePadButtonState RightAnalogStick_Status
    {
        get { return rightAnalogStick_Status; }
        set { rightAnalogStick_Status = value; }
    }
    GamePadButtonState r3;
    public GamePadButtonState R3
    {
        get { return r3; }
        set { r3 = value; }
    }
    float r3_heldTimer = 0f;
    public float R3_HeldTimer
    {
        get { return r3_heldTimer; }
        set { r3_heldTimer = value; }
    }
    float r3_releasedTimer = 0f;
    public float R3_ReleasedTimer
    {
        get { return r3_releasedTimer; }
        set { r3_releasedTimer = value; }
    }
    #endregion
}
