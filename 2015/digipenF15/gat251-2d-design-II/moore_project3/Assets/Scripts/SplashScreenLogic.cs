/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SplashScreenLogic.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;


public class SplashScreenLogic : MonoBehaviour
{
    [Header("Splash Screen Properties")]
    [SerializeField]
    float TimerThreshold = 3f;
    [SerializeField]
    Vector3 EndScale = new Vector3(0.9f, 0.9f, 1f);
    [SerializeField]
    [Tooltip("Fade out time should be kept short and NEVER exceed the TimerThreshold")]
    float FadeTime = 0.5f;
    [HideInInspector]
    float FadeoutThreshold;
    [SerializeField]
    GameObject SplashScreenObj;
    [Tooltip("Level names must be exactly that of those found in File->BuildSetings…")]
    [SerializeField]
    string LevelToLoad = null;
    [SerializeField]
    bool IsThisSceneSkippable = false;
    [HideInInspector]
    float Timer = 0f;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //fade in SplashScreenObj
        iTween.FadeFrom(this.SplashScreenObj,
                        iTween.Hash("name", "SplashScreenFadeAnimation",
                                    "alpha", 0f,
                                    "amount", 1f,
                                    "time", this.TimerThreshold));
        //scale SplashScreenObj
        iTween.ScaleTo(this.SplashScreenObj, 
                       iTween.Hash("name", "SplashScreenScaleAnimation",
                                   "time", (this.TimerThreshold * 2),
                                   "scale", this.EndScale));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (this.Timer >= this.TimerThreshold)
        {
            this.LoadNextLevel(this.LevelToLoad);
        }
        if(Input.anyKeyDown && this.IsThisSceneSkippable)
        {
            this.LoadNextLevel(this.LevelToLoad);
        }

        //update the timer
        this.Timer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadNextLevel(scene)
    /////////////////////////////////////////////////////////////////////////*/
    void LoadNextLevel(string scene_)
    {
        Application.LoadLevel(scene_);
    }
}
