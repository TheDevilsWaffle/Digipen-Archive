///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — FieldOfViewSystem.cs
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
    public EVENT_EXAMPLE() { }
}
*/ 
#endregion

public class FieldOfViewSystem : MonoBehaviour
{
    #region FIELDS
    [Header("FIELD OF VIEW SETTINGS")]
    [SerializeField]
    [Range(30f, 120f)]
    float[] fovRanges = new float[4] { 0, 0, 0, 0 };
    [SerializeField]
    float[] times = new float[4] { 0, 0, 0, 0 };
    [SerializeField]
    float delay;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInCirc;

    float time;
    float fov;
    Camera camera;
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
        camera = GetComponent<Camera>();

        //initial values
        fov = camera.fieldOfView;
        time = times[0];

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
                fov = fovRanges[0];
                time = times[0];
                break;

            case ThrustType.CRUISE:
                fov = fovRanges[1];
                time = times[1];

                break;
            case ThrustType.MAX_SPEED:
                fov = fovRanges[2];
                time = times[2];

                break;

            case ThrustType.BOOST_SPEED:
                fov = fovRanges[3];
                time = times[3];

                break;

            default:
                break;
        }
        UpdateFieldOfView();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateFieldOfView()
    {
        float _currentFOV = camera.fieldOfView;

        LeanTween.cancel(this.gameObject);

        LeanTween.value(this.gameObject, SetFieldOfViewValue, _currentFOV, fov, time)
                 .setDelay(delay)
                 .setEase(ease);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetFieldOfViewValue(float _value)
    {
        //Debug.Log("fov = " + _value);
        camera.fieldOfView = _value;
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