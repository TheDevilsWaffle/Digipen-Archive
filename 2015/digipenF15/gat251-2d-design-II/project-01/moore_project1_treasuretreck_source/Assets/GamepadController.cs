using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
    //ENUM
    private enum ButtonState
    {
        up, down, over
    }
    [SerializeField]
    float GamepadP1Axis;

    //Change these to match what you've defined in InputManager.
    private const string SELECT_AXIS = "DPadY";
    private const string SELECT_BUTTON = "Fire1";
    private const string BACK_BUTTON = "Fire2";
    //Constants for drawing menu options.
    private const float RECT_CY = 30f;
    private const float RECT_CX = 220f;
    private const float TEXT_INDENT_CX = 50f;
    //Input freeze intervals to help the menu control work intuitively.
    private const float BUTTON_FREEZE_DELAY = .1f;
    private const float AXIS_FREEZE_DELAY = .2f;
    private float noInputUntil = -1f;
    // Use this for initialization
    void Start ()
    {
        this.GamepadP1Axis = Input.GetAxis("DpadY");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(this.GamepadP1Axis);
	}
}
