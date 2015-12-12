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
    [Tooltip("Fade out time should be kept short and NEVER exceed the TimerThreshold")]
    float FadeTime = 0.5f;
    [HideInInspector]
    float FadeoutThreshold;
    [SerializeField]
    GameObject SplashScreenObject;
    [Tooltip("Level names must be exactly that of those found in File->BuildSetings…")]
    [SerializeField]
    string LevelToLoad = null;
    [SerializeField]
    bool IsThisSceneSkippable = false;
    [HideInInspector]
    float Timer = 0f;

    [Header("Camera Properties")]
    [SerializeField]
    GameObject Camera;
    [SerializeField]
    float CameraStartingSize = 10f;
    [SerializeField]
    float CameraEndingSize = 11f;
    [HideInInspector]
    float CameraScaleFactor;
    [HideInInspector]
    bool IsAnimating = false;

    [Header("Fade Out Object")]
    [SerializeField]
    GameObject ScreenMask;
    [SerializeField]
    float FadeOutTime = 1f;
    [SerializeField]
    float FadeInTime = 1f;
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //find the camera in the scene
        this.Camera = GameObject.Find("Main Camera");

        //set the camera size
        this.Camera.GetComponent<Camera>().orthographicSize = this.CameraStartingSize;

        //maths to get the correct scaling factor
        this.CameraScaleFactor = ((this.CameraEndingSize - this.CameraStartingSize) * Time.deltaTime);
        //Debug.Log("Camera Scale Factor = " + this.CameraScaleFactor);

        //create FadeOutThreshold based on time chosen for total splash screen viewing
        this.FadeoutThreshold = this.TimerThreshold - this.FadeTime;

        //ensure bools are set right
        this.IsAnimating = false;

        //fade out the alpha value of the FadeOutObject
        this.FadeOutScreenMask(this.ScreenMask, this.FadeOutTime);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //load the next level if TimerThreshold is met
        if (this.Timer > this.TimerThreshold && !this.IsAnimating)
        {
            //set IsAnimating to true so we don't keep doing this
            this.IsAnimating = true;
            //Debug.Log("LOADING NEXT LEVEL: " + this.LevelToLoad);
            this.FadeInScreenMask(this.ScreenMask, this.FadeOutTime, this.LevelToLoad);
        }
        //load the next level if button is pressed and this level is skippable
        if (Input.anyKey && this.IsThisSceneSkippable && !this.IsAnimating)
        {
            //set IsAnimating to true so we don't keep doing this
            this.IsAnimating = true;
            //Debug.Log("LOADING NEXT LEVEL: " + this.LevelToLoad);
            this.FadeInScreenMask(this.ScreenMask, this.FadeOutTime, this.LevelToLoad);
        }

        if(this.Timer > this.FadeoutThreshold && !this.IsAnimating)
        {
            //set IsAnimating to true so we don't keep doing this
            this.IsAnimating = true;
            this.FadeOutSplashScreen(this.SplashScreenObject, this.FadeTime);
            this.FadeInScreenMask(this.ScreenMask, this.FadeOutTime, this.LevelToLoad);
        }

        //constant scaling out of camera
        this.ScaleOutCamera(this.Camera, this.CameraScaleFactor);

        //Debug.Log("Current Time = " + this.Timer + " and threshold is = " + this.TimerThreshold);
        //update the timer
        this.Timer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadNextLevel(scene)
    /////////////////////////////////////////////////////////////////////////*/
    void LoadNextLevel(string scene_)
    {
        Debug.Log("LOADING NEXT LEVEL: " + this.LevelToLoad);
        Application.LoadLevel(scene_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutScreenMask(GameObject, Float)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutScreenMask(GameObject obj_, float time_)
    {
        iTween.FadeTo(obj_, 0f, time_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInScreenMask(GameObject, Float, String)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeInScreenMask(GameObject obj_, float time_, string scene_)
    {
        Debug.Log("FADE IN SCREEN MASK REACHED");
        iTween.ColorTo(obj_, iTween.Hash(
                                        "Color", new Color(22/255,22/255,22/255,255/255),
                                        "time", 1f,
                                        "easetype", "easeInQuad",
                                        "oncompletetarget", this.gameObject,
                                        "oncomplete", "LoadNextLevel",
                                        "oncompleteparams", scene_));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ScaleOutCamera()
    /////////////////////////////////////////////////////////////////////////*/
    void ScaleOutCamera(GameObject camera_, float factor_)
    {
        camera_.GetComponent<Camera>().orthographicSize += factor_;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutSplashScreen()
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutSplashScreen(GameObject splash_, float time_)
    {
        iTween.FadeTo(splash_, iTween.Hash("alpha", 0f, 
                                           "time", time_, 
                                           "easetype", "easeOutQuad"));
    }
}
