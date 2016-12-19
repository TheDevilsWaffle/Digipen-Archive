///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Follower.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum FollowerStatus
{
    READY,
    MOVING,
    MOVED,
    DEAD,
};
#endregion

#region EVENTS

#endregion

public class Follower : MonoBehaviour
{
    #region FIELDS
    FollowerStatus status = FollowerStatus.READY;
    public Transform target;
    [Header("Follow Delay")]
    public float delayMin;
    public float delayMax;
    float delay;
    float timer = 0f;
    [Header("Follow Time")]
    public AnimationCurve followAC;
    public float followMin;
    public float followMax;
    float followTime;
    public Vector3 offset = new Vector3(0.2f, 0.1f, 0.2f);
    Vector3 velocity = Vector3.zero;

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
        tr = transform;
        status = FollowerStatus.READY;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        followTime = GetRandomFloat(followMin, followMax);
        delay = GetRandomFloat(delayMin, delayMax);
        //print("delay = " + delay);
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
        timer += Time.deltaTime;

	    if(status == FollowerStatus.READY && timer < followTime)
        {
            //print("following!");
            FollowTarget();
        }
        else if(timer > followTime)
        {
            //print("delayed");
            timer = 0f;
            status = FollowerStatus.MOVED;
            delay = GetRandomFloat(delayMin, delayMax);
        }
        else if(timer > delay && status == FollowerStatus.MOVED)
        {
            timer = 0f;
            followTime = GetRandomFloat(followMin, followMax);
            status = FollowerStatus.READY;
        }
	}
    #endregion

    #region METHODS
    void FollowTarget()
    {
        tr.position = Vector3.SmoothDamp(tr.position, target.transform.position, ref velocity, followTime);
    }
    void WaitBeforeReady()
    {
        //print("waiting to be ready");
        LeanTween.delayedCall(delay, UpdateStatusReady);
    }
    void UpdateStatusMoved()
    {
        //print("changing status to moved");
        status = FollowerStatus.MOVED;
    }
    void UpdateStatusReady()
    {
        //print("changing status to ready");
        status = FollowerStatus.READY;
    }
    float GetRandomFloat(float _min, float _max)
    {
        return Random.Range(_min, _max);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
