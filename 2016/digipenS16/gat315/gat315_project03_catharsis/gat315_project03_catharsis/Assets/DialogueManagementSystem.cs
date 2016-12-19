/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DialogueManagementSystem.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class DialogueManagementSystem : MonoBehaviour
{
    #region PROPERTIES

    //references
    [SerializeField]
    private GameObject DialogueCanvas;


    //attributes



    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {

    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        if (this.DialogueCanvas == null)
            this.DialogueCanvas = GameObject.FindWithTag("DialogueCanvas").gameObject;

        //turn on things that need to be on at the start
        if (!this.DialogueCanvas.activeSelf)
            this.DialogueCanvas.SetActive(true);
    }

    #endregion
}