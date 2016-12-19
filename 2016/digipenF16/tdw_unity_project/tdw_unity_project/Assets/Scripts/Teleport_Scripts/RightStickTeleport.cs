///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — RightStickTeleport.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#
using System;

[RequireComponent(typeof(TrailRenderer))]

public class RightStickTeleport : MonoBehaviour
{
    #region FIELDS
    int playerNumber;
    float cooldownBetweenTeleport;
    [HideInInspector]
    public bool isTeleportEnabled = true;
    bool isOnCooldown;

    [Header("TELEPORT BOUNDARIES")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("TELEPORT ATTRIBUTES")]
    public float teleportDistance;
    TeleportStatus currentStatus;
    public TeleportStatus CurrentStatus
    {
        get { return currentStatus; }
    }
    GamePadInputData player;
    [Range(0f, 1f)]
    public float aimVibration = 0.25f;


    public GameObject teleportLocationPrefab;
    GameObject teleportLocationObject;
    public Vector3 teleportLocationOffset = new Vector3(0f, 0.25f, 0f);

    float timer;
    float delay;
    Vector3 teleportLocation;

    public float timeTakenDuringLerp = 3f;

    float teleportStartTime;
    Vector3 teleportStartPos;

    [Header("SFX")]
    public AudioClip teleportSFX;
    //public AudioClip initializingTeleportSFX;
    public AudioClip noChargeSFX;

    Transform tr;
    Rigidbody rb;
    TrailRenderer trenderer;
    CapsuleCollider cc;
    PlayerData pd;
    AudioSource sfx;

