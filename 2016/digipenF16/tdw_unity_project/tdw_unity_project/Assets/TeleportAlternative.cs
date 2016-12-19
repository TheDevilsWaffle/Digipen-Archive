///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TeleportAlternative.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure;

#region ENUMS
public enum AltTeleportStatus
{
    READY, PLACED, TELEPORTING, RECHARGING
}
#endregion

#region EVENTS

#endregion

public class TeleportAlternative : MonoBehaviour
{
    #region FIELDS
    PlayerData pd;
    Transform tr;
    TrailRenderer trenderer;
    int playerNumber;
    GamePadInputData player;
    bool isTeleportEnabled = true;
    AltTeleportStatus currentStatus;
    [SerializeField]
    GameObject teleportLocationPrefab;
    Vector3 teleportLocation;
    [SerializeField]
    float spriteShowDuration = 1f;
    [SerializeField]
    Vector3 teleportSpriteOffset;
    GameObject teleportObject;
    public float timeTakenDuringLerp = 3f;
    float teleportStartTime;
    Vector3 teleportStartPos;

    float spriteTimer;
    float rechargeTimer;
    [SerializeField]
    float rechargeDuration;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //get refs
        pd = GetComponent<PlayerData>();
        tr = transform;
        trenderer = GetComponent<TrailRenderer>();
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        spriteTimer = 0f;
        rechargeTimer = 0f;
        currentStatus = AltTeleportStatus.READY;
        if (pd != null)
        {
            playerNumber = pd.PlayerNumber;
            player = GamePadInput.players[playerNumber];
        }
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
	    if(currentStatus == AltTeleportStatus.READY)
        {
            player = GamePadInput.players[playerNumber];

            if(player.X == GamePadButtonState.PRESSED)
            {
                
                PlaceTeleportMarker(GetTeleportPosition(tr.position));
            }
        }
        else if (currentStatus == AltTeleportStatus.PLACED)
        {
            if(player.X == GamePadButtonState.HELD && player.X_HeldTimer > 1f)
            {
                InitiateTeleport();
            }
            print(player.X_HeldTimer);
            TimedDestructionOfTeleportSprite();
        }
	}

    void FixedUpdate()
    {
        if (currentStatus == AltTeleportStatus.TELEPORTING)
        {
            float _timeSinceStarted = Time.time - teleportStartTime;
            float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

            tr.position = Vector3.Lerp(teleportStartPos, teleportLocation, _percentageComplete);
            if (_percentageComplete >= 1.0f)
            {

                print("teleport complete");
                StartCoroutine(StartTeleportRecharge());
            }
        }
    }
    #endregion

    #region METHODS

    Vector3 GetTeleportPosition(Vector3 _pos)
    {
        print("get teleport position");
        RaycastHit _info;
        Physics.Raycast(_pos, Vector3.down, out _info, 5f);
        return _info.point;
    }

    void PlaceTeleportMarker(Vector3 _pos)
    {
        currentStatus = AltTeleportStatus.PLACED;
        //add offset
        _pos += teleportSpriteOffset;

        if (teleportObject == null)
        {
            print("creating teleport");
            teleportObject = Instantiate(teleportLocationPrefab,
                                         _pos,
                                         Quaternion.identity) as GameObject;
            teleportObject.transform.Rotate(90f, 0f, 0f);
            teleportObject.GetComponent<Image>().enabled = true;
            teleportObject.GetComponent<SpriteRenderer>().color = pd.PlayerColor;
            teleportLocation = teleportObject.transform.position;
        }
    }

    void InitiateTeleport()
    {
        if (teleportLocation != Vector3.zero)
        {
            print("starting teleport");
            teleportStartTime = Time.time;
            teleportStartPos = tr.position;
            currentStatus = AltTeleportStatus.TELEPORTING;
            trenderer.enabled = true;
        }
    }

    void TimedDestructionOfTeleportSprite()
    {
        spriteTimer += Time.deltaTime;
        if(teleportObject != null && spriteTimer > spriteShowDuration)
        {
            print("destroying sprite");
            Destroy(teleportObject);
            teleportObject.GetComponent<Image>().enabled = false;
            spriteTimer = 0f;
        }
    }

    IEnumerator StartTeleportRecharge()
    {
        print("recharging teleport");
        currentStatus = AltTeleportStatus.RECHARGING;
        yield return new WaitForSeconds(rechargeDuration);
        ResetTeleport();
    }

    void ResetTeleport()
    {
        print("reseting teleport");
        //currentStatus = AltTeleportStatus.READY;
        //teleportLocation = Vector3.zero;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
