///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LaserSystem.cs
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
public enum SystemStatus
{
    BROKEN,
    DISABLED,
    OPERATIONAL
};
#endregion

#region EVENTS
public class EVENT_LASER_FIRED : GameEvent
{
    public Transform laser;
    public EVENT_LASER_FIRED(Transform _laser)
    {
        laser = _laser;
    }
}
public class EVENT_HUD_UPDATE_LASER_INFO : GameEvent
{
    public float energy;
    public int ticks;
    public EVENT_HUD_UPDATE_LASER_INFO(float _energy, int _ticks)
    {
        energy = _energy;
        ticks = _ticks;
    }
}
public class EVENT_LASER_DISCHARGED : GameEvent
{
    public int lasersTotal;
    public EVENT_LASER_DISCHARGED(int _lasersTotal)
    {
        lasersTotal = _lasersTotal;
    }
}
public class EVENT_LASER_CHARGED : GameEvent
{
    public int lasersTotal;
    public EVENT_LASER_CHARGED(int _lasersTotal)
    {
        lasersTotal = _lasersTotal;
    }
}
public class EVENT_LASER_EMPTY : GameEvent
{
    public EVENT_LASER_EMPTY() { }
}
public class EVENT_LASER_FULLY_CHARGED : GameEvent
{
    public EVENT_LASER_FULLY_CHARGED() { }
}
public class EVENT_LASER_DRY_SHOT : GameEvent
{
    public Transform cannon;
    public EVENT_LASER_DRY_SHOT(Transform _cannon)
    {
        cannon = _cannon;
    }
}
public class EVENT_LASER_STATUS_BROKEN : GameEvent
{
    public EVENT_LASER_STATUS_BROKEN() { }
}
public class EVENT_LASER_STATUS_DISABLED : GameEvent
{
    public EVENT_LASER_STATUS_DISABLED() { }
}
public class EVENT_LASER_STATUS_OPERATIONAL : GameEvent
{
    public EVENT_LASER_STATUS_OPERATIONAL() { }
}
#endregion

