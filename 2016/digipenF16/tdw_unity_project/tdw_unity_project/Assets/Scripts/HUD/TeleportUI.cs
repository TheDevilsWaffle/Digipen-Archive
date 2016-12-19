///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TeleportUI.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using System;

#region ENUMS

#endregion

#region EVENTS
#endregion

public class TeleportUI : MonoBehaviour
{
    #region FIELDS
    public Text p1_charges;
    public Text p1_lives;
    public Text p2_charges;
    public Text p2_lives;
    public Text p3_charges;
    public Text p3_lives;
    public Text p4_charges;
    public Text p4_lives;
    Text victory;
    Transform tr;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = GetComponent<Transform>();
        victory = transform.Find("Victory").GetComponent<Text>();
        p1_charges = tr.Find("P1 Charge").GetComponent<Text>();
        p2_charges = tr.Find("P2 Charge").GetComponent<Text>();
        p3_charges = transform.Find("P3 Charge").GetComponent<Text>();
        p4_charges = tr.Find("P4 Charge").GetComponent<Text>();
        p1_lives = tr.Find("P1 Lives").GetComponent<Text>();
        p2_lives = tr.Find("P2 Lives").GetComponent<Text>();
        p3_lives = tr.Find("P3 Lives").GetComponent<Text>();
        p4_lives = tr.Find("P4 Lives").GetComponent<Text>();
        //Events.instance.AddListener<EVENT_UI_Initialize_Lives>(UpdateLives);
        Events.instance.AddListener<EVENT_UI_Initialize_Teleports>(UpdateTeleportUI);
        Events.instance.AddListener<EVENT_UI_Teleport_Used>(UpdateTeleportUI);
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


    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTeleportUI(EVENT_UI_Initialize_Teleports _event)
    {
        switch (_event.playerNumber)
        {
            case (0):
                {
                    p1_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (1):
                {
                    p2_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (2):
                {
                    p3_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (3):
                {
                    p4_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
        }

        //DEBUG
        //print("TELEPORT CHARGE CHANGED TO " + _event.playerData.Charges +" FOR PLAYER" + _event.playerData.PlayerNumber);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTeleportUI(EVENT_UI_Teleport_Used _event)
    {
        switch (_event.playerNumber)
        {
            case (0):
                {
                    p1_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (1):
                {
                    p2_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (2):
                {
                    p3_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
            case (3):
                {
                    p4_charges.text = "TELEPORTS: " + _event.teleportCharges;
                    break;
                }
        }

        //DEBUG
        //print("TELEPORT CHARGE CHANGED TO " + _event.playerData.Charges +" FOR PLAYER" + _event.playerData.PlayerNumber);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    private void UpdateLives(EVENT_Player_Died _event)
    {
        //print("LIVES CHANGED TO " + _event.playerData.Lives + " FOR PLAYER" + _event.playerData.PlayerNumber);
        switch (_event.playerData.PlayerNumber)
        {
            case (0):
                {
                    p1_lives.text = "LIVES: " + _event.playerData.Lives;
                    break;
                }
            case (1):
                {
                    p2_lives.text = "LIVES: " + _event.playerData.Lives;
                    break;
                }
            case (2):
                {
                    p3_lives.text = "LIVES: " + _event.playerData.Lives;
                    break;
                }
            case (3):
                {
                    p4_lives.text = "LIVES: " + _event.playerData.Lives;
                    break;
                }
        }
    }
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_UI_Initialize_Teleports>(UpdateTeleportUI);
        Events.instance.RemoveListener<EVENT_UI_Teleport_Used>(UpdateTeleportUI);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
