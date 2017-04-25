///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDAnimator_Alpha.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
//using UnityEngine.UI;
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

public class HUDAnimator_Alpha : MonoBehaviour
{
    #region FIELDS
    [Header("REFERENCES")]
    [SerializeField]
    RectTransform imageRT;
    [SerializeField]
    RectTransform textRT;

    [Header("ALPHA NORMAL")]
    [SerializeField]
    float alphaNormal;
    [SerializeField]
    LeanTweenType easeNormal;
    [SerializeField]
    float timeNormal;
    [SerializeField]
    float delayNormal;
    [SerializeField]
    LeanTweenType loopTypeNormal;

    [Header("ALPHA HIGHLIGHT")]
    [SerializeField]
    float alphaHighlight;
    [SerializeField]
    LeanTweenType easeHighlight;
    [SerializeField]
    float timeHighlight;
    [SerializeField]
    float delayHighlight;
    [SerializeField]
    LeanTweenType loopTypeHighlight;

    [Header("ALPHA IN")]
    [SerializeField]
    float alphaIn;
    [SerializeField]
    LeanTweenType easeIn;
    [SerializeField]
    float timeIn;
    [SerializeField]
    float delayIn;
    [SerializeField]
    LeanTweenType loopTypeIn;

    [Header("ALPHA OUT")]
    [SerializeField]
    float alphaOut;
    [SerializeField]
    LeanTweenType easeOut;
    [SerializeField]
    float timeOut;
    [SerializeField]
    float delayOut;
    [SerializeField]
    LeanTweenType loopTypeOut;

    int imageID;
    int textID;
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

        //initial values

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
    public void AnimateImageAlphaNormal()
    {
        AnimateImageAlpha(imageRT, alphaNormal, easeNormal, timeNormal, delayNormal, loopTypeNormal);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextAlphaNormal()
    {
        AnimateTextAlpha(textRT, alphaNormal, easeNormal, timeNormal, delayNormal, loopTypeNormal);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageAlphaHighlight()
    {
        AnimateImageAlpha(imageRT, alphaHighlight, easeHighlight, timeHighlight, delayHighlight, loopTypeHighlight);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextAlphaHighlight()
    {
        AnimateTextAlpha(textRT, alphaHighlight, easeHighlight, timeHighlight, delayHighlight, loopTypeHighlight);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageAlphaOut()
    {
        AnimateImageAlpha(imageRT, alphaOut, easeOut, timeOut, delayOut, loopTypeOut);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextAlphaOut()
    {
        AnimateTextAlpha(textRT, alphaOut, easeOut, timeOut, delayOut, loopTypeOut);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageAlphaIn()
    {
        AnimateImageAlpha(imageRT, alphaIn, easeIn, timeIn, delayIn, loopTypeIn);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextAlphaIn()
    {
        AnimateTextAlpha(textRT, alphaIn, easeIn, timeIn, delayIn, loopTypeIn);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rt"></param>
    /// <param name="_alpha"></param>
    /// <param name="_ease"></param>
    /// <param name="_time"></param>
    /// <param name="_delay"></param>
    /// <param name="_loops"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateImageAlpha(RectTransform _rt, float _alpha, LeanTweenType _ease, float _time, float _delay, LeanTweenType _loopType)
    {
        LeanTween.cancel(imageID);
        imageID = LeanTween.alpha(_rt, _alpha, _time).setDelay(_delay).setEase(_ease).setLoopType(_loopType).id;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rt"></param>
    /// <param name="_alpha"></param>
    /// <param name="_ease"></param>
    /// <param name="_time"></param>
    /// <param name="_delay"></param>
    /// <param name="_loops"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateTextAlpha(RectTransform _rt, float _alpha, LeanTweenType _ease, float _time, float _delay, LeanTweenType _loopType)
    {
        LeanTween.cancel(textID);
        textID = LeanTween.alphaText(_rt, _alpha, _time).setDelay(_delay).setEase(_ease).setLoopType(_loopType).id;
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
            AnimateImageAlphaIn();
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            AnimateImageAlphaOut();
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            AnimateImageAlphaNormal();
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            AnimateImageAlphaHighlight();
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