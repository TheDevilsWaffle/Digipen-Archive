/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CreditsScroller.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CreditsScroller : MonoBehaviour
{
    //PROPERITES
    GameObject Credits;
    Vector3 OriginalPos;
    Vector3 Destination;
    float Speed = 150f;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.Credits = GameObject.Find("Credits");
        this.OriginalPos = this.Credits.transform.position;
        this.Destination = this.OriginalPos + new Vector3(0f, 2400f, 0f);
        this.RollCredits(this.Credits, this.Destination, this.Speed);
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: RollCredits(GameObject, Vector3, float)
    /////////////////////////////////////////////////////////////////////////*/
    void RollCredits(GameObject obj_, Vector3 pos_, float speed_)
    {
        iTween.MoveTo(obj_, iTween.Hash("name", "CreditsAnimation",
                                          "position", pos_,
                                          "speed", speed_,
                                          "delay", 0.5f,
                                          "easetype", "linear",
                                          "looptype", "loop"));
    }
}
