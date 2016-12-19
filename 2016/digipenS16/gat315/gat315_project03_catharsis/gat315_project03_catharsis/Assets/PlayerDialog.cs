/*////////////////////////////////////////////////////////////////////////
//SCRIPT: PlayerDialog.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDialog : MonoBehaviour
{
    #region PROPERTIES

    //references
    public Text Dialog;
    private int CurrentMessageIndex = 0;
    private float DisplayTimer = 0f;
    private bool MessageDisplayed;

    //attributes
    public string[] MessageArray;
    public string Message01 = "I must retrieve the everlasting battery before I run out of energy!";
    public string Message02 = "I can jump over these obstacles with the 'A' button!";
    public string Message03 = "I better watch out for dangerous spikes!";
    public string Message04 = "I can run to clear this big pit using the 'LT' button!";
    public string Message05 = "This cave is dark, I can press the Right Analog Stick to turn my flashligh on and off!";
    public string Message06 = "There it is! The battery!";
    public string Message07 = "OH NO! I BETTER GET OUT OF HERE!";


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //CONTENT HERE
        this.MessageArray = new string[] { Message01, Message02, Message03, Message04, Message05, Message06, Message07 };
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DisplayDialog()
    ////////////////////////////////////////////////////////////////////*/
    public void DisplayDialog()
    {
        this.DisplayTimer = 5f;
        this.Dialog.text = this.MessageArray[this.CurrentMessageIndex];
        ++this.CurrentMessageIndex;
        this.Dialog.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.Dialog.CrossFadeAlpha(1f, 0.5f, false);
        this.MessageDisplayed = true;
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.DisplayTimer > 0)
        {
            this.DisplayTimer -= Time.deltaTime;
        }
        else if(this.DisplayTimer <= 0f && this.MessageDisplayed)
        {
            this.MessageDisplayed = false;
            this.Dialog.GetComponent<CanvasRenderer>().SetAlpha(1f);
            this.Dialog.CrossFadeAlpha(0f, 0.5f, false);
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