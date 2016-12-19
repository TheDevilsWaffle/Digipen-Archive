using UnityEngine;
using System.Collections;

public enum InputState { Active, Inactive, Triggered, Released }
public enum Conditional { Greater, Less, GreaterOrEqual, LessOrEqual, Equal }

/* Put enums for actions here! */

public enum InputActions
{
    ReleasedUp,
    TriggeredRightOnDpad,
    HeldRightOnRightStick
};

/* Put enums for actions here! */

public delegate void MethodDelegate();
public class InputSystem : MonoBehaviour
{
    private MethodDelegate SubHandler;

    private static InputAction[] InputActions;
    private static MethodDelegate[][] GamePadMethodDelegates = new MethodDelegate[4][];
    private static MethodDelegate[] KeyboardMethodDelegates;

    // Use InputSystem for initialization
    void Awake ()
    {
        InputSystem.InputActions = this.GetComponents<InputAction>();
        InputSystem.KeyboardMethodDelegates = new MethodDelegate[InputSystem.InputActions.Length];
        for (int i = 0; i < InputSystem.KeyboardMethodDelegates.Length; ++i)
        {
            InputSystem.KeyboardMethodDelegates[i] = delegate { };
        }

        for (int i = 0; i < 4; ++i)
        {
            InputSystem.GamePadMethodDelegates[i] = new MethodDelegate[InputSystem.InputActions.Length];
            for (int j = 0; j < InputSystem.GamePadMethodDelegates[i].Length; ++j)
            {
                InputSystem.GamePadMethodDelegates[i][j] = delegate { };
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        int inputActionIndex = 0;
	    foreach(InputAction inputAction in InputSystem.InputActions)
        {
            //if (inputAction.EvaluateKeyboard())
            //    InputSystem.KeyboardMethodDelegates[inputActionIndex].Invoke();

            for (int i = 0; i < 4; ++i)
            {
                if (inputAction.EvaluateGamePad(i))
                    InputSystem.GamePadMethodDelegates[i][inputActionIndex].Invoke();
            }

            ++inputActionIndex;
        }
    }

    public static void Subscribe(InputActions inputAction, MethodDelegate methodToCall)
    {
        for (int i = 0; i < 4; ++i)
        {
            InputSystem.GamePadMethodDelegates[i][(int)inputAction] += methodToCall;
        }

        InputSystem.KeyboardMethodDelegates[(int)inputAction] += methodToCall;
    }

    public static void Subscribe(int gamePadIndex, InputActions inputAction, MethodDelegate methodToCall)
    {
        InputSystem.GamePadMethodDelegates[gamePadIndex][(int)inputAction] += methodToCall;
        InputSystem.KeyboardMethodDelegates[(int)inputAction] += methodToCall;
    }

    public static void Unsubscribe(InputActions inputAction, MethodDelegate methodToCall)
    {
        for (int i = 0; i < 4; ++i)
        {
            InputSystem.GamePadMethodDelegates[i][(int)inputAction] -= methodToCall;
        }

        InputSystem.KeyboardMethodDelegates[(int)inputAction] -= methodToCall;
    }

    public static void Unsubscribe(int gamePadIndex, InputActions inputAction, MethodDelegate methodToCall)
    {
        InputSystem.GamePadMethodDelegates[gamePadIndex][(int)inputAction] -= methodToCall;
        InputSystem.KeyboardMethodDelegates[(int)inputAction] -= methodToCall;
    }
}
