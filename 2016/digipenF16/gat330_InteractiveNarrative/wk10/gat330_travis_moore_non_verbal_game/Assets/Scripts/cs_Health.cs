/*////////////////////////////////////////////////////////////////////////
//SCRIPT: cs_Health.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class cs_Health : MonoBehaviour
{
    #region PROPERTIES

    //references


    //attributes
    private float Health;
    public float MaxHealth;
    public float MinHealth;
    public bool IsInvulnerable;
    public float InvulnerabilityDuration;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
    }

    /*////////////////////////////////////////////////////////////////////
    //Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseHealth(5);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseHealth(7);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            CurrentHealth = 25f;
        }

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            ApplyDamageOverTime(2f, 6f);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ToggleInvulnerability();
        }
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //CurrentHealth
    ////////////////////////////////////////////////////////////////////*/
    public float CurrentHealth
    {
        get { return Health; }
        set { Health = EvaluateHealth(value); }
    }

    /*////////////////////////////////////////////////////////////////////
    //IncreaseHealth()
    ////////////////////////////////////////////////////////////////////*/
    public void IncreaseHealth(float value_)
    {
        Debug.Log(this.gameObject + " IncreaseHealth(" + value_ + ") = ");
        Health = EvaluateHealth(Health + value_);

        Debug.Log(gameObject + " CurrentHealth = " + CurrentHealth);
    }

    /*////////////////////////////////////////////////////////////////////
    //DecreaseHealth()
    ////////////////////////////////////////////////////////////////////*/
    public void DecreaseHealth(float value_, bool isDOT = false)
    {
        //damage over time (no invulnerability)
        if (!IsInvulnerable && isDOT)
        {
            Debug.Log(this.gameObject + "DOT DecreaseHealth(" + value_ + ") = ");

            Health = EvaluateHealth(Health - value_);
        }

        //regular damage w/ invulnerability
        else if (!IsInvulnerable && !isDOT)
        {
            Debug.Log(this.gameObject + " DecreaseHealth(" + value_ + ") = ");

            Health = EvaluateHealth(Health - value_);
            StartCoroutine(StartDamagedInvulnerability());
        }

        //currently invulnerable
        else
            Debug.Log(this.gameObject + " is currently invulnerable!");

        Debug.Log(gameObject + " CurrentHealth = " + CurrentHealth);
    }

    /*////////////////////////////////////////////////////////////////////
    //EvaluateHealth()
    ////////////////////////////////////////////////////////////////////*/
    float EvaluateHealth(float value_)
    {
        //value is greater than MaxHealth, return max health
        if (value_ > MaxHealth)
        {
            return MaxHealth;
        }
        //value is less than MinHealth, call DestroyOwner
        else if (value_ < MinHealth)
        {
            DestroyOwner();
            return 0f;
        }
        //value is in acceptable range, return value
        else
            return value_;
    }

    /*////////////////////////////////////////////////////////////////////
    //ToggleInvulnerability()
    ////////////////////////////////////////////////////////////////////*/
    public void ToggleInvulnerability()
    {
        if (IsInvulnerable)
        {
            Debug.Log(this.gameObject + " Invulnerability State = " + IsInvulnerable);

            IsInvulnerable = false;
        }
        else
        {
            Debug.Log(this.gameObject + " Invulnerability State = " + IsInvulnerable);

            IsInvulnerable = true;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //StartDamagedInvulnerability()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator StartDamagedInvulnerability()
    {
        Debug.Log(this.gameObject + " StartDamagedInvulnerability()");

        ToggleInvulnerability();
        yield return new WaitForSeconds(InvulnerabilityDuration);
        ToggleInvulnerability();
    }

    /*////////////////////////////////////////////////////////////////////
    //ApplyDamageOverTime()
    ////////////////////////////////////////////////////////////////////*/
    public void ApplyDamageOverTime(float damagePerSecond_, float duration_)
    {
        //just public access without having to start a coroutine
        StartCoroutine(StartDamageOverTime(damagePerSecond_, duration_));
    }

    /*////////////////////////////////////////////////////////////////////
    //StartDamageOverTime()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator StartDamageOverTime(float damagePerSecond_, float duration_)
    {
        for (float i = duration_; i > 0f; --i)
        {
            DecreaseHealth(damagePerSecond_, true);
            yield return new WaitForSeconds(1f);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //DestroyOwner()
    ////////////////////////////////////////////////////////////////////*/
    void DestroyOwner()
    {
        //CONTENT HERE
        Debug.Log(this.gameObject + " is dead now");
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}