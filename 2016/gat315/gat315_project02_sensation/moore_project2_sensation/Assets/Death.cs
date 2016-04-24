/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Death.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    #region PROPERTIES

    //references
    HUDController HUD;
    Health PlayerHealth;

    public float DamageToPlayer = 1f;

    #endregion PROPERTIES

    #region AWAKE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get references
        this.PlayerHealth = GameObject.FindWithTag("Player").gameObject.GetComponent<Health>();
        this.HUD = GameObject.Find("HUD").gameObject.GetComponent<HUDController>();
    }

    #endregion

    #region TRIGGERS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider_)
    {
        if(collider_.gameObject.GetComponent<Health>() != null)
        {
            if (!this.PlayerHealth.IsInvulnerable)
            {
                //print("Damaging player!");
                this.PlayerHealth.DecreaseHealth(this.DamageToPlayer);
                this.PlayerHealth.StartInvulnerability();
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerStay()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerStay2D(Collider2D collider_)
    {
        if (collider_.gameObject.GetComponent<Health>() != null)
        {
            if (!this.PlayerHealth.IsInvulnerable)
            {
                this.PlayerHealth.DecreaseHealth(this.DamageToPlayer);
                
                //this.PlayerHealth.StartInvulnerability();
            }
        }
    }

    #endregion
}