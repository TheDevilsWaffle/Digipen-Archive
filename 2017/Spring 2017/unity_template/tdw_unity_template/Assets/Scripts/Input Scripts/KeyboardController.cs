///////////////////////////////////////////////////////////////////////////////////////////////////
//SCRIPT: KeyboardController.cs
//AUTHOR: Travis Moore
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public struct PlayerInput
{
    public Vector2 direction;
}

public class KeyboardController : MonoBehaviour
{

    #region REFERENCES/PROPERTIES

    [HideInInspector]
    public PlayerInput currentPlayerInput;

    [Header("MOVEMENT DIRECTION")]
    public KeyCode MoveUp = KeyCode.W;
    Vector2 up = Vector2.up;
    public KeyCode MoveRight = KeyCode.D;
    Vector2 right = Vector2.right;
    public KeyCode MoveDown = KeyCode.S;
    Vector2 down = Vector2.down;
    public KeyCode MoveLeft = KeyCode.A;
    Vector2 left = Vector2.left;
    
    [Header("ACTION 01")]
    public KeyCode Action01;

    [Header("GAME START")]
    public Vector2 initialDirection = new Vector2(0f, 1f);

    #endregion

    #region INITIALIZATION

    //////////////////////////////////////////////////////////////////////////////////////////////// 
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //set an initial direction for the player to start heading in
        currentPlayerInput.direction = initialDirection;   
	}

    #endregion

    #region UPDATE

    /////////////////////////////////////////////////////////////////////////////////////////////// 
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        //update currentPlayerInput.direction to take one direction at a time
        UpdateDirection();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////// 
    /// <summary>
    /// UpdateDirection()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateDirection()
    {
        //move up
        if(Input.GetKeyDown(MoveUp))
            currentPlayerInput.direction = up;

        //move right
        else if (Input.GetKeyDown(MoveRight))
            currentPlayerInput.direction = right;

        //move down
        else if (Input.GetKeyDown(MoveDown))
            currentPlayerInput.direction = down;

        //move left
        else if (Input.GetKeyDown(MoveLeft))
            currentPlayerInput.direction = left;
    }

    #endregion

}
