/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SplashScreenController.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    bool IsSceneSkippable;
    [SerializeField]
    GameObject SplashImage;
    [SerializeField]
    float FadeTime = 0.5f;
    [SerializeField]
    float SplashScreenDuration = 3.0f;
    [SerializeField]
    Vector3 EndScale = new Vector3(1.5f, 1.5f, 1f);
    [SerializeField]
    string NameOfNextSceneToLoad;
    [SerializeField]
    string NameOfStartSplashScene;

    float Timer;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure we've got the names of the levels we need
        if(this.NameOfNextSceneToLoad == null)
        {
            Debug.LogError("ERROR! — Provide name of NameOfNextSceneToLoad!");
        }
        if (this.NameOfStartSplashScene == null)
        {
            Debug.LogError("ERROR! — Provide name of NameOfMainMenuScene!");
        }
        //ensure we also have the image we are supposed to be animating
        if(this.SplashImage != null)
        {
            this.AnimateSplashImage(this.SplashImage, 
                                    this.FadeTime, 
                                    this.SplashScreenDuration, 
                                    this.EndScale);
        }

        //initialize timer at 0f
        this.Timer = 0f;

        //start animating the 
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //if timer has passed threshold, load the next level
        if(this.Timer >= this.SplashScreenDuration)
        {
            SceneManager.LoadScene(this.NameOfNextSceneToLoad);
        }

        //if user has pressed any keyboard or mouse button
        //***NOTE*** any gamepad button will need to be added here in the future
        else if(this.IsSceneSkippable && Input.anyKeyDown)
        {
            SceneManager.LoadScene(this.NameOfStartSplashScene);
        }

        //update the timer
        this.Timer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateSplashImage(GameObject, float, float, Vector3)
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateSplashImage(GameObject obj_, float fadetime_, float duration_, Vector3 scale_)
    {
        //fade in SplashScreenObj
        iTween.FadeFrom(obj_,
                        iTween.Hash("name", "SplashScreenFadeAnimation",
                                    "alpha", 1f,
                                    "amount", 0f,
                                    "time", (duration_ / 1.5f)));
        //scale SplashScreenObj
        iTween.ScaleTo(obj_,
                       iTween.Hash("name", "SplashScreenScaleAnimation",
                                   "time", duration_,
                                   "scale", scale_));
        //scale SplashScreenObj
        iTween.MoveTo(obj_,
                      iTween.Hash("name", "SplashScreenMove",
                                  "time", duration_,
                                  "position", obj_.transform.localPosition + new Vector3(0f, 1f, 0f)));

    }
}
