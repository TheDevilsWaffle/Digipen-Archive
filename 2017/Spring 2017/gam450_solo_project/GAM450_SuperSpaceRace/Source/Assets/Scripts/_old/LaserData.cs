///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LaserData.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#region ENUMS
public enum Status
{
    OPERATIONAL,
    DISABLED,
    BROKEN,
};
#endregion

#region EVENTS

#endregion

public class LaserData : MonoBehaviour
{
    #region FIELDS
    Transform tr;
    [Header("INITIALIZATION")]
    [SerializeField]
    int initialLaserCount = 7;
    [SerializeField]
    [Range(0f,1f)]
    float initialLaserEnergy = 1f;
    static float energyPerShot = 0.1428f;
    public static float EnergyPerShot
    {
        get { return energyPerShot; }
    }
    [Header("SFX")]  
    [SerializeField]
    AudioClip sfx_laser_cannon;
    [SerializeField]
    AudioClip sfx_laser_charged;
    [SerializeField]
    AudioClip sfx_laser_discharged;
    [SerializeField]
    AudioClip sfx_laser_dry_shot;
    [SerializeField]
    AudioClip sfx_laser_empty;
    [SerializeField]
    AudioClip sfx_laser_explosion;
    [SerializeField]
    AudioClip sfx_laser_fire;
    [SerializeField]
    AudioClip sfx_laser_full;
    [SerializeField]
    AudioClip sfx_laser_operational;  

    static int totalLaserCount;
    public static int TotalLaserCount
    {
        get { return totalLaserCount; }
        set { totalLaserCount = value; }
    }

    public static int index;
    static List<bool> lasers;
    public static List<bool>Lasers
    {
        get { return lasers; }
        private set { lasers = value; }
    }
    static Status status;
    public static Status Status
    {
        get { return status; }
        set { status = value;}
    }
    Status previousStatus;

    static bool currentLaser;
    public static bool CurrentLaser
    {
        get { return currentLaser; }
        private set { currentLaser = value; }
    }
    static float laserEnergy;
    public static float LaserEnergy
    {
        get { return laserEnergy; }
        set { laserEnergy = value; }
    }
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
        tr = GetComponent<Transform>();

        //initial values
        TotalLaserCount = initialLaserCount;
        LaserEnergy = initialLaserEnergy;
        index = TotalLaserCount - 1;
        Lasers = new List<bool> { true, true, true, true, true, true, true };
        Status = Status.OPERATIONAL;
        previousStatus = Status;
        CurrentLaser = Lasers[index];

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
        Events.instance.AddListener<EVENT_LASER_FIRED>(DecreaseLaserCount);
        Events.instance.AddListener<EVENT_LASER_DISCHARGED>(DecreaseLaserCount);
        Events.instance.AddListener<EVENT_LASER_CHARGED>(IncreaseLaserCount);
        Events.instance.AddListener<EVENT_LASER_EMPTY>(LasersEmptyWarning);
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
        previousStatus = Status;

