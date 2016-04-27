using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class PlayerInputController : MonoBehaviour {


    public PlayerInput Current;
    [SerializeField]
    int PlayerNumber = 0;
    [SerializeField]
    GameObject Shield;
    [SerializeField]
    GameObject Gun;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public bool IsStunned = false;
    float StunTimer = 0f;
    [SerializeField]
    float StunThreshold = 2f;
    // Use this for initialization
    void Start () {
        Current = new PlayerInput();

        //connecting the gamepad
        this.playerIndex = (PlayerIndex)this.PlayerNumber;
        GamePad.GetState(this.playerIndex);
        prevState = state;
        state = GamePad.GetState(playerIndex);
        //Debug.Log(string.Format("Gamepad found at {0}", this.playerIndex));

        if(this.Shield == null)
        {
            this.Shield = this.gameObject.transform.FindChild("Shield").gameObject;
        }
        if (this.Gun == null)
        {
            this.Gun = this.gameObject.transform.FindChild("Gun").gameObject;
        }

        this.Gun.GetComponent<FireGun>().Gamepad = state;
        this.Gun.GetComponent<FireGun>().playerIndex = this.playerIndex;

        //start off not stunned
        this.IsStunned = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!this.IsStunned)
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);

            // Retrieve our current WASD or Arrow Key input
            // Using GetAxisRaw removes any kind of gravity or filtering being applied to the input
            // Ensuring that we are getting either -1, 0 or 1
            Vector3 moveInput = new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y);

            Vector2 mouseInput = new Vector2(state.ThumbSticks.Right.X * 2.25f, state.ThumbSticks.Right.Y);

            bool jumpInput = false;
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                jumpInput = true;
            }
            //SHIELD
            if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
            {
                this.Shield.GetComponent<ShieldController>().UseShield = true;
            }

            Current = new PlayerInput()
            {
                MoveInput = moveInput,
                MouseInput = mouseInput,
                JumpInput = jumpInput
            };
        }
        else
        {
            this.StunTimer += Time.deltaTime;
            if(this.StunTimer >= this.StunThreshold)
            {
                this.IsStunned = false;
            }
        }
	}
}

public struct PlayerInput
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
}
