///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — FloorTrap.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
#endregion

#region EVENTS

#endregion

public class FloorTrap : MonoBehaviour
{
    #region FIELDS
    TrapStatus currentStatus;
    [SerializeField]
    TrapType trapType;
    [SerializeField]
    BoxCollider bc;
    [SerializeField]
    GameObject lPanel;
    Vector3 lPanelStartPos;
    [SerializeField]
    GameObject rPanel;
    Vector3 rPanelStartPos;
    public float offset;
    public AnimationCurve acDeploy;
    public float deployTime;
    public float stayOpenTime;
    public AnimationCurve acReset;
    public float resetTime;
    public float stayClosedTime;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        lPanelStartPos = lPanel.transform.position;
        rPanelStartPos = rPanel.transform.position;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        currentStatus = TrapStatus.READY;
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
        CheckTrapStatus();
    }
    #endregion

    #region METHODS
    void CheckTrapStatus()
    {
        if(currentStatus == TrapStatus.READY)
        {
            DeployTrap();
        }
    }
    void DeployTrap()
    {
        currentStatus = TrapStatus.DEPLOYED;
        bc.enabled = false;
        LeanTween.moveLocalZ(lPanel, offset, deployTime).setEase(acDeploy);
        LeanTween.moveLocalZ(rPanel, -offset, deployTime).setEase(acDeploy);
        LeanTween.delayedCall((deployTime + stayOpenTime), ResetTrap);
    }
    void ResetTrap()
    {
        currentStatus = TrapStatus.RESETTING;
        LeanTween.move(lPanel, lPanelStartPos, resetTime).setEase(acReset);
        LeanTween.move(rPanel, rPanelStartPos, resetTime).setEase(acReset);
        LeanTween.delayedCall((resetTime + stayClosedTime), ReadyTrap);
    }
    void ReadyTrap()
    {
        currentStatus = TrapStatus.READY;
        bc.enabled = true;
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
