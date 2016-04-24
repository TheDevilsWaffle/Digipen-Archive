/*////////////////////////////////////////////////////////////////////////
//SCRIPT: CheatCodes.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    #region PROPERTIES

    int PlayerNumber = 0;
    PlayerIndex Player;

    [SerializeField]
    LevelController LevelController;
    GamePadState CurrentState;
    GamePadState PreviousState;

    #endregion

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;
    }

    #region GAMEPAD UPDATE LOOP

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update the gamepad states every update
        this.UpdateGamepadStates(this.Player);
        this.UpdateGamepadButtons(this.CurrentState, this.PreviousState);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadStates(PlayerIndex)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadStates(PlayerIndex player_)
    {
        this.PreviousState = this.CurrentState;
        this.CurrentState = GamePad.GetState(player_);
    }

    #endregion

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadButtons(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadButtons(GamePadState current_, GamePadState previous_)
    {
        //LEFT DPAD = previous level
        if (current_.DPad.Left == ButtonState.Pressed
            && previous_.DPad.Left == ButtonState.Released)
        {
            Debug.Log("LOADING PREVIOUS LEVEL!");
            this.LevelController.FadeOutToPreviousLevel();
        }

        //RIGHT DPAD = next level
        if (current_.DPad.Right == ButtonState.Pressed
            && previous_.DPad.Right == ButtonState.Released)
        {
            Debug.Log("LOADING NEXT LEVEL!");
            this.LevelController.FadeOutToNextLevel();
        }

        //DOWN DPAD = Restart level
        if (current_.DPad.Down == ButtonState.Pressed
            && previous_.DPad.Down == ButtonState.Released)
        {
            Debug.Log("RELOADING LEVEL!");
            this.LevelController.FadeOutRestart();
        }
    }
}