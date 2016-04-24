/*////////////////////////////////////////////////////////////////////////
//SCRIPT: ResetScene.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using System.Linq;

public class ResetScene : MonoBehaviour
{
    #region PROPERTIES

    //references
    private GameObject Player;
    private Vector3 Player_OriginalPos;

    private GameObject MonologueTriggers;
    
    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.Player = GameObject.FindWithTag("Player").gameObject;
        this.Player_OriginalPos = this.Player.transform.position;

        if(GameObject.FindWithTag("MonologueTriggers").gameObject != null)
            this.MonologueTriggers = GameObject.FindWithTag("MonologueTriggers").gameObject;
        this.ResetSceneObjects();
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: ResetSceneObjects()
    ////////////////////////////////////////////////////////////////////*/
    public void ResetSceneObjects()
    {
        //reset all monologue Triggers
        if (this.MonologueTriggers != null)
        {
            foreach (Transform child in this.MonologueTriggers.transform)
            {
                //print("child was " + child.gameObject.GetComponent<DialogueContainer>().HasBeenSpokenTo);
                child.gameObject.GetComponent<DialogueContainer>().SetHasBeenSpokenToFalse();
                //print("now child is " + child.gameObject.GetComponent<DialogueContainer>().HasBeenSpokenTo);
            }
        }

        //reset the player
        this.Player.transform.position = this.Player_OriginalPos;
    }

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