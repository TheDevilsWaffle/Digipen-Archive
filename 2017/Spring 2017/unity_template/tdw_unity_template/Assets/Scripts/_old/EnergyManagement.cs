///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — EnergyManagement.cs
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
public class EVENT_INCREASE_ENERGY : GameEvent
{
    public EnergyType energyType;
    public float chargeRate;
    public EVENT_INCREASE_ENERGY(EnergyType _energyType, float _chargeRate)
    {
        energyType = _energyType;
        chargeRate = _chargeRate;
    }
}
public class EVENT_DECREASE_ENERGY : GameEvent
{
    public EnergyType energyType;
    public float drainRate;
    public EVENT_DECREASE_ENERGY(EnergyType _energyType, float _drainRate)
    {
        energyType = _energyType;
        drainRate = _drainRate;
    }
}
public class EVENT_SET_ENERGY_TO_ZERO : GameEvent
{
    public EnergyType energyType;
    public EVENT_SET_ENERGY_TO_ZERO(EnergyType _energyType)
    {
        energyType = _energyType;
    }
}
public class EVENT_SET_ENERGY : GameEvent
{
    public EnergyType energyType;
    public float energy;
    public EVENT_SET_ENERGY(EnergyType _energyType, float _energy)
    {
        energyType = _energyType;
        energy = _energy;
    }
}
#endregion

