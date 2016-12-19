/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ButtonController.cs
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
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    #region PROPERTIES
    
    [SerializeField]
    public bool IsThisButtonFirst;

    MenuController MenuControllerScript;
    GameObject EventSystemObj;
    GameObject MusicController;

    #endregion PROPERTIES

    #region INITIALIZATION
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    /////////////////////////////////////////////////////////////////////////*/
    public void Awake()
    {
        //check to see if this button should be highlighted upon first load
        if(this.IsThisButtonFirst)
        {
            this.EventSystemObj = GameObject.Find("EventSystem");
            this.EventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(this.gameObject);
        }

        //get access to MenuController script
        if(GameObject.FindWithTag("MenuSystem") != null)
        {
            this.MenuControllerScript = GameObject.FindWithTag("MenuSystem").GetComponent<MenuController>();
        }

        if (GameObject.FindWithTag("SFXSystem").gameObject != null)
        {
            this.MusicController = GameObject.FindWithTag("SFXSystem").gameObject;
        }
    }

    #endregion INITIALIZATION

    #region BUTTON FUNCTIONS

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadNewMenu(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadNewMenu(string menuToLoad_)
    {
        //first get rid of the old menu
        if(this.MenuControllerScript.CurrentMenu != null)
        {
            this.MenuControllerScript.DestroyPreviousMenu();
        }
        
        //lastly load the new level
        switch(menuToLoad_)
        {
            case "startmenu":
                GameObject startMenu = (GameObject)Resources.Load("Menus/StartMenu", typeof(GameObject));
                this.MenuControllerScript.CreateMenu(startMenu);
                break;

            case "mainmenu":
                GameObject mainMenu = (GameObject)Resources.Load("Menus/MainMenu", typeof(GameObject));
                this.MenuControllerScript.CreateMenu(mainMenu);
                break;

            case "howtoplaymenu":
                GameObject howToPlayMenu = (GameObject)Resources.Load("Menus/HowToPlayMenu", typeof(GameObject));
                this.MenuControllerScript.CreateMenu(howToPlayMenu);
                break;

            case "creditsmenu":
                GameObject creditsMenu = (GameObject)Resources.Load("Menus/CreditsMenu", typeof(GameObject));
                this.MenuControllerScript.CreateMenu(creditsMenu);
                break;

            case "quitconfirmation":
                GameObject quitConfirmationMenu = (GameObject)Resources.Load("Menus/QuitConfirmationMenu", typeof(GameObject));
                this.MenuControllerScript.CreateMenu(quitConfirmationMenu);
                break;

            default:
                Debug.LogError("ERROR! INVALID MENU NAME");
                break;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadLevel(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadLevel(string levelToLoad_)
    {
        /*
        foreach(CreateObjectsOnDestroy createOnDestroyScript in GameObject.FindObjectsOfType<CreateObjectsOnDestroy>())
        {
            createOnDestroyScript.OnLoadingScene();
        }
        */

        switch(levelToLoad_)
        {
            case "newgame":
                SceneManager.LoadScene("sce_Level01");
                break;
            case "mainmenu":
                Time.timeScale = 1f;
                AudioListener.volume = 1;
                this.MusicController.GetComponent<AudioSource>().UnPause();
                SceneManager.LoadScene("sce_mainMenu");
                break;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadImage(string)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadImage(string controlType_)
    {
        switch (controlType_)
        {
            case "gamepad":
                var controlsImg = GameObject.Find("ControlsImg");
                controlsImg.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("UI/gamepad-controls");
                break;
            case "keyboard":
                var controlsImg2 = GameObject.Find("ControlsImg");
                controlsImg2.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("UI/keyboard-controls");
                break;
            default:
                Debug.LogError("ERROR! Please provide a string of either 'gamepad' or 'keyboard'");
                break;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: QuitGame()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void QuitGame()
    {
        Application.Quit();
    }
}

#endregion BUTTON FUNCTIONS
