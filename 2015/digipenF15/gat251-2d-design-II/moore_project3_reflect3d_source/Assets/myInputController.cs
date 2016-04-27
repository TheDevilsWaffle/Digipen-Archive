using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class myInputController : MonoBehaviour
{
    [SerializeField]
    int PlayerNumber = 0;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start ()
    {
        this.playerIndex = (PlayerIndex)this.PlayerNumber;
        GamePad.GetState(this.playerIndex);
        prevState = state;
        state = GamePad.GetState(playerIndex);
        Debug.Log(string.Format("Gamepad found at {0}", this.playerIndex));
    }
	
	// Update is called once per frame
	void Update ()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            Debug.Log("A was pressed");
        }
    }
}
