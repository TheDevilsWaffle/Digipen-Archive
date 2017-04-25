///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDNavigation.cs
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

public class HUDNavigation : MonoBehaviour
{
    #region FIELDS
    [Header("REFERENCES")]
    [SerializeField]
    Transform ship;
    [SerializeField]
    Transform northStar;
    float ship_angle;
    [SerializeField]
    RectTransform scroller;
    float center;
    float left;
    float right;
    float stepping;
    float range;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        SetSubscriptions();
        SetScrollerInfo();
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
        //get ship angle
        ship_angle = ship.rotation.eulerAngles.y - northStar.transform.eulerAngles.y;
        print("ship angle = " + ship_angle);
        UpdateScrollerPosition(ship_angle);
        print("scroller position = " + scroller.localPosition);
        print("scroller center = " + center +", left = " + left +", right = " + right);
        print("scroller range = " + range +", scroller stepping = " + stepping);


        //print("scroller = " + scroller.localPosition.x);

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
    void SetScrollerInfo()
    {
        center = scroller.localPosition.x;
        left = scroller.rect.xMin;
        right = scroller.rect.xMax;
        range = right - left;
        stepping = range / 360f;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_shipAngle"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateScrollerPosition(float _shipAngle)
    {
        float _y = scroller.localPosition.y;
        float _z = scroller.localPosition.z;
        float _x = 0f;


        if (ship_angle < 180f)
        {
            _x = center + (stepping * _shipAngle);
        }
        else if(ship_angle > 180f && _shipAngle < 360f)
        {
            _x = center - (stepping * _shipAngle);
        }

        scroller.localPosition = new Vector3(_x, _y, _z);
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