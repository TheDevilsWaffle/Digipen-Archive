///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MenuSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

#region NAMESPACES
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#endregion

public class MenuSystem : MonoBehaviour
{
    #region FIELDS
    [Header("MENU SETTINGS")]
    public Menu firstMenu;
    public float delayBeforeLoad = 0.25f;
    public float menuInputDelay = 0.25f;
    public float leftAnalogStickThreshold = 0.85f;
    
    //private
    GamePadInputData p1;
    Menu currentMenu;
    ButtonBase currentButton;
    Stack<Menu> previousMenus = new Stack<Menu>();
    float timer = 0f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //set the timer
        timer = 0f;

        //add the firstMenu
        if (firstMenu == null)
            Debug.LogError("firstMenu is NULL! Please designate a first menu for the menu system.");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //activate firstMenu in case it is inactive
        //StartCoroutine(LoadNewMenu(firstMenu));

        //subscribe to events
        //Events.instance.AddListener<Event_GamePadInput_Player1>(RespondToGamePadInput);
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
        //get latest input
        p1 = GamePadInput.players[0];

        //add to the timer
        timer += Time.deltaTime;

        //get button input
        RespondToGamePadButtonInput();

        //only respond to menu input if we're above the menuInputDelay threshold
        if (timer > menuInputDelay)
        {
            //get navigational input
            RespondToGamePadDirectionalInput();
        }
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Activates a new menu and deactivates and stores the previous menu if applicable
    /// </summary>
    /// <param name="_menu">the new (or old) menu to load</param>
    /// <param name="_isNewMenu">if we're loading a previous menu, set this to false</param>
    /// <returns>nothing, just a delay</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ////public IEnumerator LoadNewMenu(Menu _menu, bool _isNewMenu = true)
    ////{
    ////    yield return new WaitForSeconds(delayBeforeLoad);

    ////    //there was previous a current menu, push it to previousMenus and disable it and its button
    ////    if (currentMenu && _isNewMenu)
    ////    {
    ////        //print("loading a new menu");

    ////        //push old menu to previousMenus and deactivate it along with its button
    ////        previousMenus.Push(currentMenu);
    ////        currentMenu.DeactivateMenu();
    ////        currentButton.Inactive();
    ////    }

    ////    //print("activating " + _menu.menuName);

    ////    //set currentMenu to _menu and activate it along with its button
    ////    currentMenu = _menu;      
    ////    currentMenu.ActivateMenu();
    ////    currentButton = currentMenu.firstButtonToHover;
    ////    currentButton.Hover();
    ////}

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /////// <summary>
    /////// Attempts to load a previous menu
    /////// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    ////public void LoadPreviousMenu()
    ////{
    ////    ///print("previousMenus.Count = " + previousMenus.Count);
    ////    if(previousMenus.Count > 0)
    ////    {
    ////        //deactivate old menu along with its button that is taken out of previousMenu
    ////        currentMenu.DeactivateMenu();
    ////        currentButton.Inactive();
    ////        currentMenu = previousMenus.Pop();

    ////        //activate the menu we got out of previousMenus along with its button
    ////        currentMenu.ActivateMenu();
    ////        currentButton = currentMenu.firstButtonToHover;
    ////        currentButton.Hover();
    ////    }
    ////}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// D-Pad and analog stick support for navigating menu buttons
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RespondToGamePadDirectionalInput()
    {
        #region DPADS
        if (p1.DPadUp == GamePadButtonState.PRESSED  ||
            p1.DPadUp == GamePadButtonState.HELD && 
            currentButton.up != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.up;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.DPadRight == GamePadButtonState.PRESSED ||
                 p1.DPadRight == GamePadButtonState.HELD && 
                 currentButton.right != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.right;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.DPadDown == GamePadButtonState.PRESSED ||
                 p1.DPadDown == GamePadButtonState.HELD && 
                 currentButton.down != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.down;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.DPadLeft == GamePadButtonState.PRESSED ||
                 p1.DPadLeft == GamePadButtonState.HELD && 
                 currentButton.left != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.left;
            currentButton.Hover();
            timer = 0f;
        }
        #endregion
        #region LEFT ANALOG STICK
        if (p1.LeftAnalogStick.y > leftAnalogStickThreshold && currentButton.up != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.up;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.LeftAnalogStick.x > leftAnalogStickThreshold && currentButton.right != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.right;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.LeftAnalogStick.y < -leftAnalogStickThreshold && currentButton.down != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.down;
            currentButton.Hover();
            timer = 0f;
        }
        else if (p1.LeftAnalogStick.x < -leftAnalogStickThreshold && currentButton.left != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.left;
            currentButton.Hover();
            timer = 0f;
        }
        #endregion
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Gamepad Button support for buttons
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RespondToGamePadButtonInput()
    {
        if (currentButton != null)
        {
            #region A
            if (p1.A == GamePadButtonState.RELEASED)
            {
                currentButton.Hover();
            }
            else if (p1.A == GamePadButtonState.PRESSED && currentButton.currentState != MenuButtonState.DISABLED)
            {
                currentButton.Activate();
            }
            else if (p1.A == GamePadButtonState.PRESSED && currentButton.currentState == MenuButtonState.DISABLED)
            {
                currentButton.Disabled();
            }
            #endregion
            #region B
            else if (p1.B == GamePadButtonState.PRESSED)
            {
                //LoadPreviousMenu();
            }
            #endregion
        }
    }
    #endregion
}
