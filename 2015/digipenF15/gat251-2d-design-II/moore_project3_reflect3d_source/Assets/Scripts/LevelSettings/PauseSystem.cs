/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PauseSystem.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseSystem : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    GameObject PauseCanvas;
    [SerializeField]
    GameObject MainMenuConfirmation;
    [SerializeField]
    GameObject MainMenuButton;
    [SerializeField]
    GameObject NoButton;
    GameObject EventSystemObj;

    [SerializeField]
    KeyCode KBPauseButton = KeyCode.P;
    [SerializeField]
    KeyCode GPPauseButton = KeyCode.JoystickButton7;

    //HIDDEN PROPERTIES
    bool IsGamePaused;
    bool PauseButtonInUse;
    GameObject HUDCanvas;
    GameObject AudioController;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize variables if we reload
        this.IsGamePaused = false;
        this.PauseCanvas.SetActive(false);
        this.EventSystemObj = GameObject.Find("EventSystem");

        //get the audio controller
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
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
                Time.timeScale = 0f;
                
                //set bools
                this.IsGamePaused = true;
                this.PauseButtonInUse = true;

                //toggle PauseMenu
                this.TogglePauseMenu(this.IsGamePaused);
                //toggle MouseCursor
                this.ToggleMouseCursor(this.IsGamePaused);
            }
        }
        else
        {
            if (!this.PauseButtonInUse)
            {
                //unpause the game
                Time.timeScale = 1f;
                //set bools
                this.IsGamePaused = false;
                this.PauseButtonInUse = true;

                //toggle PauseMenu
                this.TogglePauseMenu(this.IsGamePaused);
                //toggle MouseCursor
                this.ToggleMouseCursor(this.IsGamePaused);
            }

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
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePauseMenu()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void TogglePauseMenu(bool pause_)
    {
        if (pause_)
        {
            this.PauseCanvas.SetActive(true);
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(this.MainMenuButton);
            this.AudioController.transform.FindChild("MusicAudioSource").GetComponent<AudioSource>().Pause();
        }
        else
        {
            this.PauseCanvas.SetActive(false);
            this.AudioController.transform.FindChild("MusicAudioSource").GetComponent<AudioSource>().UnPause();
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleMainMenuConfirmation(bool)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ToggleMainMenuConfirmation(bool enable_)
    {
        this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(this.NoButton);

        this.MainMenuConfirmation.SetActive(enable_);
        if(!enable_)
        {
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(this.MainMenuButton);
        }
    }
}