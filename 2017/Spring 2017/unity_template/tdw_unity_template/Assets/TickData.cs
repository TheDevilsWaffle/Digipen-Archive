///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TickData.cs
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

public class TickData : MonoBehaviour
{
    #region FIELDS
    [Header("BACKGROUND")]
    [SerializeField]
    Image background;
    [SerializeField]
    Color background_color;
    [SerializeField]
    Color background_disabled = new Color(1f, 1f, 1f, 0f);

    [Header("GLOW")]
    [SerializeField]
    Image glow;
    [SerializeField]
    Color glow_color;
    [SerializeField]
    Color glow_disabled = new Color(1f, 1f, 1f, 0f);

    [Header("OUTLINE")]
    [SerializeField]
    Image outline;
    [SerializeField]
    Color outline_color;
    [SerializeField]
    Color outline_disabled = new Color(1f, 1f, 1f, 0f);
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
    public void ToggleAll(bool _isOn)
    {
        ToggleGlow(_isOn);
        ToggleBackground(_isOn);
        ToggleOutline(_isOn);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ToggleGlow(bool _isOn)
    {
        if(_isOn)
        {
            glow.color = glow_color;
        }
        else
        {
            glow.color = glow_disabled;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ToggleOutline(bool _isOn)
    {
        if (_isOn)
        {
            outline.color = outline_color;
        }
        else
        {
            outline.color = outline_disabled;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ToggleBackground(bool _isOn)
    {
        if (_isOn)
        {
            background.color = background_color;
        }
        else
        {
            background.color = background_disabled;
        }
    }
    public void UpdateBackgroundImageFill(float _value)
    {
        //Debug.Log("UpdateBackgroundImageFill("+_value+")");
        background.fillAmount = _value;
    }
    public void UpdateGlowImageFill(float _value)
    {
        //Debug.Log(this.gameObject.name +": UpdateGlowImageFill(" + _value + ")");
        glow.fillAmount = _value;
    }
    #endregion

    #region PRIVATE METHODS
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