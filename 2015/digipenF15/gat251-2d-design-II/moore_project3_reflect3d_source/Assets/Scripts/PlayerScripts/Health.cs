/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Health.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    //PROPERTIES
    [Header("Health Attributes")]
    [SerializeField]
    private float MaximumHP;
    [SerializeField]
    public float CurrentHP;
    [Header("Damage Attributes")]
    [SerializeField]
    private bool IsDamagable = true;
    [SerializeField]
    private float DamageScalerValue;
    public enum DamageScalerType { LINEAR, MULTIPLIER }
    [SerializeField]
    public DamageScalerType ScalerType;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: IncreaseHealth(float)
    //NOTE: Calls ClampHealth() before setting CurrentHP
    /////////////////////////////////////////////////////////////////////////*/
    public void IncreaseHealth(float hp_)
    {
        this.CurrentHP += hp_;
        //perform ClampHealth() to keep health within bounds
        this.CurrentHP = this.ClampHealth(this.CurrentHP);

        //flash heal on the HUD
        if (this.gameObject.name == "Player")
        {
            GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("heal", this.CurrentHP);
        }

        //evaluate health of object
        this.EvaluateHealth();

        //DEBUG
        Debug.Log(this.gameObject + "HP has INCREASED to " + this.CurrentHP + " out of " + this.MaximumHP);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DecreaseHealth(float)
    //NOTE: Calls ClampHealth() before setting CurrentHP
    /////////////////////////////////////////////////////////////////////////*/
    public void DecreaseHealth(float damage_)
    {
        //call ApplyDamageScaler() if applicable
        if(this.DamageScalerValue > 0f)
        {
            damage_ = this.ApplyDamageScaler(damage_);
        }
        this.CurrentHP -= damage_;
        //perform ClampHealth() to keep health within bounds
        this.CurrentHP = this.ClampHealth(this.CurrentHP);
        
        //flash damage on the HUD
        if (this.gameObject.name == "Player")
        {
            GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("damage", this.CurrentHP);
        }

        //evaluate health of object
        this.EvaluateHealth();

        //DEBUG
        Debug.Log(this.gameObject + "HP has DECREASED to " + this.CurrentHP + " out of " + this.MaximumHP);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ClampHealth(float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public float ClampHealth(float hp_)
    {
        //health is higher than allowed maximum
        if (hp_ > this.MaximumHP)
        {
            return this.MaximumHP;
        }
        //health is less than or equal to zero
        else if (hp_ <= 0f)
        {
            return 0f;
        }
        //health is in bounds, return health
        else
        {
            return hp_;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: EvalulateHealth(float)
    //NOTE: Calls DestroyOwner() if applicable
    /////////////////////////////////////////////////////////////////////////*/
    public float EvaluateHealth()
    {
        //check to see if the player can even be damaged at all
        if (this.IsDamagable)
        {
            //kill the player if health <= zero
            if (this.CurrentHP <= 0f)
            {
                //future call to death animation
                //future call to death sfx

                //destroy this gameObject
                this.DestroyOwner();

                return 0;
            }
        }
        
        //DEBUG
        Debug.Log(this.gameObject + "'S HEALTH EVALUATED TO: " + this.CurrentHP + " out of " + this.MaximumHP);

        //return the value of this gameObject's CurrentHP
        return this.CurrentHP;
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetHealthToValue(float)
    //NOTE: Calls ClampHealth() to ensure value is within range
    /////////////////////////////////////////////////////////////////////////*/
    public void SetHealthToValue(float hp_)
    {
        //set CurrentHP equal to new value after ClampHealth()
        this.CurrentHP = this.ClampHealth(hp_);
        //evaluate health to make sure we're not dead now
        this.EvaluateHealth();
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyOwner()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void DestroyOwner()
    {
        //kill this game object
        Destroy(this.gameObject);
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyDamageScaler(float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public float ApplyDamageScaler(float damage_)
    {
        //apply correct damage scaling based on ScalerType
        switch (this.ScalerType)
        {
            case DamageScalerType.LINEAR:
                damage_ = (damage_ + this.DamageScalerValue);
                break;
            case DamageScalerType.MULTIPLIER:
                damage_ = (damage_ * this.DamageScalerValue);
                break;
            default:
                Debug.LogError("APPLYDAMAGESCALER() ERROR - default case reached");
                break;
        }
        return damage_;
    }
}
