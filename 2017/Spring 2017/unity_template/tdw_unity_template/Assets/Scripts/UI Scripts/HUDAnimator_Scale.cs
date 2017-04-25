///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDAnimator_Scale.cs
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

public class HUDAnimator_Scale : HUDAnimatorBase
{
    #region FIELDS
    RectTransform rt;
    [Header("SCALE IN")]
    [SerializeField]
    LeanTweenType easeIn;
    [SerializeField]
    RectTransform rtIn;
    [SerializeField]
    float scaleInFactor;
    Vector3 scaleIn;
    [SerializeField]
    float timeIn;
    [SerializeField]
    float delayIn;

    [Header("SCALE OUT")]
    [SerializeField]
    LeanTweenType easeOut;
    [SerializeField]
    RectTransform rtOut;
    [SerializeField]
    bool returnToInitialScale;
    [SerializeField]
    float scaleOutFactor;
    Vector3 scaleOut;
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

        //set values
        scaleIn = rt.localScale * scaleInFactor;

        if (returnToInitialScale)
        {
            scaleOut = rt.localScale;
        }
        else
        {
            scaleOut = rt.localScale * scaleOutFactor;
        }

        //initial values
        if (rtIn != null)
        {
            scaleIn = rtIn.localScale * scaleInFactor;
        }
        if (rtOut != null)
        {
            scaleOut = rtOut.localScale * scaleInFactor;
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
    public void AnimateScaleIn()
    {
        //Debug.Log("AnimateScaleIn()");
        LeanTween.scale(rt, scaleIn, timeIn).setDelay(delayIn).setEase(easeIn);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AnimateScaleOut()
    {
        //Debug.Log("AnimateScaleOut()");
        LeanTween.scale(rt, scaleOut, timeOut).setDelay(delayOut).setEase(easeOut);
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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            AnimateScaleIn();
        }
        //Keypad 1
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            AnimateScaleOut();
        }
        //Keypad 2
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {

        }
        //Keypad 3
        if (Input.GetKeyDown(KeyCode.Keypad3))
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
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
    }
    #endregion
}