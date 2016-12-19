///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_Slider.cs
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

public class UI_Slider : MonoBehaviour
{
    #region FIELDS
    RectTransform rt;
    Image img;

    [Range(0f, 1f)]
    public float startValue = 0f;
    [HideInInspector]
    public int rechargeQueue = 0;
    float duration;
    int playerNumber;
    PlayerData pd;
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
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        Events.instance.AddListener<EVENT_Teleport_Used>(PlayerCheck);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        playerNumber = pd.PlayerNumber;
        img.color = pd.PlayerColor;
        img.fillAmount = 0f;
        rechargeQueue = 0;
	}
    #endregion

    #region METHODS
    void PlayerCheck(EVENT_Teleport_Used _event)
    {
        if(_event.playerData.PlayerNumber == playerNumber)
        {
            AnimateSlider(_event.playerData.TeleportRechargeTime);
        }
    }
    public void AnimateSlider(float _duration)
    {
        if(LeanTween.isTweening(this.gameObject))
        {
            ++rechargeQueue;
            duration = _duration;
            return;
        }
        else
        {
            LeanTween.value(this.gameObject, SetValue, 0f, 1f, _duration);
            LeanTween.delayedCall(_duration + 0.25f, ResetSlider);
            LeanTween.delayedCall(_duration, CheckRechargeQueue);
        }
    }
    void ResetSlider()
    {
        img.fillAmount = startValue;
    }
    void CheckRechargeQueue()
    {
        --rechargeQueue;
        if(rechargeQueue <= 0)
        {
            rechargeQueue = 0;
            return;
        }
        else
        {
            AnimateSlider(duration);
        }
    }
    void SetValue(float _value)
    {
        img.fillAmount = _value;
    }
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Teleport_Used>(PlayerCheck);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
