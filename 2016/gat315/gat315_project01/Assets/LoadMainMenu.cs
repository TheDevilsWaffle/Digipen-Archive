/*////////////////////////////////////////////////////////////////////////
//SCRIPT: LoadMainMenu.cs
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

public class LoadMainMenu : MonoBehaviour
{
    int PlayerNumber = 0;
    PlayerIndex Player;
    GamePadState CurrentState;
    GamePadState PreviousState;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update the gamepad states every update
        this.UpdateGamepadStates(this.Player);
        this.UpdateLoadMainMenuButton(this.CurrentState, this.PreviousState);
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
    //FUNCTION: UpdateLoadMainMenuButton(GamePadState, GamePadState)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateLoadMainMenuButton(GamePadState current_, GamePadState previous_)
    {
        if(current_.Buttons.Start == ButtonState.Pressed 
            && previous_.Buttons.Start == ButtonState.Released)
        {
            SceneManager.LoadScene("sce_mainMenu");
        }
    }
}