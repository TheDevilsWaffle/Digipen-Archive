/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DeathZone.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    #region PROPERTIES

    GameObject Player1;
    GameObject Player2;
    public GameObject PlayerToKill;

    LevelController LevelSettings;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get players
        this.Player1 = GameObject.FindWithTag("Player1").gameObject;
        this.Player2 = GameObject.FindWithTag("Player2").gameObject;

        //get LevelSettings.LevelController
        this.LevelSettings = GameObject.FindWithTag("LevelSettings").GetComponent<LevelController>();

        if (this.gameObject.name == "DeathZone-Green")
        {
            this.PlayerToKill = this.Player2;
        }
        else if (this.gameObject.name == "DeathZone-Purple")
        {
            this.PlayerToKill = this.Player1;
        }

        //ensure a player to kill has been set
        if (this.PlayerToKill == null)
        {
            Debug.LogError("ERROR! You must set a GameObject Player to kill with this DeathZone!");
        }   
    }

    #endregion INITIALIZATION

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider objCollider_)
    {
        GameObject obj;
        //make sure the object touching this thing 
        if (objCollider_.gameObject.transform.parent.gameObject != null)
        {
            obj = objCollider_.gameObject.transform.parent.gameObject;
            //if this is large exit and this is the large player
            if (obj == this.PlayerToKill)
            {
                //kill this player and call LevelSettings function to restart level
                Destroy(this.PlayerToKill);
                this.LevelSettings.FadeOutRestart();
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_02()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_02()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_03()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_03()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_04()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_04()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_05()
    {
        //CONTENT HERE
    }

    #endregion X_FUNCTIONS

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion ANIMATION
}