        //Debug.Log("Laser[" + index + "] = " + Lasers[index]);
#if true
        UpdateTesting();
    #endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTicks()
    {
        
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LasersEmptyWarning(EVENT_LASER_EMPTY _event)
    {
        //audio warning
        AudioSystem.Instance.MakeAudioSource(sfx_laser_empty.name, tr.position);
        status = Status.DISABLED;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool CheckLaserStatus(Status _status)
    {
        bool _state;
        switch (_status)
        {
            case Status.OPERATIONAL:
                Events.instance.Raise(new EVENT_LASER_STATUS_OPERATIONAL());
                _state = true;
                break;
            case Status.DISABLED:
                Events.instance.Raise(new EVENT_LASER_STATUS_DISABLED());
                _state = false;
                break;
            case Status.BROKEN:
                Events.instance.Raise(new EVENT_LASER_STATUS_DISABLED());
                _state = false;
                break;
            default:
                _state = false;
                break;
        }
        return _state;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void WaitBeforeEmptyLaserWarning()
    {
        Events.instance.Raise(new EVENT_LASER_EMPTY());
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseLaserCount(EVENT_LASER_FIRED _event)
    {
        //check laser status
        if(CheckLaserStatus(Status))
        {
            //edge case, lasers is empty
            if(index == 0 && Lasers[index] == false)
            {
                Debug.Log("Lasers[" + index + "] = " + Lasers[index] + "\nCANNOT FIRE");
            }
            else
            {
                //correct index from being less than zero, and let the player know lasers are empty
                if (index <= 0)
                {
                    index = 0;
                    //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(index));
                    Lasers[index] = false;
                    CurrentLaser = Lasers[index];
                    //print("CurrentLaser = " + CurrentLaser);
                    UpdateLaserEnergy(-EnergyPerShot);

                    //sfx fire laser cannon and make laser noise
                    AudioSystem.Instance.MakeAudioSource(sfx_laser_fire.name, _event.laser.position);
                    AudioSystem.Instance.MakeAudioSource(sfx_laser_cannon.name, tr.position);
                    LeanTween.delayedCall(0.35f, WaitBeforeEmptyLaserWarning);
                }
                else
                {
                    //update hud before we change index
                    //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(index));

                    Lasers[index--] = false;
                    //print("laser index is now at: " + index);
                    //Debug.Log("Laser fired and is now at [" + index + "] = " + Lasers[index]);
                    CurrentLaser = Lasers[index];
                    //print("CurrentLaser = "+CurrentLaser);
                    UpdateLaserEnergy(-EnergyPerShot);

                    //sfx fire laser cannon and make laser noise
                    AudioSystem.Instance.MakeAudioSource(sfx_laser_fire.name, _event.laser.position);
                    AudioSystem.Instance.MakeAudioSource(sfx_laser_cannon.name, tr.position);
                }
                
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseLaserCount(EVENT_LASER_DISCHARGED _event)
    {
        Debug.Log("DecreaseLaserCount()");
        if(CheckLaserStatus(Status))
        {
            //edge case, we are on the last laser
            if(index == 0 && Lasers[index] == true)
            {
                Lasers[index] = false;
                CurrentLaser = Lasers[index];
                //UpdateLaserEnergy(-EnergyPerShot);
                return;
            }
            //we have no lasers left, dry shot
            else if(index == 0 && Lasers[index] == false)
            {
                CurrentLaser = Lasers[index];
                return;
            }
            //decrease laser count as normal
            Lasers[index--] = false;
            CurrentLaser = Lasers[index];
            //UpdateLaserEnergy(-EnergyPerShot);
            //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(index));
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void IncreaseLaserCount(EVENT_LASER_CHARGED _event)
    {
        Debug.Log("IncreaseLaserCount()");
        if (CheckLaserStatus(Status))
        {
            //we were at zero, charge it
            if(index == 0 && Lasers[index] == false)
            {
                Debug.Log("index = 0, charging zero laser");
                Lasers[index] = true;
                CurrentLaser = Lasers[index];
                //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(index));
            }
            //we have shot zero charged, time to move on
            else if(index == 0 && Lasers[index] == true)
            {
                Debug.Log("index still at zero, moving on to 1");
                Lasers[++index] = true;
                CurrentLaser = Lasers[index];
                //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(index + 1));
            }
            else if(index < 6)
            {
                Debug.Log("normal charge");
                Lasers[++index] = true;
                CurrentLaser = Lasers[index];
                //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(index + 1));
            }
            else if(index >= 6)
            {
                Debug.Log("full");
                index = 6;
                CurrentLaser = Lasers[index];
                //Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(7));
                //Events.instance.Raise(new EVENT_LASER_FULLY_CHARGED());
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateLaserEnergy(float _energyPerShot)
    {
        LaserEnergy += _energyPerShot;
        if (LaserEnergy < 0)
        {
            LaserEnergy = 0;
        }
        else if(LaserEnergy > 1f)
        {
            LaserEnergy = 1f;
        }
        Debug.Log("UpdateLaserEnergy() LaserEnergy is now: " + LaserEnergy);
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
        Events.instance.RemoveListener<EVENT_LASER_FIRED>(DecreaseLaserCount);
        Events.instance.RemoveListener<EVENT_LASER_DISCHARGED>(DecreaseLaserCount);
        Events.instance.RemoveListener<EVENT_LASER_CHARGED>(IncreaseLaserCount);
        Events.instance.RemoveListener<EVENT_LASER_EMPTY>(LasersEmptyWarning);
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
            //Events.instance.Raise(new EVENT_LASER_FIRED());
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            //Events.instance.Raise(new EVENT_LASER_DISCHARGED());
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            //Events.instance.Raise(new EVENT_LASER_CHARGED());
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