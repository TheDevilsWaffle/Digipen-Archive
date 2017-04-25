///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — SplashScreenSystem.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class SplashScreenSystem : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    Sprite[] splashSprites;
    [SerializeField]
    RectTransform splash_rt;
    Image splash_img;
    CanvasGroup splash_cg;

    [Header("SPLASH ANIMATION")]
    public float fadeTime = 0.25f;
    public float scaleInFactor = 1.15f;
    public float scaleInTime = 3.5f;
    public float delayBeforeNextSplash = 0.1f;

    [Header("NEXT SCENE TO LOAD")]
    public string startScene = "SCENE_HERE";

    [Header("SKIP SPLASH SCREEN CONTROLS")]
    public KeyCode kbSkip = KeyCode.A;

    //attributes
    private Vector3 originalScale;
    private int currentImage;
    private int totalImages;
    private float timer = 0f;
    float timerThreshold = 3.5f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        splash_img = splash_rt.GetComponent<Image>();
        splash_cg = splash_rt.GetComponent<CanvasGroup>();

        currentImage = 0;
        originalScale = splash_rt.localScale;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //how many images in the array?
        totalImages = splashSprites.Length;

        //set initial splash screen image
        splash_img.sprite = splashSprites[0];
        splash_cg.alpha = 0;

        //everything is set, start animating splashscreen
        AnimateSplashScreen();
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        timer += Time.deltaTime;
        if (GamePadInput.players[0].A == GamePadButtonState.PRESSED
            || Input.GetKeyDown(kbSkip) == true
            && timer > timerThreshold)
        {
            SceneManager.LoadScene(startScene);
        }
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateSplashScreen()
    {
        LeanTween.alphaCanvas(splash_cg, 1f, fadeTime).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(splash_rt, (splash_rt.localScale * scaleInFactor), scaleInTime + (fadeTime * 2))
                 .setOnComplete(EvaluateSplashScreensRemaining);
        LeanTween.alphaCanvas(splash_cg, 0f, fadeTime).setEase(LeanTweenType.easeOutQuad).setDelay(scaleInTime + fadeTime);
    }
    void EvaluateSplashScreensRemaining()
    {
        //DEBUG
        //print("EvaluateSplashScreensRemaining()");

        //increment to next image
        ++currentImage;
        //DEBUG
        //print("CurrentImage number is now " + CurrentImage);

        //are we still within the bounds of the array of splash screen images
        if (currentImage < totalImages)
        {
            //reset splash screen scale/alpha
            StartCoroutine(ResetSplashScreen());
            //update to new splash screen image
            splash_img.sprite = splashSprites[currentImage];
        }

        else
        {
            //DEBUG
            //print("NEXT LEVEL");

            //load the main menu
            SceneManager.LoadScene(startScene);
        }
    }
    IEnumerator ResetSplashScreen()
    {
        //DEBUG
        //print("ResetSplashScreen()");

        yield return new WaitForSeconds(delayBeforeNextSplash);
        splash_rt.localScale = originalScale;
        splash_cg.alpha = 0f;
        AnimateSplashScreen();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
		//remove listeners
	}
    #endregion
}
