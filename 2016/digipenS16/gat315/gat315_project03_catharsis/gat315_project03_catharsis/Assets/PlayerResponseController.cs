/*////////////////////////////////////////////////////////////////////////
//SCRIPT: PlayerResponseController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#


public class PlayerResponseController : MonoBehaviour
{
    #region PROPERTIES

    //references
    public DialoguePanelController s_DialoguePanelController;

    //attributes
    private int PlayerNumber = 0;
    PlayerIndex Player;
    GamePadState CurrentState;
    GamePadState PreviousState;

    public bool EnableGamepadPlayerResponse;


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;

        this.EnableGamepadPlayerResponse = false;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (this.EnableGamepadPlayerResponse)
        {
            //update the gamepad states every update
            this.UpdateGamepadStates(this.Player);

            this.UpdatePlayerResponse(this.CurrentState, this.PreviousState);
        }
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadStates(PlayerIndex)
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadStates(PlayerIndex player_)
    {
        this.PreviousState = this.CurrentState;
        this.CurrentState = GamePad.GetState(player_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SetDefaultButtonToSelect()
    ////////////////////////////////////////////////////////////////////*/
    public void SetDefaultButtonToSelect()
    {
        //this.DefaultSelectedButton.OnActivate();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateDialogueButton()
    ////////////////////////////////////////////////////////////////////*/
    void UpdatePlayerResponse(GamePadState current_, GamePadState previous_)
    {
        //pressing X
        if (current_.Buttons.X == ButtonState.Pressed && previous_.Buttons.X == ButtonState.Released)
        {
            this.s_DialoguePanelController.PlayerResponded("X");
            this.s_DialoguePanelController.HasPlayerResponded = true;
        }
        //pressing B
        if (current_.Buttons.B == ButtonState.Pressed && previous_.Buttons.B == ButtonState.Released)
        {
            this.s_DialoguePanelController.PlayerResponded("B");
            this.s_DialoguePanelController.HasPlayerResponded = true;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_03()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_03()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_04()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_04()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_05()
    {
        //CONTENT HERE
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}