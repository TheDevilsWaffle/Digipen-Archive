/*////////////////////////////////////////////////////////////////////////
//SCRIPT: CursorRingController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2015 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CursorRingController : MonoBehaviour
{
    [SerializeField]
    GameObject Cursor;
    Vector3 CursorPos;
    Vector3 CursorRingPos;
    Vector3 CursorRingOriginalSize;
    [SerializeField]
    float Speed = 20.0f;
    float ScaleMultiplier = 1.5f;

    //ANIMATION
    float AnimationTime = 0.5f;

    bool IsCursorRingLocked;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get cursor ring things
        this.CursorRingPos = this.gameObject.transform.position;
        this.CursorRingOriginalSize = this.gameObject.transform.localScale;

        //set bools
        this.IsCursorRingLocked = false;

        //try to find the cursor if it isn't linked
        if (this.Cursor == null)
        {
            Debug.LogWarning("CURSOR not assigned, attempting to find 'CursorDot'.Please assign a gameobject to 'Cursor'");
            this.Cursor = GameObject.Find("CursorDot").gameObject;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: LateUpdate()
    ////////////////////////////////////////////////////////////////////*/
    void LateUpdate()
    {
        //ensure we know where we are and the parent cursor is
        this.CursorPos = this.Cursor.transform.position;
        this.CursorRingPos = this.gameObject.transform.position;

        //as long as we're not locked by the bool
        if (!this.IsCursorRingLocked)
        {
            //do the lerp
            this.gameObject.transform.position = Vector3.Lerp(this.CursorRingPos, this.CursorPos, this.Speed * Time.deltaTime);
        }
    }
    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DetermineCursorRingSize(float buttonHeight_, float buttonWidth_, Vector3 pos_)
    ////////////////////////////////////////////////////////////////////*/
    public void DetermineCursorRingSize(float buttonHeight_, float buttonWidth_, Vector3 pos_)
    {
        //lock the CursorRing's movement using bool
        this.IsCursorRingLocked = true;

        //get CursorRing current size
        float cursorRingHeight = this.gameObject.GetComponent<RectTransform>().rect.height;
        float cursorRingWidth = this.gameObject.GetComponent<RectTransform>().rect.width;

        //compare these values with passed parameters of the button
        float newHeight = cursorRingHeight / buttonHeight_;
        float newWidth = cursorRingWidth / buttonWidth_;

        //DEBUG - what are the above values?
        //Debug.Log("CursorRingHeight = " + newHeight + " (" + cursorRingHeight + " / " + buttonHeight_ + ")");
        //Debug.Log("CursorRingWidth = " + newWidth + " (" + cursorRingWidth + " / " + buttonWidth_ + ")");

        //move the cursor ring to the center point of the button
        this.CursorMovePosAnimator(pos_, this.AnimationTime);

        //use the larger dimension
        if(newHeight > newWidth)
        {
            //pass new values to CursorScaleAnimator
            this.CursorEnterAnimator(newHeight, this.ScaleMultiplier, this.AnimationTime);
        }
        else
        {
            //pass new values to CursorScaleAnimator
            this.CursorEnterAnimator(newWidth, this.ScaleMultiplier, this.AnimationTime);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CursorMovePosAnimator(Vector2 pos_, float time_)
    ////////////////////////////////////////////////////////////////////*/
    void CursorMovePosAnimator(Vector3 pos_, float time_)
    {
        iTween.MoveTo(this.gameObject,
                      iTween.Hash("name", "CursorMovePosAnimator",
                                  "position", pos_,
                                  "time", time_,
                                  "easetype", "easeInOutQuad"));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CursorEnterAnimator(float buttonScale_, float scaleMultiplier_, float time_)
    ////////////////////////////////////////////////////////////////////*/
    void CursorEnterAnimator(float buttonScale_, float scaleMultiplier_, float time_)
    {
        iTween.ScaleTo(this.gameObject,
                       iTween.Hash("name", "CursorScaleAnimator",
                                   "scale", new Vector3((buttonScale_ * scaleMultiplier_), (buttonScale_ * scaleMultiplier_), 1f),
                                   "time", time_,
                                   "easetype", "easeInOutQuad"));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CursorScaleAnimator(float buttonScale_, float scaleMultiplier_, float time_)
    ////////////////////////////////////////////////////////////////////*/
    public void CursorExitAnimator()
    {
        //unlock the cursor ring using the bool
        this.IsCursorRingLocked = false;

        iTween.ScaleTo(this.gameObject,
                       iTween.Hash("name", "CursorExitAnimator",
                                   "scale", this.CursorRingOriginalSize,
                                   "time", this.AnimationTime,
                                   "easetype", "easeInOutQuad"));
    }
}
