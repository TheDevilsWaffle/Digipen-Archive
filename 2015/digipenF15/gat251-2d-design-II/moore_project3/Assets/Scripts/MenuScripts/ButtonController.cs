/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ButtonController.cs
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

public class ButtonController : MonoBehaviour
{
    //BUTTON PROPERTIES
    [SerializeField]
    bool IsThisButtonFirst;
    //HIDDEN PROPERTIES
    [HideInInspector]
    MenuController MenuControllerScript;
    [HideInInspector]
    GameObject EventSystemObj;
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void Start()
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
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadNewMenu(string)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadNewMenu(string menuToLoad_)
    {
        //first get rid of the old menu
        this.MenuControllerScript.DestroyPreviousMenu();

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
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadLevel(string levelToLoad_)
    {
        switch(levelToLoad_)
        {
            case "newgame":
                Application.LoadLevel(1);
                break;
            case "mainmenu":
                Time.timeScale = 1f;
                Application.LoadLevel("startmenu");
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

