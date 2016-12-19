using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

public enum GamePadButton { A, B, Back, Guide, LeftShoulder, LeftStick, RightShoulder, RightStick, Start, X, Y }
public enum DpadDirection { Up, Down, Left, Right }
public enum GamePadTrigger { Left, Right }
public enum GamePadStick { LeftVertical, RightVertical, LeftHorizontal, RightHorizontal }

public class GamePadInput : MonoBehaviour
{
    //REGULAR BUTTONS
    private static ButtonState[][] GamePadButtonStatesThisFrame = { new ButtonState[11], new ButtonState[11], new ButtonState[11], new ButtonState[11] };
    private static ButtonState[][] GamePadButtonStatesLastFrame = { new ButtonState[11], new ButtonState[11], new ButtonState[11], new ButtonState[11] };

    //DPAD DIRECTIONS
    private static ButtonState[][] DpadButtonStatesThisFrame = { new ButtonState[4], new ButtonState[4], new ButtonState[4], new ButtonState[4] };
    private static ButtonState[][] DpadButtonStatesLastFrame = { new ButtonState[4], new ButtonState[4], new ButtonState[4], new ButtonState[4] };

    //TRIGGERS
    private static float[][] GamePadTriggerStatesThisFrame = { new float[2], new float[2], new float[2], new float[2] };
    private static float[][] GamePadTriggerStatesLastFrame = { new float[2], new float[2], new float[2], new float[2] };

    //STICK 
    private static float[][] GamePadStickStatesThisFrame = { new float[4], new float[4], new float[4], new float[4] };
    private static float[][] GamePadStickStatesLastFrame = { new float[4], new float[4], new float[4], new float[4] };

    //GamePad States
    private static GamePadState[] States = new GamePadState[4];
    private static GamePadState[] StatesLastFrame = new GamePadState[4];

    void Awake()
    {
        this.Update();
    }

