/*////////////////////////////////////////////////////////////////////////
//SCRIPT: AttackPlayer.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    #region PROPERTIES

    //references
    Rigidbody2D RigidBody2D;
    public SoundManager EnemySFX;
    public SoundManager EnemySecondarySFX;

    //attributes
    float BurstSpeed = 4000f;
    float DamageToPlayer = 1f;
    float DelayAttackTime = 0.5f;

    //sfx
    public AudioClip BatSqueakSFX;
    public AudioClip BatDeathSFX;
    public AudioClip BatFlappingSFX;

    GameObject Player;
    Vector3 PlayerInitialPos;
    [HideInInspector]
    public bool IsAttacking = false;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //find the player
        this.Player = GameObject.FindWithTag("Player").gameObject;

        //get references
        this.RigidBody2D = GetComponent<Rigidbody2D>();
    }

    #endregion INITIALIZATION

    #region ATTACK

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Attack()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator Attack()
    {
        //print("attacking!");
        //wait a brief moment before attacking
        yield return new WaitForSeconds(this.DelayAttackTime);

        //enemy sfx
        this.EnemySFX.PlaySingle(this.BatSqueakSFX);

        //get initial player location
        this.PlayerInitialPos = this.Player.transform.position;
        this.RigidBody2D.AddForce((this.PlayerInitialPos - this.transform.position) * this.BurstSpeed * Time.deltaTime);
        print(this.RigidBody2D.velocity);
        //set IsAttacking
        this.IsAttacking = true;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.IsAttacking)
            this.EnemySecondarySFX.PlayPaitiently(this.BatFlappingSFX, true);
    }

    #endregion

    #region COLLISION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnCollisionEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnCollisionEnter2D(Collision2D collider_)
    {
        if(collider_.gameObject == this.Player)
        {
            this.Player.GetComponent<Health>().DecreaseHealth(this.DamageToPlayer);
        }

        this.IsAttacking = false;

        //sfx of bat death
        this.EnemySFX.PlaySingle(this.BatDeathSFX);
        
        //destroy
        Destroy(this.gameObject);
    }

    #endregion 
}