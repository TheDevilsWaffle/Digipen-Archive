/*////////////////////////////////////////////////////////////////////////
//SCRIPT: NewButtonUI.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2015 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewButtonUI : MonoBehaviour
{
    //CURSOR RING PROPERTIES
    GameObject CursorRing;
    float Height;
    float Width;
    Vector3 Position;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get the cursor with the name "CursorDot"
        this.CursorRing = GameObject.Find("CursorRing").gameObject;

        //get properties of this button
        this.Height = this.gameObject.GetComponent<RectTransform>().rect.height;
        this.Width = this.gameObject.GetComponent<RectTransform>().rect.width;
        this.Position = this.gameObject.transform.position;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SizeUpCursorRing()
    ////////////////////////////////////////////////////////////////////*/
    public void IncreaseCursorRingSize()
    {
        this.CursorRing.GetComponent<CursorRingController>().DetermineCursorRingSize(this.Height, 
                                                                                     this.Width,
                                                                                     this.Position);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SizeOriginalCursorRing()
    ////////////////////////////////////////////////////////////////////*/
    public void OriginalCursorRingSize()
    {
        this.CursorRing.GetComponent<CursorRingController>().CursorExitAnimator();
    }
}
