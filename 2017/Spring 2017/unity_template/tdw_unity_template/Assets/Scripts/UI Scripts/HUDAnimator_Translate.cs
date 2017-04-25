///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDAnimator_Translate.cs
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

public class HUDAnimator_Translate : HUDAnimatorBase
{
    #region FIELDS
    RectTransform rt;
    [Header("TRANSLATE IN")]
    [SerializeField]
    LeanTweenType easeIn;
    [SerializeField]
    RectTransform rtIn;
    [SerializeField]
    Vector3 positionIn;
    [SerializeField]
    float timeIn;
    [SerializeField]
    float delayIn;

    [Header("TRANSLATE OUT")]
    [SerializeField]
    LeanTweenType easeOut;
    [SerializeField]
    RectTransform rtOut;
    [SerializeField]
    Vector3 positionOut;
    [SerializeField]
    float timeOut;
    [SerializeField]
    float delayOut;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void Awake()
    {
        base.Awake();

        //refs
        rt = GetComponent<RectTransform>();

        //initial values
        if (rtIn != null)
        {
            positionIn = rtIn.anchoredPosition;
        }
        if (rtOut != null)
        {
            positionOut = rtOut.anchoredPosition;
        }

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void Start()
    {
        base.Start();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void SetSubscriptions()
    {
        base.SetSubscriptions();
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

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void TranslateIn()
    {
        //Debug.Log("TranslateIn()");
        LeanTween.move(rt, positionIn, timeIn).setDelay(delayIn).setEase(easeIn);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void TranslateOut()
    {
        //Debug.Log("TranslateOut()");
        LeanTween.move(rt, positionOut, timeOut).setDelay(delayOut).setEase(easeOut);
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void OnDestroy()
    {
        base.OnDestroy();
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
            TranslateIn();
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            TranslateOut();
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