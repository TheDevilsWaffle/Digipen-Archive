///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ThrustParticleSystem.cs
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

public class ThrustParticleSystem : MonoBehaviour
{
    #region FIELDS
    [Header("")]
    [SerializeField]
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;
    ParticleSystem.VelocityOverLifetimeModule volm;

    [Header("EMISSION RATE")]
    [SerializeField]
    int[] rates;

    [Header("VELOCITY OVER LIFETIME")]
    [SerializeField]
    int[] velocities;
    float z;
    [SerializeField]
    float brakeCorrection = 0.05f;
    [SerializeField]
    float speedMultiplier = 2.5f;
    [SerializeField]
    float emissionRateMultiplier = 3.5f;
    [SerializeField]
    float[] modifiers;
    float modifier;
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
        em = ps.emission;
        volm = ps.velocityOverLifetime;
        modifier = modifiers[0];

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
        Events.instance.AddListener<EVENT_UPDATE_THRUST>(UpdateThrustParticleSystem);
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
        volm.z = -(ShipData.SpeedValue * speedMultiplier);
        em.rateOverTime = (ShipData.SpeedValue * emissionRateMultiplier * modifier);
        if(BoostSystem.thrust == ThrustType.BRAKING)
        {
            if(z >= 0)
            {
                z -= brakeCorrection;
            }
            volm.z = z;
        }
        else if (BoostSystem.thrust == ThrustType.CRUISE)
        {
            volm.z = -((ShipData.SpeedValue * speedMultiplier) + 100);
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
    void UpdateThrustParticleSystem(EVENT_UPDATE_THRUST _event)
    {
        switch (_event.thrust)
        {
            case ThrustType.BRAKING:
                em.rateOverTime = rates[0];
                volm.z = velocities[0];
                z = velocities[0];
                modifier = modifiers[0];


                break;
            case ThrustType.CRUISE:
                em.rateOverTime = rates[1];
                volm.z = velocities[1];
                z = velocities[1];
                modifier = modifiers[1];


                break;
            case ThrustType.MAX_SPEED:
                em.rateOverTime = rates[2];
                volm.z = velocities[2];
                z = velocities[2];
                modifier = modifiers[2];


                break;
            case ThrustType.BOOST_SPEED:
                em.rateOverTime = rates[3];
                volm.z = velocities[3];
                z = velocities[3];
                modifier = modifiers[3];


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
        Events.instance.RemoveListener<EVENT_UPDATE_THRUST>(UpdateThrustParticleSystem);

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