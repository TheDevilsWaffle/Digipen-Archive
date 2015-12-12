/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - DamageEffects.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]

public class DamageEffects : MonoBehaviour
{
    [SerializeField]
    public bool IsAffectedByDamageEffects = true;
    [HideInInspector]
    Rigidbody RBody;
    [HideInInspector]
    bool IsDisabled = false;
    [HideInInspector]
    float Timer = 0f;
    [HideInInspector]
    float FreezeDuration;
    [HideInInspector]
    float StunDuration;
    [HideInInspector]
    bool IsPoisoned;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get the rigidbody
        this.RBody = this.gameObject.GetComponent<Rigidbody>();

        //ensure we're not disabled to begin with
        this.IsDisabled = false;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyKnockback(float, Vector3)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyKnockback(float force_, Vector3 dir_)
    {
        if (this.IsAffectedByDamageEffects)
        {
            Debug.Log("APPLYING KNOCKBACK EFFECT");
            this.RBody.AddForce(dir_ * force_);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyFreeze(float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyFreeze(float threshold_)
    {
        if (this.IsAffectedByDamageEffects)
        {
            Debug.Log("APPLYING FREEZE EFFECT");
            //FUTURE CODE NEEDED HERE
            //if(this.gameObject.GetComponent<MovementController>() != null)
            //{
            //    //disable this object's ability to move
            //    this.gameObject.GetComponent<MovementController>().IsDisabled = true;
            //    //start the disabled timer
            //    this.IsDisabled = true;
            //    this.FreezeDuration = threshold_;
            //}
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyStun(float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyStun(float threshold_)
    {
        if (this.IsAffectedByDamageEffects)
        {
            Debug.Log("APPLYING STUN EFFECT");
            //FUTURE CODE NEEDED HERE
            //if(this.gameObject.GetComponent<MovementController>() != null)
            //{
            //    //disable this object's ability to move & shoot
            //    this.gameObject.GetComponent<MovementController>().IsDisabled = true;
            //    this.gameObject.GetComponent<Shoot>().IsDisabled = true;
            //    //start the disabled timer
            //    this.IsDisabled = true;
            //    this.StunDuration = threshold_;
            //}
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyExplosion(float, Vector3, float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyExplosion(float force_, Vector3 dir_, float explosiveRadius_)
    {
        if (this.IsAffectedByDamageEffects)
        {
            Debug.Log("APPLYING Explosion EFFECT");
            this.RBody.AddExplosionForce(force_, dir_, explosiveRadius_);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyPoison(float, float, float, float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyPoison(float duration_, float damage_, float miniStunCount_, float miniStunDuration_)
    {
        if (this.IsAffectedByDamageEffects)
        {
            Debug.Log("APPLYING POISON EFFECT");

        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void Update()
    {
        if (this.IsDisabled)
        {
            //update the timer
            this.Timer += Time.deltaTime;

            if (this.FreezeDuration != 0f)
            {
                this.FreezeUpdate();
            }
            if (this.StunDuration != 0f)
            {
                this.StunUpdate();
            }
        }

        if(this.IsPoisoned)
        {
            this.PoisonUpdate();
        }

        else
        {
            this.Timer = 0f;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ResetDurationsAndTimers()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void ResetDurationsAndTimers()
    {
        this.IsDisabled = false;
        this.Timer = 0f;
        this.FreezeDuration = 0f;
        this.StunDuration = 0f;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FreezeUpdate()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void FreezeUpdate()
    {
        //freeze is up, enable this object to move again
        if (this.Timer >= this.FreezeDuration)
        {
            //FUTURE CODE NEEDED
            //this.gameObject.GetComponent<MovementController>().IsDisabled = false;
            //reset everything
            this.ResetDurationsAndTimers();
        }
        //keep this object disabled
        else
        {
            //FUTURE CODE NEEDED
            //this.gameObject.GetComponent<MovementController>().IsDisabled = true;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: StunUpdate()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void StunUpdate()
    {
        //stun is up, enable this object to move/shoot again
        if (this.Timer >= this.StunDuration)
        {
            //FUTURE CODE NEEDED
            //this.gameObject.GetComponent<MovementController>().IsDisabled = false;
            //this.gameObject.GetComponent<Shoot>().IsDisabled = false;
            //reset everything
            this.ResetDurationsAndTimers();
        }
        //keep this object disabled
        else
        {
            //FUTURE CODE NEEDED
            //this.gameObject.GetComponent<MovementController>().IsDisabled = true;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: PoisonUpdate()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void PoisonUpdate()
    {
        
    }
}
