///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — EnergySystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;



#region ENUMS
#endregion

#region EVENTS
public class EVENT_UPDATE_SHIELD_ENERGY : GameEvent
{
    public float energy;
    public EVENT_UPDATE_SHIELD_ENERGY(float _energy)
    {
        energy = _energy;
    }
}
public class EVENT_UPDATE_WEAPON_ENERGY : GameEvent
{
    public float energy;
    public EVENT_UPDATE_WEAPON_ENERGY(float _energy)
    {
        energy = _energy;
    }
}
public class EVENT_UPDATE_THROTTLE_ENERGY : GameEvent
{
    public float energy;
    public EVENT_UPDATE_THROTTLE_ENERGY(float _energy)
    {
        energy = _energy;
    }
}
#endregion

public class EnergySystem : MonoBehaviour
{
    #region FIELDS
    [Header("WEAPONS")]
    [SerializeField]
    RectTransform weapons;
    RectTransform weaponsBar;
    EnergyData weaponsEnergy;

    [Header("SHIELDS")]
    [SerializeField]
    RectTransform shields;
    RectTransform shieldsBar;
    EnergyData shieldsEnergy;

    [Header("THROTTLE")]
    [SerializeField]
    RectTransform throttle;
    RectTransform throttleBar;
    EnergyData throttleEnergy;
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
        weaponsBar = weapons.transform.FindChild("Bar").GetComponent<RectTransform>();
        weaponsEnergy = weapons.GetComponent<EnergyData>();

        shieldsBar = shields.transform.FindChild("Bar").GetComponent<RectTransform>();
        shieldsEnergy = shields.GetComponent<EnergyData>();

        throttleBar = throttle.transform.FindChild("Bar").GetComponent<RectTransform>();
        throttleEnergy = throttle.GetComponent<EnergyData>();
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
        Events.instance.AddListener<EVENT_INCREASE_ENERGY>(IncreaseEnergy);
        Events.instance.AddListener<EVENT_DECREASE_ENERGY>(DecreaseEnergy);
        Events.instance.AddListener<EVENT_SET_ENERGY_TO_ZERO>(SetEnergyToZero);
        Events.instance.AddListener<EVENT_SET_ENERGY>(SetEnergy);
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

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void IncreaseEnergy(EVENT_INCREASE_ENERGY _event)
    {
        switch (_event.energyType)
        {
            case EnergyType.WEAPONS:
                ShipData.WeaponsEnergy += _event.chargeRate;
                //weaponsEnergy.UpdateEnergy(ShipData.WeaponsEnergy);
                Events.instance.Raise(new EVENT_UPDATE_WEAPON_ENERGY(ShipData.WeaponsEnergy));

                break;
            case EnergyType.SHIELDS:
                ShipData.ShieldsEnergy += _event.chargeRate;
                //shieldsEnergy.UpdateEnergy(ShipData.ShieldsEnergy);
                Events.instance.Raise(new EVENT_UPDATE_SHIELD_ENERGY(ShipData.ShieldsEnergy));
                break;
            case EnergyType.THROTTLE:
                ShipData.ThrottleEnergy += _event.chargeRate;
                Events.instance.Raise(new EVENT_UPDATE_THROTTLE_ENERGY(ShipData.ThrottleEnergy));
                //throttleEnergy.UpdateEnergy(ShipData.ThrottleEnergy);
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseEnergy(EVENT_DECREASE_ENERGY _event)
    {
        switch (_event.energyType)
        {
            case EnergyType.WEAPONS:
                ShipData.WeaponsEnergy -= _event.drainRate;
                Events.instance.Raise(new EVENT_UPDATE_WEAPON_ENERGY(ShipData.WeaponsEnergy));
                //weaponsEnergy.UpdateEnergy(ShipData.WeaponsEnergy);
                break;
            case EnergyType.SHIELDS:
                ShipData.ShieldsEnergy -= _event.drainRate;
                //shieldsEnergy.UpdateEnergy(ShipData.ShieldsEnergy);
                Events.instance.Raise(new EVENT_UPDATE_SHIELD_ENERGY(ShipData.ShieldsEnergy));
                break;
            case EnergyType.THROTTLE:
                ShipData.ThrottleEnergy -= _event.drainRate;
                Events.instance.Raise(new EVENT_UPDATE_THROTTLE_ENERGY(ShipData.ThrottleEnergy));
                //throttleEnergy.UpdateEnergy(ShipData.ThrottleEnergy);
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetEnergyToZero(EVENT_SET_ENERGY_TO_ZERO _event)
    {
        switch (_event.energyType)
        {
            case EnergyType.WEAPONS:
                ShipData.WeaponsEnergy = 0;
                Events.instance.Raise(new EVENT_UPDATE_WEAPON_ENERGY(ShipData.WeaponsEnergy));
                //weaponsEnergy.SetEnergyToZero();
                Mathf.Clamp01(ShipData.ThrottleEnergy += (ShipData.ThrottleStepping * 4));
                //throttleEnergy.AddToEnergy((ShipData.ThrottleStepping * 4));
                break;
            case EnergyType.SHIELDS:
                ShipData.ShieldsEnergy = 0;
                Events.instance.Raise(new EVENT_UPDATE_SHIELD_ENERGY(ShipData.ShieldsEnergy));
                //shieldsEnergy.SetEnergyToZero();
                Mathf.Clamp01(ShipData.ThrottleEnergy += (ShipData.ThrottleStepping * 4));
                //throttleEnergy.AddToEnergy((ShipData.ThrottleStepping * 4));
                break;
            case EnergyType.THROTTLE:
                Events.instance.Raise(new EVENT_UPDATE_THROTTLE_ENERGY(ShipData.ThrottleEnergy));
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetEnergy(EVENT_SET_ENERGY _event)
    {
        switch (_event.energyType)
        {
            case EnergyType.WEAPONS:
                ShipData.WeaponsEnergy = _event.energy;
                Events.instance.Raise(new EVENT_UPDATE_WEAPON_ENERGY(ShipData.WeaponsEnergy));
                //weaponsEnergy.UpdateEnergy(ShipData.WeaponsEnergy);
                break;
            case EnergyType.SHIELDS:
                ShipData.ShieldsEnergy = _event.energy;
                //shieldsEnergy.UpdateEnergy(ShipData.ShieldsEnergy);
                break;
            case EnergyType.THROTTLE:
                ShipData.ThrottleEnergy = _event.energy;
                Events.instance.Raise(new EVENT_UPDATE_THROTTLE_ENERGY(ShipData.ThrottleEnergy));
                //throttleEnergy.UpdateEnergy(ShipData.ThrottleEnergy);
                break;
            default:
                break;
        }
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
        Events.instance.RemoveListener<EVENT_INCREASE_ENERGY>(IncreaseEnergy);
        Events.instance.RemoveListener<EVENT_DECREASE_ENERGY>(DecreaseEnergy);
        Events.instance.RemoveListener<EVENT_SET_ENERGY_TO_ZERO>(SetEnergyToZero);
        Events.instance.RemoveListener<EVENT_SET_ENERGY>(SetEnergy);
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