///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — PulsateEngines.cs
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

public class PulsateEngines : MonoBehaviour
{
    #region FIELDS
    [Header("ENGINE TRAIL MODEL")]
    [SerializeField]
    Transform etm;
    [Header("ENGINE TRAIL SCALES")]
    [SerializeField]
    float[] minScales;
    [SerializeField]
    float[] maxScales;
    [SerializeField]
    float[] minTimes;
    [SerializeField]
    float[] maxTimes;
    [Header("RANDOMNESS")]
    [SerializeField]
    bool isRandom;
    [SerializeField]
    bool flipPositiveNegative;
    bool isPositive;
    [Header("VALUE (if not random use min)")]
    [SerializeField]
    float valueMin;
    [SerializeField]
    float valueMax;
    float value;
    [Header("TIME (if not random use min)")]
    [SerializeField]
    float timeMin;
    [SerializeField]
    float timeMax;
    float time;
    [Header("DELAY")]
    [SerializeField]
    float delayMin;
    [SerializeField]
    float delayMax;
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
        StartValueBounce();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_UPDATE_THRUST>(UpdateThrustMaxMin);
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
    void StartValueBounce()
    {
        DetermineValues();
        PingPongValues();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    float RandomFloat(float _min, float _max)
    {
        return Random.Range(_min, _max);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetRandomValues()
    {
        //Debug.Log("flipped! now we are " + isPositive);

        value = RandomFloat(valueMin, valueMax);
        time = RandomFloat(timeMin, timeMax);
        delay = RandomFloat(delayMin, delayMax);

        if (flipPositiveNegative)
        {
            if (isPositive)
            {
                Mathf.Abs(value);
                isPositive = false;
            }
            else
            {
                isPositive = true;
                if (value <= 0)
                {
                    return;
                }
                else
                {
                    value *= -1f;
                }
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineValues()
    {
        if (isRandom)
        {
            SetRandomValues();
        }
        else
        {
            if (value == valueMin)
            {
                value = valueMax;
                time = timeMax;
                delay = delayMax;
            }
            else
            {
                value = valueMin;
                time = timeMin;
                delay = delayMin;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void PingPongValues()
    {
        float _currentValue = etm.localScale.y;
        LeanTween.value(this.gameObject, UpdateEngineScale, _currentValue, value, time)
                 .setDelay(delay).setEase(ease)
                 .setOnComplete(StartValueBounce);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateEngineScale(float _value)
    {
        //Debug.Log("value = " + _value);

        etm.localScale = new Vector3(etm.localScale.x, _value, _value);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateThrustMaxMin(EVENT_UPDATE_THRUST _event)
    {
        switch (_event.thrust)
        {
            case ThrustType.BRAKING:
                valueMin = minScales[0];
                valueMax = maxScales[0];
                timeMin = minTimes[0];
                timeMax = maxTimes[0];
                break;
            case ThrustType.CRUISE:
                valueMin = minScales[1];
                valueMax = maxScales[1];
                timeMin = minTimes[1];
                timeMax = maxTimes[1];
                break;
            case ThrustType.MAX_SPEED:
                valueMin = minScales[2];
                valueMax = maxScales[2];
                timeMin = minTimes[2];
                timeMax = maxTimes[2];
                break;
            case ThrustType.BOOST_SPEED:
                valueMin = minScales[3];
                valueMax = maxScales[3];
                timeMin = minTimes[3];
                timeMax = maxTimes[3];
                break;
            default:
                break;
        }
        LeanTween.cancel(this.gameObject);
        StartValueBounce();
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
        Events.instance.RemoveListener<EVENT_UPDATE_THRUST>(UpdateThrustMaxMin);
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