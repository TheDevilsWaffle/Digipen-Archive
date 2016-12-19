/*////////////////////////////////////////////////////////////////////////
//SCRIPT: BossController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    #region PROPERTIES

    public SoundManager EnemySFX;
    public SoundManager EnemySecondarySFX;

    //references
    GameObject Player;
    [SerializeField]
    GameObject CaveEntrance;
    [SerializeField]
    GameObject[] BossCheckPoints;
    [SerializeField]
    float TripTime1 = 3f;
    [SerializeField]
    float TripTime2 = 7;
    float CompletionTime;
    Vector3 BossStartPos;
    bool IsLerping = false;
    float StartedLerpingTime;
    Vector3 Destination;
    [SerializeField]
    float AwakenTimeDelay = 3f;
    [SerializeField]
    Vector2 HurtPlayerForce;

    //sfx
    public AudioClip BossGruntsSFX;
    public AudioClip BossMusicSFX;

    //attributes
    [SerializeField]
    private Sprite BossSprite_Inactive;
    [SerializeField]
    private Sprite BossSprite_Active;


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.Player = GameObject.FindWithTag("Player").gameObject;
        this.BossStartPos = this.transform.position;

        //start with inactive sprite
        this.GetComponent<SpriteRenderer>().sprite = this.BossSprite_Inactive;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.IsLerping)
        {
            float timeSinceStarted = Time.time - this.StartedLerpingTime;
            float percentageComplete = timeSinceStarted / this.CompletionTime;

            this.gameObject.transform.position = Vector3.Lerp(this.BossStartPos,
                                                              this.Destination,
                                                              percentageComplete);
        }
        if(this.BossCheckPoints[0].GetComponent<BossPositionsTrigger>().CheckpointReached)
        {
            this.AttackPlayer();
        }
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AttackPlayer()
    ////////////////////////////////////////////////////////////////////*/
    public void AttackPlayer()
    {
        //print("ATTACKPLAYER()");
        this.IsLerping = true;
        this.StartedLerpingTime = Time.time;
        if(!this.BossCheckPoints[0].GetComponent<BossPositionsTrigger>().CheckpointReached)
        {
            this.CompletionTime = this.TripTime1;
            this.Destination = this.BossCheckPoints[0].transform.position;
        }
        else
        {
            this.BossStartPos = this.gameObject.transform.position;
            this.CompletionTime = this.TripTime2;
            this.Destination = this.BossCheckPoints[1].transform.position;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AwakenBoss()
    ////////////////////////////////////////////////////////////////////*/
    public void AwakenBoss()
    {
        //play boss roar sfx
        this.EnemySFX.PlaySingle(this.BossMusicSFX);
        this.EnemySecondarySFX.PlaySingle(this.BossGruntsSFX);

        //reveal boss
        GetComponent<SpriteRenderer>().sprite = this.BossSprite_Active;

        //shake the camera

        StartCoroutine(this.DelayBeforeAttack());
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DelayBeforeAttack()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator DelayBeforeAttack()
    {
        //wait a second
        yield return new WaitForSeconds(this.AwakenTimeDelay);
        //attack the player
        this.AttackPlayer();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D col2D_)
    {
        if(col2D_.gameObject == this.Player)
        {
            //hurt the player
            if (!this.Player.GetComponent<Health>().IsInvulnerable)
            {
                //print("Damaging player!");
                this.Player.GetComponent<Health>().DecreaseHealth(1);
                this.Player.GetComponent<Health>().StartInvulnerability();
            }

            //add force to the player
            this.Player.GetComponent<Rigidbody2D>().AddForce(this.HurtPlayerForce * 100f);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerStay2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerStay2D(Collider2D col2D_)
    {
        if (col2D_.gameObject == this.Player)
        {
            //hurt the player
            if (!this.Player.GetComponent<Health>().IsInvulnerable)
            {
                //print("Damaging player!");
                this.Player.GetComponent<Health>().DecreaseHealth(1);
                this.Player.GetComponent<Health>().StartInvulnerability();
            }

            //add force to the player
            this.Player.GetComponent<Rigidbody2D>().AddForce(this.HurtPlayerForce * 100f);
        }
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}