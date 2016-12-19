///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — FlipTrap.cs
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

public class FlipTrap : MonoBehaviour
{
    #region FIELDS
    TrapStatus currentStatus;
    public Transform pivot;
    Vector3 pivotStartPos;
    public Vector3 pivotRotation;
    public GameObject flipper;
    Rigidbody flipper_rb;
    public float speed;
    float timer;
    public float threshold;
    float trapStartTime;
    public float timeTakenDuringLerp;
    public float resetTime;
    public float angle;
    public float cooldown;
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
        flipper_rb = flipper.GetComponent<Rigidbody>();
        
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        pivotStartPos = pivot.position;
        print(pivotStartPos);
        ResetTimer();
        currentStatus = TrapStatus.READY;
	}
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FixedUpdate()
    {
        IncrementTimer();
        if (timer > threshold && currentStatus == TrapStatus.READY)
            DeployTrap();
        else if (currentStatus == TrapStatus.TRIGGERED)
            AnimateTrap();
        else if (timer > cooldown && currentStatus == TrapStatus.DEPLOYED)
            PrepareToReset();
        else if (currentStatus == TrapStatus.RESETTING)
            ResetTrap();
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
        currentStatus = TrapStatus.TRIGGERED;
        trapStartTime = Time.time;
        ResetTimer();
    }

    void PrepareToReset()
    {
        currentStatus = TrapStatus.RESETTING;
        trapStartTime = Time.time;
        ResetTimer();
    }

    void AnimateTrap()
    {
        float _timeSinceStarted = Time.time - trapStartTime;
        float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

        flipper.transform.RotateAround(pivotStartPos, transform.up, angle);
        if (_percentageComplete >= 1.0f)
        {
            ResetTimer();
            currentStatus = TrapStatus.DEPLOYED;
        }
    }
    void ResetTrap()
    {
        float _timeSinceStarted = Time.time - trapStartTime;
        float _percentageComplete = _timeSinceStarted / resetTime;

        flipper.transform.RotateAround(pivotStartPos, transform.up, -angle);
        if (_percentageComplete >= 1.0f)
        {
            ResetTimer();
            currentStatus = TrapStatus.READY;
        }
    }

    void OnTriggerStay(Collider _collider)
    {
        print(_collider.gameObject.tag);
        if (currentStatus == TrapStatus.DEPLOYED && _collider.gameObject.tag == "Player")
        {
            print("firing!");
            _collider.gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(-50f, 0f, 50f));
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
