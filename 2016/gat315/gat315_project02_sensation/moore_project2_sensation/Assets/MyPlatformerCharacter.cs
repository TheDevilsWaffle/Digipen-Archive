/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MyPlatformerCharacter.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyPlatformerCharacter : MonoBehaviour
{
    //references
    [SerializeField]
    SoundManager PlayerMovementSFX;
    [SerializeField]
    SoundManager PlayerJumpLandSFX;
    [SerializeField]
    SFXList SFX;


    //attributes
    public float WalkSpeed = 3f; 
    public float RunSpeed = 9f;
    public float JumpSpeed = 500f;
    public bool HasAirControl = false;
    public bool FacingRight = true;
    public Vector2 WallClingGravity = new Vector2(0f, 1f);

    //ground
    [SerializeField]
    private LayerMask LayerThatIsGround;
    private Transform GroundCheck;
    private bool IsGrounded = false;
    private bool IsCurrentlyJumping = false;
    float GroundedRadius = 0.2f;

    //components
    private Animator Animator;
    private Rigidbody2D Rigidbody;
    private MyInput CurrentInput;
    private SpriteRenderer CharacterSprite;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get references
        this.GroundCheck = this.gameObject.transform.Find("GroundCheck");
        this.Animator = this.gameObject.GetComponent<Animator>();
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        this.CharacterSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FixedUpdate() <-- better physics
    ////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //first things first, get the most current input
        this.CurrentInput = this.gameObject.GetComponent<MyPlatformerController>().Current;

        //check to make sure the character is grounded
        this.CheckIfGrounded();

        //update the movement of this character
        this.UpdateMovement(this.CurrentInput.ThumbstickInput.x);

        //update jumping
        this.UpdateJumping(this.CurrentInput.JumpInput);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FixedUpdate() <-- better physics
    ////////////////////////////////////////////////////////////////////*/
    void CheckIfGrounded()
    {
        //set IsGrounded to false
        this.IsGrounded = false;

        //get any colliders we're in contact with using GroundCheck's transform is inside any layer's that we said are ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.GroundCheck.position, this.GroundedRadius, this.LayerThatIsGround);

        //now check to see if anything we're collided with matches what we're looking for
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                this.IsGrounded = true;

                //if we were currently in the air and have now landed
                if (this.IsCurrentlyJumping)
                {
                    //tell sfx system to play landing sfx
                    this.PlayerJumpLandSFX.PlaySingle(this.SFX.Land);
                    this.IsCurrentlyJumping = false;
                }
            }
        }
        //tell the animator if we're grounded or not
        this.Animator.SetBool("Ground", this.IsGrounded);

        //tell the animator what speed we're falling at
        this.Animator.SetFloat("vSpeed", this.Rigidbody.velocity.y);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateMovement()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateMovement(float move_)
    {
        //only allow movement if the character is grounded
        if(this.IsGrounded || this.HasAirControl)
        {
            if (move_ != 0)
            {
                //check to see if the character is running
                if (this.CurrentInput.RunInput)
                {
                    //move the character (retain the value of y if we're already falling/jumping
                    this.Rigidbody.velocity = new Vector2(move_ * this.RunSpeed, this.Rigidbody.velocity.y);

                    //send the absolute value of move to Animator
                    this.Animator.SetFloat("Speed", Mathf.Abs(this.RunSpeed));

                    //ensure we're on the ground
                    if (this.IsGrounded)
                    {
                        //tell the SFXSystem what sfx to loop
                        this.PlayerMovementSFX.PlayPaitiently(this.SFX.Walk, true, 0.9f, 1.2f);
                    }
                }
                //else we're using walking speed
                else
                {
                    //move the character (retain the value of y if we're already falling/jumping
                    this.Rigidbody.velocity = new Vector2(move_ * this.WalkSpeed, this.Rigidbody.velocity.y);

                    //send the absolute value of move to Animator
                    this.Animator.SetFloat("Speed", Mathf.Abs(this.WalkSpeed));

                    //ensure we're on the ground
                    if (this.IsGrounded)
                    {
                        //tell the SFXSystem what sfx to loop
                        this.PlayerMovementSFX.PlayPaitiently(this.SFX.Walk, true, 0.8f, 0.9f);
                    }
                }
            }
            else
            {
                //send the absolute value of move to Animator
                this.Animator.SetFloat("Speed", Mathf.Abs(move_));
            }


            //check if character's sprite needs to be flipped left or right
            this.CheckOrientation(move_);
        }
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
        else if(move_ < 0 && this.FacingRight)
        {
            this.CharacterSprite.flipX = true;
            this.FacingRight = false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateJumping()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateJumping(bool isJumping_)
    {
        // If the player can jump
        if (this.IsGrounded && isJumping_ && this.Animator.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            this.IsGrounded = false;
            this.Animator.SetBool("Ground", false);
            this.Rigidbody.AddForce(new Vector2(0f, this.JumpSpeed));

            //tell the SFXSystem we're jumping
            this.PlayerJumpLandSFX.PlaySingle(this.SFX.Jump);
            this.IsCurrentlyJumping = true;
        }
    }
}
