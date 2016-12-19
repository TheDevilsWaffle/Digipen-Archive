///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — PlayerData.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum PlayerStatus
{
    VULNERABLE,
    INVULNERABLE
};
#endregion

public class PlayerData : MonoBehaviour
{
    #region FIELDS
    PlayerStatus status = PlayerStatus.INVULNERABLE;
    public PlayerStatus Status
    {
        get { return status; }
    }
    int playerNumber;
    public int PlayerNumber
    {
        get { return playerNumber; }
    }
    Color playerColor;
    public Color PlayerColor
    {
        get { return playerColor; }
    }
    int lives;
    public int Lives
    {
        get { return lives; }
    }
    int teleportCharges;
    public int TeleportCharges
    {
        get { return teleportCharges; }
    }
    int maxTeleportCharges;
    public int MaxTeleportCharges
    {
        get { return maxTeleportCharges; }
    }
    float teleportRechargeTime;
    public float TeleportRechargeTime
    {
        get { return teleportRechargeTime; }
    }
    float cooldownBetweenTeleports;
    public float CooldownBetweenTeleport
    {
        get { return cooldownBetweenTeleports; }
    }
    Transform spawnPoint;
    public Transform SpawnPoint
    {
        get { return spawnPoint; }
    }

    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    GameObject tpParticleSystem;
    Color tpColor;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        status = PlayerStatus.VULNERABLE;
        Events.instance.AddListener<EVENT_Teleport_Restored>(RestoreTeleportCharge);
        Events.instance.AddListener<EVENT_Teleport_Used>(DecreaseTeleportCharge);
        Events.instance.AddListener<EVENT_Player_Died>(SubtractALife);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
	}
    #endregion

    #region SET PROPERTIES
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_number"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_PlayerNumber(int _number)
    {
        playerNumber = _number;

        //DEBUG
        //Debug.Log(this.gameObject.name + " is player number" + playerNumber);

        //enable disabled components now
        GetComponent<Movement>().enabled = true;
        GetComponent<TelefragDetector>().enabled = true;
        GetComponent<RightStickTeleport>().enabled = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_color"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_PlayerColor(Color _color)
    {
        playerColor = _color;
        mesh.material.color = _color;
        GetComponent<TrailRenderer>().material.color = _color;
        GetComponent<ExplosiveDeath>().fragmentMaterial.color = _color;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_lives"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_Lives(int _lives)
    {
        lives = _lives;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_maxTeleportCharges"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_MaxTeleportCharges(int _maxTeleportCharges)
    {
        maxTeleportCharges = _maxTeleportCharges;
        teleportCharges = MaxTeleportCharges;
        Events.instance.Raise(new EVENT_UI_Initialize_Teleports(playerNumber, TeleportCharges));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_cooldown"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_CoolDownBetweenTeleports(float _cooldown)
    {
        cooldownBetweenTeleports = _cooldown;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_time"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Set_TeleportRechargeTime(float _time)
    {
        teleportRechargeTime = _time;
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SubtractALife(EVENT_Player_Died _event)
    {
        if (_event.playerNumber == PlayerNumber)
        {
            if (lives >= 0)
            {
                --lives;
                //print(this.gameObject.name + " has " + Lives + " left!");
                Events.instance.Raise(new EVENT_UI_Life_Lost(playerNumber, Lives, PlayerColor, _event.deathPosition));
            }
            if(lives == 0)
            {
                Events.instance.Raise(new EVENT_Player_Final_Life(this.gameObject.GetComponent<PlayerData>()));
            }
            if (lives < 0)
            {
                //print(this.gameObject.name + " has " + Lives + " left and is now DEAD!");
                Events.instance.Raise(new EVENT_UI_Player_Eliminated(playerNumber));
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DecreaseTeleportCharge(EVENT_Teleport_Used _event)
    {
        if (_event.playerData.playerNumber == PlayerNumber)
        {
            Events.instance.Raise(new EVENT_UI_Teleport_Used(PlayerNumber, TeleportCharges, TeleportRechargeTime));
            --teleportCharges;
        }
        //DEBUG
        //Debug.Log("PlayerData 'charges' has changed to " + teleportCharges);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void RestoreTeleportCharge(EVENT_Teleport_Restored _event)
    {
        if (_event.playerData.playerNumber == PlayerNumber)
        {
            ++teleportCharges;
            Events.instance.Raise(new EVENT_UI_Teleport_Restored(PlayerNumber, TeleportCharges));
        }
            //DEBUG
            //Debug.Log("PlayerData 'charges' has changed to " + teleportCharges);
        }
    public void SetPlayerStatus(PlayerStatus _status)
    {
        status = _status;
    }
    public void SetSpawnPoint(Transform _spawnPoint)
    {
        spawnPoint = _spawnPoint;
    }
    public void SetParticleSystemColor(Color _color)
    {
        tpColor = _color;
    }
    public void PlayTeleportParticle()
    {
        //print("making a teleport particle!");
        TeleportParticleToggle();
    }
    void TeleportParticleToggle()
    {
        GameObject _ps = (GameObject)GameObject.Instantiate(tpParticleSystem, this.gameObject.transform.position, Quaternion.identity);
        _ps.GetComponent<ParticleSystem>().startColor = tpColor;
        _ps.GetComponent<ParticleSystem>().Play();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Teleport_Restored>(RestoreTeleportCharge);
        Events.instance.RemoveListener<EVENT_Teleport_Used>(DecreaseTeleportCharge);
        Events.instance.RemoveListener<EVENT_Player_Died>(SubtractALife);
    }
    #endregion
}
