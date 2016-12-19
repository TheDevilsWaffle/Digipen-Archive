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

    MusicController SFXController;
    public AudioClip SFX_Victory;
    public AudioClip SFX_Defeat;

    //animation
    float FadeInTime = 1.5f;
    float FadeOutTime = 1.0f;

    float DelayTime = 3.5f;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //find the objects we need
        this.LevelCanvas = GameObject.Find("LevelCanvas").gameObject;
        this.Background = this.LevelCanvas.transform.Find("Background").gameObject;
        this.LevelText = this.LevelCanvas.transform.Find("LevelText").gameObject;
        this.Player1 = GameObject.FindWithTag("Player1").gameObject;
        this.Player2 = GameObject.FindWithTag("Player2").gameObject;
        this.SFXController = GameObject.FindWithTag("SFXSystem").GetComponent<MusicController>();

        this.TogglePlayerControls(false);

        //get the current scene
        this.CurrentScene = SceneManager.GetActiveScene();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set the level number
        this.LevelText.GetComponent<Text>().text = "LEVEL " + this.LevelNumber;
        //fade in the level
        StartCoroutine(this.FadeInLevel());
    }

    #endregion INITIALIZATION

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePlayerControls(bool)
    ////////////////////////////////////////////////////////////////////*/
    void TogglePlayerControls(bool areControlsEnabled_)
    {
        if(areControlsEnabled_)
        {
            //activate the player
            this.Player1.GetComponent<PlayerMachine>().WalkSpeed = 1f;
            this.Player1.GetComponent<PlayerMachine>().RotationSpeed = 2.5f;
            this.Player2.GetComponent<PlayerMachine>().WalkSpeed = 1f;
            this.Player2.GetComponent<PlayerMachine>().RotationSpeed = 2.5f;
        }
        else
        {
            //deactivate the player
            this.Player1.GetComponent<PlayerMachine>().WalkSpeed = 0f;
            this.Player1.GetComponent<PlayerMachine>().RotationSpeed = 0f;
            this.Player2.GetComponent<PlayerMachine>().WalkSpeed = 0f;
            this.Player2.GetComponent<PlayerMachine>().RotationSpeed = 0f;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInLevel()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator FadeInLevel()
    {
        yield return new WaitForSeconds(this.FadeInTime);
        this.Background.GetComponent<Image>().CrossFadeAlpha(0f, this.FadeInTime, false);
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(0f, this.FadeInTime, false);
        this.TogglePlayerControls(true);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutRestart()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeOutRestart()
    {
        this.LevelText.GetComponent<Text>().text = this.DefeatText;
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(1f, this.FadeInTime, false);
        this.Background.GetComponent<Image>().CrossFadeAlpha(1f, this.FadeInTime, false);

        //play defeat sfx
        this.SFXController.PlaySingle(this.SFX_Defeat);

        StartCoroutine(this.DelayBeforeRestart(this.DelayTime));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToNextLevel()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeOutToNextLevel()
    {
        //play victory sfx
        this.SFXController.PlaySingle(this.SFX_Victory);

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
        this.TogglePlayerControls(false);
        this.LevelText.GetComponent<Text>().text = this.VictoryText;
        this.LevelText.GetComponent<Text>().CrossFadeAlpha(1f, this.FadeInTime, false);
        this.FadeOutToNextLevel();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_04()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_04()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_05()
    {
        //CONTENT HERE
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