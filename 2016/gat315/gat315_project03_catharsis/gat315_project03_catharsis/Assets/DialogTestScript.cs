/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DialogTestScript.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class DialogTestScript : MonoBehaviour
{
    #region PROPERTIES

    //references
    public GameObject ThingToTest;

    //attributes
    float Timer = 0.0f;


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

    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && this.Timer > 0.5f)
        {
            this.ThingToTest.GetComponent<DialoguePanelController>().AdvanceText();
        }

        if (Input.GetKeyDown(KeyCode.D) && this.Timer > 0.5f)
        {
           //this.ThingToTest.GetComponent<DialogueContainer>().SendDialogue();
        }

        this.Timer += Time.deltaTime;
    }

    #endregion

}