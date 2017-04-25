///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MessageSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

#region ENUMS
public enum HUDMessageType
{
    MAJOR,
    MINOR
};
#endregion

#region EVENTS
public class EVENT_HUD_MESSAGE : GameEvent
{
    public HUDMessageType type;
    public string message;
    public float duration;
    public bool isAllCaps;
    public EVENT_HUD_MESSAGE(HUDMessageType _type, string _message, float _duration, bool _isAllCaps)
    {
        type = _type;
        message = _message;
        duration = _duration;
        isAllCaps = _isAllCaps;
    }
}
#endregion

public class MessageSystem : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    RectTransform major_rt;
    Text major_txt;
    [SerializeField]
    RectTransform minor_rt;
    Text minor_txt;

    [Header("")]
    [SerializeField]
    int x;

    [Header("")]
    [SerializeField]
    int y;

    [Header("")]
    [SerializeField]
    int z;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //refs
        if (major_rt != null)
            major_txt = major_rt.GetChild(0).gameObject.GetComponent<Text>();
        if (minor_rt != null)
            minor_txt = minor_rt.GetChild(0).gameObject.GetComponent<Text>();
        //initial values

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        TurnOffMessage(major_rt);
        TurnOffMessage(minor_rt);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_HUD_MESSAGE>(DisplayMessage);
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


    #if true
        UpdateTesting();
    #endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FadeOutMajorMessage()
    {
        major_rt.GetComponent<HUDAnimator_Alpha>().AnimateTextAlphaOut();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FadeOutMinorMessage()
    {
        minor_rt.GetComponent<HUDAnimator_Alpha>().AnimateTextAlphaOut();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TurnOffMessage(RectTransform _rt)
    {
        _rt.GetComponent<HUDAnimator_Alpha>().AnimateTextAlphaOut();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DisplayMessage(EVENT_HUD_MESSAGE _event)
    {
        //Debug.Log("DisplayMessage()\n\t" + _event.message + ", " + _event.type);
        RectTransform _rt = null;
        switch (_event.type)
        {
            case HUDMessageType.MAJOR:
                _rt = major_rt;
                major_txt.text = _event.message;
                LeanTween.delayedCall(_event.duration, FadeOutMajorMessage);
                if (_event.isAllCaps)
                {
                    major_txt.text.ToUpper();
                }
                break;
            case HUDMessageType.MINOR:
                _rt = minor_rt;
                minor_txt.text = _event.message;
                LeanTween.delayedCall(_event.duration, FadeOutMinorMessage);
                if (_event.isAllCaps)
                {
                    minor_txt.text.ToUpper();
                }
                break;
            default:
                Debug.Log("This case should never be reached!");
                break;
        }
        _rt.GetComponent<HUDAnimator_Alpha>().AnimateTextAlphaIn();
        
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_HUD_MESSAGE>(DisplayMessage);
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            Events.instance.Raise(new EVENT_HUD_MESSAGE(HUDMessageType.MAJOR, "poop", 2f, true));
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}