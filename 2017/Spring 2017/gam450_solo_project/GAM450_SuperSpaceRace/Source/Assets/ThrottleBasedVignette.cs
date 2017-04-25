///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ThrottleBasedVignette.cs
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
using UnityStandardAssets.ImageEffects;

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

public class ThrottleBasedVignette : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    VignetteAndChromaticAberration vca;

    [Header("THRUST VALUES")]
    [SerializeField]
    float[] chromaticValues;
    float chromaticValue;
    [SerializeField]
    float[] vignetteValues;
    float vignetteValue;

    [Header("ANIMATION")]
    [SerializeField]
    float time;
    [SerializeField]
    float delay;
    [Header("EASING")]
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInOutBounce;
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

        SetSubscriptions();
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
        Events.instance.AddListener<EVENT_UPDATE_THRUST>(UpdateThrust);
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
    void UpdateThrust(EVENT_UPDATE_THRUST _event)
    {
        //Debug.Log("UpdateThrust(" + _event.thrust + ")");
        switch (_event.thrust)
        {
            case ThrustType.BRAKING:
                chromaticValue = chromaticValues[0];
                vignetteValue = vignetteValues[0];
                break;
            case ThrustType.CRUISE:
                chromaticValue = chromaticValues[1];
                vignetteValue = vignetteValues[1];
                break;
            case ThrustType.MAX_SPEED:
                chromaticValue = chromaticValues[2];
                vignetteValue = vignetteValues[2];
                break;
            case ThrustType.BOOST_SPEED:
                chromaticValue = chromaticValues[3];
                vignetteValue = vignetteValues[3];
                break;
            default:
                break;
        }
        UpdateChromaticAndVignette();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateChromaticAndVignette()
    {
        float _currentValue = vca.chromaticAberration;
        float _currentIntensity = vca.intensity;
        LeanTween.value(this.gameObject, UpdateChromaticAberration, _currentValue, chromaticValue, time)
                 .setDelay(delay)
                 .setEase(ease);
        LeanTween.value(this.gameObject, UpdateVignette, _currentIntensity, vignetteValue, time)
                 .setDelay(delay)
                 .setEase(ease);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateChromaticAberration(float _value)
    {
        //Debug.Log("chromaticAberration = " + _value);
        vca.chromaticAberration = _value;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateVignette(float _value)
    {
        //Debug.Log("Vignette = " + _value);
        vca.intensity = _value;
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
        Events.instance.RemoveListener<EVENT_UPDATE_THRUST>(UpdateThrust);
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