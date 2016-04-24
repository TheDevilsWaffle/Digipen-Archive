/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CreditsScroller.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CreditsScroller : MonoBehaviour
{
    //PROPERITES
    Vector3 OriginalPos;
    Vector3 Destination = new Vector3(0f, 1500f, 0f);

    [SerializeField]
    private MenuObject CreditsButton;

    [SerializeField]
    private float ScrollTime = 7f;
    [SerializeField]
    private float ScrollDelay = 0.5f;

    UIAnimations UIAnimation;


    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get UIAnimation
        this.UIAnimation = this.gameObject.GetComponent<UIAnimations>();

        //get OriginalPos
        this.OriginalPos = this.gameObject.transform.localPosition;

        //subscribe to the buttons
        this.CreditsButton.Subscribe(MenuObject.ActivationState.Selected, this.RollCredits);
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: RollCredits()
    /////////////////////////////////////////////////////////////////////////*/
    private void RollCredits()
    {
        iTween.Stop(this.gameObject);
        this.gameObject.transform.localPosition = this.OriginalPos;
        this.UIAnimation.MoveTo(this.gameObject, this.Destination, this.ScrollTime, this.ScrollDelay, "linear");
    }
}
