/*////////////////////////////////////////////////////////////////////////
//SCRIPT: cs_SplashScreenController.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cs_SplashScreenController : MonoBehaviour
{
    #region PROPERTIES

    //references
    public GameObject SplashScreenImage;
    public Sprite[] Array_SplashImages;

    [Header("SPLASH ANIMATION")]
    public float FadeTime;
    public float ScaleInFactor;
    public float ScaleInTime;
    public float DelayBetweenSplashScreens;

    [Header("NEXT SCENE TO LOAD")]
    public string StartScene;

    [Header("SKIP SPLASH SCREEN CONTROLS")]
    public KeyCode KBSkipKey = KeyCode.A;

    //attributes
    private Vector3 SplashScreenOriginalScale;
    private int CurrentImage;
    private int TotalImages;
    private float Timer;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        if(SplashScreenImage == null)
            Debug.LogWarning("SplashScreenImage is not set, please set GameObject in Inspector");
    }

    /*////////////////////////////////////////////////////////////////////
    //Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize CurrentImage
        CurrentImage = 0;
        SplashScreenOriginalScale = SplashScreenImage.transform.localScale;

        //how many images in the array?
        TotalImages = Array_SplashImages.Length;

        //set initial splash screen image
        SplashScreenImage.GetComponent<Image>().sprite = Array_SplashImages[0];
        SplashScreenImage.GetComponent<CanvasGroup>().alpha = 0;

        //everything is set, start animating splashscreen
        AnimateFadeIn();
        AnimateScaleIn();
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //AnimateFadeIn()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateFadeIn()
    {
        //DEBUG
        //print("AnimateFadeIn()");
        LeanTween.alphaCanvas(SplashScreenImage.GetComponent<CanvasGroup>(), 1f, FadeTime).setOnComplete(AnimateFadeOut);
    }

    /*////////////////////////////////////////////////////////////////////
    //AnimateScaleIn(GameObject)
    ////////////////////////////////////////////////////////////////////*/
    void AnimateScaleIn()
    {
        //DEBUG
        //print("AnimateScaleIn()");

        LeanTween.scale(SplashScreenImage.GetComponent<RectTransform>(), (SplashScreenImage.transform.localScale * ScaleInFactor), ScaleInTime).setOnComplete(EvaluateSplashScreensRemaining);
    }

    /*////////////////////////////////////////////////////////////////////
    //AnimateFadeOut()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateFadeOut()
    {
        //DEBUG
        //print("AnimateFadeOut()");

        LeanTween.alphaCanvas(SplashScreenImage.GetComponent<CanvasGroup>(), 0f, FadeTime).setDelay(FadeTime * 2);
    }

    #endregion

    #region EVALUATION

    /*////////////////////////////////////////////////////////////////////
    //EvaluateSplashScreensRemaining()
    ////////////////////////////////////////////////////////////////////*/
    void EvaluateSplashScreensRemaining()
    {
        //DEBUG
        //print("EvaluateSplashScreensRemaining()");

        //increment to next image
        ++CurrentImage;
        //DEBUG
        //print("CurrentImage number is now " + CurrentImage);

        //are we still within the bounds of the array of splash screen images
        if (CurrentImage < TotalImages)
        {
            //reset splash screen scale/alpha
            StartCoroutine(ResetSplashScreen());
            //update to new splash screen image
            SplashScreenImage.GetComponent<Image>().sprite = Array_SplashImages[CurrentImage];
        }

        else
        {
            //DEBUG
            //print("NEXT LEVEL");

            //load the main menu
            SceneManager.LoadScene(StartScene);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //ResetSplashScreen()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator ResetSplashScreen()
    {
        //DEBUG
        //print("ResetSplashScreen()");

        yield return new WaitForSeconds(DelayBetweenSplashScreens);
        SplashScreenImage.transform.localScale = SplashScreenOriginalScale;
        SplashScreenImage.GetComponent<CanvasGroup>().alpha = 0f;
        AnimateFadeIn();
        AnimateScaleIn();
    }

    #endregion
}