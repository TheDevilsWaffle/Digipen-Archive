/*////////////////////////////////////////////////////////////////////////
//SCRIPT: LevelController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class LevelController : MonoBehaviour
{
    #region PROPERTIES

    GameObject LevelCanvas;
    GameObject Background;
    public string LevelNumber;
    GameObject LevelText;
    
    public string VictoryText;
    public string DefeatText;

    Scene CurrentScene;
    public string NextLevel;
    public string PreviousLevel;

    GameObject Player1;
    GameObject Player2;

    public AudioClip SFX_Victory;
    public AudioClip SFX_Defeat;

    //animation
    float FadeInTime = 1.5f;
    float FadeOutTime = 1.0f;

    float DelayTime = 3.5f;

    [SerializeField]
    private MenuSystem MenuSystem;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //find the objects we need

        //get the current scene
        this.CurrentScene = SceneManager.GetActiveScene();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set the level number
        //fade in the level
        //StartCoroutine(this.FadeInLevel());
    }

    #endregion INITIALIZATION

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInLevel()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator FadeInLevel()
    {
        yield return new WaitForSeconds(this.FadeInTime);
        this.Background.GetComponent<Image>().CrossFadeAlpha(0f, this.FadeInTime, false);
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(0f, this.FadeInTime, false);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutRestart()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeOutRestart()
    {
        this.LevelText.GetComponent<Text>().text = this.DefeatText;
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(1f, this.FadeInTime, false);
        this.Background.GetComponent<Image>().CrossFadeAlpha(1f, this.FadeInTime, false);

        StartCoroutine(this.DelayBeforeRestart(this.DelayTime));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToNextLevel()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeOutToNextLevel()
    {
        this.Background.GetComponent<Image>().CrossFadeAlpha(1f, this.FadeInTime, false);

        StartCoroutine(this.DelayBeforeLoad(this.DelayTime));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToPreviousLevel()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeOutToPreviousLevel()
    {
        this.Background.GetComponent<Image>().CrossFadeAlpha(1f, this.FadeInTime, false);

        StartCoroutine(this.DelayBeforeLoadPrevious(this.DelayTime));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: LevelComplete(string)
    ////////////////////////////////////////////////////////////////////*/
    public void LevelComplete()
    {
        //immediate change the LevelText
        this.LevelText.GetComponent<Text>().text = this.VictoryText;
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(1f, this.FadeInTime, false);
        this.FadeOutToNextLevel();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: RetryScreen()
    ////////////////////////////////////////////////////////////////////*/
    public void RetryScreen()
    {
        //take away player input for the game
        this.MenuSystem.DisplayTryAgain();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: RetryScreen()
    ////////////////////////////////////////////////////////////////////*/
    public void WinScreen()
    {
        //take away player input for the game
        this.MenuSystem.DisplayWinMenu();
    }

    #endregion X_FUNCTIONS

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DelayBeforeLoad(float, string)
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator DelayBeforeLoad(float delay_)
    {
        yield return new WaitForSeconds(delay_);
        SceneManager.LoadScene(this.NextLevel);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DelayBeforeRestart(float)
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator DelayBeforeRestart(float delay_)
    {
        yield return new WaitForSeconds(delay_);
        SceneManager.LoadScene(this.CurrentScene.name);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DelayBeforeLoadPrevious(float)
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator DelayBeforeLoadPrevious(float delay_)
    {
        yield return new WaitForSeconds(delay_);
        SceneManager.LoadScene(this.PreviousLevel);
    }

    #endregion ANIMATION
}