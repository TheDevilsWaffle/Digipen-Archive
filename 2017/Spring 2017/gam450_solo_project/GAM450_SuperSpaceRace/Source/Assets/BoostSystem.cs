///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — BoostSystem.cs
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
using XInputDotNetPure;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_UPDATE_THRUST : GameEvent
{
    public ThrustType thrust;
    public EVENT_UPDATE_THRUST(ThrustType _thrust)
    {
        thrust = _thrust;
    }
}
#endregion

public class BoostSystem : MonoBehaviour
{
    #region FIELDS
    public static ThrustType thrust;

    [Header("RUMBLE")]
    [SerializeField]
    [Range(0f, 1f)]
    float[] leftIntensity;
    [SerializeField]
    [Range(0f, 1f)]
    float[] rightIntensity;
    [SerializeField]
    float rumbleNormalizer = 0.05f;
    float rumbleConstant;
    float rumbleVariable;
    float currentConstant;

    [Header("DRAIN")]
    [SerializeField]
    float drain = 0.00025f;
    [SerializeField]
    float drainIncreased = 0.001f;

    [Header("CHARGE")]
    [SerializeField]
    float charge = 0.00025f;
    [SerializeField]
    float chargeIncreased = 0.001f;

    [Header("SFX")]
    [SerializeField]
    AudioClip sfx_brake;
    [SerializeField]
    AudioClip sfx_thrust_shift_up;
    [SerializeField]
    AudioClip sfx_thrust_boost_to_max;
    [SerializeField]
    AudioClip sfx_thrust_cruise_to_max;
    [SerializeField]
    AudioClip sfx_thrust_max_to_cruise;

    LaserSystem ls;
    ShieldSystem ss;
    Transform tr;
    ThrustType previousThrust;
    float timer;
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
        ls = GetComponent<LaserSystem>();
        ss = GetComponent<ShieldSystem>();
        tr = GetComponent<Transform>();

