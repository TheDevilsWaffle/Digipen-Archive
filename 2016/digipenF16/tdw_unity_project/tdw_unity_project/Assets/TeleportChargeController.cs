///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TeleportChargeController.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class TeleportChargeController : MonoBehaviour
{
    #region FIELDS
    public GameObject[] charges;
    [Range(0f,1f)]
    public float usedAlpha = 0.1f;
    [Range(0f,1f)]
    public float restoredAlpha = 1f;
    public LeanTweenType ease = LeanTweenType.easeInBack;
    public float time = 0.5f;
    public float scaleFactor = 5f;
    Vector3 scaleOriginal;
    Vector3 scaleOut;
    PlayerData pd;
    int playerNumber;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        pd = transform.root.gameObject.GetComponent<PlayerData>();
        Events.instance.AddListener<EVENT_UI_Teleport_Restored>(RestoreTeleportCharges);
        Events.instance.AddListener<EVENT_UI_Teleport_Used>(DecreaseTeleportCharge);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        scaleOriginal = Vector3.one;
        scaleOut = scaleOriginal * scaleFactor;
        playerNumber = pd.PlayerNumber;
        SetTeleportChargeColor(pd.PlayerColor);
	}
    #endregion
    void SetTeleportChargeColor(Color _color)
    {
        foreach(GameObject charge in charges)
        {
            charge.GetComponent<Image>().color = _color;
        }
    }
    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RestoreTeleportCharges(EVENT_UI_Teleport_Restored _event)
    {
        if (_event.playerNumber == playerNumber)
        {
            //print("TELEPORT CHARGE (" + _event.teleportCharges + " of "+ pd.MaxTeleportCharges +") RESTORED for PLAYER " + _event.playerNumber + "!");
            switch (_event.teleportCharges)
            {
                case (3):
                    AnimateTeleportChargeIn(charges[2].GetComponent<RectTransform>());
                    break;
                case (2):
                    AnimateTeleportChargeIn(charges[1].GetComponent<RectTransform>());
                    break;
                case (1):
                    AnimateTeleportChargeIn(charges[0].GetComponent<RectTransform>());
                    break;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseTeleportCharge(EVENT_UI_Teleport_Used _event)
    {
        if(_event.playerNumber == playerNumber)
        {
            //print("TELEPORT CHARGE (" + _event.teleportCharges + " of " + pd.MaxTeleportCharges + ") USED for PLAYER " + _event.playerNumber + "!");
            switch (_event.teleportCharges)
            {
                case (3):
                    AnimateTeleportChargeOut(charges[2].GetComponent<RectTransform>());
                    break;
                case (2):
                    AnimateTeleportChargeOut(charges[1].GetComponent<RectTransform>());
                    break;
                case (1):
                    AnimateTeleportChargeOut(charges[0].GetComponent<RectTransform>());
                    break;
            } 
        }
    }
    void AnimateTeleportChargeOut(RectTransform _rt)
    {
        LeanTween.alpha(_rt, usedAlpha, time).setEase(ease);
        LeanTween.scale(_rt, scaleOut, time).setEase(ease);
        LeanTween.scale(_rt, scaleOriginal, 0.001f).setDelay(time);
    }
    void AnimateTeleportChargeIn(RectTransform _rt)
    {
        _rt.localScale = scaleOut;
        LeanTween.alpha(_rt, restoredAlpha, time).setEase(ease);
        LeanTween.scale(_rt, scaleOriginal, time).setEase(ease);
    }
    #endregion

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_UI_Teleport_Restored>(RestoreTeleportCharges);
        Events.instance.RemoveListener<EVENT_UI_Teleport_Used>(DecreaseTeleportCharge);
    }
}
