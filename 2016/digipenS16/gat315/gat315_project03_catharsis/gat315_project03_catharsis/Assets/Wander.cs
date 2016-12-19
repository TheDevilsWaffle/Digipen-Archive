/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Wander.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    #region PROPERTIES

    //references
    private Rigidbody2D s_RigidBody2D;
    private SpriteRenderer s_SpriteRenderer;
    private GameObject Player;

    //attributes
    private float Timer = 0f;
    private float MovementTimer = 0f;
    private float minTime = 2f;
    private float maxTime = 5f;
    private float MoveThreshold;

    [Header("MOVEMENT")]
    public float minMove = -2.5f;
    public float maxMove = 2.5f;
    public float Speed = 10f;
    public EaseType Ease = EaseType.linear;
    private float MovementTimerThreshold = 0.5f;

    [HideInInspector]
    public bool IsNPCTalking;
    private bool IsNPCMoving;
    private Vector2 EndMarker;
    private float OriginalZPos;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get self-references
        this.s_RigidBody2D = GetComponent<Rigidbody2D>();
        this.s_SpriteRenderer = GetComponent<SpriteRenderer>();

        //get player reference
        this.Player = GameObject.Find("Player").gameObject;

        //set bools
        this.IsNPCTalking = false;
        this.IsNPCMoving = false;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize MoveThreshold
        this.MoveThreshold = Random.Range(this.minTime, this.maxTime);

        this.OriginalZPos = this.gameObject.transform.position.z;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FixedUpdate()
    ////////////////////////////////////////////////////////////////////*/
    void FixedUpdate()
    {
        //only move if this NPC is not currently talking
        if (!this.IsNPCTalking && !this.IsNPCMoving)
        {
            //if Timer has surpassed the timer threshold for movement
            if (this.Timer > this.MoveThreshold)
            {
                //set bool
                this.IsNPCMoving = true;
                //reset timer
                this.Timer = 0f;
                //randomly move this NPC
                this.RandomMovement();
                this.MovementTimer = 0f;
                
            }

            //update Timer
            this.Timer += Time.deltaTime;
        }

        if(this.IsNPCMoving)
        {
            this.MovementTimer += Time.deltaTime;
            if (this.MovementTimer > this.MovementTimerThreshold)
                this.StopNPCMovement();
        }
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: RandomMovement()
    ////////////////////////////////////////////////////////////////////*/
    void RandomMovement()
    {
        this.IsNPCMoving = true;
        Vector2 randomVector2 = new Vector2((Random.Range(this.minMove, this.maxMove)),
                                            (Random.Range(this.minMove, this.maxMove)));
        randomVector2.Normalize();

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(randomVector2 * this.Speed, ForceMode2D.Impulse);

        //determine facing
        if (randomVector2.x >= 0)
            this.s_SpriteRenderer.flipX = false;
        else if (randomVector2.x < 0)
            this.s_SpriteRenderer.flipX = true;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: StopNPCMovement()
    ////////////////////////////////////////////////////////////////////*/
    void StopNPCMovement()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        this.IsNPCMoving = false;
        this.MoveThreshold = Random.Range(this.minTime, this.maxTime);

        //DEBUG
        //print("new MoveThreshold = " + this.MoveThreshold);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: StopWandering()
    ////////////////////////////////////////////////////////////////////*/
    public void StopWandering()
    {
        iTween.StopByName(this.gameObject, "AnimateMoveNPC");
        this.IsNPCTalking = true;
        this.FacePlayer();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: ResumeWandering()
    ////////////////////////////////////////////////////////////////////*/
    public void ResumeWandering()
    {
        this.Timer = 0f;
        this.IsNPCMoving = false;
        this.IsNPCTalking = false;

    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FacePlayer()
    ////////////////////////////////////////////////////////////////////*/
    void FacePlayer()
    {
        if (this.transform.position.x > this.Player.transform.position.x)
            this.s_SpriteRenderer.flipX = true;
        else
            this.s_SpriteRenderer.flipX = false;
    }

    #endregion

}