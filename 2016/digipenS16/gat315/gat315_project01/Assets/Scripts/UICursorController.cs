/*////////////////////////////////////////////////////////////////////////
//SCRIPT: UICursorController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2015 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure; // Required in C# for GamePad

public class UICursorController : MonoBehaviour
{
    //PROPERTIES
    bool IsPlayerIndexSet;
    PlayerIndex PlayerIndex;
    GamePadState CurrentState;
    GamePadState PreviousState;
    float CursorX;
    float CursorY;
    [SerializeField]
    float Speed = 100.0f;
    GameObject MenuCanvas;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure IsPlayerIndexSet is false to begin with
        this.IsPlayerIndexSet = false;
        this.MenuCanvas = GameObject.Find("MenuCanvas").gameObject;
        this.AssignGamepad();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //if gamepad is connected, use it
        if(IsPlayerIndexSet)
        {
            //keeping track of the gamepad
            this.UpdateGamepadStates();

            //update Cursor axis
            this.CursorX = this.UpdateCursorValue("x");
            this.CursorY = this.UpdateCursorValue("y");

            //update position of cursor gameobject
            this.gameObject.transform.position += new Vector3(this.CursorX, this.CursorY, 0.0f);
        }
        //otherwise, use mouse
        else
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.MenuCanvas.transform as RectTransform, Input.mousePosition, Camera.main, out mousePos);
            this.gameObject.transform.position = this.MenuCanvas.transform.TransformPoint(mousePos);

            //DEBUG - TRACK CURSOR POSITION
            //Debug.Log(this.gameObject.transform.position);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AssignGamepad()
    ////////////////////////////////////////////////////////////////////*/
    void AssignGamepad()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!this.IsPlayerIndexSet || !this.PreviousState.IsConnected)
        {
            for (int index = 0; index < 4; ++index)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)index;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    this.PlayerIndex = testPlayerIndex;
                    this.IsPlayerIndexSet = true;
                }
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateGamepadStates()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateGamepadStates()
    {
        this.PreviousState = this.CurrentState;
        this.CurrentState = GamePad.GetState(this.PlayerIndex);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateCursorValue(string)
    ////////////////////////////////////////////////////////////////////*/
    float UpdateCursorValue(string axis_)
    {
        switch(axis_)
        {
            case "x":
                return this.CurrentState.ThumbSticks.Left.X * this.Speed * Time.deltaTime;
            case "y":
                return this.CurrentState.ThumbSticks.Left.Y * this.Speed * Time.deltaTime;
            default:
                Debug.LogError("ERROR! wrong axis given, please use 'x' or 'y'");
                return 0;
        }
    }
}
