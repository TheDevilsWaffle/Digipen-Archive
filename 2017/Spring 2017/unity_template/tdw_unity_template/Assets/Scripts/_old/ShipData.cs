///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ShipData.cs
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
public class EVENT_UPDATE_HUD_WEAPON_FIRED : GameEvent
{
    public int ticks;
    public float energy;
    public EVENT_UPDATE_HUD_WEAPON_FIRED(int _ticks, float _energy)
    {
        ticks = _ticks;
        energy = _energy;
    }
}

#endregion

public class ShipData : MonoBehaviour
{
    #region FIELDS
    [Header("WEAPONS")]
    [SerializeField]
    Color weaponColor;

    [Header("SHIELDS")]
    [SerializeField]
    Color shieldsColor;

    
    [Header("THROTTLE")]
    [SerializeField]
    Color throttleColor;
    //THROTTLE
    static int speedValue;
    public static int SpeedValue
    {
        get { return speedValue; }
        set { speedValue = value; }
    }
    static float speedPercentage;
    public static float SpeedPercentage
    {
        get { return speedPercentage; }
        set { speedPercentage = value; }
    }
    static int speedMax;
    public static int SpeedMax
    {
        get { return speedMax; }
        set { speedMax = value; }
    }
    //BOOST
    static bool isBoosting;
    public static bool IsBoosting
    {
        get { return isBoosting; }
        set { isBoosting = value; }
    }
    static int boostMaxSpeed;
    public static int BoostMaxSpeed
    {
        get { return boostMaxSpeed; }
        set { boostMaxSpeed = value; }
    }
    static float boostSpeedPercentage;
    public static float BoostSpeedPercentage
    {
        get { return boostSpeedPercentage; }
        set { boostSpeedPercentage = value; }
    }
    //BREAKING
    static bool isBraking;
    public static bool IsBraking
    {
        get { return isBraking; }
        set { isBraking = value; }
    }

    [SerializeField]
    float weaponsStep;
    [SerializeField]
    float shieldsStep;
    [SerializeField]
    float throttleStep;
    [SerializeField]
    float startingEnergy = 0.5f;
    public static float minThrottleEnergy = 0.15f;
    public static float cruiseThrottleEnergy = 0.35f;
    public static float maxThrottleEnergy = 0.75f;
    public static float boostThrottleEnergy = 1f;
    public static ThrustType currentThrust;
    
    static float energyMax = 1f;
    public static float EnergyMax
    {
        get { return energyMax; }
    }
    //WEAPONS
    static float weaponsStepping;
    public static float WeaponsStepping
    {
        get { return weaponsStepping; }
        set { weaponsStepping = value; }
    }
    static float weaponsEnergy;
    public static float WeaponsEnergy
    {
        get { return weaponsEnergy; }
        set { weaponsEnergy = value; }
    }
    public static int weaponTicks = 7;
    public static int WeaponTicks
    {
        get { return weaponTicks; }
        set { weaponTicks = value; }
    }
    public static float weaponEnergyPerShot = .1428f;
    public static float WeaponEnergyPerShot
    {
        get { return weaponEnergyPerShot; }
        private set { weaponEnergyPerShot = value; }
    }
    //static SystemStatus weaponsStatus = SystemStatus.WORKING;
    //public static SystemStatus WeaponsStatus
    //{
    //    get { return weaponsStatus; }
    //    private set { weaponsStatus = value; }
    //}

    //SHIELDS
    static float shieldsStepping;
    public static float ShieldsStepping
    {
        get { return shieldsStepping; }
        set { shieldsStepping = value; }
    }
    static float shieldsEnergy;
    public static float ShieldsEnergy
    {
        get { return shieldsEnergy; }
        set { shieldsEnergy = value; }
    }
    //static SystemStatus shieldsStatus = SystemStatus.WORKING;
    //public static SystemStatus ShieldsStatus
    //{
    //    get { return shieldsStatus; }
    //    set { shieldsStatus = value; }
    //}
    public static int shieldTicks = 2;
    public static int ShieldTicks
    {
        get { return shieldTicks; }
        set { shieldTicks = value; }
    }
    public static float shieldEnergyPerTick = 0.5f;
    //THROTTLE
    static float throttleStepping;
    public static float ThrottleStepping
    {
        get { return throttleStepping; }
        set { throttleStepping = value; }
    }
    static float throttleEnergy;
    public static float ThrottleEnergy
    {
        get { return throttleEnergy; }
        set { throttleEnergy = value; }
    }
    //static SystemStatus throttleStatus = SystemStatus.WORKING;
    //public static SystemStatus ThrottleStatus
    //{
    //    get { return throttleStatus; }
    //    private set { throttleStatus = value; }
    //}
    int previousWeaponTick;
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
        IsBoosting = false;
        IsBraking = false;

        WeaponsStepping = weaponsStep;
        ShieldsStepping = shieldsStep;
        ThrottleStepping = throttleStep;

        WeaponsEnergy = startingEnergy;
        ShieldsEnergy = startingEnergy;
        ThrottleEnergy = startingEnergy;

        previousWeaponTick = WeaponTicks;

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Events.instance.Raise(new EVENT_SET_ENERGY(EnergyType.WEAPONS, startingEnergy));
        Events.instance.Raise(new EVENT_SET_ENERGY(EnergyType.SHIELDS, startingEnergy));
        Events.instance.Raise(new EVENT_SET_ENERGY(EnergyType.THROTTLE, startingEnergy));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_CANNON_FIRED>(WeaponFired);
        Events.instance.AddListener<EVENT_LASER_EMPTY>(UpdateStatus);
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
        //UpdateWeaponEnergy();
        //if(previousWeaponTick > WeaponTicks)
        //{
        //    WeaponTicks++;
        //    //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(WeaponTicks));
        //}
        //else if(previousWeaponTick < WeaponTicks)
        //{
        //    --WeaponTicks;
        //    //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(WeaponTicks));
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
    void UpdateStatus(EVENT_LASER_EMPTY _event)
    {
        //WeaponsStatus = SystemStatus.DISABLED;
        //LaserData.Status = Status.DISABLED;
        //LaserData.index = WeaponTicks;
        //if(LaserData.index > 6)
        //{
        //    LaserData.index = 6;
        //}
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateWeaponEnergy()
    {
        //print("previousWeaponTick = " + previousWeaponTick);
        //print("weaponTick = " + WeaponTicks);
        //previousWeaponTick = (int)(Mathf.Floor(WeaponsEnergy / WeaponEnergyPerShot));
        //print(previousWeaponTick);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void WeaponFired(EVENT_CANNON_FIRED _event)
    {
        //WeaponsEnergy -= weaponEnergyPerShot;
        //if (WeaponsEnergy < 0)
        //    WeaponsEnergy = 0;
        ////--WeaponTicks;
        //Events.instance.Raise(new EVENT_UPDATE_HUD_WEAPON_FIRED(WeaponTicks, WeaponsEnergy));
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
        Events.instance.RemoveListener<EVENT_LASER_EMPTY>(UpdateStatus);
        Events.instance.RemoveListener<EVENT_CANNON_FIRED>(WeaponFired);
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