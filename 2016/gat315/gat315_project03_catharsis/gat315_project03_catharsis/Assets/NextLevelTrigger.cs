/*////////////////////////////////////////////////////////////////////////
//SCRIPT: NextLevelTrigger.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class NextLevelTrigger : MonoBehaviour
{
    #region PROPERTIES

    //references
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private SceneManagementSystem s_SceneManagementSystem;

    //attributes



    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get player reference if not set
        if(this.Player == null)
            this.Player = GameObject.FindWithTag("Player").gameObject;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //CONTENT HERE
    }

    #endregion

    #region ONTRIGGERENTER2D

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider2D_)
    {
        //if the player enters this trigger zone
        if (collider2D_.gameObject == this.Player)
            StartCoroutine(this.s_SceneManagementSystem.FadeOutToNextScene());
    }

    #endregion

}