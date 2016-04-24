/*////////////////////////////////////////////////////////////////////////
//SCRIPT: GooButtonController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class GooButtonController : MonoBehaviour
{
    #region PROPERTIES

    public GameObject Button;

    public Color ActiveColor;
    public Color InactiveColor;
    GameObject ObjectControlledByButton;
    public bool startTransparent;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        this.ObjectControlledByButton = this.gameObject.transform.Find("ButtonObject").gameObject;
    }

    void Start()
    {
        if(startTransparent)
        {
            this.AnimateTransparency_On(this.ObjectControlledByButton);
        }
    }

    #endregion INITIALIZATION

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider collider_)
    {
        GameObject obj;
        //make sure the object touching this thing 
        if (collider_.gameObject.transform.parent.gameObject != null)
        {
            obj = collider_.gameObject.transform.parent.gameObject;
            //if this is the large player
            if (obj.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.LARGE)
            {
                //do the button things
                //this.ObjectControlledByButton.GetComponent<ButtonAction>().

                //change this thing's material
                this.AnimateColor(this.ActiveColor);

                if (this.startTransparent)
                {
                    this.AnimateTransparency_Off(this.ObjectControlledByButton);
                }
                else
                {
                    this.AnimateTransparency_On(this.ObjectControlledByButton);
                }
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerExit(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider collider_)
    {
        
        GameObject obj;
        //make sure the object touching this thing 
        if (collider_.gameObject.transform.parent.gameObject != null)
        {
            obj = collider_.gameObject.transform.parent.gameObject;
            //if this is the large player
            if (obj.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.LARGE)
            {
                //do the button things
                //this.ObjectControlledByButton.GetComponent<ButtonAction>().

                //animate the buttons
                this.AnimateColor(this.InactiveColor);

                if (this.startTransparent)
                {
                    this.AnimateTransparency_On(this.ObjectControlledByButton);
                }
                else
                {
                    this.AnimateTransparency_Off(this.ObjectControlledByButton);
                }
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_03()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_03()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateTransparency_On(GameObject)
    ////////////////////////////////////////////////////////////////////*/
    void AnimateTransparency_On(GameObject obj_)
    {
        iTween.FadeTo(obj_,
                            iTween.Hash("name", "AnimateTransparency_On",
                                        "alpha", 0.05f,
                                        "time", 1f));
        obj_.GetComponent<BoxCollider>().enabled = false;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateTransparency_Off(GameObject)
    ////////////////////////////////////////////////////////////////////*/
    void AnimateTransparency_Off(GameObject obj_)
    {
        iTween.FadeTo(obj_,
                            iTween.Hash("name", "AnimateTransparency_Off",
                                        "alpha", 1f,
                                        "time", 1f));
        obj_.GetComponent<BoxCollider>().enabled = true;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateColor(Color color_)
    {
        //animate the exit
        iTween.ColorTo(this.Button,
                            iTween.Hash("name", "AnimateColor",
                                        "color", color_,
                                        "time", 0.25f,
                                        "includechildren", false));
    }

    #endregion X_FUNCTIONS

    #region ANIMATION

    #endregion ANIMATION
}