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
}

#endregion

public class MyPlatformerController : MonoBehaviour
{
    #region PROPERTIES
    [SerializeField]
    [Range(0, 3)]
    int PlayerNumber = 0;
    PlayerIndex Player;
    GamePadState CurrentState;
    GamePadState PreviousState;
    public MyInput Current;
    Vector2 CurrentThumbstickInput;
    [HideInInspector]
    public bool IsByNPC;
    [HideInInspector]
    public DialogueContainer NPCDialogueContainer;
    [HideInInspector]
    public bool PlayerIsSpeaking;
    public DialoguePanelController s_DialoguePanelController;
    public bool PlayerCanMove;

    #endregion

    #region INITIALIZE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {

        //set PlayerIndex = PlayerNumber
        this.Player = (PlayerIndex)this.PlayerNumber;

        //set bools
        this.PlayerIsSpeaking = false;
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

        if (!this.PlayerIsSpeaking && this.PlayerCanMove)
            this.UpdateThumbstickInput(this.CurrentState);
        else
            this.CurrentThumbstickInput = new Vector2(0f, 0f);

        this.UpdateDialogueButton(this.CurrentState, this.PreviousState);

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
    //FUNCTION: UpdateDialogueButton()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateDialogueButton(GamePadState current_, GamePadState previous_)
    {
        //pressing A
        if (current_.Buttons.A == ButtonState.Pressed && previous_.Buttons.A == ButtonState.Released)
        {
            //if already speaking
            if(this.IsByNPC && this.PlayerIsSpeaking)
            {
                //print("ADVANCING TEXT!");
                this.s_DialoguePanelController.AdvanceText();
            }

            //else start speaking
            else if(this.IsByNPC)
            {
                //print("TALKING TO " + NPCDialogueContainer.gameObject.name);
                this.PlayerIsSpeaking = true;
                this.NPCDialogueContainer.SendDialogue();
            }
        }
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
            ThumbstickInput = this.CurrentThumbstickInput
        };
    }

    #endregion

}
