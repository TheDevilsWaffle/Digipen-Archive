#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using UnityEngine.SceneManagement;

public class PlayerInput : InputBase
{
    bool PlayerIndexSet = false;
    [SerializeField]
    public PlayerIndex PlayerIndex;
    private static bool[] GamePadIsAvailable = new bool[4] { true, true, true, true };

    private GamePadState State;
    private GamePadState StateLastFrame;

    [SerializeField]
    private KeyCode MoveForwardKey = KeyCode.W;
    [SerializeField]
    private KeyCode MoveBackwardKey = KeyCode.S;
    [SerializeField]
    private KeyCode MoveLeftKey = KeyCode.A;
    [SerializeField]
    private KeyCode MoveRightKey = KeyCode.D;
    [SerializeField]
    private KeyCode LookLeftKey = KeyCode.Q;
    [SerializeField]
    private KeyCode LookRightKey = KeyCode.E;
    [SerializeField]
    private KeyCode ActionKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode AttackKey = KeyCode.F;
    [SerializeField]
    private KeyCode JumpKey = KeyCode.Space;

    private AnimationCurve VibrationDropoffCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private float VibrationLeftIntensity;
    private float VibrationRightIntensity;

    private float VibrationLeftTime;
    private float VibrationRightTime;

    public bool AllowInput = true;

    bool ShowPosition;
    string Status;

    private MenuSystem MenuSystem;

    // Use this for initialization
    void Start ()
    {
        this.PlayerIndexSet = true;

        this.MenuSystem = FindObjectOfType<MenuSystem>();
        //PlayerInput.GamePadIsAvailable = new bool[4] { true, true, true, true };

        //InputSystem.Subscribe(0, InputActions.ReleasedUp, this.InputActionTest);
        //InputSystem.Subscribe(0, InputActions.HeldRightOnRightStick, this.InputActionTest2);
        //InputSystem.Subscribe(0, InputActions.TriggeredRightOnDpad, this.InputActionTest3);
    }

    void InputActionTest()
    {
        //print("ReleasedUp");
    }
    void InputActionTest2()
    {
        //print("HeldRightOnRightStick");
    }
    void InputActionTest3()
    {
        //print("TriggeredRightOnDpad");
    }

    // Update is called once per frame
    void Update()
    {
        this.AllowInput = this.MenuSystem.AllowPlayerInput;

        this.StateLastFrame = this.State;
        this.State = GamePad.GetState(this.PlayerIndex);

        //Finding the first controller (PlayerIndex) that is connected but not in use
        //if (!this.PlayerIndexSet)
        //{
        //    for (int i = 0; i < 4; ++i)
        //    {
        //        if (PlayerInput.GamePadIsAvailable[i] == false)
        //            continue;

        //        PlayerIndex testPlayerIndex = (PlayerIndex)i;
        //        GamePadState testState = GamePad.GetState(testPlayerIndex);
        //        if (testState.IsConnected)
        //        {
        //            Debug.Log(string.Format("GamePad " + ((int)testPlayerIndex + 1) + " found for " + this.gameObject.name));
        //            this.PlayerIndex = testPlayerIndex;
        //            this.PlayerIndexSet = true;
        //            PlayerInput.GamePadIsAvailable[(int)this.PlayerIndex] = false;
        //            break;
        //        }
        //    }
        //}
        //else
        //{
        //    if (this.State.IsConnected == false)
        //    {
        //        Debug.Log(string.Format("GamePad " + ((int)this.PlayerIndex + 1) + " lost for " + this.gameObject.name));
        //        this.PlayerIndexSet = false;
        //        PlayerInput.GamePadIsAvailable[(int)this.PlayerIndex] = true;
        //    }
        //}

        if (Time.timeScale == 0)
        {
            GamePad.SetVibration(this.PlayerIndex, 0, 0);
        }

        if (this.AllowInput)
        {
            this.UpdateMovementInput();
            this.UpdateLookInput();
            this.UpdateActionInput();
            this.UpdateShootInput();
            this.UpdateJumpInput();
        }
        else
            this.ResetAll();

        if (this.State.IsConnected && this.PlayerIndexSet)
            this.UpdateVibration();
    }

    private void ResetAll()
    {
        this.Movement = Vector2.zero;
        //this.Look = Vector2.zero;
        this.Attack = false;
        //this.Boost = 0;
        //this.Dash = 0;
        this.Jump = false;
    }

