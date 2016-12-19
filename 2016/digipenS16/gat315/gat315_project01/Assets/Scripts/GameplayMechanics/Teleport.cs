/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Teleport.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    GameObject TunnelA;
    GameObject TunnelA_Hole;
    GameObject TunnelB;
    GameObject TunnelB_Hole;

    Vector3 XOffset = new Vector3(0.5f, 0.25f, 0f);

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        this.TunnelA = this.gameObject.transform.Find("tunnelA").gameObject;
        this.TunnelA_Hole = this.TunnelA.transform.Find("exitA").gameObject;

        this.TunnelB = this.gameObject.transform.Find("tunnelB").gameObject;
        this.TunnelB_Hole = this.TunnelB.transform.Find("exitB").gameObject;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TeleportPlayer(GameObject, string)
    ////////////////////////////////////////////////////////////////////*/
    public void TeleportPlayer(GameObject player_, string entrance_)
    {
        GameObject playerArt = player_.transform.Find("PlayerArt").gameObject;
        print(playerArt);
        switch(entrance_)
        {
            case ("exitA"):
                //send player to exit b
                player_.transform.position = (this.TunnelB_Hole.transform.position + this.XOffset);
                //turn the player
                break;
            case ("exitB"):
                //send player to exit a
                player_.transform.position = (this.TunnelA_Hole.transform.position + this.XOffset);
                //turn the player
                player_.GetComponent<PlayerRotater>().RotatePlayer();
                break;
            default:
                Debug.LogError("ERROR! tunnel exit problem. Default should never be reached!");
                break;
        }
    }
}