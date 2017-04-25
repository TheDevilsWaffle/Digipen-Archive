///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ButtonBase.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

#region ENUMS
public enum MenuButtonState
{
    INACTIVE,
    HOVER,
    ACTIVATE,
    DISABLED
};
#endregion

#region EVENTS
public class EVENT_UI_BAR_UPDATE : GameEvent
{
    public List<UIBarInfo> uiBarInfos;
    public EVENT_UI_BAR_UPDATE(List<UIBarInfo> _uiBarInfos)
    {
        uiBarInfos = _uiBarInfos;
    }
}
#endregion

public class ButtonBase : MonoBehaviour, 
                          IPointerEnterHandler, 
                          IPointerExitHandler, 
                          IPointerDownHandler, 
                          IPointerUpHandler
{
    #region FIELDS
    [Header("MENU TO LOAD")]
    MenuToLoad menuToLoad = MenuToLoad.UNASSIGNED;
    [Header("INACTIVE")]
    [SerializeField]
    protected Color color_inactive = new Color(30f/255f, 35f/255f, 40f/255f,1f);
    [SerializeField]
    protected Color textColor_inactive = new Color(160f/255f, 180f/255f, 200f/255f, 1f);
    [SerializeField]
    protected ButtonActionBase inactiveAction;

    [Header("HOVER")]
    [SerializeField]
    protected Color color_hover = new Color(25f/255f, 200f/255f, 180f/255f, 1f);
    [SerializeField]
    protected Color textColor_hover = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    protected ButtonActionBase hoverAction;
    [SerializeField]
    List<UIBarInfo> uiBarInfos;

    [Header("ACTIVATE")]
    [SerializeField]
    protected Color color_activate = new Color(5f / 255f, 180f / 255f, 160f / 255f, 1f);
    [SerializeField]
    protected Color textColor_activate = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    protected ButtonActionBase activateAction;

    [Header("DISABLED")]
    [SerializeField]
    protected Color color_disabled = new Color(160f/255f, 180f/255f, 200f/255f, 1f);
    [SerializeField]
    protected Color textColor_disabled = new Color(75f/255f, 85f/255f, 90f/255f, 1f);
    protected ButtonActionBase disabled;

    [Header("NODE MAP")]
    public ButtonBase up;
    public ButtonBase right;
    public ButtonBase down;
    public ButtonBase left;

    [Header("SFX")]
    [SerializeField]
    AudioClip sfx_activate;
    [SerializeField]
    AudioClip sfx_hover;

    [HideInInspector]
    public MenuButtonState currentState = MenuButtonState.INACTIVE;

    //trs
    protected Vector3 originalPosition;
    protected Vector3 originalScale;
    protected Quaternion originalRotation;

    //refs
    protected RectTransform rt;
    protected Image img;
    protected Text txt;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Awake()
    {
        //get refs
        rt = GetComponent<RectTransform>();
        if(GetComponent<Image>() != null)
            img = GetComponent<Image>();
        if(transform.Find("Text").gameObject.GetComponent<Text>() != null)
            txt = transform.Find("Text").gameObject.GetComponent<Text>();

        //set trs
        originalPosition = rt.localPosition;
        originalScale = rt.localScale;
        originalRotation = rt.localRotation;

        //start inactive
        ChangeState(MenuButtonState.INACTIVE);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Start()
    {
        
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_state"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ChangeState(MenuButtonState _state)
    {
        if (_state == currentState)
        {
            return;
        }

        currentState = _state;
        switch (_state)
        {
            case MenuButtonState.INACTIVE:
                Inactive();
                break;
            case MenuButtonState.HOVER:
                Hover();
                break;
            case MenuButtonState.ACTIVATE:
                Activate();
                break;
            case MenuButtonState.DISABLED:
                break;
            default:
                Debug.LogError("INCORRECT MENU BUTTON STATE! Check the passed parameter to make sure it is a valid MenuButtonState!");
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Updates's a button's state based on passed MenuButtonState
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Inactive()
    {
        img.color = color_inactive;
        txt.color = textColor_inactive;
        if (inactiveAction != null)
        {
            inactiveAction.Inactive();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.HOVER
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Hover()
    {
        if (img != null)
        {
            img.color = color_hover;
        }
        if (txt != null)
        {
            txt.color = textColor_hover;
        }

        if(uiBarInfos != null || uiBarInfos.Count != 0)
        {
            Events.instance.Raise(new EVENT_UI_BAR_UPDATE(uiBarInfos));
        }
        if (hoverAction != null)
        {
            hoverAction.Hover();
        }
        if(sfx_hover != null)
        {
            AudioSystem.Instance.MakeAudioSource("sfx_btn_hover");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.ACTIVATE
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Activate()
    {
        img.color = color_activate;
        txt.color = textColor_activate;

        if(activateAction != null)
        {
            activateAction.Activate();
        }
        if (sfx_activate != null)
        {
            AudioSystem.Instance.MakeAudioSource("sfx_btn_activate");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.DISABLED
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Disabled()
    {
        img.color = color_disabled;
        txt.color = textColor_disabled;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_ped"></param>
    public void OnPointerEnter(PointerEventData _ped)
    {
        ChangeState(MenuButtonState.HOVER);
        //print("hovering");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_ped"></param>
    public void OnPointerExit(PointerEventData _ped)
    {
        ChangeState(MenuButtonState.INACTIVE);
        //print("exited");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_ped"></param>
    public void OnPointerDown(PointerEventData _ped)
    {
        ChangeState(MenuButtonState.ACTIVATE);
        //print("activate");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_ped"></param>
    public void OnPointerUp(PointerEventData _ped)
    {
        ChangeState(MenuButtonState.HOVER);
        //print("hover after click");
    }
    #endregion
}
