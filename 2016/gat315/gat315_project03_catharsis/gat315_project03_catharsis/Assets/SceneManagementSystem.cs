/*////////////////////////////////////////////////////////////////////////
//SCRIPT: SceneManagementSystem.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagementSystem : MonoBehaviour
{
    #region PROPERTIES
    [Header("PLAYER REFERENCE")]
    [SerializeField]
    private MyPlatformerController s_MyPlatformerController;


    [Header("HUD CANVAS")]
    [SerializeField]
    private GameObject HUDCanvas;

    [Header("SCENE FADER")]
    [SerializeField]
    private Image Background;
    [SerializeField]
    private float FadeInTime = 1f;
    [SerializeField]
    private float FadeOutTime = 1f;
    
    [Header("INTRO/OUTRO TEXT")]
    [SerializeField]
    private Text SceneText;
    [SerializeField]
    private string Intro_Text;
    [SerializeField]
    private float DelayTime_ReadText = 1.5f;
    [SerializeField]
    private float DelayTime_Intro = 1f;
    [SerializeField]
    private string Outro_Text;
    [SerializeField]
    private float DelayTime_Outro = 1f;

    [Header("NEXT/PREVIOUS SCENE")]
    [SerializeField]
    public string NextScene;
    [SerializeField]
    private string PreviousScene;
    [SerializeField]
    private float DelayTime_SceneLoad = 1.5f;

    #endregion

    #region INITIALIZATION
    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //turn on the hudcanvas if it's left off initially
        if(this.HUDCanvas.activeSelf == false)
        {
            this.HUDCanvas.SetActive(true);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //disable player movement at the start of the game
        this.s_MyPlatformerController.PlayerCanMove = false;

        //at the start of this scene, fade in the scene
        StartCoroutine(this.FadeInScene());
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInScene()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeInScene()
    {
        //set the text
        this.SceneText.text = this.Intro_Text;

        //ensure background is already set
        this.Background.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.Background.CrossFadeAlpha(1f, 0.001f, false);
        //display text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.SceneText.CrossFadeAlpha(1f, this.FadeInTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_ReadText);

        //fade out background & text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.SceneText.CrossFadeAlpha(0f, this.FadeOutTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_ReadText);

        //fade out background
        this.Background.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.Background.CrossFadeAlpha(0f, this.FadeOutTime, false);

        //allow the player to move
        this.s_MyPlatformerController.PlayerCanMove = true;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToNextScene()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeOutToNextScene()
    {
        //set the text
        this.SceneText.text = this.Outro_Text;

        //fade in background
        this.Background.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.Background.CrossFadeAlpha(1f, this.FadeInTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_Outro);

        //display text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.SceneText.CrossFadeAlpha(1f, this.FadeInTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_Intro);

        //fade out background & text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.SceneText.CrossFadeAlpha(0f, this.FadeOutTime, false);

        //wait before loading the next scene
        yield return new WaitForSeconds(this.FadeOutTime);

        //load the next scene
        SceneManager.LoadScene(this.NextScene);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToPreviousScene()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeOutToPreviousScene()
    {
        //set the text
        this.SceneText.text = this.Outro_Text;

        //fade in background
        this.Background.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.Background.CrossFadeAlpha(1f, this.FadeInTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_Outro);

        //display text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.SceneText.CrossFadeAlpha(1f, this.FadeInTime, false);

        //delay
        yield return new WaitForSeconds(this.DelayTime_ReadText);

        //fade out background & text
        this.SceneText.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.SceneText.CrossFadeAlpha(0f, this.FadeOutTime, false);
        this.Background.GetComponent<CanvasRenderer>().SetAlpha(1f);
        this.Background.CrossFadeAlpha(0f, this.FadeOutTime, false);

        //wait before loading the next scene
        yield return new WaitForSeconds(this.FadeOutTime * this.DelayTime_SceneLoad);

        //load the next scene
        SceneManager.LoadScene(this.PreviousScene);
    }

    #endregion
}