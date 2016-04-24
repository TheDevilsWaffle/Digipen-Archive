/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MenuInput.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

using System.Diagnostics;

#region ENUM — MenuInputActions

internal enum MenuInputActions { MoveUp,
                                 MoveDown,
                                 MoveLeft,
                                 MoveRight,
                                 Select,
                                 Cancel,
                                 Pause }

#endregion

public class MenuInput : MonoBehaviour
{
    [HideInInspector]
    public bool Active = true;

    private float[] Timers = new float[7];

    internal delegate void MenuDelegate();
    internal MenuDelegate[] MenuDelegates = {delegate { }, delegate { }, delegate { }, delegate { }, delegate { }, delegate { }, delegate { }};

    [SerializeField]
    private KeyCode[] UpKeys = { KeyCode.UpArrow };
    [SerializeField]
    private KeyCode[] DownKeys = { KeyCode.DownArrow };
    [SerializeField]
    private KeyCode[] LeftKeys = { KeyCode.LeftArrow };
    [SerializeField]
    private KeyCode[] RightKeys = { KeyCode.RightArrow };

    [SerializeField]
    private KeyCode[] SelectKeys = { KeyCode.Return };
    [SerializeField]
    private KeyCode[] CancelKeys = { KeyCode.Backspace };
    [SerializeField]
    private KeyCode[] PauseKeys = { KeyCode.Escape };

    [SerializeField]
    private float InputDelay;

    [SerializeField]
    [Range(0,1)]
    private float GamePadStickThreshold;

    // Use this for initialization
    void Start()
    {
        this.Active = true;
    }

    internal void Subscribe(MenuInputActions inputAction, MenuDelegate menuDelegate)
    {
        this.MenuDelegates[(int)inputAction] += menuDelegate;
    }
    internal void Unsubscribe(MenuInputActions inputAction, MenuDelegate menuDelegate)
    {
        this.MenuDelegates[(int)inputAction] -= menuDelegate;
    }

    // Update is called once per frame
    void Update()
    {
        //Update all the timers
        for (int i = 0; i < 7; ++i)
            this.Timers[i] += Time.unscaledDeltaTime;

        //Loop through each player and update all necessary info
        for (int i = 0; i < 4; ++i)
        {
            this.EvaluateGamePad(i);
        }

        this.EvaluateKeys();
    }

    private void EvaluateGamePad(int gamePadIndex)
    {
        this.InvokeOnButtonState(gamePadIndex, DpadDirection.Up, MenuInputActions.MoveUp);
        this.InvokeOnButtonState(gamePadIndex, DpadDirection.Down, MenuInputActions.MoveDown);
        this.InvokeOnButtonState(gamePadIndex, DpadDirection.Left, MenuInputActions.MoveLeft);
        this.InvokeOnButtonState(gamePadIndex, DpadDirection.Right, MenuInputActions.MoveRight);

        this.InvokeOnButtonState(gamePadIndex, GamePadStick.LeftVertical, Conditional.Greater, this.GamePadStickThreshold, MenuInputActions.MoveUp);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.LeftVertical, Conditional.Less, -this.GamePadStickThreshold, MenuInputActions.MoveDown);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.RightVertical, Conditional.Greater, this.GamePadStickThreshold, MenuInputActions.MoveUp);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.RightVertical, Conditional.Less, -this.GamePadStickThreshold, MenuInputActions.MoveDown);

        this.InvokeOnButtonState(gamePadIndex, GamePadStick.LeftHorizontal, Conditional.Greater, this.GamePadStickThreshold, MenuInputActions.MoveRight);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.LeftHorizontal, Conditional.Less, -this.GamePadStickThreshold, MenuInputActions.MoveLeft);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.RightHorizontal, Conditional.Greater, this.GamePadStickThreshold, MenuInputActions.MoveRight);
        this.InvokeOnButtonState(gamePadIndex, GamePadStick.RightHorizontal, Conditional.Less, -this.GamePadStickThreshold, MenuInputActions.MoveLeft);

        this.InvokeOnButtonState(gamePadIndex, GamePadButton.A, MenuInputActions.Select);
        this.InvokeOnButtonState(gamePadIndex, GamePadButton.B, MenuInputActions.Cancel);
        this.InvokeOnButtonState(gamePadIndex, GamePadButton.Start, MenuInputActions.Pause);
    }

    private void EvaluateKeys()
    {
        foreach (KeyCode key in this.UpKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.MoveUp);
        foreach (KeyCode key in this.DownKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.MoveDown);
        foreach (KeyCode key in this.RightKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.MoveRight);
        foreach (KeyCode key in this.LeftKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.MoveLeft);

        foreach (KeyCode key in this.SelectKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.Select);
        foreach (KeyCode key in this.CancelKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.Cancel);
        foreach (KeyCode key in this.PauseKeys)
            this.InvokeOnKeyActive(key, MenuInputActions.Pause);
    }

    private void InvokeOnButtonState(int gamePadIndex, DpadDirection direction, MenuInputActions menuAction, InputState inputState = InputState.Active)
    {
        //If the input state is not what it should be OR we haven't reached input delay for this action, don't do anything
        if (GamePadInput.GetInputState(gamePadIndex, direction) != inputState || this.Timers[(int)menuAction] <= this.InputDelay)
            return;

        //Reset the input timer for this action
        this.Timers[(int)menuAction] = 0;

        //Involk the menu action delegate
        this.MenuDelegates[(int)menuAction]();
    }

    private void InvokeOnButtonState(int gamePadIndex, GamePadButton button, MenuInputActions menuAction, InputState inputState = InputState.Active)
    {
        //If the input state is not what it should be OR we haven't reached input delay for this action, don't do anything
        if (GamePadInput.GetInputState(gamePadIndex, button) != inputState || this.Timers[(int)menuAction] <= this.InputDelay)
            return;

        //Reset the input timer for this action
        this.Timers[(int)menuAction] = 0;

        //Involk the menu action delegate
        this.MenuDelegates[(int)menuAction]();
    }

    private void InvokeOnButtonState(int gamePadIndex, GamePadStick axis, Conditional condition, float threshold, MenuInputActions menuAction, InputState inputState = InputState.Active)
    {
        //print(GamePadInput.GetInputState(gamePadIndex, axis, condition, threshold));

        //If the input state is not what it should be OR we haven't reached input delay for this action, don't do anything
        if (GamePadInput.GetInputState(gamePadIndex, axis, condition, threshold) != inputState || this.Timers[(int)menuAction] <= this.InputDelay)
            return;

        //print("Got to stick!");
        

        //Reset the input timer for this action
        this.Timers[(int)menuAction] = 0;

        //Involk the menu action delegate
        this.MenuDelegates[(int)menuAction]();
    }

    private void InvokeOnKeyActive(KeyCode key, MenuInputActions menuAction)
    {
       
        //If the input state is not what it should be OR we haven't reached input delay for this action, don't do anything
        if (!Input.GetKey(key) || this.Timers[(int)menuAction] <= this.InputDelay)
            return;

        //Reset the input timer for this action
        this.Timers[(int)menuAction] = 0;

        //print(this.MenuDelegates[(int)menuAction].GetInvocationList());

        //Involk the menu action delegate
        this.MenuDelegates[(int)menuAction]();
    }
}