    // Update is called once per frame
    void Update()
    {
        //Loop through each player index and update all necessary info
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex currentPlayerIndex = (PlayerIndex)i;

            GamePadInput.StatesLastFrame[i] = GamePadInput.States[i];
            GamePadInput.States[i] = GamePad.GetState(currentPlayerIndex);

            GamePadInput.StoreButtonStates(i);
            GamePadInput.StoreDpadStates(i);
            GamePadInput.StoreTriggerStates(i);
            GamePadInput.StoreStickStates(i);
        }
    }

    //All overloades ways to check all types of INPUT STATES for a REGULAR BUTTON
    public static bool GetInputTriggered(int gamePadIndex, GamePadButton button)
    {
        return GamePadInput.GetInputState(gamePadIndex, button) == InputState.Triggered;
    }
    public static bool GetInputReleased(int gamePadIndex, GamePadButton button)
    {
        return GamePadInput.GetInputState(gamePadIndex, button) == InputState.Released;
    }
    public static bool GetInputInactive(int gamePadIndex, GamePadButton button)
    {
        return GamePadInput.GetInputState(gamePadIndex, button) == InputState.Inactive;
    }
    public static bool GetInputActive(int gamePadIndex, GamePadButton button)
    {
        return GamePadInput.GetInputState(gamePadIndex, button) == InputState.Active;
    }

    //All overloades ways to check all types of INPUT STATES for a DPAD DIRECTOIN
    public static bool GetInputTriggered(int gamePadIndex, DpadDirection direction)
    {
        return GamePadInput.GetInputState(gamePadIndex, direction) == InputState.Triggered;
    }
    public static bool GetInputReleased(int gamePadIndex, DpadDirection direction)
    {
        return GamePadInput.GetInputState(gamePadIndex, direction) == InputState.Released;
    }
    public static bool GetInputInactive(int gamePadIndex, DpadDirection direction)
    {
        return GamePadInput.GetInputState(gamePadIndex, direction) == InputState.Inactive;
    }
    public static bool GetInputActive(int gamePadIndex, DpadDirection direction)
    {
        return GamePadInput.GetInputState(gamePadIndex, direction) == InputState.Active;
    }

    //All overloades ways to check all types of INPUT STATES for a TRIGGER
    public static bool GetInputTriggered(int gamePadIndex, GamePadTrigger trigger, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, trigger, condition, value) == InputState.Triggered;
    }
    public static bool GetInputReleased(int gamePadIndex, GamePadTrigger trigger, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, trigger, condition, value) == InputState.Released;
    }
    public static bool GetInputInactive(int gamePadIndex, GamePadTrigger trigger, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, trigger, condition, value) == InputState.Inactive;
    }
    public static bool GetInputActive(int gamePadIndex, GamePadTrigger trigger, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, trigger, condition, value) == InputState.Active;
    }

    //All overloades ways to check all types of INPUT STATES for a STICK AXIS
    public static bool GetInputTriggered(int gamePadIndex, GamePadStick stickAxis, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, stickAxis, condition, value) == InputState.Triggered;
    }
    public static bool GetInputReleased(int gamePadIndex, GamePadStick stickAxis, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, stickAxis, condition, value) == InputState.Released;
    }
    public static bool GetInputInactive(int gamePadIndex, GamePadStick stickAxis, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, stickAxis, condition, value) == InputState.Inactive;
    }
    public static bool GetInputActive(int gamePadIndex, GamePadStick stickAxis, Conditional condition, float value)
    {
        return GamePadInput.GetInputState(gamePadIndex, stickAxis, condition, value) == InputState.Active;
    }

    //Gets an INPUT STATE from a REGULAR BUTTON
    public static InputState GetInputState(int gamePadIndex, GamePadButton button)
    {
        ButtonState buttonStateThisFrame = GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][(int)button];
        ButtonState buttonStateLastFrame = GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][(int)button];

        return GamePadInput.ToInputState(buttonStateThisFrame, buttonStateLastFrame);
    }

    //Gets a INPUT STATE from a DPAD DIRECTION
    public static InputState GetInputState(int gamePadIndex, DpadDirection direction)//Dpad
    {
        ButtonState buttonStateThisFrame = GamePadInput.DpadButtonStatesThisFrame[gamePadIndex][(int)direction];
        ButtonState buttonStateLastFrame = GamePadInput.DpadButtonStatesLastFrame[gamePadIndex][(int)direction];

        return GamePadInput.ToInputState(buttonStateThisFrame, buttonStateLastFrame);
    }

    //Get a STICK AXIS value as a INPUT STATE by passing a CONDITION
    public static InputState GetInputState(int gamePadIndex, GamePadStick stickAxis, Conditional condition, float value)//Stick
    {
        float stickStateThisFrame = GamePadInput.GamePadStickStatesThisFrame[gamePadIndex][(int)stickAxis];
        float stickStateLastFrame = GamePadInput.GamePadStickStatesLastFrame[gamePadIndex][(int)stickAxis];

        bool stickConditionMetThisFrame = GamePadInput.EvaluateFloatCondition(stickStateThisFrame, condition, value);
        bool stickConditionMetLastFrame = GamePadInput.EvaluateFloatCondition(stickStateLastFrame, condition, value);

        return GamePadInput.ToInputState(stickConditionMetThisFrame, stickConditionMetLastFrame);
    }

    //Gets a STICK AXIS value as a FLOAT (between -1 and 1)
    public static float GetInputValue(int gamePadIndex, GamePadStick stickAxis)
    {
        return GamePadInput.GamePadTriggerStatesThisFrame[gamePadIndex][(int)stickAxis];
    }

    //Get a TRIGGER value as a INPUT STATE by passing a CONDITION
    public static InputState GetInputState(int gamePadIndex, GamePadTrigger trigger, Conditional condition, float value)//Trigger
    {
        float triggerStateThisFrame = GamePadInput.GamePadTriggerStatesThisFrame[gamePadIndex][(int)trigger];
        float triggerStateLastFrame = GamePadInput.GamePadTriggerStatesLastFrame[gamePadIndex][(int)trigger];

        bool triggerConditionMetThisFrame = GamePadInput.EvaluateFloatCondition(triggerStateThisFrame, condition, value);
        bool triggerConditionMetLastFrame = GamePadInput.EvaluateFloatCondition(triggerStateLastFrame, condition, value);

        return GamePadInput.ToInputState(triggerConditionMetThisFrame, triggerConditionMetLastFrame);
    }

    //Get a TRIGGER value as a FLOAT (between 0 and 1)
    public static float GetInputValue(int gamePadIndex, GamePadTrigger trigger)
    {
        return GamePadInput.GamePadTriggerStatesThisFrame[gamePadIndex][(int)trigger];
    }

    //Evaluate a float condition using a conditional enum
    private static bool EvaluateFloatCondition(float leftValue, Conditional condition, float rightValue)
    {
        switch (condition)
        {
            case Conditional.Equal:
                return leftValue == rightValue;
            case Conditional.Greater:
                return leftValue > rightValue;
            case Conditional.Less:
                return leftValue < rightValue;
            case Conditional.GreaterOrEqual:
                return leftValue >= rightValue;
            case Conditional.LessOrEqual:
                return leftValue <= rightValue;
        }

        return false;
    }

    //Get an input state with pressed/released information from a regular Button state
    private static InputState ToInputState(ButtonState stateThisFrame, ButtonState stateLastFrame)
    {
        //If we had the button up last frame and this frame it is down, we just "Pressed" the button
        if (stateLastFrame == ButtonState.Released && stateThisFrame == ButtonState.Pressed)
            return InputState.Triggered;

        //If we had the button down last frame and this frame it is up, we just "Released" the button
        else if (stateLastFrame == ButtonState.Pressed && stateThisFrame == ButtonState.Released)
            return InputState.Released;

        //Otherwise, if the normal button state is "Pressed", the button is up
        else if (stateThisFrame == ButtonState.Pressed)
            return InputState.Active;

        //Otherwise the button is not in use at all
        else
            return InputState.Inactive;
    }

    //Get an input state with pressed/released information from a boolean value
    private static InputState ToInputState(bool stateThisFrame, bool stateLastFrame)
    {
        //If we had the button up last frame and this frame it is down, we just "Pressed" the button
        if (!stateLastFrame && stateThisFrame)
            return InputState.Triggered;

        //If we had the button down last frame and this frame it is up, we just "Released" the button
        else if (stateLastFrame && !stateThisFrame)
            return InputState.Released;

        //Otherwise, if the normal button state is "Pressed", the button is active
        else if (stateThisFrame)
            return InputState.Active;

        //Otherwise the button is not in use at all
        else
            return InputState.Inactive;
    }

    /*************************************************************************************************************/
    /*       Below are functions which store values for various types of buttons and axes on the gamepad         */

    private static void StoreButtonStates(int gamePadIndex)
    {
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][0] = GamePadInput.States[gamePadIndex].Buttons.A;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][1] = GamePadInput.States[gamePadIndex].Buttons.B;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][2] = GamePadInput.States[gamePadIndex].Buttons.Back;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][3] = GamePadInput.States[gamePadIndex].Buttons.Guide;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][4] = GamePadInput.States[gamePadIndex].Buttons.LeftShoulder;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][5] = GamePadInput.States[gamePadIndex].Buttons.LeftStick;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][6] = GamePadInput.States[gamePadIndex].Buttons.RightShoulder;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][7] = GamePadInput.States[gamePadIndex].Buttons.RightStick;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][8] = GamePadInput.States[gamePadIndex].Buttons.Start;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][9] = GamePadInput.States[gamePadIndex].Buttons.X;
        GamePadInput.GamePadButtonStatesThisFrame[gamePadIndex][10] = GamePadInput.States[gamePadIndex].Buttons.Y;

        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][0] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.A;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][1] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.B;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][2] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.Back;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][3] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.Guide;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][4] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.LeftShoulder;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][5] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.LeftStick;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][6] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.RightShoulder;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][7] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.RightStick;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][8] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.Start;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][9] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.X;
        GamePadInput.GamePadButtonStatesLastFrame[gamePadIndex][10] = GamePadInput.StatesLastFrame[gamePadIndex].Buttons.Y;
    }

    private static void StoreDpadStates(int gamePadIndex)
    {
        GamePadInput.DpadButtonStatesThisFrame[gamePadIndex][0] = GamePadInput.States[gamePadIndex].DPad.Up;
        GamePadInput.DpadButtonStatesThisFrame[gamePadIndex][1] = GamePadInput.States[gamePadIndex].DPad.Down;
        GamePadInput.DpadButtonStatesThisFrame[gamePadIndex][2] = GamePadInput.States[gamePadIndex].DPad.Left;
        GamePadInput.DpadButtonStatesThisFrame[gamePadIndex][3] = GamePadInput.States[gamePadIndex].DPad.Right;

        GamePadInput.DpadButtonStatesLastFrame[gamePadIndex][0] = GamePadInput.StatesLastFrame[gamePadIndex].DPad.Up;
        GamePadInput.DpadButtonStatesLastFrame[gamePadIndex][1] = GamePadInput.StatesLastFrame[gamePadIndex].DPad.Down;
        GamePadInput.DpadButtonStatesLastFrame[gamePadIndex][2] = GamePadInput.StatesLastFrame[gamePadIndex].DPad.Left;
        GamePadInput.DpadButtonStatesLastFrame[gamePadIndex][3] = GamePadInput.StatesLastFrame[gamePadIndex].DPad.Right;
    }

    private static void StoreTriggerStates(int gamePadIndex)
    {
        GamePadInput.GamePadTriggerStatesThisFrame[gamePadIndex][0] = GamePadInput.States[gamePadIndex].Triggers.Left;
        GamePadInput.GamePadTriggerStatesThisFrame[gamePadIndex][1] = GamePadInput.States[gamePadIndex].Triggers.Right;

        GamePadInput.GamePadTriggerStatesLastFrame[gamePadIndex][0] = GamePadInput.StatesLastFrame[gamePadIndex].Triggers.Left;
        GamePadInput.GamePadTriggerStatesLastFrame[gamePadIndex][1] = GamePadInput.StatesLastFrame[gamePadIndex].Triggers.Right;
    }

    private static void StoreStickStates(int gamePadIndex)
    {
        GamePadInput.GamePadStickStatesThisFrame[gamePadIndex][0] = GamePadInput.States[gamePadIndex].ThumbSticks.Left.Y;
        GamePadInput.GamePadStickStatesThisFrame[gamePadIndex][1] = GamePadInput.States[gamePadIndex].ThumbSticks.Right.Y;
        GamePadInput.GamePadStickStatesThisFrame[gamePadIndex][2] = GamePadInput.States[gamePadIndex].ThumbSticks.Left.X;
        GamePadInput.GamePadStickStatesThisFrame[gamePadIndex][3] = GamePadInput.States[gamePadIndex].ThumbSticks.Right.X;

        GamePadInput.GamePadStickStatesLastFrame[gamePadIndex][0] = GamePadInput.StatesLastFrame[gamePadIndex].ThumbSticks.Left.Y;
        GamePadInput.GamePadStickStatesLastFrame[gamePadIndex][1] = GamePadInput.StatesLastFrame[gamePadIndex].ThumbSticks.Right.Y;
        GamePadInput.GamePadStickStatesLastFrame[gamePadIndex][2] = GamePadInput.StatesLastFrame[gamePadIndex].ThumbSticks.Left.X;
        GamePadInput.GamePadStickStatesLastFrame[gamePadIndex][3] = GamePadInput.StatesLastFrame[gamePadIndex].ThumbSticks.Right.X;
    }
}