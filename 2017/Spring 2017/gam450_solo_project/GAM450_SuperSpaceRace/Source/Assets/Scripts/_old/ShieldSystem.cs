///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ShieldSystem.cs
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
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
public class EVENT_SHIELD_DISCHARGED : GameEvent
{
    public int index;
    public int total;
    public EVENT_SHIELD_DISCHARGED(int _index, int _total)
    {
        index = _index;
        total = _total;
    }
}
public class EVENT_SHIELD_CHARGED : GameEvent
{
    public int index;
    public int total;
    public EVENT_SHIELD_CHARGED(int _index, int _total)
    {
        index = _index;
        total = _total;
    }
}
public class EVENT_SHIELD_FULLY_CHARGED : GameEvent
{
    public EVENT_SHIELD_FULLY_CHARGED() { }
}
public class EVENT_SHIELD_NO_CHARGE : GameEvent
{
    public EVENT_SHIELD_NO_CHARGE() { }
}
public class EVENT_SHIELD_EMPTY : GameEvent
{
    public EVENT_SHIELD_EMPTY() { }
}
public class EVENT_SHIELD_STATUS_BROKEN : GameEvent
{
    public EVENT_SHIELD_STATUS_BROKEN() { }
}
public class EVENT_SHIELD_STATUS_DISABLED : GameEvent
{
    public EVENT_SHIELD_STATUS_DISABLED() { }
}
public class EVENT_SHIELD_STATUS_OPERATIONAL : GameEvent
{
    public EVENT_SHIELD_STATUS_OPERATIONAL() { }
}
#endregion

public class ShieldSystem : MonoBehaviour
{
    #region FIELDS
    [Header("REFERENCES")]
    [SerializeField]
    ParticleSystem shieldUp;
    [SerializeField]
    ParticleSystem shieldDown;
    [SerializeField]
    MeshRenderer shipMR;
    Material shipMaterial;
    GameObject ship;
    [SerializeField]
    GameObject shieldMeshGameObject;
    [SerializeField]
    MeshRenderer cannonMR;
    Material cannonMaterial;
    Color currentColor;

    [Header("ENERGY")]
    [SerializeField]
    int startingShieldTotal = 2;
    public static int shieldTotal;
    [SerializeField]
    [Range(0f, 1f)]
    float energyPerShield = 0.5f;
    public static float perShield;

    [Header("SHIELD COLORS")]
    [SerializeField]
    Color shieldColor;

    [Header("SHIELD INTENSITY")]
    [SerializeField]
    float intensityFactor = 4f;
    float currentIntensity;
    bool shieldsUp = true;

    [Header("SHIELDS DANGER")]
    [SerializeField]
    GameObject dangerMeshObject;
    [SerializeField]
    GameObject shieldsDanger;

    [Header("SFX")]
    [SerializeField]
    AudioClip sfx_shield_down;

    public static float energy = 1f;
    public static int index;
    public static bool[] shields;
    public static bool currentShield;
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
        ship = shipMR.gameObject;
        shipMaterial = shipMR.material;
        cannonMaterial = cannonMR.material;

        //initial values
        ShipData.ShieldsEnergy = 1f;
        perShield = energyPerShield;
        shieldTotal = startingShieldTotal;
        index = shieldTotal - 1;
        shields = new bool[] { true, true };
        currentShield = shields[index];

        currentColor = shipMaterial.color;
        currentIntensity = currentColor.a;

        shieldsUp = true;

        StopShieldUp();
        StopShieldDown();

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        shieldMeshGameObject.SetActive(true);
        dangerMeshObject.SetActive(true);
        ToggleDanger(false);
        UpdateShieldIntensity();
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

