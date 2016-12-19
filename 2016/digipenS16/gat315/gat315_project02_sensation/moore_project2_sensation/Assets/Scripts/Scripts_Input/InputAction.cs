using UnityEngine;
using System.Collections;

public class InputAction : MonoBehaviour
{
    [SerializeField]
    private string Name;

    [SerializeField]
    private KeyCode KeyboardInputCode;
    [SerializeField]
    private GamePadCode GamepadInputMethod;

    private bool KeyLastFrame;
    private bool KeyThisFrame;

    [SerializeField]
    private InputState StateToSendOn;

    [SerializeField]
    [Tooltip("Used to compare trigger and stick axis values in order to evaluate the input state")]
    private Conditional CompareConditional;

    [SerializeField][Range(-1,1)]
    [Tooltip("Used to compare trigger and stick axis values in order to evaluate the input state")]
    private float CompareValue;

    private enum GamePadCode
    {
        A, B, Back, Guide, LeftShoulder, LeftStickButton, RightShoulder, RightStickButton, Start, X, Y,
        DpadUp, DpadDown, DpadLeft, DpadRight, LeftTrigger, RightTrigger, LeftVerticalStick,
        RightVerticalStick, LeftHorizontalStick, RightHorizontalStick
    };

    internal bool EvaluateKeyboard()
    {
        this.KeyLastFrame = this.KeyThisFrame;
        this.KeyThisFrame = Input.GetKey(this.KeyboardInputCode);
        InputState keyState = this.ToInputState(this.KeyThisFrame, this.KeyLastFrame);

        return keyState == this.StateToSendOn;
    }

    internal bool EvaluateGamePad(int playerIndex)
    {
        InputState gamePadState = InputState.Inactive;

        if ((int)this.GamepadInputMethod < 11)
            gamePadState = GamePadInput.GetInputState(playerIndex, (GamePadButton)this.GamepadInputMethod);
        else if ((int)this.GamepadInputMethod < 15)
            gamePadState = GamePadInput.GetInputState(playerIndex, (DpadDirection)((int)(this.GamepadInputMethod - 11)));
        else if ((int)this.GamepadInputMethod < 17)
            gamePadState = GamePadInput.GetInputState(playerIndex, (GamePadTrigger)((int)(this.GamepadInputMethod - 15)), this.CompareConditional, this.CompareValue);
        else
            gamePadState = GamePadInput.GetInputState(playerIndex, (GamePadStick)((int)(this.GamepadInputMethod - 17)), this.CompareConditional, this.CompareValue);

        return gamePadState == this.StateToSendOn;
    }

    //Get an input state with pressed/released information from a boolean value
    private InputState ToInputState(bool stateThisFrame, bool stateLastFrame)
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

    // Update is called once per frame
    void Update ()
    {
	    
	}
}
