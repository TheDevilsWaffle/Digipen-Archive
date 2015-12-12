/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - VacuumCollider.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using UnityEngine;
using System.Collections;

//DEPENDENCY
[RequireComponent(typeof(Rigidbody))]

public class VacuumCollider : MonoBehaviour
{
        /*/////////////////////////////////////////////////////////////////////////
        //FUNCTION: OnTriggerEnter(Collider)
        //NOTE:
        /////////////////////////////////////////////////////////////////////////*/
        void OnTriggerEnter(Collider obj_)
    {
        if (obj_ != null && obj_.gameObject.GetComponent<SuctionTarget>() != null)
        {
            if (obj_.attachedRigidbody && obj_.gameObject.GetComponent<SuctionTarget>().IsTargetSuckable)
            {
                //set IsBeingSuctioned to true
                obj_.gameObject.GetComponent<SuctionTarget>().IsBeingSuctioned = true;
                //Debug.Log("COLLISION DETECTED = " + obj_.gameObject.name);
                this.gameObject.GetComponentInParent<Vacuum>().ApplyInitialForceToSuctionTarget(obj_.gameObject, obj_.gameObject.transform.position);

            }
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerExit(Collider)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider obj_)
    {
        if (obj_.attachedRigidbody && obj_.gameObject.GetComponent<SuctionTarget>() != null && obj_.gameObject.GetComponent<SuctionTarget>().IsTargetSuckable)
        {
            //set IsBeingSuctioned to false
            obj_.gameObject.GetComponent<SuctionTarget>().IsBeingSuctioned = false;
        }
    }
}
