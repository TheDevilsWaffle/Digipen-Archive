///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDAnimator_Rotation.cs
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

public class HUDAnimator_Rotation : HUDAnimatorBase
{
    #region FIELDS
    RectTransform rt;
    [Header("ROTATE CLOCKWISE")]
    [SerializeField]
    LeanTweenType easeCW;
    [SerializeField]
    RectTransform rtCW;
    [SerializeField]
    Vector3 rotationCW;
    [SerializeField]
    float timeCW;
    [SerializeField]
    float delayCW;

    [Header("ROTATE COUTNER-CLOCKWISE")]
    [SerializeField]
    LeanTweenType easeCCW;
    [SerializeField]
    RectTransform rtCCW;
    [SerializeField]
    Vector3 rotationCCW;
    [SerializeField]
    float timeCCW;
    [SerializeField]
    float delayCCW;
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
        if (rtCW != null)
        {
            rotationCW = rtCW.anchoredPosition;
        }
        if (rtCCW != null)
        {
            rotationCCW = rtCCW.anchoredPosition;
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


    #if true
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
    public void RotateClockwise()
    {
        //Debug.Log("RotateClockwise()");
        LeanTween.rotate(rt, rotationCW, timeCW).setDelay(delayCW).setEase(easeCW);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void RotateCounterClockwise()
    {
        //Debug.Log("RotateCounterClockwise()");
        LeanTween.rotate(rt, rotationCCW, timeCCW).setDelay(delayCCW).setEase(easeCCW);
    }
    #endregion

    #region PRIVATE METHODS

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
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            RotateClockwise();
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            RotateCounterClockwise();
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