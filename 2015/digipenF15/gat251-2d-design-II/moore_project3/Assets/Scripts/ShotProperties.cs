/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ShotProperties.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class ShotProperties : MonoBehaviour
{
    //PROPERTIES
    public Vector3 ShotSize;
    public bool CanBreakBlocks;
    public GameObject ShotBy;
    public Vector3 CurrentVelocity;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.gameObject.transform.localScale = this.ShotSize;
        //Debug.Log("SHOT FIRED, SIZE = " + this.gameObject.transform.localScale);
        this.CurrentVelocity = this.gameObject.GetComponent<Rigidbody>().velocity;
        Debug.Log("CURRENT VELOCITY AT CREATION " + this.CurrentVelocity);
        this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnCollisionEnter(Collider)
    /////////////////////////////////////////////////////////////////////////*/
    void OnCollisionEnter(Collision obj_)
    {
        //Debug.Log("OBJECT HIT = " + Obj_.gameObject);

        if (this.CanBreakBlocks)
        {
            if (obj_.gameObject.tag == "BreakableBlock")
            {
                Destroy(obj_.gameObject);
            }
        }

        if (obj_.gameObject != this.ShotBy)
        {
            //DEBUG
            //Debug.Log("HIT SOMTHING THAT IS NOT THE PLAYER");
            
            //destroy the shot
            //Destroy(this.gameObject);
        }
        
        if (obj_.gameObject.tag == "Shield")
        {
                // get the point of contact
                ContactPoint contact = obj_.contacts[0];

                // reflect our old velocity off the contact point's normal vector
                Vector3 reflectedVelocity = Vector3.Reflect(this.CurrentVelocity, contact.normal);

                // assign the reflected velocity back to the rigidbody
                this.gameObject.GetComponent<Rigidbody>().velocity = reflectedVelocity;
                // rotate the object by the same ammount we changed its velocity
                Quaternion rotation = Quaternion.FromToRotation(this.CurrentVelocity, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;
         }
        else
        {
            Destroy(this.gameObject);
        }
    }
}