/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Simple2DCharacterController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System;
using UnityEngine;

public class Simple2DCharacterController : MonoBehaviour
{
    [SerializeField]
    private float xAxisMoveSpeed = 10f; // The fastest the player can travel in the x axis.
    [SerializeField]
    private float yAxisMoveSpeed = 5f;

    private Rigidbody2D m_Rigidbody;

    private bool FacingRight = true;  // For determining which way the player is currently facing.
    private SpriteRenderer CharacterSprite;
    private MyInput CurrentInput;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    private void Awake()
    {
        // Setting up references.
        this.m_Rigidbody = GetComponent<Rigidbody2D>();
        this.CharacterSprite = GetComponent<SpriteRenderer>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FixedUpdate() <-- better physics
    ////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //first things first, get the most current input
        this.CurrentInput = this.gameObject.GetComponent<MyPlatformerController>().Current;

        //update the movement of this character
        this.UpdateMovement(this.CurrentInput.ThumbstickInput.x, this.CurrentInput.ThumbstickInput.y);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateMovement()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateMovement(float xInput_, float yInput_)
    {
        // Move the character
        m_Rigidbody.velocity = new Vector2(xInput_ * this.xAxisMoveSpeed,
                                            yInput_ * this.yAxisMoveSpeed);

        //check which way the player is facing
        this.CheckOrientation(xInput_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CheckOrientation()
    ////////////////////////////////////////////////////////////////////*/
    void CheckOrientation(float move_)
    {
        //if moving right, but facing left
        if (move_ > 0 && !this.FacingRight)
        {
            this.CharacterSprite.flipX = false;
            this.FacingRight = true;
        }
        //moving left, but facing right
        else if (move_ < 0 && this.FacingRight)
        {
            this.CharacterSprite.flipX = true;
            this.FacingRight = false;
        }
    }
}
