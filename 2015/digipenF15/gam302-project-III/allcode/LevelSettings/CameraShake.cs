/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CameraShake.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    Vector3 ShakeAmount = new Vector3(0.5f, 0.25f, 0.5f);
    [SerializeField]
    float ShakeDuration = 0.35f;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    public void ShakeCamera()
    {
        //Debug.Log("SHAKING THE CAMERA");
        iTween.ShakePosition(this.MainCamera.gameObject, iTween.Hash("name", "CameraShakeAnimation",
                                                          "amount", this.ShakeAmount,
                                                          "time", this.ShakeDuration,
                                                          "islocal", true,
                                                          "orienttopath", true));
    }
}