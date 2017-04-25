///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — PauseMenuSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using XInputDotNetPure;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class PauseMenuSystem : MonoBehaviour
{
    #region FIELDS
    public static bool isPaused = false;
    [SerializeField]
    bool disablePreviousMenuOnLoad = false;
    [SerializeField]
    KeyCode kb_pause = KeyCode.Escape;
    [SerializeField]
    float delayBeforeUnpause = 0.25f;
    [SerializeField]
    Canvas pauseCanvas;
    Canvas activeMenu;
    Stack<Canvas> activeMenuStack;
    [SerializeField]
    RectTransform mask;
    UI_FadeIn uiFadeIn;
    UI_FadeOut uiFadeOut;
    [SerializeField]
    Canvas uiBar;
    ButtonBase currentButton;
    float timer = 0f;
    GamePadInputData p1;
    [SerializeField]
    float leftAnalogStickThreshold = 0.85f;
    [SerializeField]
    float menuInputDelay = 0.25f;
    MenuToLoad previousMenu;
    [Header("CAMERAS")]
    [SerializeField]
    Camera inGameCamera;
    [SerializeField]
    Camera pauseCamera;
    [SerializeField]
    Camera HUDCamera;
    BlurOptimized HUDCamera_blur;
    BlurOptimized inGameCamera_blur;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        inGameCamera_blur = inGameCamera.GetComponent<BlurOptimized>();
        inGameCamera_blur.enabled = false;
        HUDCamera_blur = HUDCamera.GetComponent<BlurOptimized>();
        HUDCamera_blur.enabled = false;
        pauseCamera.enabled = false;
        uiFadeIn = mask.GetComponent<UI_FadeIn>();
        uiFadeOut = mask.GetComponent<UI_FadeOut>();
        activeMenuStack = new Stack<Canvas> { };
        SetSubscriptions();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (pauseCanvas != null)
        {
            activeMenuStack.Push(pauseCanvas);
            activeMenu = activeMenuStack.Peek();
            HoverFirstButton(activeMenu.GetComponent<Menu>());
            previousMenu = MenuToLoad.PAUSE;
        }
        Unpause();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_RESUME_GAME>(Unpause);
        Events.instance.AddListener<EVENT_RESTART_GAME>(Restart);
        Events.instance.AddListener<EVENT_LOAD_NEW_MENU>(LoadNewMenu);
        Events.instance.AddListener<EVENT_LOAD_PREVIOUS_MENU>(LoadPreviousMenu);
        Events.instance.AddListener<EVENT_RETURN_FROM_CODA_MENU>(ReturnFromCoda);
        Events.instance.AddListener<EVENT_LOAD_SCENE>(LoadPassedScene);
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
        if (GamePadInput.players[0] != null)
        {
            //get latest input
            p1 = GamePadInput.players[0];

            if (!isPaused)
                DetectPauseRequest(p1);
            else
            {
                DetectInput(p1);
                DetectUnpauseRequest(p1);
            }
        }
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseCanvas.enabled = false;
        uiBar.enabled = false;
        FadeOutMask();
        pauseCamera.enabled = false;
        inGameCamera_blur.enabled = false;
        HUDCamera_blur.enabled = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Unpause(EVENT_RESUME_GAME _event)
    {
        Unpause();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
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
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadPreviousMenu(EVENT_LOAD_PREVIOUS_MENU _event)
    {
        TogglePreviousMenu();

        activeMenuStack.Pop();
        activeMenu = activeMenuStack.Peek();
        activeMenu.enabled = true;
        HoverFirstButton(activeMenu.GetComponent<Menu>());

        Events.instance.Raise(new EVENT_CAMERA_ZOOM_START(previousMenu, true));
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_menu"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void HoverFirstButton(Menu _menu)
    {
        //Debug.Log("HoverFirstButton(Menu _menu.activeButton = " + _menu.activeButton + ")");
        if (currentButton != null)
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
    void DetectPauseRequest(GamePadInputData _p1)
    {
        //pause the game
        if(_p1.Start == GamePadButtonState.PRESSED || Input.GetKeyDown(kb_pause))
        {
            Pause();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetectUnpauseRequest(GamePadInputData _p1)
    {
        //unpause the game
        if (_p1.Start == GamePadButtonState.PRESSED || Input.GetKeyDown(kb_pause))
        {
            FadeOutMask();
            LeanTween.delayedCall(delayBeforeUnpause, Unpause).setIgnoreTimeScale(true);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetectInput(GamePadInputData _p1)
    {
        //add to the timer
        timer += Time.unscaledDeltaTime;

        //get button input
        RespondToGamePadButtonInput();

        //only respond to menu input if we're above the menuInputDelay threshold
        if (timer > menuInputDelay)
        {
            //get navigational input
            RespondToGamePadDirectionalInput(_p1);
            RespondToKeyboardInput();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Pause()
    {
        GamePad.SetVibration((PlayerIndex)0, 0f, 0f);
        isPaused = true;
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
        uiBar.enabled = true;
        FadeInMask();
        pauseCanvas.enabled = true;
        inGameCamera_blur.enabled = true;
        HUDCamera_blur.enabled = true;
        pauseCamera.enabled = true;
        
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Restart(EVENT_RESTART_GAME _event)
    {
        //Application.LoadLevel(Application.loadedLevel);
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadPassedScene(EVENT_LOAD_SCENE _event)
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        SceneManager.LoadScene(_event.scene);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FadeInMask()
    {
        uiFadeIn.Animate();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FadeOutMask()
    {
        uiFadeOut.Animate();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// D-Pad and analog stick support for navigating menu buttons
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RespondToGamePadDirectionalInput(GamePadInputData _p1)
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
                    currentButton.Activate();
                    //Events.instance.Raise(new EVENT_LOAD_PREVIOUS_MENU(activeMenuStack.Peek(), previousMenu));
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

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_RESUME_GAME>(Unpause);
        Events.instance.RemoveListener<EVENT_RESTART_GAME>(Restart);
        Events.instance.RemoveListener<EVENT_LOAD_NEW_MENU>(LoadNewMenu);
        Events.instance.RemoveListener<EVENT_LOAD_PREVIOUS_MENU>(LoadPreviousMenu);
        Events.instance.RemoveListener<EVENT_RETURN_FROM_CODA_MENU>(ReturnFromCoda);
        Events.instance.RemoveListener<EVENT_LOAD_SCENE>(LoadPassedScene);
    }
    #endregion
}
