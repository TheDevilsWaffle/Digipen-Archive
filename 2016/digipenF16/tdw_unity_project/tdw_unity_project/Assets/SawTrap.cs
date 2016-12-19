///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — SawTrap.cs
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

public class SawTrap : MonoBehaviour
{
    #region FIELDS
    TrapStatus currentStatus;
    [SerializeField]
    TrapType trapType;
    public GameObject saw;
    [SerializeField]
    Transform[] trapRoute;
    public float verticalTime;
    public float horizontalTime;
    public AnimationCurve acHorizontal;
    public AnimationCurve acVertical;
    public float delay;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {

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
        if (currentStatus == TrapStatus.READY)
        {
            DeploySawBlade();
        }
	}
    #endregion

    #region METHODS
    void DeploySawBlade()
    {
        currentStatus = TrapStatus.DEPLOYED;
        LeanTween.move(saw, trapRoute[0], verticalTime).setEase(acVertical);
        LeanTween.delayedCall(verticalTime + delay, ActiveSawBlade);
    }
    void ActiveSawBlade()
    {
        LeanTween.move(saw, trapRoute[1], horizontalTime).setEase(acHorizontal);
        LeanTween.delayedCall(horizontalTime + delay, LowerSawBlade);
    }
    void LowerSawBlade()
    {
        currentStatus = TrapStatus.RESETTING;
        LeanTween.move(saw, trapRoute[2], verticalTime).setEase(acVertical);
        LeanTween.delayedCall(verticalTime + delay, ResetSawBlade);
    }
    void ResetSawBlade()
    {
        LeanTween.move(saw, trapRoute[3], horizontalTime).setEase(acHorizontal);
        LeanTween.delayedCall(horizontalTime + delay, SawBladeReady);
    }
    void SawBladeReady()
    {
        currentStatus = TrapStatus.READY;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
