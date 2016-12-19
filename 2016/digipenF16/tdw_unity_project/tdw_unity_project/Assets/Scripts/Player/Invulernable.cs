///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Invulernable.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;
//using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

public class Invulernable : MonoBehaviour
{
    #region FIELDS
    public Material invulnerableMaterial;
    public float numberOfFlashes;
    public float flashDuration;
    public GameObject playerModel;
    Material playerMaterial;
    PlayerData pd;
    Renderer re;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        playerMaterial = playerModel.GetComponent<Renderer>().material;
        pd = GetComponent<PlayerData>();
        re = playerModel.GetComponent<Renderer>();
        Events.instance.AddListener<EVENT_Player_Spawned>(FlashInvulnerability);
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
    void FlashInvulnerability(EVENT_Player_Spawned _event)
    {
        //only flash if this player is invulnerable
        if(_event.playerNumber == pd.PlayerNumber)
        {
            pd.SetPlayerStatus(PlayerStatus.INVULNERABLE);
            StartCoroutine(Invulnerable());
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator Invulnerable()
    {
        //DEBUG
        //Debug.log("flashing invulnerable for PLAYER " + pd.PlayerNumber);
        for (int i = 1; i <= numberOfFlashes; ++i)
        {
            if (i % 2 == 0)
                re.material = playerMaterial;
            else
                re.material = invulnerableMaterial;

            yield return new WaitForSeconds(flashDuration);
        }
        //return to normal
        PlayerIndex _player = (PlayerIndex)pd.PlayerNumber;
        GamePad.SetVibration(_player, 0f, 0f);

        re.material = playerMaterial;
        pd.SetPlayerStatus(PlayerStatus.VULNERABLE);

    }
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Player_Spawned>(FlashInvulnerability);
    }
    #endregion
}
