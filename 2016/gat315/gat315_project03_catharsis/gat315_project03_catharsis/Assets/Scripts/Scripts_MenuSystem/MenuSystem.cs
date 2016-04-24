#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    #region PROPERTIES

    //main menu
    private string MainMenu = "sce_mainMenu"; 

    //canvases that we need to use
    [SerializeField]
    private Canvas FirstMenuToSelect;
    [SerializeField]
    private Canvas PauseMenu;
    [SerializeField]
    private Canvas TryAgain;
    [SerializeField]
    private Canvas WinMenu;
    [SerializeField]
    public Image FadeOverlay;
    [SerializeField]
    public CanvasGroup PersistentCanvas;

    //bools
    [SerializeField]
    private bool AllowUnpause = true;
    private bool GameIsPaused = false;
    public bool AllowPlayerInput = true;
    public bool LoadingLevel;
    public bool AllowCursorDuringPlayGame = false;

    //animations
    UIAnimations UIAnimation;
    private Vector3 CenterPos = new Vector3(0.5f, 0.5f, 0f);
    private Vector3 ExitPos = new Vector3(0f, -500f, 0f);

    //access MenuInput.cs
    private MenuInput MenuInput;

    //access the active MenuObject
    private MenuObject ActiveObject;

    //used to store canvases
    private List<Canvas> CanvasStack = new List<Canvas>();

    //used to traverse the CanvasStack
    private Canvas CurrentCanvas
    {
        get { return this.CanvasStack[this.CanvasStack.Count - 1]; }
    }
    private Canvas PreviousCanvas
    {
        get { return this.CanvasStack[this.CanvasStack.Count - 2]; }
    }

    #endregion

    #region INITIALIZE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //make PauseMenu's canvas starts disabled
        //this.PauseMenu.enabled = false;

        this.UIAnimation = this.gameObject.GetComponent<UIAnimations>();

        //let audio play
        AudioListener.pause = false;

        if (!this.AllowCursorDuringPlayGame)
        {
            //turn off the mouse
            Cursor.visible = false;
        }

        //get MenuInput.cs attached to this gameObject
        this.MenuInput = this.gameObject.GetComponent<MenuInput>();

        //subscribing to MenuInputActions
        this.MenuInput.Subscribe(MenuInputActions.MoveUp, this.MoveUp);
        this.MenuInput.Subscribe(MenuInputActions.MoveDown, this.MoveDown);
        this.MenuInput.Subscribe(MenuInputActions.MoveLeft, this.MoveLeft);
        this.MenuInput.Subscribe(MenuInputActions.MoveRight, this.MoveRight);

        this.MenuInput.Subscribe(MenuInputActions.Select, this.OnSelect);
        this.MenuInput.Subscribe(MenuInputActions.Cancel, this.GoToPrevious);
        this.MenuInput.Subscribe(MenuInputActions.Pause, this.TogglePause);

        //check to see if this is the main menu in the game
        string currentScene = SceneManager.GetActiveScene().name;
        this.MainMenuCheck(currentScene);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MainMenuCheck(string)
    ////////////////////////////////////////////////////////////////////*/
    private void MainMenuCheck(string scene_)
    {
        //check to see if this menu system belongs to the main menu
        if(scene_ == this.MainMenu)
        {
            this.AllowUnpause = false;
            this.GoTo(this.FirstMenuToSelect);
        }

        //not the main menu, we're acting as a pause menu
        else
        {
            this.PersistentCanvas.alpha = 0f;
            this.FadeOverlay.CrossFadeAlpha(0.0f, 0.001f, true);
        }
    }

    #endregion

    #region BUTTON TRAVERSAL

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveUp()
    ////////////////////////////////////////////////////////////////////*/
    private void MoveUp()
    {
        //print("MoveUp");

        //null check
        if (this.ActiveObject == null || this.ActiveObject.UpObject == null)
            return;

        //deactivate the active button
        this.ActiveObject.OnDeactivate();
        //set the new active button to the one we went up to
        this.ActiveObject = this.ActiveObject.UpObject;
        //change this active button to look active
        this.ActiveObject.OnActivate();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveDown()
    ////////////////////////////////////////////////////////////////////*/
    private void MoveDown()
    {
        //print("MoveDown");

        if (this.ActiveObject == null || this.ActiveObject.DownObject == null)
            return;

        //deactivate the active button
        this.ActiveObject.OnDeactivate();
        //set the new active button to the one we went up to
        this.ActiveObject = this.ActiveObject.DownObject;
        //change this active button to look active
        this.ActiveObject.OnActivate();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveLeft()
    ////////////////////////////////////////////////////////////////////*/
    private void MoveLeft()
    {
        //print("MoveLeft");

        if (this.ActiveObject == null || this.ActiveObject.LeftObject == null)
            return;

        //deactivate the active button
        this.ActiveObject.OnDeactivate();
        //set the new active button to the one we went up to
        this.ActiveObject = this.ActiveObject.LeftObject;
        //change this active button to look active
        this.ActiveObject.OnActivate();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveRight()
    ////////////////////////////////////////////////////////////////////*/
    private void MoveRight()
    {
        //print("MoveRight");

        if (this.ActiveObject == null || this.ActiveObject.RightObject == null)
            return;

        //deactivate the active button
        this.ActiveObject.OnDeactivate();
        //set the new active button to the one we went up to
        this.ActiveObject = this.ActiveObject.RightObject;
        //change this active button to look active
        this.ActiveObject.OnActivate();
    }

    #endregion

    #region BUTTON ACTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnSelect()
    ////////////////////////////////////////////////////////////////////*/
    private void OnSelect()
    {
        //nullcheck
        if (this.ActiveObject == null)
            return;

        //call Active MenuObject's OnSelected()
        this.ActiveObject.OnSelected();

        //perfom the action that this button can perform
        switch (this.ActiveObject.Action)
        {
            case MenuObjectActions.NewCanvas:
                this.GoTo(this.ActiveObject.CanvasToGoTo);
                break;
            case MenuObjectActions.CODA:
                this.GoToCODA(this.ActiveObject.CanvasToGoTo);
                break;
            case MenuObjectActions.Previous:
                this.GoToPrevious();
                break;
            case MenuObjectActions.RestartLevel:
                this.AllowUnpause = true;
                this.AllowPlayerInput = true;
                this.PlayGame();
                this.LoadingLevel = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case MenuObjectActions.NextLevel:
                this.AllowUnpause = true;
                this.AllowPlayerInput = true;
                this.PlayGame();
                this.LoadingLevel = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case MenuObjectActions.MainMenu:
                this.AllowUnpause = true;
                this.AllowPlayerInput = true;
                this.PlayGame();
                this.LoadingLevel = true;
                SceneManager.LoadScene(this.MainMenu);
                break;
            case MenuObjectActions.QuitGame:
                Application.Quit();
                break;
            default:
                Debug.LogError("MenuObjectActions is not a defined enum, please choose a new option.");
                break;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: GoTo()
    ////////////////////////////////////////////////////////////////////*/
    private void GoTo(Canvas targetCanvas)
    {
        if(this.CanvasStack.Count > 0)
            this.CurrentCanvas.enabled = false;

        targetCanvas.enabled = true;
        
        
        

        GameObject group = targetCanvas.gameObject.transform.Find("Group").gameObject;
        //IMPORTANT: need to set these because they are not set automatically
        group.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        group.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(1f, 1f);
        this.UIAnimation.MoveFrom(group, this.ExitPos, 0.25f);


        if(this.ActiveObject != null)
            this.ActiveObject.OnDeactivate();
        this.ActiveObject = targetCanvas.GetComponent<MenuCanvas>().DefaultButtonToActivate;
        this.ActiveObject.OnActivate();

        this.CanvasStack.Add(targetCanvas);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: GoToCODA()
    ////////////////////////////////////////////////////////////////////*/
    private void GoToCODA(Canvas targetCanvas)
    {
        if (this.CanvasStack.Count > 0)
            this.CurrentCanvas.enabled = false;

        targetCanvas.enabled = true;
        GameObject group = targetCanvas.gameObject.transform.Find("Group").gameObject;
        //IMPORTANT: need to set these because they are not set automatically
        group.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        group.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(1f, 1f);
        this.UIAnimation.MoveFrom(group, this.ExitPos, 0.25f);

        StartCoroutine(this.UIAnimation.FadeImage(this.FadeOverlay, this.FadeOverlay.GetComponent<Image>().color, 0.8f, 0.25f));


        if (this.ActiveObject != null)
            this.ActiveObject.OnDeactivate();
        this.ActiveObject = targetCanvas.GetComponent<MenuCanvas>().DefaultButtonToActivate;
        this.ActiveObject.OnActivate();

        this.CanvasStack.Add(targetCanvas);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: GoToPrevious()
    ////////////////////////////////////////////////////////////////////*/
    public void GoToPrevious()
    {
        //null checking
        if (this.CanvasStack.Count <= 0)
            return;
        
        //this was the last menu, restart the game
        if (this.CanvasStack.Count <= 1)
        {
            this.PlayGame();
            return;
        }

        //disable the canvas script on this canvas
        this.CurrentCanvas.enabled = false;
        //print("the previous canvas was = " + this.CurrentCanvas.gameObject + " and is currently " + this.CurrentCanvas.enabled);

        //print(this.PreviousCanvas.gameObject);
        //enable the last canvas we were in
        this.PreviousCanvas.enabled = true;

        //print(this.PreviousCanvas.enabled);
        //deactivate this button
        this.ActiveObject.OnDeactivate();
        //get the new button to activate
        this.ActiveObject = this.PreviousCanvas.GetComponent<MenuCanvas>().DefaultButtonToActivate;
        //activate the new button
        this.ActiveObject.OnActivate();
        //remove this canvas from CanvasStack
        this.CanvasStack.Remove(this.CurrentCanvas);
    }


    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePause()
    ////////////////////////////////////////////////////////////////////*/
    public void TogglePause()
    {
        if (this.GameIsPaused)
            this.PlayGame();
        else
            this.PauseGame();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: PauseGame()
    ////////////////////////////////////////////////////////////////////*/
    public void PauseGame()
    {
        if (this.PauseMenu != null)
        {
            if (this.CanvasStack.Count != 0)
                return;

            //turn on the persistent canvas group
            this.PersistentCanvas.alpha = 1f;
            //quickly set alpha to 0, then animate fade overlay in
            this.FadeOverlay.GetComponent<CanvasRenderer>().SetAlpha(0);
            this.FadeOverlay.CrossFadeAlpha(1.0f, 0.25f, true);

            Time.timeScale = 0;
            AudioListener.pause = true;
            this.GoTo(this.PauseMenu);
            this.GameIsPaused = true;
            this.AllowPlayerInput = false;
            Cursor.visible = true;

            
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DisplayTryAgain()
    ////////////////////////////////////////////////////////////////////*/
    public void DisplayTryAgain()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        this.GoTo(this.TryAgain);
        this.GameIsPaused = true;
        this.AllowPlayerInput = false;
        Cursor.visible = true;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DisplayWinMenu()
    ////////////////////////////////////////////////////////////////////*/
    public void DisplayWinMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        this.GoTo(this.WinMenu);
        this.GameIsPaused = true;
        this.AllowPlayerInput = false;
        Cursor.visible = true;
        GameObject winFade = this.WinMenu.transform.Find("FadeOverlay").gameObject;
        print(winFade);
        winFade.GetComponent<CanvasRenderer>().SetAlpha(0f);
        winFade.GetComponent<Image>().CrossFadeAlpha(1f, 1.5f, true);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: PlayGame()
    ////////////////////////////////////////////////////////////////////*/
    public void PlayGame()
    {
        if (!this.AllowUnpause)
            return;

        this.CurrentCanvas.enabled = false;
        this.ActiveObject.OnDeactivate();
        this.ActiveObject = null;
        Time.timeScale = 1;
        AudioListener.pause = false;
        this.CanvasStack.Clear();
        this.GameIsPaused = false;
        this.AllowPlayerInput = true;

        if (!this.AllowCursorDuringPlayGame)
        {
            //turn off the mouse
            Cursor.visible = false;
        }

        this.AllowUnpause = true;

        //turn off the persistent canvas group
        this.PersistentCanvas.alpha = 0f;
    }

    #endregion
}
