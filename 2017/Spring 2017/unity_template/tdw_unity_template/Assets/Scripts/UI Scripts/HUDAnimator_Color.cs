///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDAnimator_Color.cs
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

#endregion

#region EVENTS
/*
public class EVENT_EXAMPLE
{
    public class EVENT_EXAMPLE() { }
}
*/ 
#endregion

public class HUDAnimator_Color : MonoBehaviour
{
    #region FIELDS
    [Header("IMAGE")]
    [SerializeField]
    RectTransform imageRT;
    [SerializeField]
    Color imageColor;
    Color imageColorOriginal;
    [SerializeField]
    LeanTweenType imageEase;
    [SerializeField]
    float imageTime;
    [SerializeField]
    float imageDelay;
    [SerializeField]
    LeanTweenType imageLoopType;

    [Header("TEXT")]
    [SerializeField]
    RectTransform textRT;
    [SerializeField]
    Color textColor;
    Color textColorOriginal;
    [SerializeField]
    LeanTweenType textEase;
    [SerializeField]
    float textTime;
    [SerializeField]
    float textDelay;
    [SerializeField]
    LeanTweenType textLoopType;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        if (imageRT != null)
        {
            imageColorOriginal = imageRT.GetComponent<Image>().color;
        }
        if (textRT != null)
        {
            textColorOriginal = textRT.GetComponent<Text>().color;
        }

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
    
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        //Events.instance.AddListener<>();
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


    #if false
        UpdateTesting();
    #endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageColorTo()
    {
        AnimateImageColor(imageRT, imageColor, imageTime, imageDelay, imageEase, imageLoopType);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextColorTo()
    {
        AnimateTextColor(textRT, textColor, textTime, textDelay, textEase, textLoopType);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageColorBackToOriginal()
    {
        AnimateImageColor(imageRT, imageColorOriginal, imageTime, imageDelay, imageEase, imageLoopType);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextColorBackToOriginal()
    {
        AnimateTextColor(textRT, textColorOriginal, textTime, textDelay, textEase, textLoopType);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rt"></param>
    /// <param name="_color"></param>
    /// <param name="_time"></param>
    /// <param name="_delay"></param>
    /// <param name="_ease"></param>
    /// <param name="_loopType"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateImageColor(RectTransform _rt, Color _color, float _time, float _delay, LeanTweenType _ease, LeanTweenType _loopType)
    {
        if (imageRT != null)
        {
            LeanTween.color(_rt, _color, _time).setDelay(_delay).setEase(_ease).setLoopType(_loopType);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rt"></param>
    /// <param name="_color"></param>
    /// <param name="_time"></param>
    /// <param name="_delay"></param>
    /// <param name="_ease"></param>
    /// <param name="_loopType"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateTextColor(RectTransform _rt, Color _color, float _time, float _delay, LeanTweenType _ease, LeanTweenType _loopType)
    {
        if (textRT != null)
        {
            LeanTween.colorText(_rt, _color, _time).setDelay(_delay).setEase(_ease).setLoopType(_loopType);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

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
        //Events.instance.RemoveListener<>();
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
            AnimateImageColorTo();
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            AnimateImageColorBackToOriginal();
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
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
        }
        //Keypad 5
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}