    public float rightStickThreshold = 0.5f;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        pd = GetComponent<PlayerData>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        trenderer = GetComponent<TrailRenderer>();
        sfx = GameObject.FindWithTag("SFX").gameObject.GetComponent<AudioSource>();
        Events.instance.AddListener<EVENT_Round_Start>(EnableTeleport);
    }

    private void EnableTeleport(EVENT_Round_Start _event)
    {
        isTeleportEnabled = true;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        playerNumber = pd.PlayerNumber;
        cooldownBetweenTeleport = pd.CooldownBetweenTeleport;
        timer = 0f;
        delay = 0f;
        currentStatus = TeleportStatus.READY;
        GamePad.SetVibration((PlayerIndex)playerNumber, 0f, 0f);
        trenderer.enabled = false;
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
        //reset gamepad
        ResetGamePadVibration();
        //go through loop only if teleport is enabled and player exists
        if (isTeleportEnabled && playerNumber < GamePadInput.players.Count)
        {
            //assign player, because they exist
            player = GamePadInput.players[playerNumber];

            //ready?
            if (ReadyCheck())
            {
                //aiming?
                if (AimingCheck())
                {
                    AimTeleport(tr.position, player.RightAnalogStick, player.RightAnalogStickAngle);
                }
                //no longer aiming?
                else if (AimingReleasedCheck())
                {
                    //print("aim released");
                    StartCoroutine(ResetTeleport());
                }
            }

            //do we need to update charges?
            if (pd.TeleportCharges < pd.MaxTeleportCharges)
            {
                timer += Time.deltaTime;
                if (timer >= pd.TeleportRechargeTime)
                {
                    timer = 0f;
                    //update charges UI
                    Events.instance.Raise(new EVENT_Teleport_Restored(GetComponent<PlayerData>()));
                }
            }

            //if teleporting, start the teleport cooldown
            if (currentStatus == TeleportStatus.RECHARGING)
            {
                delay += Time.deltaTime;
                if (delay >= pd.CooldownBetweenTeleport)
                {
                    delay = 0f;
                    currentStatus = TeleportStatus.READY;
                }
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FixedUpdate()
    {
        if (currentStatus == TeleportStatus.TELEPORTING)
        {
            float _timeSinceStarted = Time.time - teleportStartTime;
            float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

            tr.position = Vector3.Lerp(teleportStartPos, teleportLocation, _percentageComplete);
            if (_percentageComplete >= 1.0f)
            {
                pd.PlayTeleportParticle();
                currentStatus = TeleportStatus.RECHARGING;
                pd.SetPlayerStatus(PlayerStatus.VULNERABLE);
                StartCoroutine(ResetTeleport(cooldownBetweenTeleport));
            }
        }
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// checks if we're ready or already aiming
    /// </summary>
    /// <returns>true/false</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool ReadyCheck()
    {
        return currentStatus == TeleportStatus.READY
            || currentStatus == TeleportStatus.AIMING;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// checks if player is using right stick and they have charges available to teleport
    /// </summary>
    /// <returns>true/false</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool AimingCheck()
    {
        //print("AimCheck() charges = " + pd.TeleportCharges);
        return player.RightAnalogStick_Status == GamePadButtonState.HELD
            || player.RightAnalogStick_Status == GamePadButtonState.PRESSED
            && pd.TeleportCharges > 0;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// checks if we're aiming, right trigger is held, and teleport location is not reset
    /// </summary>
    /// <returns>true/false</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool InitiateTeleportCheck()
    {
        //print("InitiateTeleportCheck() charges = " + pd.TeleportCharges);
        if (pd.TeleportCharges < 0 && currentStatus == TeleportStatus.AIMING)
            sfx.PlayOneShot(noChargeSFX, 0.5f);

        return currentStatus == TeleportStatus.AIMING
                && pd.TeleportCharges > 0
                && teleportLocation != Vector3.zero;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// checks if we were aiming previous and released aim without teleporting
    /// </summary>
    /// <returns>true/false</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool AimingReleasedCheck()
    {
        return currentStatus == TeleportStatus.AIMING
            && player.RightAnalogStick_Status == GamePadButtonState.INACTIVE
            || player.RightAnalogStick_Status == GamePadButtonState.RELEASED;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// stop vibrating the gamepad for this player
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ResetGamePadVibration()
    {
        GamePad.SetVibration((PlayerIndex)playerNumber, 0f, 0f);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_origin">where the player is</param>
    /// <param name="_inputVector">value of this player's gamepad right stick x & y input</param>
    /// <param name="_inputAngle">angle of this player's gamepad right stick input</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AimTeleport(Vector3 _origin, Vector3 _inputVector, float _inputAngle)
    {
        currentStatus = TeleportStatus.AIMING;
        //DEBUG — input of right stick x & y
        //print(_inputVector);

        //used to determine the position of where our ray casts to
        RaycastHit _info;
        RaycastHit _info2;

        //start aim vibration
        GamePad.SetVibration((PlayerIndex)playerNumber, aimVibration, aimVibration);

        //get distance and rotation of input to establish where to create the teleport aim sprite
        Vector3 _distance = GetInputDistance(_inputVector.x, _inputVector.y);
        Quaternion _rotation = Quaternion.AngleAxis(_inputAngle, tr.up);
        Vector3 _endPoint = _rotation * _distance;

        //find out where we are teleporting to in terms of position
        Vector3 _endPointPos = _origin + _endPoint;

        //DEBUG — raycast drawing
        //initial ray out from player
        Debug.DrawRay(_origin, _endPoint, Color.red);
        Physics.Raycast(_origin, _endPoint, out _info, teleportDistance);

        //print(_inputVector);
        if(_info.collider != null && _info.collider.gameObject.tag == "Wall")
        {
            //ray pointing down from initial ray
            Debug.DrawRay(_info.point, Vector3.down, Color.green);
            //the actual ray pointing down
            Physics.Raycast(_info.point, Vector3.down, out _info, (teleportDistance));
            //create our aiming sprite
            CreateAimSprite(_info.point);

            if (Mathf.Abs(_inputVector.x) > rightStickThreshold || Mathf.Abs(_inputVector.y) > rightStickThreshold
                && InitiateTeleportCheck())
            {
                InitiateTeleportation();
            }
        }
        else
        {
            //print(_endPointPos);
            //ray pointing down from initial ray
            Debug.DrawRay(_endPointPos, Vector3.down, Color.blue);
            //the actual ray pointing down
            Physics.Raycast(_endPointPos, Vector3.down, out _info2, teleportDistance);

            //create our aiming sprite
            CreateAimSprite(_info2.point);

            if (Mathf.Abs(_inputVector.x) > rightStickThreshold || Mathf.Abs(_inputVector.y) > rightStickThreshold)
                InitiateTeleportation();
        }
        //if (!sfx.isPlaying)
        //sfx.PlayOneShot(initializingTeleportSFX);

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// destroys/nulls aim sprite, sets status to ready, resets vibration, zeros position and
    /// resets physics layer of the player
    /// </summary>
    /// <param name="_delay">time before destryoing pos (allows teleport)</param>
    /// <returns>nothing</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator ResetTeleport(float _delay = 0f)
    {
        if (teleportLocationObject != null)
            Destroy(teleportLocationObject);
        teleportLocationObject = null;
        ResetGamePadVibration();
        yield return new WaitForSeconds(_delay);
        trenderer.enabled = false;
        //cc.enabled = true;
        rb.useGravity = true;
        //this.gameObject.layer = LayerMask.NameToLayer("Player");
        //print(this.gameObject.layer);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// adds absolute value of input x & y, multiplies by teleport distance and returns this value
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    /// <returns>position of where input is as relative to right stick input value</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    Vector3 GetInputDistance(float _x, float _y)
    {
        float _sum = Mathf.Clamp((Mathf.Abs(_x) + Mathf.Abs(_y)), 0.1f, 1f);
        _sum *= teleportDistance;
        Vector3 _distance = new Vector3(_sum, 1f, 1f);
        return _distance;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// creates the teleport sprite at the passed position and changes current status to AIMING
    /// </summary>
    /// <param name="_pos">where to place the teleport sprite</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CreateAimSprite(Vector3 _pos)
    {
        //potentially correct aim sprite
        if (_pos.x < minX)
            _pos.x = minX;
        if(_pos.x > maxX)
            _pos.x = maxX;
        if (_pos.z < minZ)
            _pos.z = minZ;
        if (_pos.z > maxZ)
            _pos.z = maxZ;
        if (_pos.y < 0)
            _pos.y = 0.5f;

        //print("TELEPORT TO COORDS: " + _pos);
        //set current status to aiming
        currentStatus = TeleportStatus.AIMING;

        //create teleport location object at passed position if null
        if (teleportLocationObject == null)
        {
            teleportLocationObject = Instantiate(teleportLocationPrefab,
                                                 _pos,
                                                 Quaternion.identity) as GameObject;
            teleportLocationObject.transform.Rotate(90f, 0f, 0f);
        }
        teleportLocationObject.GetComponent<SpriteRenderer>().color = pd.PlayerColor;
        //add offset so sprite isn't in the ground and apply position
        teleportLocation = _pos + teleportLocationOffset;
        teleportLocationObject.transform.position = teleportLocation;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// increase gamepad vibration and teleport player if trigger is pressed all the way
    /// </summary>
    /// <param name="_value">vibration intensity (1f == max)</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitiateTeleportation()
    {
        if (pd.TeleportCharges > 0)
        {
            TeleportPlayer(tr.position);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// teleports player to passed position
    /// </summary>
    /// <param name="_pos">the location to teleport</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TeleportPlayer(Vector3 _pos)
    {
        //DEBUG — status check
        //print("TELEPORTING!");
        teleportStartTime = Time.time;
        teleportStartPos = tr.position;
        //update status to teleporting
        currentStatus = TeleportStatus.TELEPORTING;
        pd.SetPlayerStatus(PlayerStatus.INVULNERABLE);
        trenderer.enabled = true;
        //switch to physics layer: teleporting
        //this.gameObject.layer = LayerMask.NameToLayer("Teleporting");
        //cc.enabled = false;
        rb.useGravity = false;

        //update charges UI
        Events.instance.Raise(new EVENT_Teleport_Used(GetComponent<PlayerData>()));

        //play sfx
        sfx.PlayOneShot(teleportSFX);
        pd.PlayTeleportParticle();

        //move player
        //rb.MovePosition(_pos);

        //reset our aiming
        //StartCoroutine(ResetTeleport(delayBeforeTeleport));
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Round_Start>(EnableTeleport);
    }
    #endregion
}
