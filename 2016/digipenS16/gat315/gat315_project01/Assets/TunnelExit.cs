/*////////////////////////////////////////////////////////////////////////
//SCRIPT: TunnelExit.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class TunnelExit : MonoBehaviour
{
    Teleport TunnelGroup;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        this.TunnelGroup = this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Teleport>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider collider_)
    {
        GameObject obj = collider_.gameObject.transform.parent.gameObject;

        if(obj.GetComponent<Mass>() != null && 
            obj.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.SMALL)
        {
            this.TunnelGroup.TeleportPlayer(collider_.transform.parent.gameObject, this.gameObject.name);
        }
    }
}