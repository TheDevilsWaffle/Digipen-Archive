///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MenuController.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_CAMERA_ZOOM_START : GameEvent
{
    public MenuToLoad menuToLoad;
    public bool backwards;
    public EVENT_CAMERA_ZOOM_START(MenuToLoad _menuToLoad, bool _backwards = false)
    {
        menuToLoad = _menuToLoad;
        backwards = _backwards;
    }
}
public class EVENT_LOAD_PREVIOUS_MENU : GameEvent
{
    public Canvas canvasToLoad;
    public MenuToLoad menuToLoad;
    public EVENT_LOAD_PREVIOUS_MENU(Canvas _canvasToLoad, MenuToLoad _menuToLoad)
    {
        canvasToLoad = _canvasToLoad;
        menuToLoad = _menuToLoad;
    }
}
#endregion

public class MenuController : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    bool disableAllMenusOnStart = false;
    [SerializeField]
    bool disablePreviousMenuOnLoad = false;
    Stack<Canvas> activeMenuStack;
    [SerializeField]
    Canvas rootMenu;
    Canvas activeMenu;
    Transform tr;
    GamePadInputData p1;
    float timer = 0f;
    ButtonBase currentButton;
    public float leftAnalogStickThreshold = 0.85f;
    public float menuInputDelay = 0.25f;

    MenuToLoad previousMenu;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Time.timeScale = 1f;
        tr = transform;
        activeMenuStack = new Stack<Canvas> { };
        
        timer = 0f;

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_LOAD_NEW_MENU>(LoadNewMenu);
        Events.instance.AddListener<EVENT_LOAD_PREVIOUS_MENU>(LoadPreviousMenu);
        Events.instance.AddListener<EVENT_RETURN_FROM_CODA_MENU>(ReturnFromCoda);
        Events.instance.AddListener<EVENT_LOAD_SCENE>(LoadPassedScene);
        Events.instance.AddListener<EVENT_LOAD_CODA>(LoadCODAMenu);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (disableAllMenusOnStart)
        {
            DisableAllMenus();
        }
        else
        {
            EnableAllMenus();
        }
        if (rootMenu != null)
        {
            rootMenu.enabled = true;
            activeMenuStack.Push(rootMenu);
            activeMenu = activeMenuStack.Peek();
            HoverFirstButton(activeMenu.GetComponent<Menu>());
            previousMenu = MenuToLoad.START;
        }
        else
        {
            Debug.LogWarning("rootMenu is null, please select a menu to start as the root!");
        }
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
            RespondToKeyboardInput();
        }
    }
    #endregion

    #region PUBLIC METHODS
    
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TogglePreviousMenu()
    {
        if (disablePreviousMenuOnLoad)
        {
            activeMenu.enabled = false;
        }
        else
        {
            activeMenu.enabled = true;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadNewMenu(EVENT_LOAD_NEW_MENU _event)
    {
        //Debug.Log("LoadNewMenu(_event.menuToLoad = " + _event.menuToLoad + ")");
        TogglePreviousMenu();

        activeMenuStack.Push(_event.canvasToLoad);
        activeMenu = activeMenuStack.Peek();
        activeMenu.enabled = true;
        HoverFirstButton(activeMenu.GetComponent<Menu>());

        Events.instance.Raise(new EVENT_CAMERA_ZOOM_START(_event.menuToLoad));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadCODAMenu(EVENT_LOAD_CODA _event)
    {
        //Debug.Log("LoadCODAMenu(_event.menuToLoad = " + _event.menuToLoad + ")");
        TogglePreviousMenu();

        activeMenuStack.Push(_event.canvasToLoad);
        activeMenu = activeMenuStack.Peek();
        activeMenu.enabled = true;
        HoverFirstButton(activeMenu.GetComponent<Menu>());
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadPreviousMenu(EVENT_LOAD_PREVIOUS_MENU _event)
    {
        //Debug.Log("LoadPreviousMenu("+_event.canvasToLoad+ ", " + _event.menuToLoad + ")");
        TogglePreviousMenu();

        activeMenuStack.Pop();
        activeMenu = activeMenuStack.Peek();
        activeMenu.enabled = true;
        HoverFirstButton(activeMenu.GetComponent<Menu>());

        Events.instance.Raise(new EVENT_CAMERA_ZOOM_START(_event.menuToLoad, true));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// essentially no camera traveling
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ReturnFromCoda(EVENT_RETURN_FROM_CODA_MENU _event)
    {
        TogglePreviousMenu();

        activeMenuStack.Pop();
        activeMenu = activeMenuStack.Peek();
        activeMenu.enabled = true;
        HoverFirstButton(activeMenu.GetComponent<Menu>());
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadPassedScene(EVENT_LOAD_SCENE _event)
    {
        StopAllCoroutines();
        SceneManager.LoadScene(_event.scene);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_menu"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void HoverFirstButton(Menu _menu)
    {
        //Debug.Log("HoverFirstButton(Menu _menu.activeButton = " + _menu.activeButton + ")");
        if(currentButton != null)
        {
            currentButton.Inactive();
        }

        currentButton = _menu.activeButton;
        currentButton.Hover();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DisableAllMenus()
    {
        foreach(Transform child in tr)
        {
            if (child.GetComponent<Canvas>() != null)
            {
                child.GetComponent<Canvas>().enabled = false;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void EnableAllMenus()
    {
        foreach (Transform child in tr)
        {
            if (child.GetComponent<Canvas>() != null)
            {
                child.GetComponent<Canvas>().enabled = true;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// D-Pad and analog stick support for navigating menu buttons
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RespondToGamePadDirectionalInput()
    {
        #region DPADS
        if (p1.DPadUp == GamePadButtonState.PRESSED ||
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
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RespondToKeyboardInput()
    {
        if (KeyboardInput.keys[KeyCode.UpArrow] == KeyboardKeyStatus.PRESSED
            || KeyboardInput.keys[KeyCode.UpArrow] == KeyboardKeyStatus.HELD
            && currentButton.up != null)
        {
            //currentButton.ChangeState(MenuButtonState.INACTIVE);
            currentButton.Inactive();
            currentButton = currentButton.up;
            currentButton.Hover();
            timer = 0f;
        }
        else if (KeyboardInput.keys[KeyCode.DownArrow] == KeyboardKeyStatus.PRESSED
                 || KeyboardInput.keys[KeyCode.DownArrow] == KeyboardKeyStatus.HELD
                 && currentButton.down != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.down;
            currentButton.Hover();
            timer = 0f;
        }
        else if (KeyboardInput.keys[KeyCode.RightArrow] == KeyboardKeyStatus.PRESSED
                 || KeyboardInput.keys[KeyCode.RightArrow] == KeyboardKeyStatus.HELD
                 && currentButton.right != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.right;
            currentButton.Hover();
            timer = 0f;
        }
        else if (KeyboardInput.keys[KeyCode.LeftArrow] == KeyboardKeyStatus.PRESSED
                 || KeyboardInput.keys[KeyCode.UpArrow] == KeyboardKeyStatus.HELD
                 && currentButton.left != null)
        {
            currentButton.Inactive();
            currentButton = currentButton.left;
            currentButton.Hover();
            timer = 0f;
        }
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
                if (activeMenuStack.Count > 1)
                {
                    if (currentButton.GetComponent<LoadMenu>() != null && currentButton.GetComponent<LoadMenu>().backToPreviousMenu == true)
                    {
                        currentButton.GetComponent<LoadMenu>().Activate();
                    }
                    else
                    {
                        Events.instance.Raise(new EVENT_LOAD_PREVIOUS_MENU(activeMenuStack.Peek(), previousMenu));
                    }
                }
                else
                {
                    return;
                }
            }
            #endregion
        }
    }
    #endregion

    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_LOAD_NEW_MENU>(LoadNewMenu);
        Events.instance.RemoveListener<EVENT_LOAD_PREVIOUS_MENU>(LoadPreviousMenu);
        Events.instance.RemoveListener<EVENT_RETURN_FROM_CODA_MENU>(ReturnFromCoda);
        Events.instance.RemoveListener<EVENT_LOAD_SCENE>(LoadPassedScene);
        Events.instance.RemoveListener<EVENT_LOAD_CODA>(LoadCODAMenu);
    }
    #endregion
}