public class LaserSystem : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    [Range(0f,1f)]
    float startingEnergy = 1f;
    public static float energy;
    [SerializeField]
    int startingLaserTotal = 7;
    public static int lasersTotal;
    [SerializeField]
    [Range(0f, 1f)]
    float energyUsedPerLaser = 0.1428f;
    public static float perLaserEnergy;

    [Header("SFX")]
    [SerializeField]
    AudioClip sfx_laser_fire;
    [SerializeField]
    AudioClip sfx_laser_cannon;
    [SerializeField]
    AudioClip sfx_laser_dry_shot;
    [SerializeField]
    AudioClip sfx_laser_charged;
    [SerializeField]
    AudioClip sfx_laser_discharged;
    [SerializeField]
    AudioClip sfx_laser_empty;
    [SerializeField]
    AudioClip sfx_laser_full;


    public static int index;
    public static bool currentLaser;
    public static bool[] lasers;     
    public static SystemStatus status = SystemStatus.OPERATIONAL;
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
        energy = startingEnergy;
        lasersTotal = startingLaserTotal;
        lasers = new bool[] { true, true, true, true, true, true, true };
        index = lasersTotal - 1;
        currentLaser = lasers[index];
        perLaserEnergy = energyUsedPerLaser;
        status = SystemStatus.OPERATIONAL;

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
        Events.instance.AddListener<EVENT_LASER_FIRED>(FireLaser);
        Events.instance.AddListener<EVENT_LASER_DRY_SHOT>(DryShot);
        Events.instance.AddListener<EVENT_LASER_CHARGED>(LaserCharged);
        Events.instance.AddListener<EVENT_LASER_DISCHARGED>(LaserDischarged);
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
    public void UpdateLaserEnergy(float _value)
    {
        //Debug.Log("UpdateLaserEnergy(" + _value + ")");

        energy += _value;
        int _previousTotal = lasersTotal;
        if (energy > 1)
        {
            energy = 1;
        }
        else if (energy < 0f)
        {
            lasersTotal = 0;
            energy = 0f;
        }

        //Debug.Log("energy after addition/subtraction/clamp = "+energy);

        

        if (energy >= (perLaserEnergy * 6) && energy <= 1)
        {
            lasersTotal = 7;
        }
        else if (energy >= (perLaserEnergy * 5) && energy <= (perLaserEnergy * 6))
        {
            lasersTotal = 6;
        }
        else if (energy >= (perLaserEnergy * 4) && energy <= (perLaserEnergy * 5))
        {
            lasersTotal = 5;
        }
        else if (energy >= (perLaserEnergy * 3) && energy <= (perLaserEnergy * 4))
        {
            lasersTotal = 4;
        }
        else if (energy >= (perLaserEnergy * 2) && energy <= (perLaserEnergy * 3))
        {
            lasersTotal = 3;
        }
        else if (energy >= (perLaserEnergy * 1) && energy <= (perLaserEnergy * 2))
        {
            lasersTotal = 2;
        }
        else if (energy > (perLaserEnergy * 0) && energy <= (perLaserEnergy * 1))
        {
            lasersTotal = 1;
        }
        else
        {
            lasersTotal = 0;
        }


        //Debug.Log("previousTotal = " + _previousTotal + "\nlasersTotal = " + lasersTotal);

        //check to see if we've gained a laser
        DetermineCurrentLaser(_previousTotal);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FireLaser(EVENT_LASER_FIRED _event)
    {
        UpdateLaserEnergy(-perLaserEnergy);

        //sfx
        AudioSystem.Instance.MakeAudioSource(sfx_laser_cannon.name);
        AudioSystem.Instance.MakeAudioSource(sfx_laser_fire.name);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DryShot(EVENT_LASER_DRY_SHOT _event)
    {
        //sfx
        AudioSystem.Instance.MakeAudioSource(sfx_laser_dry_shot.name, _event.cannon.position, _event.cannon);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateLaserStatus(SystemStatus _status)
    {
        status = _status;

        switch (_status)
        {
            case SystemStatus.BROKEN:
                Events.instance.Raise(new EVENT_LASER_STATUS_BROKEN());
                break;
            case SystemStatus.DISABLED:
                Events.instance.Raise(new EVENT_LASER_STATUS_DISABLED());
                break;
            case SystemStatus.OPERATIONAL:
                Events.instance.Raise(new EVENT_LASER_STATUS_OPERATIONAL());
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineCurrentLaser(int _previousTotal)
    {
        //Debug.Log("lasersTotal = "+ lasersTotal + ", and previousTotal = " + _previousTotal);
        if(lasersTotal == 0 && _previousTotal == 0)
        {
            return;
        }
        if (lasersTotal > _previousTotal)
        {
            if (lasersTotal == 7)
            {
                index = 6;
                lasers[index] = true;
                currentLaser = lasers[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(lasersTotal, index));

                Events.instance.Raise(new EVENT_LASER_CHARGED(lasersTotal));
                Events.instance.Raise(new EVENT_LASER_FULLY_CHARGED());
                AudioSystem.Instance.MakeAudioSource(sfx_laser_full.name);

            }
            else if (lasersTotal == 1)
            {
                //update the HUD
                Events.instance.Raise(new EVENT_LASER_STATUS_OPERATIONAL());
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(lasersTotal, index));

                index = 0;
                lasers[index] = true;
                currentLaser = lasers[index];

                Events.instance.Raise(new EVENT_LASER_CHARGED(lasersTotal));
            }
            else
            {
                ++index;
                lasers[index] = true;
                currentLaser = lasers[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(lasersTotal, index));

                Events.instance.Raise(new EVENT_LASER_CHARGED(lasersTotal));
            }
            //Debug.Log("gained a laser, lasers[" + index + "] = " + currentLaser);
        }
        //or check to see if we've lost a laser
        if (lasersTotal < _previousTotal || lasersTotal == 0)
        {
            //check to make sure we don't go out of bounds
            if (lasersTotal == 0)
            {
                index = 0;
                lasers[index] = false;
                currentLaser = lasers[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(lasersTotal, index));

                Events.instance.Raise(new EVENT_LASER_DISCHARGED(lasersTotal));
                Events.instance.Raise(new EVENT_LASER_EMPTY());
                AudioSystem.Instance.MakeAudioSource(sfx_laser_empty.name);
            }
            else if (lasersTotal == 1)
            {
                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(lasersTotal, index));

                lasers[index] = false;
                index = 0;
                currentLaser = lasers[index];

                Events.instance.Raise(new EVENT_LASER_DISCHARGED(lasersTotal));
            }
            else
            {
                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(lasersTotal, index));

                lasers[index] = false;
                --index;
                currentLaser = lasers[index];
                Events.instance.Raise(new EVENT_LASER_DISCHARGED(lasersTotal));
            }
            //Debug.Log("lost a laser, lasers[" + index + "] = " + currentLaser);
        }
        //update the HUD's energy level shown
        Events.instance.Raise(new EVENT_UPDATE_HUD_LASER_ENERGY(index, energy, perLaserEnergy));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LaserCharged(EVENT_LASER_CHARGED _event)
    {
        //sfx
        AudioSystem.Instance.MakeAudioSource(sfx_laser_charged.name);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LaserDischarged(EVENT_LASER_DISCHARGED _event)
    {
        //sfx
        AudioSystem.Instance.MakeAudioSource(sfx_laser_discharged.name);
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
        Events.instance.RemoveListener<EVENT_LASER_FIRED>(FireLaser);
        Events.instance.RemoveListener<EVENT_LASER_DRY_SHOT>(DryShot);
        Events.instance.RemoveListener<EVENT_LASER_CHARGED>(LaserCharged);
        Events.instance.RemoveListener<EVENT_LASER_DISCHARGED>(LaserDischarged);
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
            UpdateLaserEnergy(-perLaserEnergy);
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            UpdateLaserEnergy(perLaserEnergy);
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