/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PauseSystem.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class PauseSystem : MonoBehaviour
{
    #region PROPERTIES

    //pause canvas elements
    GameObject PauseMenu;
    GameObject PauseTitle;
    GameObject PauseBackground;
    GameObject CurrentMenu;
    GameObject AudioController;

    //bools
    bool IsGamePaused;
    bool PauseButtonInUse;

    //buttons to pause the game
    KeyCode KBPauseButton = KeyCode.Escape;
    KeyCode GPPauseButton = KeyCode.JoystickButton7;

    //animation
    float FadeInTime = 0.2f;
    float FadeOutTime = 0.2f;
    float AnimatePosTime = 0.2f;
    Vector3 CanvasCenter;
    Vector3 CanvasExit;
    
    //players
    GameObject Player1;
    GameObject Player2;

    GameObject MusicController;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    /////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get all pause elements & menus
        this.PauseTitle = this.gameObject.transform.Find("PauseTitle").gameObject;
        this.PauseBackground = this.gameObject.transform.Find("PauseBackground").gameObject;
        this.PauseMenu = this.gameObject.transform.Find("PauseMenu").gameObject;

        this.MusicController = GameObject.FindWithTag("SFXSystem").gameObject;

        //set canvas specifics (NEED THIS TO POSITION ELEMENTS PROPERTLY)
        this.CanvasExit = this.gameObject.transform.localPosition - new Vector3(0f, 1000f, 0f);
        this.CanvasCenter = this.gameObject.transform.localPosition;

        //initialize variables if we reload
        this.IsGamePaused = false;
        //this.EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        this.Player1 = GameObject.Find("Player1").gameObject;
        this.Player2 = GameObject.Find("Player2").gameObject;

        //get the audio controller
        //this.AudioController = GameObject.Find("AudioController").gameObject;

        this.AnimatePauseCanvasObjects(false);
        this.TogglePauseElement(this.PauseMenu);
    }

    #endregion INITIALIZATION

    #region UPDATE

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(this.KBPauseButton) || Input.GetKeyDown(this.GPPauseButton))
        {
            this.TogglePause();
        }
        else
        {
            this.PauseButtonInUse = false;
        }
    }

    #endregion UPDATE

    #region TOGGLES

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePause()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void TogglePause()
    {
        if (!this.IsGamePaused)
        {
            //ensure we're not holding down the button
            if (!this.PauseButtonInUse)
            {
                //pause the game
                //Time.timeScale = 0f;

                //set bools
                this.IsGamePaused = true;
                this.PauseButtonInUse = true;

                //deactivate the player
                this.TogglePlayerControls(false);

                this.MusicController.GetComponent<AudioSource>().Pause();

                this.AnimatePauseCanvasObjects(true);
                this.TogglePauseElement(this.PauseMenu);
                //animate PauseMenu into place
                this.AnimatePosition(this.PauseMenu, this.CanvasCenter);

                this.SetSelectedButton(this.PauseMenu.GetComponent<FirstButtonSelected>().ButtonToSelectFirst.gameObject);



                //set the current menu
                this.CurrentMenu = this.PauseMenu;
            }
        }
        else
        {
            if (!this.PauseButtonInUse)
            {
                //unpause the game
                //Time.timeScale = 1f;
                //set bools
                this.IsGamePaused = false;
                this.PauseButtonInUse = true;

                this.MusicController.GetComponent<AudioSource>().UnPause();

                //animate PauseMenu into place
                this.AnimatePosition(this.PauseMenu, this.CanvasExit);
                this.AnimatePauseCanvasObjects(false);

                //wait before deactivating
                StartCoroutine(this.DelayDeactvation(this.CurrentMenu, this.AnimatePosTime));
                this.CurrentMenu = null;
            }

        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePauseElement(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void TogglePauseElement(GameObject element_)
    {
        //shortcut to element's CanvasGroup
        CanvasGroup elementCG = element_.GetComponent<CanvasGroup>();

        if (element_.activeSelf != true)
        {
            //make it active
            element_.SetActive(true);

            //set the first selected button
            EventSystem.current.SetSelectedGameObject(element_.GetComponent<FirstButtonSelected>().ButtonToSelectFirst);

            //set alpha to 0, then fade it in
            elementCG.alpha = 0f;
            while (elementCG.alpha < 1f)
            {
                elementCG.alpha += Time.deltaTime * this.FadeInTime;
            }
        }
        else
        {
            //set alpha to 1, then fade it out
            elementCG.alpha = 1f;
            while (elementCG.alpha > 0f)
            {
                elementCG.alpha -= Time.deltaTime * this.FadeInTime;
            }

            //make it inactive
            element_.SetActive(false);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleMouseCursor()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void ToggleMouseCursor(bool pause_)
    {
        if (pause_)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePlayerControls(bool)
    ////////////////////////////////////////////////////////////////////*/
    void TogglePlayerControls(bool areControlsEnabled_)
    {
        if (areControlsEnabled_)
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

    #endregion TOGGLES

    #region MENU SWITCHING
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SwitchMenus(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void SwitchMenus(string pauseMenuToSwitchTo_)
    {
        //animate out the current menu
        this.AnimatePosition(this.CurrentMenu, this.CanvasExit);
        //StartCoroutine(this.DelayDeactvation(this.CurrentMenu, this.AnimatePosTime));

        //animate in the new menu
        this.CurrentMenu = this.gameObject.transform.Find(pauseMenuToSwitchTo_).gameObject;
        this.AnimatePosition(this.CurrentMenu, this.CanvasCenter);

        //call function to set the right button
        this.SetSelectedButton(this.CurrentMenu.GetComponent<FirstButtonSelected>().ButtonToSelectFirst.gameObject);
    }

    #endregion MENU SWITCHING

    #region SET SELECTED BUTTON

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetSelectedButton(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void SetSelectedButton(GameObject btn_)
    {
        //set to null (precaution)
        EventSystem.current.SetSelectedGameObject(null);

        //set first btn to passed btn
        EventSystem.current.SetSelectedGameObject(btn_);

        //ensure the button is "highlighted"
        btn_.GetComponent<Button>().Select();
    }

    #endregion SET SELECTED BUTTON

    #region ANIMATION

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimatePauseCanvasObjects(bool)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimatePauseCanvasObjects(bool isFadeIn_)
    {
        if (isFadeIn_)
        {
            this.PauseTitle.GetComponent<Text>().CrossFadeAlpha(1f, 0.2f, true);
            this.PauseBackground.GetComponent<Image>().CrossFadeAlpha(0.8f, 0.2f, true);
        }
        else
        {
            this.PauseTitle.GetComponent<Text>().CrossFadeAlpha(0f, 0.2f, true);
            this.PauseBackground.gameObject.GetComponent<Image>().CrossFadeAlpha(0f, 0.2f, true);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimatePosition(GameObject, Vector3)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void AnimatePosition(GameObject obj_, Vector3 pos_)
    {
        iTween.MoveTo(obj_,
                      iTween.Hash("name", "AnimateIntoPosition",
                                  "position", pos_,
                                  "time", this.AnimatePosTime,
                                  "islocal", true,
                                  "easetype", "easeInOutQuad",
                                  "ignoretimescale", true));
    }

    #endregion ANIMATION

    #region IENUMERATORS

    /*/////////////////////////////////////////////////////////////////////////
    //IENUMERATOR: DelayDeactvation(GameObject, float)
    /////////////////////////////////////////////////////////////////////////*/
    IEnumerator DelayDeactvation(GameObject menu_, float delayTime_)
    {
        yield return new WaitForSeconds(delayTime_);
        menu_.SetActive(false);

        //activate the player
        this.TogglePlayerControls(true);
    }

    #endregion IENUMERATORS
}