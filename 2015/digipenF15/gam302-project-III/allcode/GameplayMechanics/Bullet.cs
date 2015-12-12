/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Bullet.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Bullet:MonoBehaviour
{
    //PROPERTIES
    enum DamageEffect { NONE, KNOCKBACK, POISON, FREEZE, STUN }
    [Header("DAMAGE & DAMAGE TYPE")]
    [SerializeField]
    float Damage = 2f;
    [SerializeField]
    DamageEffect DamageEffectType = DamageEffect.NONE;

    [Header("KNOCKBACK EFFECT")]
    [SerializeField]
    float KnockBackForce = 2f;

    [Header("FREEZE EFFECT")]
    [SerializeField]
    float FreezeDuration = 2f;

    [Header("POISON EFFECT")]
    [SerializeField]
    float PoisonDuration = 10f;
    [SerializeField]
    float PoisonDamage = 1f;
    [SerializeField]
    float NumberOfMiniStuns = 3f;
    [SerializeField]
    float MiniStunDuration = 1.5f;

    [Header("STUN EFFECT")]
    [SerializeField]
    float StunDuration;

    //OTHER PROPERTIES
    [SerializeField]
    float Speed = 10f;
    [SerializeField]
    public float HeightOffset = 0;
    public GameObject Planet = null;
    private Vector3 ToPlanetCenter = new Vector3(0,0,0);
    private float GroundCheckDistance = 1;
    private Vector3 GroundNormal = new Vector3(0,0,0);
    private bool IsGrounded = false;
    [HideInInspector] public float HeightFromCenter = 0;
    [HideInInspector] public GameObject ShotBy = null;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //PURPOSE: Initialize
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //fire the bullet upon creation
        //GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, this.Speed));
        //Vector3 nextAssumedPosition = transform.position + (transform.forward * Mathf.Sqrt(Speed));
        //Vector3 nextToPlanetCenter = Planet.transform.position - nextAssumedPosition;
        //float heightOffset = 1 - Speed * 0.003f;
        HeightFromCenter = (Planet.transform.position - this.transform.position).magnitude + HeightOffset;
    }

    void FixedUpdate()
    {
        ToPlanetCenter = (Planet.transform.position - this.transform.position).normalized;
        CheckGroundStatus();

        Vector3 velocity = transform.forward * Speed;

        ////Curving trajectory (and accounting for extra height that would be gained):
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(velocity, -ToPlanetCenter).normalized * Speed; //Get the projectedVelocity
        Vector3 pointAtHeight = -ToPlanetCenter * HeightFromCenter;                                      //Find the correct point relative to the object's height above the planet
        Vector3 nextVelocityPoint = (pointAtHeight + Planet.transform.position) + projectedVelocity;     //Move the vector to the correct height
        Vector3 newVelocity = nextVelocityPoint - transform.position;                                    //Get another vector that points to the end of that

        GetComponent<Rigidbody>().velocity = newVelocity.normalized * Speed;
        transform.LookAt(transform.position + newVelocity, ToPlanetCenter);
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (-transform.up * 2f), -transform.up, out hitInfo, GroundCheckDistance))
        {
            GroundNormal = hitInfo.normal;
            IsGrounded = true;
            //Animator.applyRootMotion = true;
        }
        else
        {
            IsGrounded = false;
            GroundNormal = -ToPlanetCenter;
            //Animator.applyRootMotion = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //float currentSpeed = velocity.magnitude;

        ////Curving trajectory (and accounting for extra height that would be gained):
        //Vector3 nextAssumedPosition = transform.position + (velocity * Time.deltaTime);
        //Vector3 nextToPlanetCenter = (Planet.transform.position - nextAssumedPosition).normalized;

        //print("Next: " + nextAssumedPosition);
        //print("Current: " + transform.position);

        ////Vector3 projectedVelocity = Vector3.ProjectOnPlane(velocity, -nextToPlanetCenter).normalized * Speed; //Get the projectedVelocity
        //Vector3 nextCorrectPosition = nextAssumedPosition + (-nextToPlanetCenter * HeightFromCenter);      //Find the correct point relative to the object's height above the planet
        ////Vector3 nextVelocityPoint = (nextCorrectPosition + Planet.transform.position) + projectedVelocity;     //Move the vector to the correct height
        //Vector3 newVelocity = nextCorrectPosition - transform.position;                                           //Get another vector that points to the end of that
        ////Vector3 newVelocity = nextCorrectPosition - transform.position;

        //GetComponent<Rigidbody>().velocity = newVelocity.normalized * Speed;
        //transform.LookAt(transform.position + newVelocity, ToPlanetCenter);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(this.gameObject);
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            //check to see if we can apply a damage effect on this enemy
            if (collisionInfo.gameObject.GetComponent<DamageEffects>() != null && collisionInfo.gameObject.GetComponent<DamageEffects>().IsAffectedByDamageEffects)
            {
                this.DamageEffectCheck(this.DamageEffectType, collisionInfo.gameObject, collisionInfo.contacts[0].point.normalized);
            }
            Destroy(collisionInfo.gameObject);
        }
        if (collisionInfo.gameObject.tag == "Player")
        {
            collisionInfo.gameObject.GetComponent<Health>().DecreaseHealth(5);
        }
    }

    /*//////////////////////////////////////////////////////////////////////////////
    //FUNCTION - DamageEffectCheck(DamageEffect, GameObject)
    //////////////////////////////////////////////////////////////////////////////*/
    void DamageEffectCheck(DamageEffect effect_, GameObject obj_, Vector3 dir_)
    {
        //check to see if we have a damage effect to send
        if (this.DamageEffectType != DamageEffect.NONE)
        {
             switch (effect_)
             {
                case DamageEffect.KNOCKBACK:
                    obj_.gameObject.GetComponent<DamageEffects>().ApplyKnockback(this.KnockBackForce, dir_);
                    break;

                case DamageEffect.FREEZE:
                    obj_.gameObject.GetComponent<DamageEffects>().ApplyFreeze(this.FreezeDuration);
                    break;

                case DamageEffect.POISON:
                    obj_.gameObject.GetComponent<DamageEffects>().ApplyPoison(this.PoisonDuration,
                                                                                this.PoisonDamage,
                                                                                this.NumberOfMiniStuns,
                                                                                this.MiniStunDuration);
                    break;

                case DamageEffect.STUN:
                    obj_.gameObject.GetComponent<DamageEffects>().ApplyStun(this.StunDuration);
                    break;

                default:
                    break;
             }
        }
    }
}
