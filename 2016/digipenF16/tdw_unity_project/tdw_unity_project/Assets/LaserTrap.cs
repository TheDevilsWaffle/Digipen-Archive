///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LaserTrap.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class LaserTrap : MonoBehaviour
{
    #region FIELDS
    TrapStatus currentStatus;
    [Header("LASER")]
    public float laserDuration;
    public float resetDuration;
    public GameObject laserBase;
    Color laserBase_color;
    public GameObject laserLargeFin;
    Color laserLargeFin_color;
    public GameObject laserSmallFin;
    Color laserSmallFin_color;
    public GameObject laserBall;
    Color laserBall_color;
    Transform laserStartPos;
    public LineRenderer laser;
    [Header("ANIMATION")]
    public AnimationCurve warmUpAC;
    public Color warmUpColor = new Color(1f, 1f, 1f, 1f);
    public float warmUpTime;
    public float warmUpDelay;
    public AnimationCurve cooldownAC;
    public float cooldownTime;
    public float cooldownDelay;
    float timer = 0f;
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
        laserBase_color = laserBase.GetComponent<Renderer>().material.color;
        laserLargeFin_color = laserLargeFin.GetComponent<Renderer>().material.color;
        laserSmallFin_color = laserSmallFin.GetComponent<Renderer>().material.color;
        laserBall_color = laserBall.GetComponent<Renderer>().material.color;
        laserStartPos = laserBall.transform;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        laser.SetPosition(0, laserStartPos.position);
        laser.enabled = false;
        ResetTimer();
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
        if(currentStatus == TrapStatus.READY)
        {
            WarmUpLaser();
        }
	    else if(currentStatus == TrapStatus.DEPLOYED)
        {
            FireLaser();
            CheckTimer("LaserFired");
            IncrementTimer();
        }
        else if(currentStatus == TrapStatus.RESETTING)
        {
            CheckTimer("LaserResetting");
            IncrementTimer();
        }
	}
    #endregion

    #region METHODS
    void ResetTimer()
    {
        timer = 0f;
    }
    void IncrementTimer()
    {
        timer += Time.deltaTime;
    }
    void CheckTimer(string _timer)
    {
        switch(_timer)
        {
            case ("LaserFired"):
                if (timer > laserDuration)
                {
                    CooldownLaser();
                    ResetTimer();
                }
                break;
            case ("LaserResetting"):
                if (timer > resetDuration)
                {
                    SetLaserToReady();
                    ResetTimer();
                }
                break;
        }
        
    }
    void WarmUpLaser()
    {
        currentStatus = TrapStatus.TRIGGERED;
        LeanTween.color(laserBase, warmUpColor, warmUpTime).setEase(warmUpAC);
        LeanTween.color(laserLargeFin, warmUpColor, warmUpTime).setEase(warmUpAC).setDelay(warmUpDelay);
        LeanTween.color(laserSmallFin, warmUpColor, warmUpTime).setEase(warmUpAC).setDelay((warmUpDelay * 1.5f));
        LeanTween.color(laserBall, warmUpColor, warmUpTime).setEase(warmUpAC).setDelay((warmUpDelay * 2f));
        LeanTween.delayedCall((warmUpTime + (warmUpDelay * 3)), ChangeLaserStatus);
    }

    void CooldownLaser()
    {
        currentStatus = TrapStatus.RESETTING;
        LeanTween.color(laserBase, laserBase_color, cooldownTime).setEase(cooldownAC).setDelay(cooldownDelay * 2f);
        LeanTween.color(laserLargeFin, laserLargeFin_color, cooldownTime).setEase(cooldownAC).setDelay(cooldownDelay * 1.5f);
        LeanTween.color(laserSmallFin, laserSmallFin_color, cooldownTime).setEase(cooldownAC).setDelay(cooldownDelay);
        LeanTween.color(laserBall, laserBall_color, cooldownTime).setEase(cooldownAC);
        laser.enabled = false;
    }

    void ChangeLaserStatus()
    {
        currentStatus = TrapStatus.DEPLOYED;
    }
    void FireLaser()
    {
        RaycastHit _hitInfo;
        Physics.Raycast(laserStartPos.position, laserStartPos.right, out _hitInfo);
        laser.enabled = true;
        laser.SetPosition(1, _hitInfo.point);

        if(_hitInfo.collider.gameObject.tag == "Player"
            && _hitInfo.collider.gameObject.GetComponent<PlayerData>() != null
            && _hitInfo.collider.gameObject.GetComponent<PlayerData>().Status == PlayerStatus.VULNERABLE)
        {
            GameObject _player = _hitInfo.collider.gameObject;
            _player.GetComponent<TelefragDetector>().KillPlayer();
        }

    }
    void SetLaserToReady()
    {
        currentStatus = TrapStatus.READY;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
