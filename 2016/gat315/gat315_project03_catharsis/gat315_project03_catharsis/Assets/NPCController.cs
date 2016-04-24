﻿/*////////////////////////////////////////////////////////////////////////
//SCRIPT: NPCController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    #region PROPERTIES

    //references
    private DialogueContainer NPCDialougeContainer;
    private GameObject Player;

    //attributes



    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //CONTENT HERE
        this.NPCDialougeContainer = transform.parent.GetComponent<DialogueContainer>();
        this.Player = GameObject.Find("Player").gameObject;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //CONTENT HERE
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider2D_)
    {
        //if(collider2D_.gameObject = )
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_01()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_01()
    {
        //CONTENT HERE
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

    #endregion

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

    #endregion
}