/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PauseController.cs
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

public class PauseController : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    GameObject PauseCanvas;
    [SerializeField]
    GameObject MainMenuConfirmation;
    [SerializeField]
    GameObject MainMenuButton;
    [SerializeField]
    GameObject HowToPlayMenu;
    [SerializeField]
    GameObject QuitGameConfirmationMenu;
    [SerializeField]
    GameObject NoButton;
    GameObject EventSystemObj;
    
    GameObject EndGameObject;

    //HIDDEN PROPERTIES
    bool IsGamePaused;
    bool PauseButtonInUse;
    GameObject HUDCanvas;

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
        this.EndGameObject = GameObject.Find("EndGameCanvas");
        this.ToggleMouseCursor(false);
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetAxisRaw("Pause") != 0f)
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
                //reduce volume slightly
                AudioListener.volume = 0.1f;
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
                //raise volume back to normal
                AudioListener.volume = 1f;
                //set bools
                this.IsGamePaused = false;
                this.PauseButtonInUse = true;

                //toggle PauseMenu
                this.TogglePauseMenu(this.IsGamePaused);
                //toggle MouseCursor
                this.ToggleMouseCursor(this.IsGamePaused);

                this.ToggleMainMenuConfirmation(false);
                this.ToggleHowToPlay(false);
            }

        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleMouseCursor()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void ToggleMouseCursor(bool mouseShouldDisplay)
    {
        if (mouseShouldDisplay)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("UnpauseButton"));
        }
        else
        {
            this.PauseCanvas.SetActive(false);
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
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleQuitGameConfirmation(bool)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ToggleQuitGameConfirmation(bool enable_)
    {
        this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(this.NoButton);

        this.QuitGameConfirmationMenu.SetActive(enable_);
        if (!enable_)
        {
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("QuitGameButton"));
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleHowToPlay(bool)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void ToggleHowToPlay(bool enable_)
    {
        if (enable_)
        {
            //this.TogglePauseMenu(false);
            this.HowToPlayMenu.SetActive(true);
            print(this.HowToPlayMenu);
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("HTPBackButton"));           
        }
        this.HowToPlayMenu.SetActive(enable_);
        if (!enable_)
        {
            //this.TogglePauseMenu(true);
            this.HowToPlayMenu.SetActive(false);
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("HowToPlayButton"));
        }
    }

}
