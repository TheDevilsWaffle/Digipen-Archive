/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DetectPlayer.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{
    #region PROPERTIES

    //references
    AttackPlayer AttackPlayer;
    GameObject Player;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //find the player
        this.Player = GameObject.FindWithTag("Player").gameObject;

        //get references
        this.AttackPlayer = GetComponentInParent<AttackPlayer>();
    }

    #endregion INITIALIZATION

    #region TRIGGER

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider_)
    {
        //print("detecting " + collider_.name);
        if (collider_.gameObject == this.Player)
        {
            //print("Attacking Player");
            if(!this.AttackPlayer.IsAttacking)
                StartCoroutine(this.AttackPlayer.Attack());
        }
    }

    #endregion 
}