    #if false
        UpdateTesting();
    #endif

    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    void PlayShieldUp()
    {
        if (shieldUp.enableEmission == false)
        {
            shieldUp.Play();
            shieldUp.enableEmission = true;
            LeanTween.delayedCall(2f, StopShieldUp);
        }
    }
    void StopShieldUp()
    {
        shieldUp.enableEmission = false;
        shieldUp.Stop();
    }
    void PlayShieldDown()
    {
        if (shieldDown.enableEmission == false)
        {
            shieldDown.Play();
            shieldDown.enableEmission = true;
            LeanTween.delayedCall(2f, StopShieldDown);
        }
    }
    void StopShieldDown()
    {
        shieldDown.enableEmission = false;
        shieldDown.Stop();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateShieldIntensity()
    {
        //Debug.Log("UpdateShieldIntensity(" + _event.energy + ")");
        shipMaterial.color = new Color((shieldColor.r * (energy / intensityFactor)), 
                                         (shieldColor.g * (energy / intensityFactor)), 
                                         (shieldColor.b * (energy / intensityFactor)), 
                                         (shieldColor.a * (energy / intensityFactor)));
        cannonMaterial.color = new Color((shieldColor.r * (energy / intensityFactor)),
                                         (shieldColor.g * (energy / intensityFactor)),
                                         (shieldColor.b * (energy / intensityFactor)),
                                         (shieldColor.a * (energy / intensityFactor)));
        currentColor = shipMaterial.color;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void UpdateShieldEnergy(float _value)
    {
        //Debug.Log("UpdateShieldEnergy(" + _value + ")");
        float _previousEnergy = energy;
        energy += _value;
        int _previousTotal = shieldTotal;
        if (energy > _previousEnergy)
        {
            PlayShieldUp();
        }
        else if(energy < _previousEnergy)
        {
            PlayShieldDown();
        }

        if (energy > 1f)
        {
            energy = 1;
        }
        else if (energy < 0f)
        {
            shieldTotal = 0;
            energy = 0;
        }

        //Debug.Log("energy after addition/subtraction/clamp = " + energy);

        if(energy > perShield)
        {
            shieldTotal = 2;
        }
        else if (energy > 0 && energy < perShield)
        {
            shieldTotal = 1;
            shieldsUp = true;
            UpdateShieldIntensity();
        }
        else if(energy <= 0f)
        {
            shieldTotal = 0;
        }

        if(_previousTotal > shieldTotal && shieldTotal == 0)
        {
            AudioSystem.Instance.MakeAudioSource(sfx_shield_down.name);
            shieldsUp = false;
        }

        //Debug.Log("previousTotal = " + _previousTotal + "\nlasersTotal = " + lasersTotal);

        //check to see if we've gained a shield
        DetermineCurrentShield(_previousTotal);
        if (shieldsUp)
        {
            ToggleDanger(false);
        }
        else
        {
            ToggleDanger(true);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleDanger(bool _on)
    {
        ship.SetActive(!_on);
        shieldsDanger.SetActive(_on);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineCurrentShield(int _previousTotal)
    {
        //gained a shield
        if (shieldTotal > _previousTotal)
        {
            //fully charged
            if (shieldTotal == 2)
            {
                index = 1;
                shields[index] = true;
                currentShield = shields[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_INCREASED(energy, index));
                
                Events.instance.Raise(new EVENT_SHIELD_CHARGED(index, shieldTotal));
                Events.instance.Raise(new EVENT_SHIELD_FULLY_CHARGED());
            }
            else if (shieldTotal== 1)
            {
                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_INCREASED(energy, index));
                Events.instance.Raise(new EVENT_SHIELD_STATUS_OPERATIONAL());
                index = 0;
                shields[index] = true;
                currentShield = shields[index];

                Events.instance.Raise(new EVENT_LASER_CHARGED(shieldTotal));
            }
            else
            {
                ++index;
                shields[index] = true;
                currentShield = shields[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_INCREASED(energy, index));

                Events.instance.Raise(new EVENT_SHIELD_CHARGED(index, shieldTotal));
            }
            //Debug.Log("gained a shield, shields[" + index + "] = " + currentShield);
        }
        //or check to see if we've lost a shield
        if (shieldTotal < _previousTotal || shieldTotal == 0)
        {
            //check to make sure we don't go out of bounds
            if (shieldTotal == 0)
            {
                index = 0;
                shields[index] = false;
                currentShield = shields[index];

                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_DECREASED(energy, index));
                Events.instance.Raise(new EVENT_SHIELD_DISCHARGED(index, shieldTotal));
                Events.instance.Raise(new EVENT_SHIELD_EMPTY());
            }
            else if (shieldTotal == 1)
            {
                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_DECREASED(energy, index));

                shields[index] = false;
                index = 0;
                currentShield = shields[index];

                Events.instance.Raise(new EVENT_SHIELD_DISCHARGED(index, shieldTotal));
            }
            else
            {
                //update the HUD
                Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_DECREASED(energy, index));

                shields[index] = false;
                --index;
                currentShield = shields[index];
                Events.instance.Raise(new EVENT_SHIELD_DISCHARGED(index, shieldTotal));
            }
            //Debug.Log("lost a shield, shields[" + index + "] = " + currentShield);
        }
        //update the HUD's energy level shown
        Events.instance.Raise(new EVENT_UPDATE_HUD_SHIELD_ENERGY(energy, index, perShield));
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
            UpdateShieldEnergy(-perShield);
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            UpdateShieldEnergy(perShield);
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