        //initial values
        previousThrust = ThrustType.BRAKING;
        thrust = ThrustType.CRUISE;
        rumbleConstant = leftIntensity[1];
        currentConstant = rumbleConstant;
        rumbleVariable = rightIntensity[0];
        timer = 0f;

        

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        GamePad.SetVibration((PlayerIndex)0, leftIntensity[1], rightIntensity[1]);
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
        if (!PauseMenuSystem.isPaused)
        {
            rumbleVariable -= rumbleNormalizer;
            //if (thrust == ThrustType.BRAKING || thrust == ThrustType.CRUISE)
            //{
            //    if(thrust == ThrustType.CRUISE)
            //    {
            //        if (timer <= 0f)
            //        {
            //            timer = 3f;
            //            rumbleConstant = rightIntensity[1];
            //        }
            //        else if (timer > 0f)
            //        {
            //            timer -= Time.deltaTime;
            //        }
            //    }
            //    rumbleConstant -= rumbleNormalizer;
            //    if (rumbleConstant < 0)
            //    {
            //        rumbleConstant = 0;
            //    }
            //}
            if (rumbleVariable < 0f)
            {
                rumbleVariable = 0f;
            }
            if(currentConstant > rumbleConstant)
            {
                currentConstant -= rumbleNormalizer;
            }
            else if (currentConstant < rumbleConstant)
            {
                currentConstant += rumbleNormalizer;
            }
            if (currentConstant < 0f || currentConstant > 1f)
            {
                currentConstant = 0f;
            }
            GamePad.SetVibration((PlayerIndex)0, currentConstant, rumbleVariable);
            //Debug.Log("vibration values =" + currentConstant +", " + rumbleVariable);
        }
        if (thrust == ThrustType.BOOST_SPEED)
        {
            DrainSystem();
        }
        else if(thrust == ThrustType.BRAKING)
        {
            ChargeSystem();
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
    void DrainSystem()
    {
        //Debug.Log("DrainSystem()");
        
        //ensure we can drain laser energy
        if(LaserSystem.energy >= 0)
        {
            //if shield energy is already at 0, increase the drain rate
            if(ShieldSystem.energy <= 0)
            {
                //Debug.Log("LaserSystem Drain Increased");

                ls.UpdateLaserEnergy(-drainIncreased);
            }
            //else drain normally
            else
            {
                //Debug.Log("LaserSystem Drain Normal");

                ls.UpdateLaserEnergy(-drain);
            }
        }
        //ensure we can drain shield energy
        if (ShieldSystem.energy >= 0)
        {
            //if laser energy is already at 0, increase the drain rate
            if (LaserSystem.energy <= 0)
            {
                //Debug.Log("ShieldsSystem Drain Increased");

                ss.UpdateShieldEnergy(-drainIncreased);
            }
            //else drain normally
            else
            {
                //Debug.Log("ShieldsSystem Drain Normal");

                ss.UpdateShieldEnergy(-drain);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ChargeSystem()
    {
        //Debug.Log("ChargeSystem()");

        //ensure we can charge laser energy
        if (LaserSystem.energy < 1)
        {
            //if shield energy is already at 1, increase the charge rate
            if (ShieldSystem.energy >= 1)
            {
                //Debug.Log("LaserSystem charge increased");

                ls.UpdateLaserEnergy(chargeIncreased);
            }
            //else charge normally
            else
            {
                //Debug.Log("LaserSystem charge normal");

                ls.UpdateLaserEnergy(charge);
            }
        }
        //ensure we can charge shield energy
        if (ShieldSystem.energy < 1)
        {
            //if laser energy is already at 1, increase the charge rate
            if (LaserSystem.energy >= 1)
            {
                //Debug.Log("ShieldsSystem charge increased");

                ss.UpdateShieldEnergy(chargeIncreased);
            }
            //else charge normally
            else
            {
                //Debug.Log("ShieldsSystem charge normal");

                ss.UpdateShieldEnergy(charge);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateThrust(EVENT_UPDATE_THRUST _event)
    {
        //Debug.Log("UpdateThrust(" + _event.thrust + ")");
        previousThrust = thrust;
        thrust = _event.thrust;
        switch (thrust)
        {
            case ThrustType.BRAKING:
                GamePad.SetVibration((PlayerIndex)0, leftIntensity[0], rightIntensity[0]);
                rumbleConstant = leftIntensity[0];
                rumbleVariable = rightIntensity[0];

                AudioSystem.Instance.MakeAudioSource(sfx_brake.name);
                AudioSystem.Instance.UpdateAmbientPitch(0);
                break;

            case ThrustType.CRUISE:
                GamePad.SetVibration((PlayerIndex)0, leftIntensity[1], rightIntensity[1]);
                rumbleConstant = leftIntensity[1];
                rumbleVariable = rightIntensity[1];

                if (previousThrust == ThrustType.MAX_SPEED)
                {
                    AudioSystem.Instance.MakeAudioSource(sfx_thrust_max_to_cruise.name, tr.position);
                }
                else
                {
                    AudioSystem.Instance.UpdateAmbientPitch(1);
                }

                break;

            case ThrustType.MAX_SPEED:
                GamePad.SetVibration((PlayerIndex)0, leftIntensity[2], rightIntensity[2]);
                rumbleConstant = leftIntensity[2];
                rumbleVariable = rightIntensity[2];

                if(previousThrust == ThrustType.BOOST_SPEED)
                {
                    AudioSystem.Instance.MakeAudioSource(sfx_thrust_boost_to_max.name, tr.position);
                    AudioSystem.Instance.ToggleBoost(false);
                }
                else
                {
                    AudioSystem.Instance.MakeAudioSource(sfx_thrust_cruise_to_max.name, tr.position);
                    AudioSystem.Instance.UpdateAmbientPitch(3);
                }

                break;

            case ThrustType.BOOST_SPEED:
                GamePad.SetVibration((PlayerIndex)0, leftIntensity[3], rightIntensity[3]);
                rumbleConstant = leftIntensity[3];
                rumbleVariable = rightIntensity[3];

                AudioSystem.Instance.ToggleBoost(true);
                AudioSystem.Instance.MakeAudioSource(sfx_thrust_shift_up.name, tr.position);

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
        GamePad.SetVibration((PlayerIndex)0, 0, 0);
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