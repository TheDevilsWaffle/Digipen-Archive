/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MenuObject.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#region ENUMS

public enum MenuObjectActions { Previous,
                                NewCanvas,
                                CODA,
                                RestartLevel,
                                MainMenu,
                                NextLevel,
                                QuitGame }
#endregion

public class MenuObject : MonoBehaviour
{
    #region PROPERTIES

    [SerializeField]
    public MenuObjectActions Action = MenuObjectActions.NewCanvas;

    [SerializeField]
    public Canvas CanvasToGoTo;
    private Image ButtonBorder;
    private Image ButtonBackground;

    [SerializeField]
    private Color Border_ActiveColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    private Color Border_InactiveColor = new Color(0.1f, 0.1f, 0.1f, 1f);

    [SerializeField]
    private Color Background_ActiveColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField]
    private Color Background_InactiveColor = new Color(0.1f, 0.1f, 0.1f, 1f);

    private Text ButtonText;
    [SerializeField]
    private Color Text_ActiveColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    private Color Text_InactiveColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    //node map
    [SerializeField]
    public MenuObject UpObject;
    [SerializeField]
    public MenuObject DownObject;
    [SerializeField]
    public MenuObject LeftObject;
    [SerializeField]
    public MenuObject RightObject;

    public enum ActivationState { Activate, Selected, Deactivate };

    //animations
    [SerializeField]
    private float ButtonAnimationTime = 0.25f;
    private Vector3 ActiveScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 OriginalScale = new Vector3(1f, 1f, 1f);
    private UIAnimations UIAnimation;

    public delegate void MenuObjectDelegate();
    public MenuObjectDelegate[] MenuObjectDelegates = { delegate { }, delegate { }, delegate { } };

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get the animation script
        this.UIAnimation = this.gameObject.GetComponent<UIAnimations>();

        //get the original color of the text and image
        this.ButtonBackground = this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>();
        this.ButtonBorder = this.gameObject.GetComponent<Image>();
        this.ButtonText = this.gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    #endregion

    public void Subscribe(ActivationState activationState, MenuObjectDelegate menuObjectDelegate)
    {
        this.MenuObjectDelegates[(int)activationState] += menuObjectDelegate;
    }
    public void Unsubscribe(ActivationState activationState, MenuObjectDelegate menuObjectDelegate)
    {
        this.MenuObjectDelegates[(int)activationState] -= menuObjectDelegate;
    }

    #region BUTTON STATES

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnActivate()
    ////////////////////////////////////////////////////////////////////*/
    public void OnActivate()
    {
        //start the scale loop
        this.UIAnimation.ScaleTo(this.gameObject, this.ActiveScale, 0.25f, "easeInOutQuad", "pingPong");

        //animate ButtonBackground and Text to ACTIVE color
        StartCoroutine(this.UIAnimation.FadeImage(this.ButtonBackground, this.Background_ActiveColor, 1f, this.ButtonAnimationTime));
        StartCoroutine(this.UIAnimation.FadeImage(this.ButtonBorder, this.Border_ActiveColor, 1f, this.ButtonAnimationTime));
        StartCoroutine(this.UIAnimation.FadeText(this.ButtonText, this.Text_ActiveColor, 1f, this.ButtonAnimationTime));

        //if (this.MenuObjectDelegates[(int)ActivationState.Activate] != null)
            this.MenuObjectDelegates[(int)ActivationState.Activate]();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnDeactivate()
    ////////////////////////////////////////////////////////////////////*/
    public void OnDeactivate()
    {
        //stop the scale loop
        //iTween.StopByName(this.gameObject, "UIAnimations_Scale");
        
        //scale this button back to its original scale
        this.UIAnimation.ScaleTo(this.gameObject, this.OriginalScale, 0.25f, "easeInOutQuad", "none");

        //animate ButtonBackground and Text to INACTIVE color
        StartCoroutine(this.UIAnimation.FadeImage(this.ButtonBackground, this.Background_InactiveColor, 1f, this.ButtonAnimationTime));
        StartCoroutine(this.UIAnimation.FadeImage(this.ButtonBorder, this.Border_InactiveColor, 1f, this.ButtonAnimationTime));
        StartCoroutine(this.UIAnimation.FadeText(this.ButtonText, this.Text_InactiveColor, 1f, this.ButtonAnimationTime));

        //if (this.MenuObjectDelegates[(int)ActivationState.Deactivate] != null)
            this.MenuObjectDelegates[(int)ActivationState.Deactivate]();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnSelected()
    ////////////////////////////////////////////////////////////////////*/
    public void OnSelected()
    {
        //if (this.MenuObjectDelegates[(int)ActivationState.Selected] != null)
            this.MenuObjectDelegates[(int)ActivationState.Selected]();
    }

    #endregion 

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}