public class EnergyManagement : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    float drainRate = 0.025f;
    [SerializeField]
    float boostDrainRate = 0.05f;
    [SerializeField]
    float throttleDrainRate = 0.0025f;
    [SerializeField]
    float chargeRate = 0.125f;
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
        //get player input every frame
        GamePadInputData _player = GamePadInput.players[0];

        //CYCLE SYSTEM -- not in use, keeping just in case
        //CycleSystem(_player.X, _player.Y);

        //BRAKE CHARGE / SLOW DRAIN
        //if(_player.RB == GamePadButtonState.HELD)
        //{
        //    ReallocateEnergy(ShipData.ThrottleEnergy);
        //}
        //else if (_player.RB == GamePadButtonState.INACTIVE)
        //{
        //    UpdateThrust(ShipData.currentThrust, ShipData.ThrottleEnergy);
        //    EnergyDrain(drainRate);
        //}

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
    /// independent energy cycling for weapons/shields
    /// </summary>
    /// <param name="_x"> button state of x</param>
    /// <param name="_y"> button state of y</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CycleSystem(GamePadButtonState _x, GamePadButtonState _y)
    {
        if (_x == GamePadButtonState.PRESSED)
        {
            CycleThrough(EnergyType.SHIELDS);
        }
        if (_y == GamePadButtonState.PRESSED)
        {
            CycleThrough(EnergyType.WEAPONS);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// passed energy type increases/decreases all other energy types
    /// </summary>
    /// <param name="_energyType">current energy type</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CycleThrough(EnergyType _energyType)
    {
        //switch (_energyType)
        //{
        //    case EnergyType.WEAPONS:
        //        if (ShipData.WeaponsStatus == SystemStatus.WORKING)
        //        {
        //            //check if full
        //            if (ShipData.WeaponsEnergy < ShipData.EnergyMax)
        //            {
        //                Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.WEAPONS, chargeRate));
        //            }
        //            else
        //            {
        //                Events.instance.Raise(new EVENT_SET_ENERGY_TO_ZERO(EnergyType.WEAPONS));
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("Weapon's energy cannot increase energy because system status = " + ShipData.WeaponsStatus);
        //        }
        //        break;
        //    case EnergyType.SHIELDS:
        //        if (ShipData.ShieldsStatus == SystemStatus.WORKING)
        //        {
        //            //check if full
        //            if (ShipData.ShieldsEnergy < ShipData.EnergyMax)
        //            {
        //                Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.SHIELDS, chargeRate));
        //            }
        //            else
        //            {
        //                Events.instance.Raise(new EVENT_SET_ENERGY_TO_ZERO(EnergyType.SHIELDS));
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("Shield's energy cannot increase energy because system status = " + ShipData.ShieldsStatus);
        //        }
        //        break;
        //    case EnergyType.THROTTLE:
        //        if (ShipData.ThrottleStatus == SystemStatus.WORKING)
        //        {
        //            //check if full
        //            if (ShipData.ThrottleEnergy < ShipData.EnergyMax)
        //            {
        //                Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.THROTTLE, chargeRate));
        //            }
        //            else
        //            {
        //                Events.instance.Raise(new EVENT_SET_ENERGY_TO_ZERO(EnergyType.THROTTLE));
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("Throttle's energy cannot increase energy because system status = " + ShipData.ThrottleStatus);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// takes energy from the throttle pool and puts it towards weapons/shields
    /// </summary>
    /// <param name="_energy">throttle energy available</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ReallocateEnergy(float _energy)
    {
        if (_energy > 0f)
        {
            Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.THROTTLE, throttleDrainRate));
            if (ShipData.WeaponsEnergy < 1f)
            {
                Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.WEAPONS, chargeRate));
            }
            if (ShipData.ShieldsEnergy < 1f)
            {
                Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.SHIELDS, chargeRate));
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the thrust energy pool depending on the current thrust type
    /// </summary>
    /// <param name="_thrustType">current thrust type</param>
    /// <param name="_energy">current thrust energy level</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateThrust(ThrustType _thrustType, float _energy)
    {
        switch (_thrustType)
        {
            case ThrustType.CRUISE:
                if(GamePadInput.players[0].RT == GamePadButtonState.INACTIVE 
                   && _energy > ShipData.cruiseThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.THROTTLE, throttleDrainRate));
                }
                else if (GamePadInput.players[0].RT == GamePadButtonState.INACTIVE 
                         && _energy < ShipData.cruiseThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.THROTTLE, chargeRate));
                }
                break;
            case ThrustType.MAX_SPEED:
                if (GamePadInput.players[0].RT == GamePadButtonState.HELD
                    && _energy > ShipData.maxThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.THROTTLE, throttleDrainRate));
                }
                else if (GamePadInput.players[0].RT == GamePadButtonState.HELD
                         && _energy < ShipData.maxThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.THROTTLE, chargeRate));
                }
                break;
            case ThrustType.BOOST_SPEED:
                if (GamePadInput.players[0].RT == GamePadButtonState.HELD
                    && GamePadInput.players[0].R3 == GamePadButtonState.HELD
                    && _energy > ShipData.boostThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.THROTTLE, throttleDrainRate));
                }
                else if (GamePadInput.players[0].RT == GamePadButtonState.HELD
                         && GamePadInput.players[0].R3 == GamePadButtonState.HELD
                         && _energy < ShipData.boostThrottleEnergy)
                {
                    Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.THROTTLE, chargeRate));
                }
                EnergyDrain(boostDrainRate);
                break;
            case ThrustType.BRAKING:
            //case ThrustType.MIN_SPEED:
            //    if (GamePadInput.players[0].LT == GamePadButtonState.HELD
            //        && _energy > ShipData.minThrottleEnergy)
            //    {
            //        Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.THROTTLE, throttleDrainRate));
            //    }
            //    else if (GamePadInput.players[0].LT == GamePadButtonState.HELD
            //             && _energy < ShipData.minThrottleEnergy)
            //    {
            //        Events.instance.Raise(new EVENT_INCREASE_ENERGY(EnergyType.THROTTLE, chargeRate));
            //    }
            //    break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// constantly drains some energy from weapons/shields
    /// </summary>
    /// <param name="_drainRate">rate of drain</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void EnergyDrain(float _drainRate)
    {
        //print(ShipData.ShieldsEnergy);
        if (ShipData.WeaponsEnergy > 0f)
        {
            Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.WEAPONS, _drainRate));
        }
        if (ShipData.ShieldsEnergy > 0f)
        {
            Events.instance.Raise(new EVENT_DECREASE_ENERGY(EnergyType.SHIELDS, _drainRate));
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