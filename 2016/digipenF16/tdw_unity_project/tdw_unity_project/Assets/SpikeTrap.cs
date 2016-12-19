///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — SpikeTrap.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum TrapStatus
{
    READY, TRIGGERED, DEPLOYED, RESETTING
};
public enum TrapType
{
    TIMED, TRIGGERED
};
#endregion

#region EVENTS

#endregion

public class SpikeTrap : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    TrapType trapType;
    public bool isCooldownRandom;
    public float minCooldown;
    public float maxCooldown;
    TrapStatus trapStatus;
    [SerializeField]
    GameObject spikes;
    [SerializeField]
    BoxCollider spikeHitBox;
    public AnimationCurve acDeploy;
    public float deployOffset = -0.5f;
    public float deployTime;
    public AnimationCurve acReset;
    public float resetDelay;
    public float resetTime;
    public float cooldown;
    public float triggerDelay = 0.5f;
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

    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        timer = 0f;
        trapStatus = TrapStatus.READY;
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
	    if(trapType == TrapType.TIMED && trapStatus == TrapStatus.READY)
        {
            IncrementTimer();

            if(timer > cooldown)
            {
                ResetTimer();
                DeployTrap();
            }
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

    void DeployTrap()
    {
        trapStatus = TrapStatus.DEPLOYED;
        spikeHitBox.enabled = true;
        spikeHitBox.isTrigger = true;
        LeanTween.moveLocalX(spikes, deployOffset, deployTime).setEase(acDeploy);
        LeanTween.delayedCall(this.gameObject, resetDelay, ResetTrap);
    }

    void ResetTrap()
    {
        spikeHitBox.enabled = true;
        trapStatus = TrapStatus.RESETTING;
        LeanTween.moveLocalX(spikes, -deployOffset, resetTime).setEase(acReset).setDelay(resetDelay);
        LeanTween.delayedCall(this.gameObject, resetDelay, TrapReady);
    }

    void TrapReady()
    {

        spikeHitBox.isTrigger = false;
        if (isCooldownRandom)
            cooldown = GetRandomValue();
        trapStatus = TrapStatus.READY;
    }

    float GetRandomValue()
    {
        return Random.Range(minCooldown, maxCooldown);
    }

    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player" && trapType == TrapType.TRIGGERED && trapStatus == TrapStatus.READY)
        {

        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
