/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SplashScreenLogic.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SplashScreenLogic : MonoBehaviour
{
    [Header("Splash Screen Properties")]
    [SerializeField]
    float TimerThreshold = 3f;
    [SerializeField]
    Vector3 EndScale = new Vector3(1.5f, 1.5f, 1f);
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
    bool IsSceneSkippable = false;
    [HideInInspector]
    float Timer = 0f;
    string MainMenu;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //name the main menu scene
        this.MainMenu = "sce_mainMenu";

        //null check
        if(this.SplashScreenObj == null)
        {
            this.SplashScreenObj = GameObject.Find("SplashScreenImage");
        }

        //animate splash screen object
        this.AnimateSplashScreen(this.SplashScreenObj, 
                                 this.TimerThreshold, 
                                 this.EndScale);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //load next splash screen if time is up
        if (this.Timer >= this.TimerThreshold)
        {
            this.LoadScene(this.LevelToLoad);
        }

        //skip all remaining splash screens if button is pressed
        if(Input.anyKeyDown && this.IsSceneSkippable)
        {
            this.LoadScene(this.MainMenu);
        }

        //update the timer
        this.Timer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadScene(string)
    /////////////////////////////////////////////////////////////////////////*/
    void LoadScene(string scene_)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateSplashScreen(GameObject, float, Vector3)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateSplashScreen(GameObject obj_, float timer_, Vector3 scale_)
    {
        //fade in SplashScreenObj
        iTween.FadeFrom(obj_,
                        iTween.Hash("name", "SplashScreenFadeAnimation",
                                    "alpha", 0f,
                                    "amount", 1f,
                                    "time", timer_));
        //scale SplashScreenObj
        iTween.ScaleTo(obj_,
                       iTween.Hash("name", "SplashScreenScaleAnimation",
                                   "time", (timer_ * 2),
                                   "scale", scale_));
        //scale SplashScreenObj
        iTween.MoveTo(obj_,
                      iTween.Hash("name", "SplashScreenMove",
                                  "time", (timer_ * 2),
                                  "position", obj_.transform.localPosition + new Vector3(0f, 1f, 0f)));
    }
}
