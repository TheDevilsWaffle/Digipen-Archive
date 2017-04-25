///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDThrottleSystem.cs
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
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class HUDThrottleSystem : MonoBehaviour
{
    #region FIELDS
    [Header("METERS")]
    [SerializeField]
    RectTransform throttle;
    Image throttle_image;
    [SerializeField]
    Color throttle_color;
    [SerializeField]
    RectTransform boost;
    Image boost_image;
    [SerializeField]
    Color boost_color;

    [Header("TEXT")]
    [SerializeField]
    RectTransform valueTxt;
    Text valueTxt_text;
    [SerializeField]
    RectTransform speedTxt;
    Text speedTxt_text;
    [SerializeField]
    RectTransform boostTxt;
    Text boostTxt_text;
    bool isBoosting = false;
    [SerializeField]
    Color colorOn = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    Color colorOff = new Color(1f, 1f, 1f, 0.5f);

    [Header("SPEED VALUE MODIFIER")]
    [SerializeField]
    float speedModifier = 3f;
    [SerializeField]
    float boostModifier = 4.25f;

    RectTransform rt;
    CanvasGroup cg;
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
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();

        throttle_image = throttle.GetComponent<Image>();
        boost_image = boost.GetComponent<Image>();
        valueTxt_text = valueTxt.GetComponent<Text>();
        speedTxt_text = speedTxt.GetComponent<Text>();
        boostTxt_text = boostTxt.GetComponent<Text>();

        //initial values
        SetInitialValues();
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
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetInitialValues()
    {
        throttle_image.color = throttle_color;
        boost_image.color = boost_color;

        SetThrottle(0f);
        SetValueText(0);

        SetBoost(0f);
        ToggleBoostText(false);
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
        UpdateThrottle(ShipData.SpeedPercentage, ShipData.SpeedValue);
        UpdateBoost(ShipData.IsBoosting, ShipData.BoostSpeedPercentage);

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
    void UpdateThrottle(float _percentage, int _value)
    {
        //Debug.Log("Speed Percentage = " + _percentage);
        SetThrottle(_percentage);
        SetValueText(_value);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateBoost(bool _isBoosting, float _percentage)
    {
        if(_isBoosting != isBoosting)
        {
            ToggleBoostText(_isBoosting);
        }
        SetBoost(_percentage);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetThrottle(float _value)
    {
        throttle_image.fillAmount = _value;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetBoost(float _value)
    {
        //Debug.Log("boost value = "+ _value);
        boost_image.fillAmount = _value;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetValueText(int _value)
    {
        if(isBoosting)
        {
            _value = (int)(_value * boostModifier);
        }
        else
        {
            _value = (int)(_value * speedModifier);
        }
        valueTxt_text.text = _value.ToString();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleBoostText(bool _isOn)
    {
        if (_isOn)
        {
            boostTxt_text.color = colorOn;
        }
        else
        {
            boostTxt_text.color = colorOff;
        }
        isBoosting = _isOn;
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