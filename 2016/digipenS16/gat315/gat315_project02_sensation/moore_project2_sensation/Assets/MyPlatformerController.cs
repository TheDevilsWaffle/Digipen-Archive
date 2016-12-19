/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MyPlatformerController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

#region STRUCTS

public struct MyInput
{
    public Vector2 ThumbstickInput;
    public bool JumpInput;
    public int FlashlightDegrees;
    public FlashlightStatus FlashlightActive;
    public bool RunInput;
}

#endregion

public class MyPlatformerController : MonoBehaviour
{
    #region PROPERTIES

    FlashlightStatus CurrentFlashLightStatus = FlashlightStatus.OFF;

    [SerializeField]
    [Range(0, 3)]
    int PlayerNumber = 0;
    PlayerIndex Player;
    GamePadState CurrentState;
    GamePadState PreviousState;
    bool IsJumping;
    bool IsRunning;

    public MyInput Current;
    Vector2 CurrentThumbstickInput;
    Vector2 CurrentFlashlightRotation;
    float CurrentFlashlightDegrees;

    #endregion

    #region INITIALIZE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update the gamepad states every update
        this.UpdateGamepadStates(this.Player);

        this.UpdateThumbstickInput(this.CurrentState);
        this.UpdateFlashlight(this.CurrentState, this.PreviousState);

        //rest IsJumping before evaluating
        this.IsJumping = false;
        this.UpdateJump(this.CurrentState, this.PreviousState);

        //update the flashlight
        this.UpdateFlashlightRotation(this.CurrentState);

        //update running
        this.UpdateRunning(this.CurrentState);

        this.UpdatePlayerInput();
    }

    #endregion

    #region GAMEPAD UPDATES

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadStates(PlayerIndex)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadStates(PlayerIndex player_)
    {
        this.PreviousState = this.CurrentState;
        this.CurrentState = GamePad.GetState(player_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateThumbstickInput(GamePadState,)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateThumbstickInput(GamePadState current_)
    {
        //GAMEPAD SUPPORT
        this.CurrentThumbstickInput = new Vector2(current_.ThumbSticks.Left.X, current_.ThumbSticks.Left.Y);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateJump(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateJump(GamePadState current_, GamePadState previous_)
    {
        if (current_.Buttons.A == ButtonState.Pressed && previous_.Buttons.A == ButtonState.Released)
        {
            this.IsJumping = true;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlight(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateFlashlight(GamePadState current_, GamePadState previous_)
    {
        if (current_.Buttons.RightStick == ButtonState.Pressed && previous_.Buttons.RightStick == ButtonState.Released)
        {
            if(this.CurrentFlashLightStatus == FlashlightStatus.OFF)
            {
                this.CurrentFlashLightStatus = FlashlightStatus.ON;
            }
            else
            {
                this.CurrentFlashLightStatus = FlashlightStatus.OFF;
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlight()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateRunning(GamePadState current_)
    {
        if (current_.Triggers.Left > 0f)
        {
            this.IsRunning = true;
        }
        else
        {
            this.IsRunning = false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlightRotation(GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateFlashlightRotation(GamePadState current_)
    {
        this.CurrentFlashlightRotation = new Vector2(current_.ThumbSticks.Right.X, current_.ThumbSticks.Right.Y);

        float angleRadians = Mathf.Atan2(this.CurrentFlashlightRotation.x, this.CurrentFlashlightRotation.y);
        this.CurrentFlashlightDegrees = angleRadians * Mathf.Rad2Deg;
    }

    #endregion

    #region UPDATE PLAYER INPUT

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdatePlayerInput()
    ////////////////////////////////////////////////////////////////////*/
    void UpdatePlayerInput()
    {
        this.Current = new MyInput()
        {
            ThumbstickInput = this.CurrentThumbstickInput,
            JumpInput = this.IsJumping,
            FlashlightDegrees = (int)this.CurrentFlashlightDegrees,
            FlashlightActive = this.CurrentFlashLightStatus,
            RunInput = this.IsRunning
        };
    }

    #endregion

}
