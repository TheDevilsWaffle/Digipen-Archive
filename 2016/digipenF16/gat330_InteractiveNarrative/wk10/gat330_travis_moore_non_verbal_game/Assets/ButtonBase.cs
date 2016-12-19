///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ButtonBase.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#region NAMESPACES
using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#endregion

#region ENUMS
public enum MenuButtonState
{
    INACTIVE,
    HOVER,
    ACTIVE,
    DISABLED
};
#endregion

public class ButtonBase : MonoBehaviour
{
    #region FIELDS
    [HideInInspector]
    public MenuButtonState currentState = MenuButtonState.INACTIVE;

    //trs
    protected Vector3 pos
    {
        get { return transform.localPosition ; }
        set { transform.localPosition = value; }
    }
    protected Vector3 sca
    {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }
    protected Quaternion rot
    {
        get { return transform.localRotation; }
        set { transform.localRotation = value; }
    }
    protected Vector3 pos_original
    {
        get { return transform.position; }
    }
    protected Vector3 sca_original
    {
        get { return transform.localScale; }
    }
    protected Quaternion rot_original
    {
        get { return transform.localRotation; }
    }

    MenuSystem menuSystem;
    public string levelToLoad;
    public Menu menuToLoad;
    public Button_CharacterSelect customActive;

    [Header("INACTIVE")]
    [SerializeField]
    protected Color color_inactive = new Color(1f,1f,1f,1f);
    [SerializeField]
    protected Color textColor_inactive = new Color(0f, 0f, 0f, 1f);

    protected ButtonActionBase inactive;

    [Header("HOVER")]
    [SerializeField]
    protected Color color_hover = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    protected Color textColor_hover = new Color(0f, 0f, 0f, 1f);
    protected ButtonActionBase hover;

    [Header("ACTIVE")]
    [SerializeField]
    protected Color color_active = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    protected Color textColor_active = new Color(0f, 0f, 0f, 1f);
    public ButtonActionBase active;

    [Header("DISABLED")]
    [SerializeField]
    protected Color color_disabled = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    protected Color textColor_disabled = new Color(0f, 0f, 0f, 1f);
    protected ButtonActionBase disabled;

    [HideInInspector]
    protected Image button;
    [HideInInspector]
    protected Text text;

    [Header("NODE MAP")]
    public ButtonBase up;
    public ButtonBase right;
    public ButtonBase down;
    public ButtonBase left;

    [HideInInspector]
    public bool isActive;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Awake()
    {
        //get MenuSystem
        menuSystem = transform.root.gameObject.GetComponent<MenuSystem>();

        //get the button and text
        button = GetComponent<Image>();
        text = transform.Find("Text").gameObject.GetComponent<Text>();

        //set trs
        sca = gameObject.transform.localScale;
        rot = gameObject.transform.localRotation;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Start()
    {
        Inactive();
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.INACTIVE
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Inactive()
    {
        //update status
        currentState = MenuButtonState.INACTIVE;

        //simple change real quick
        button.color = color_inactive;
        text.color = textColor_inactive;

        //perform action
        if (inactive != null)
        {
            //action_inactive.Action();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.HOVER
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Hover()
    {
        //update status
        currentState = MenuButtonState.HOVER;

        //simple change real quick
        button.color = color_hover;
        text.color = textColor_hover;

        //perform action
        if (hover != null)
        {
            //action_hover.Action();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.ACTIVE
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Active()
    {
        //update status
        currentState = MenuButtonState.ACTIVE;

        //simple change real quick
        button.color = color_active;
        text.color = textColor_active;

        //perform action
        if (active != null)
        {
            active.Active();
        }

        else if(menuToLoad != null)
        {
            StartCoroutine(menuSystem.LoadNewMenu(menuToLoad));
        }
        else if(levelToLoad != "")
        {
            SceneManager.LoadScene(levelToLoad.ToString());
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets a menu button to MenuButtonState.DISABLED
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public virtual void Disabled()
    {
        //update status
        currentState = MenuButtonState.DISABLED;
        button.color = color_disabled;
        text.color = textColor_disabled;

        //perform action
        if (disabled != null)
        {
            //action_inactive.Action();
        }
    }
    #endregion
}
