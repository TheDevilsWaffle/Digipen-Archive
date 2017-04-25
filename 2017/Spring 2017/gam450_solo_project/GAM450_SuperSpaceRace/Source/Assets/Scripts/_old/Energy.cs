///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Energy.cs
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
//public class EVENT_ : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class Energy : MonoBehaviour
{
    #region FIELDS
    [Header("BOOST DRAIN")]
    [SerializeField]
    float drainRateNormal = 0.25f;
    [SerializeField]
    float drainRateIncreased = 0.5f;

    float shieldDrainRate;
    float weaponDrainRate;

    [Header("BRAKE CHARGE")]
    [SerializeField]
    float chargeRateNormal = 0.25f;
    [SerializeField]
    float chargeRateIncreased = 0.5f;

    float shieldChargeRate;
    float weaponChargeRate;
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
        weaponDrainRate = drainRateNormal;
        shieldDrainRate = drainRateNormal;

        weaponChargeRate = chargeRateNormal;
        shieldChargeRate = chargeRateNormal;


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
        //Debug.Log("BOOST = " + ShipData.IsBoosting + ", and BRAKE = " + ShipData.IsBraking);
        //Debug.Log("WEAPON ENERGY = "+ShipData.WeaponsEnergy+", SHIELD ENERGY = "+ShipData.ShieldsEnergy);
        if (ShipData.IsBoosting && ShipData.IsBraking == false)
        {
            DrainWeapons();
            DrainShields();
        }
        else if(ShipData.IsBraking)
        {
            //Debug.Log("BRAKING!");
            ChargeWeapons();
            ChargeShields();
        }

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
    void DrainWeapons()
    {
        //WEAPONS
        if (LaserData.Status == Status.OPERATIONAL)
        {
            //still have energy, drain it
            if (LaserData.LaserEnergy > 0)
            {
                shieldDrainRate = drainRateNormal;
                LaserData.LaserEnergy -= weaponDrainRate;
                if (LaserData.LaserEnergy <= 0)
                {
                    LaserData.LaserEnergy = 0;
                    Events.instance.Raise(new EVENT_LASER_EMPTY());
                }
                //update tick data
                UpdateLaserTicks(LaserData.LaserEnergy, LaserData.EnergyPerShot);
            }
            //energy is drained, increase shield drain rate and send event
            else
            {
                shieldDrainRate = drainRateIncreased;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ChargeWeapons()
    {
        //WEAPONS
        if (LaserData.Status == Status.OPERATIONAL || LaserData.Status == Status.DISABLED)
        {
            //if we were previous disabled, set back to operational, send message
            if(LaserData.Status == Status.DISABLED)
            {
                LaserData.Status = Status.OPERATIONAL;
                Events.instance.Raise(new EVENT_LASER_STATUS_OPERATIONAL());

            }
            //not yet full, charge it
            if (LaserData.LaserEnergy < 1)
            {
                shieldChargeRate = chargeRateNormal;

                LaserData.LaserEnergy += weaponChargeRate;
                //Debug.Log("ChargeWeapons()\nLaserEnergy = " + LaserData.LaserEnergy);
                if (LaserData.LaserEnergy >= 1f)
                {
                    LaserData.LaserEnergy = 1f;
                    Events.instance.Raise(new EVENT_LASER_FULLY_CHARGED());
                }
                //update tick data
                UpdateLaserTicks(LaserData.LaserEnergy, LaserData.EnergyPerShot);
            }
            //energy is full, increase shield charge rate and send event
            else
            {
                shieldDrainRate = chargeRateIncreased;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DrainShields()
    {
        //SHIELDS
        //if (ShipData.ShieldsStatus == SystemStatus.WORKING)
        //{
        //    //still have energy, drain it
        //    if (ShipData.ShieldsEnergy > 0)
        //    {
        //        weaponDrainRate = drainRateNormal;

        //        ShipData.ShieldsEnergy -= shieldDrainRate;
        //        if(ShipData.ShieldsEnergy < 0)
        //        {
        //            ShipData.ShieldsEnergy = 0;
        //        }
        //    }
        //    //energy is drained, increase weapon drain rate and send event
        //    else
        //    {
        //        Events.instance.Raise(new EVENT_SHIELD_EMPTY());
        //        weaponDrainRate = drainRateIncreased;
        //    }
        //}
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ChargeShields()
    {
        //SHIELDS
        //if (ShipData.ShieldsStatus == SystemStatus.WORKING || ShipData.ShieldsStatus == SystemStatus.DISABLED)
        //{
        //    //if we were previous disabled, set back to operational, send message
        //    if (ShipData.ShieldsStatus == SystemStatus.DISABLED)
        //    {
        //        ShipData.ShieldsStatus = SystemStatus.WORKING;
        //        Events.instance.Raise(new EVENT_LASER_STATUS_OPERATIONAL());

        //    }
        //    //not yet full, charge it
        //    if (ShipData.ShieldsEnergy < 1)
        //    {
        //        weaponChargeRate = chargeRateNormal;

        //        ShipData.ShieldsEnergy += shieldChargeRate;
        //        if (ShipData.ShieldsEnergy > 1f)
        //        {
        //            ShipData.ShieldsEnergy = 1f;
        //        }
        //    }
        //    //energy is full, increase weapon charge rate and send event
        //    else
        //    {
        //        Events.instance.Raise(new EVENT_SHIELD_FULLY_CHARGED());
        //        weaponChargeRate = chargeRateIncreased;
        //    }
        //}
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateLaserTicks(float _energy, float _energyPerShot)
    {
        ////Debug.Log("UpdateLaserTicks("+_energy+", "+_energyPerShot+")");
        //int _tempIndex = LaserData.index;
        //_tempIndex = (int)(_energy / _energyPerShot);
        //Debug.Log("LaserData.index = " + LaserData.index + ", and tempIndex = " +_tempIndex);
        ////increased by a tick
        //if(_tempIndex > LaserData.index)
        //{
        //    Events.instance.Raise(new EVENT_LASER_CHARGED());
        //}
        //else if(_tempIndex < LaserData.index)
        //{
        //    Events.instance.Raise(new EVENT_LASER_DISCHARGED());
        //}
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