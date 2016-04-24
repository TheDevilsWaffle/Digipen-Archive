/*////////////////////////////////////////////////////////////////////////
//SCRIPT: PlayerDialogueController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class PlayerDialogueController : MonoBehaviour
{
    #region PROPERTIES

    //references

    //attributes
    [HideInInspector]
    private MyPlatformerController s_MyPlatformController;
    [HideInInspector]
    public DialogueContainer NPCDialogueContainer;


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //CONTENT HERE
        this.s_MyPlatformController = transform.parent.gameObject.GetComponent<MyPlatformerController>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //CONTENT HERE
    }

    #endregion

    #region TRIGGER2D

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider2D_)
    {
        //print(collider2D_.gameObject.name);
        if(collider2D_.gameObject.GetComponent<DialogueContainer>() != null)
        {
            //DEBUG
            //print("we can talk to this guy");
            this.s_MyPlatformController.IsByNPC = true;
            this.s_MyPlatformController.NPCDialogueContainer = collider2D_.gameObject.GetComponent<DialogueContainer>();
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerExit2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit2D(Collider2D collider2D_)
    {
        if (collider2D_.gameObject.GetComponent<DialogueContainer>() != null)
        {
            this.s_MyPlatformController.IsByNPC = false;
            this.s_MyPlatformController.NPCDialogueContainer = null;
        }
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