    private void UpdateMovementInput()
    {
        Vector2 gamePadInput = Vector2.zero;
        if (this.State.IsConnected && this.PlayerIndexSet)
            gamePadInput = new Vector2(this.State.ThumbSticks.Left.X, this.State.ThumbSticks.Left.Y);

        float keyboardInputX = Convert.ToSingle(Input.GetKey(this.MoveRightKey)) - Convert.ToSingle(Input.GetKey(this.MoveLeftKey));
        float keyboardInputY = Convert.ToSingle(Input.GetKey(this.MoveForwardKey)) - Convert.ToSingle(Input.GetKey(this.MoveBackwardKey));
        Vector2 keyboardInput = new Vector2(keyboardInputX, keyboardInputY);

        this.Movement = new Vector2(Mathf.Clamp(gamePadInput.x + keyboardInput.x, -1, 1), Mathf.Clamp(gamePadInput.y + keyboardInput.y, -1, 1));
    }
    private void UpdateLookInput()
    {
        Vector2 gamePadInput = Vector2.zero;
        if (this.State.IsConnected && this.PlayerIndexSet)
            gamePadInput = new Vector2(this.State.ThumbSticks.Right.X, this.State.ThumbSticks.Right.Y);

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float keyboardInput = Convert.ToSingle(Input.GetKey(this.LookLeftKey)) - Convert.ToSingle(Input.GetKey(this.LookRightKey));

        this.Look = new Vector2(Mathf.Clamp(gamePadInput.x + mouseInput.x + keyboardInput, -1, 1), Mathf.Clamp(gamePadInput.y + mouseInput.y, -1, 1));
    }
    private void UpdateShootInput()
    {
        bool gamePadInput = false;
        if (this.State.IsConnected && this.PlayerIndexSet)
            gamePadInput = this.State.Buttons.B == ButtonState.Pressed;

        bool keyboardInput = Input.GetKey(this.JumpKey);
        this.Attack = gamePadInput || keyboardInput;
    }

    private void UpdateActionInput()
    {
        float gamePadInput = 0;
        if (this.State.IsConnected && this.PlayerIndexSet)
            gamePadInput = this.State.Triggers.Right;

        float mouseInput = 0;//Convert.ToSingle(Input.GetMouseButton(0));
        float keyboardInput = Convert.ToSingle(Input.GetKey(this.ActionKey));

        this.Action = Convert.ToBoolean(Mathf.Clamp(gamePadInput + mouseInput + keyboardInput, 0, 1));
    }

    //private void UpdateBoostInput()
    //{
    //    float gamePadInput = 0;
    //    if (this.State.IsConnected && this.PlayerIndexSet)
    //        gamePadInput = this.State.Triggers.Left;
    //    float keyboardInput = Convert.ToSingle(Input.GetKey(this.BoostKey));

    //    this.Boost = Mathf.Clamp(gamePadInput + keyboardInput, 0, 1);
    //}
    //private void UpdateDashInput()
    //{
    //    float gamePadInput = 0;
    //    if (this.State.IsConnected && this.PlayerIndexSet)
    //        gamePadInput = (int)this.State.Buttons.LeftShoulder - (int)this.State.Buttons.RightShoulder;

    //    float keyboardInput = Convert.ToSingle(Input.GetKey(this.DashRightKey)) - Convert.ToSingle(Input.GetKey(this.DashLeftKey));
    //    this.Dash = Mathf.Clamp(gamePadInput + keyboardInput, -1, 1);
    //}
    private void UpdateJumpInput()
    {
        bool gamePadInput = false;
        if (this.State.IsConnected && this.PlayerIndexSet)
            gamePadInput = this.State.Buttons.A == ButtonState.Pressed;

        bool keyboardInput = Input.GetKey(this.JumpKey);
        this.Jump = gamePadInput || keyboardInput;
    }

    private void UpdateVibration()
    {
        //Subtracting the time that has passed from the time remaining on the vibration
        this.VibrationLeftTime -= Time.deltaTime;
        this.VibrationRightTime -= Time.deltaTime;

        //Eliminating vibration for the left side if the duration has passed
        if (this.VibrationLeftTime <= 0)
            this.VibrationLeftIntensity = 0;

        //Eliminating vibration for the right side if the duration has passed
        if (this.VibrationRightTime <= 0)
            this.VibrationRightIntensity = 0;

        //Setting the the vibration on the controller
        GamePad.SetVibration(this.PlayerIndex, this.VibrationLeftIntensity, this.VibrationRightIntensity);
    }

    //public void SetControllerVibration(float intensity)
    //{
    //    intensity = Mathf.Clamp01(intensity);
    //    GamePad.SetVibration(this.PlayerIndex, intensity, intensity);
    //}
    //public void SetControllerVibration(float leftIntensity, float rightIntensity)
    //{
    //    leftIntensity = Mathf.Clamp01(leftIntensity);
    //    rightIntensity = Mathf.Clamp01(rightIntensity);
    //    GamePad.SetVibration(this.PlayerIndex, leftIntensity, rightIntensity);
    //}

    public void VibrateController(float intensity, float duration)
    {
        intensity = Mathf.Clamp01(intensity);
        this.VibrationLeftIntensity = intensity;
        this.VibrationRightIntensity = intensity;
        this.VibrationLeftTime = duration;
        this.VibrationRightTime = duration;
    }
    public void VibrateController(float leftIntensity, float rightIntensity, float duration)
    {
        leftIntensity = Mathf.Clamp01(leftIntensity);
        rightIntensity = Mathf.Clamp01(rightIntensity);
        this.VibrationLeftIntensity = leftIntensity;
        this.VibrationRightIntensity = rightIntensity;
        this.VibrationLeftTime = duration;
        this.VibrationRightTime = duration;
    }

    void OnApplicationQuit()
    {
        GamePad.SetVibration(this.PlayerIndex, 0, 0);
    }

    //void OnDestroy()
    //{
    //    //PlayerInput.GamePadIsAvailable = new bool[4] { true, true, true, true };
    //}
}
