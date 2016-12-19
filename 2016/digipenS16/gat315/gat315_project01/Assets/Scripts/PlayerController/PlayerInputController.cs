/*////////////////////////////////////////////////////////////////////////
//SCRIPT: PlayerInputController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public struct PlayerInput
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
}

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    int PlayerNumber = 0;
    [SerializeField]
    GameObject OtherPlayer;
    PlayerIndex Player;
    GamePadState CurrentState;
    GamePadState PreviousState;
    bool IsJumping;

    public PlayerInput Current;
    Vector3 CurrentPlayerMovement;
    Vector2 CurrentPlayerMouseLook;

    //SCRIPTS
    Mass PlayerMassScript;
    Mass OtherPlayerMassScript;

    //SFX
    public AudioClip SFX_SmallGooJump;
    public AudioClip SFX_LargeGooJump;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;

        //get mass components
        this.PlayerMassScript = this.gameObject.GetComponent<Mass>();
        this.OtherPlayerMassScript = this.OtherPlayer.GetComponent<Mass>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update the gamepad states every update
        this.UpdateGamepadStates(this.Player);

        this.UpdateMovement(this.CurrentState);
        this.UpdateMouseLook(this.CurrentState);
        this.UpdateTransferMass(this.CurrentState, this.PreviousState);

        //rest IsJumping before evaluating
        this.IsJumping = false;
        this.UpdateJump(this.CurrentState, this.PreviousState);

        this.UpdatePlayerInput();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadStates(PlayerIndex)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadStates(PlayerIndex player_)
    {
        this.PreviousState = this.CurrentState;
        this.CurrentState = GamePad.GetState(player_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateMovement(GamePadState,)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateMovement(GamePadState current_)
    {
        //GAMEPAD SUPPORT
        this.CurrentPlayerMovement = new Vector3(0f, 
                                                 0f,
                                                 current_.ThumbSticks.Left.Y);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateMouseLook(GamePadState,)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateMouseLook(GamePadState current_)
    {
        //GAMEPAD SUPPORT
        this.CurrentPlayerMouseLook = new Vector2(current_.ThumbSticks.Left.X, 0f);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateJump(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateJump(GamePadState current_, GamePadState previous_)
    {
        if(current_.Buttons.A == ButtonState.Pressed && previous_.Buttons.A == ButtonState.Released)
        {
            this.IsJumping = true;

            if(this.gameObject.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.SMALL)
            {
                this.gameObject.GetComponent<SoundManager>().RandomizeSfx(this.SFX_SmallGooJump);
            }
            else
            {
                this.gameObject.GetComponent<SoundManager>().RandomizeSfx(this.SFX_LargeGooJump);
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateTransferMass(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateTransferMass(GamePadState current_, GamePadState previous_)
    {
        if (current_.Buttons.LeftShoulder == ButtonState.Pressed && previous_.Buttons.LeftShoulder == ButtonState.Released)
        {
            if (this.gameObject.GetComponent<Mass>().PlayerCurrentSize != PlayerSize.SMALL)
            {
                //change the size of this player
                this.PlayerMassScript.DecreasePlayerSize();
                this.OtherPlayerMassScript.IncreasePlayerSize();
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdatePlayerInput()
    ////////////////////////////////////////////////////////////////////*/
    void UpdatePlayerInput()
    {
        this.Current = new PlayerInput()
        {
            MoveInput = this.CurrentPlayerMovement,
            MouseInput = this.CurrentPlayerMouseLook,
            JumpInput = this.IsJumping
        };
    }
}
