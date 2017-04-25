///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Shoot.cs
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
using XInputDotNetPure;

#region ENUMS
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
public class EVENT_CANNON_FIRED : GameEvent
{
    public float energy;
    public int ticks;
    public EVENT_CANNON_FIRED(float _energy, int _ticks)
    {
        energy = _energy;
        ticks = _ticks;
    }
}
#endregion

public class Shoot : MonoBehaviour
{
    #region FIELDS
    [Header("SHOOTING")]
    [SerializeField]
    GameObject blastPrefab;
    [SerializeField]
    float cooldown;
    [SerializeField]
    GameObject cannon;
    Transform cannon_tr;

    [Header("LASER")]
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    float speed = 75f;
    [SerializeField]
    float lifetime = 3f;
    [SerializeField]
    bool destroyUponImpact = true;
    [SerializeField]
    int laserCount = 7;
    List<bool> lasers;


    [Header("SFX")]
    AudioClip sfx_cannon;

    GamePadInputData player;
    float timer = 0f;
    float shotVibration = 0f;
    float normalizer = 0.05f;
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
        cannon_tr = cannon.GetComponent<Transform>();
        shotVibration = 0f;

        //initial values
        timer = 0f;

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
        UpdateTimer();
        UpdateInput();
        Rumble();
        if (player.LT == GamePadButtonState.HELD && timer > cooldown)
        {
            
            if (LaserSystem.currentLaser)
            {
                Fire(laserPrefab);
            }
            else
            {
                FireDryShot();
            }

            ResetTimer();
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
    void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ResetTimer()
    {
        timer = 0f;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateInput()
    {
        player = GamePadInput.players[0];
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Fire(GameObject _prefab)
    {
        //cannon blast
        GameObject _blast = Instantiate(blastPrefab, cannon_tr.position, cannon_tr.rotation);
        _blast.transform.SetParent(cannon_tr);
        //laser bolt
        GameObject _laser = Instantiate(_prefab, cannon_tr.position, cannon_tr.rotation);

        if (_laser != null)
        {
            Events.instance.Raise(new EVENT_LASER_FIRED(_laser.GetComponent<Transform>()));

            //Debug.Log(_laser.name + " fired!");
            _laser.GetComponent<LaserBolt>().Initialize(speed, lifetime, cannon_tr.forward, true);

            shotVibration = 1f;
        }
    }
    void FireDryShot()
    {
        Events.instance.Raise(new EVENT_LASER_DRY_SHOT(cannon_tr));
        shotVibration = 0.33f;
    }

    void Rumble()
    {
        if (shotVibration > 0f)
        {
            shotVibration -= normalizer;
            GamePad.SetVibration((PlayerIndex)0, shotVibration, 0f);
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