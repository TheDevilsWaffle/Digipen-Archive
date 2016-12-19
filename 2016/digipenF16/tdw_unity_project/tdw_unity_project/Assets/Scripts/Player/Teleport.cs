///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Teleport.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

#region ENUMS
public enum TeleportStatus
{
    READY,
    RECHARGING,
    AIMING,
    TELEPORTING
};
#endregion

[RequireComponent(typeof(TrailRenderer))]

public class Teleport : MonoBehaviour
{
    #region FIELDS
    int playerNumber = 1;
    [HideInInspector]
    public bool isTeleportEnabled = true;
    bool isOnCooldown;
    [Header("TELEPORT ATTRIBUTES")]
    public float teleportDistance;
    TeleportStatus currentStatus;
    public TeleportStatus CurrentStatus
    {
        get { return currentStatus; }
    }
    public float delayBeforeNextTeleport;
    GamePadInputData player;
    [Range(0f, 1f)]
    public float aimVibration = 0.25f;

    public GameObject teleportLocationPrefab;
    GameObject teleportLocationObject;
    public Vector3 teleportLocationOffset = new Vector3(0f, 0.25f, 0f);

    public float chargeCooldown = 3f;
    public float teleportCooldown = 0.35f;
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
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        playerNumber = pd.PlayerNumber;
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
        if (isTeleportEnabled && GamePadInput.players[playerNumber] != null)
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
                //teleporting?
                if (InitiateTeleportCheck())
                {
                    InitiateTeleportation(player.RTValue);
                }
                //no longer aiming?
                else if (AimingReleasedCheck())
                {
                    StartCoroutine(ResetTeleport());
                }
            }

            //do we need to update charges?
            if(pd.TeleportCharges < pd.MaxTeleportCharges)
            {
                timer += Time.deltaTime;
                if (timer >= chargeCooldown)
                {
                    timer = 0f;
                    //update charges UI
                    Events.instance.Raise(new EVENT_Teleport_Restored(GetComponent<PlayerData>()));
                }
            }

            //if teleporting, start the teleport cooldown
            if (currentStatus == TeleportStatus.TELEPORTING)
            {
                delay += Time.deltaTime;
                if (delay >= teleportCooldown)
                {
                    delay = 0f;
                    currentStatus = TeleportStatus.READY;
                }
            }
        }
	}

    void FixedUpdate()
    {
        if(currentStatus == TeleportStatus.TELEPORTING)
        {
            float _timeSinceStarted = Time.time - teleportStartTime;
            float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;
            
            tr.position = Vector3.Lerp(teleportStartPos, teleportLocation, _percentageComplete);
            if(_percentageComplete >= 1.0f)
            {
                StartCoroutine(ResetTeleport(delayBeforeNextTeleport));
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
        if (pd.TeleportCharges <= 0 && currentStatus == TeleportStatus.AIMING && player.RT == GamePadButtonState.HELD)
            sfx.PlayOneShot(noChargeSFX, 0.5f);

        return currentStatus == TeleportStatus.AIMING
            && player.RT == GamePadButtonState.HELD
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
        //DEBUG — input of right stick x & y
        //print(_inputVector);

        //used to determine the position of where our ray casts to
        RaycastHit _info;

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
        Debug.DrawRay(_origin, _endPoint,  Color.red);
        //ray pointing down from initial ray
        Debug.DrawRay(_endPointPos, Vector3.down, Color.blue);

        //the actual ray pointing down
        Physics.Raycast(_endPointPos, Vector3.down, out _info, teleportDistance);

        //create our aiming sprite
        CreateAimSprite(_info.point);

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
    void InitiateTeleportation(float _value)
    {
        //DEBUG — teleport status and vibration intensity
        //print("INITIATING TELEPORT, RT value = " + _value);

        //apply vibration
        GamePad.SetVibration((PlayerIndex)playerNumber, _value, _value);

        //teleport player if trigger is fully pressed
        if(_value == 1f)
            TeleportPlayer(teleportLocation);
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
        trenderer.enabled = true;
        //switch to physics layer: teleporting
        //this.gameObject.layer = LayerMask.NameToLayer("Teleporting");
        //cc.enabled = false;
        rb.useGravity = false;
        //print(this.gameObject.layer);

        //update charges UI
        //--pd.Charges;
        Events.instance.Raise(new EVENT_Teleport_Used(GetComponent<PlayerData>()));


        //play sfx
        sfx.PlayOneShot(teleportSFX);

        //move player
        //rb.MovePosition(_pos);

        //reset our aiming
        //StartCoroutine(ResetTeleport(delayBeforeTeleport));
    }
    #endregion
}
