///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TelefragDetector.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(ExplosiveDeath))]
[RequireComponent(typeof(PlayerData))]

public class TelefragDetector : MonoBehaviour
{
    #region FIELDS
    ExplosiveDeath ed;
    Transform tr;
    PlayerData pd;
    [SerializeField]
    AudioClip sfx_playerDeath;
    [SerializeField]
    AudioClip sfx_spikeDeath;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        ed = GetComponent<ExplosiveDeath>();
        tr = GetComponent<Transform>();
        pd = GetComponent<PlayerData>();
    }
    #endregion

    #region TRIGGERS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_collider"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.GetComponent<RightStickTeleport>() != null 
            && _collider.gameObject.GetComponent<RightStickTeleport>().CurrentStatus == TeleportStatus.TELEPORTING)
        {
            //DEBUG
            //Debug.Log(_collider.gameObject.name + "(LIVES = " + _collider.gameObject.GetComponent<PlayerData>().Lives + ", and STATUS = " + _collider.gameObject.GetComponent<PlayerData>().Status + ")" + " teleported into " + this.gameObject.name + "(LIVES = " + pd.Lives + ", and STATUS = " + pd.Status + ")");
            if(pd.Status == PlayerStatus.VULNERABLE)
            {
                KillPlayer();
            }
        }
        else if(_collider.gameObject.tag == "Trap")
        {
            if (pd.Status == PlayerStatus.VULNERABLE)
            {
                if (sfx_spikeDeath != null)
                    SFXSystem.sfxSystem.PlaySFX(sfx_spikeDeath);
                KillPlayer();
            }
        }
    }

    public void KillPlayer()
    {
        if (sfx_playerDeath != null)
            SFXSystem.sfxSystem.PlaySFX(sfx_playerDeath);

        //don't get hit again
        pd.SetPlayerStatus(PlayerStatus.INVULNERABLE);
        //print(this.gameObject.name + " IS DEAD, BLECHKS!");
        ed.Explode(tr.position, pd.PlayerColor);
        Events.instance.Raise(new EVENT_Player_Died(this.gameObject.GetComponent<PlayerData>(), this.gameObject.transform.position));
    }
    #endregion
}
