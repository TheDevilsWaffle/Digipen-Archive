/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - BlasterBoltProperties.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

//REQUIRED COMPONENTS
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class BlasterBoltProperties : MonoBehaviour
{
    //PROPERTIES
    [HideInInspector]
    public GameObject ShotBy;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(this.gameObject);
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            Destroy(collisionInfo.gameObject);
        }
        if (collisionInfo.gameObject.tag == "Player")
        {
            //collisionInfo.gameObject.GetComponent<Health>().DecreaseHealth(5);
        }
    }
}