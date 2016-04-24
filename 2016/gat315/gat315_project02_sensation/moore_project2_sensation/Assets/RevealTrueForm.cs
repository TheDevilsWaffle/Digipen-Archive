/*////////////////////////////////////////////////////////////////////////
//SCRIPT: _CSHARPTEMPLATE_.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using LOS.Event;

public class RevealTrueForm : MonoBehaviour
{

    #region PROPERTIES

    //references
    private MeshRenderer Mesh;
    private SpriteRenderer Sprite;
    private ParticleSystem ParticleSystem;
    private AttackPlayer AttackPlayer;
    private LOSEventTrigger LightTrigger;

    //attributes
    bool WasLit = false;

    #endregion

    #region INITIALIZE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get references (if they exist on this object)
        if (GetComponent<MeshRenderer>() != null)
            this.Mesh = GetComponent<MeshRenderer>();
        if (GetComponent<SpriteRenderer>() != null)
            this.Sprite = GetComponent<SpriteRenderer>();
        if (GetComponent<AttackPlayer>() != null)
            this.AttackPlayer = GetComponent<AttackPlayer>();

        this.ParticleSystem = GetComponent<ParticleSystem>();

        //get this object's LOSEventTrigger
        this.LightTrigger = GetComponent<LOSEventTrigger>();

        //add functions based on if this object is triggered
        this.LightTrigger.OnNotTriggered += OnNotLit;
        this.LightTrigger.OnTriggered += OnLit;
    }

    #endregion

    #region NOT LIT / LIT

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnLit()
    ////////////////////////////////////////////////////////////////////*/
    private void OnLit()
    {
        //print(this.gameObject.name + " was lit!");
        //reveal the object for what it really is
        if (this.Mesh != null)
            this.Mesh.enabled = true;
        else if (this.Sprite != null)
            this.Sprite.enabled = true;

        //attack the player if this object can
        if (this.AttackPlayer != null)
        {
            //if we aren't alreay attacking, attack
            if (!this.AttackPlayer.IsAttacking)
                StartCoroutine(this.AttackPlayer.Attack());
        }

        this.LightTrigger.OnTriggered -= OnNotLit;
        this.LightTrigger.OnNotTriggered -= OnNotLit;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnNotLit()
    ////////////////////////////////////////////////////////////////////*/
    private void OnNotLit()
    {

        this.LightTrigger.OnNotTriggered -= OnLit;
    }
    
    #endregion
}
