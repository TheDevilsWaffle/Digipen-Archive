/*////////////////////////////////////////////////////////////////////////
//SCRIPT: TransferMass.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class TransferMass : MonoBehaviour
{
    [SerializeField] GameObject OtherPlayer;
    [SerializeField] float MinMass = 75f;
    [SerializeField] float MaxMass = 125f;
    float OtherPlayerMass;
    float MyMass;
    float TransferRate = 0.25f;
   [SerializeField] KeyCode TransferMassKey = KeyCode.KeypadPlus;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        if(this.OtherPlayer == null)
        {
            Debug.LogWarning("ASSIGN OTHER PLAYER to OtherPlayer!");
        } 
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(Input.GetKey(this.TransferMassKey))
        {
            this.Transfer();
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateMasses(float, float)
    ////////////////////////////////////////////////////////////////////*/
    bool EvaluateMasses(float myMass_, float theirMass_)
    {
        if(myMass_ > this.MinMass && theirMass_ < this.MaxMass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Transfer()
    ////////////////////////////////////////////////////////////////////*/
    void Transfer()
    {
        //get mine and the other player's current mass
        if(this.EvaluateMasses(this.gameObject.GetComponent<Rigidbody>().mass, 
                               this.OtherPlayer.GetComponent<Rigidbody>().mass))
        {
            //increase the other player's mass and scale
            this.AffectMass(this.OtherPlayer.GetComponent<Rigidbody>(), this.TransferRate);
            this.AffectScale(this.OtherPlayer.transform, (this.TransferRate / 10f));

            //decrease this player's mass and scale
            this.AffectMass(this.gameObject.GetComponent<Rigidbody>(), -this.TransferRate);
            this.AffectScale(this.gameObject.transform, -(this.TransferRate / 10f));
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AffectMass(Rigidbody, float)
    ////////////////////////////////////////////////////////////////////*/
    void AffectMass(Rigidbody obj_, float rate_)
    {
        obj_.mass += rate_;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AffectScale(Transform, float)
    ////////////////////////////////////////////////////////////////////*/
    void AffectScale(Transform obj_, float rate_)
    {
        obj_.localScale += new Vector3(rate_, rate_, rate_);
    }
}
