///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ShootProjectile.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS
public enum ShooterTrapStatus
{
    RELOADING, AIMING, FIRING
};
#endregion

#region EVENTS

#endregion

public class ShootProjectile : MonoBehaviour
{
    #region FIELDS
    public GameObject bullet;
    public float bulletSpeed;
    public Transform spawnPos;
    ShooterTrapStatus currentStatus;
    public float reloadTime;
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
        currentStatus = ShooterTrapStatus.RELOADING;
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
        Reload();
        timer += Time.deltaTime;
	}
    #endregion

    #region METHODS
    
    void ResetTimer()
    {
        timer = 0f;
    }

    void Reload()
    {
        if(currentStatus == ShooterTrapStatus.RELOADING && timer > reloadTime)
        {
            //print("reloading");
            //create a bullet
            GameObject _bullet = Instantiate(bullet, spawnPos.position, Quaternion.identity) as GameObject;
            _bullet.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            StartCoroutine(FireBullet(_bullet));
        }
    }

    IEnumerator Move(Transform _bullet, Vector3 pos1, Vector3 pos2, AnimationCurve ac, float time)
    {
        float _timer = 0.0f;
        while (_timer <= time)
        {
            //print("loading bullet to position");
            _bullet.position = Vector3.Lerp(pos1, pos2, ac.Evaluate(timer / time));
            _timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FireBullet(_bullet.gameObject));
    }

    IEnumerator FireBullet(GameObject _bullet)
    {
        //print("firing");
        currentStatus = ShooterTrapStatus.FIRING;
        _bullet.GetComponent<ConstantVelocity>().speed = bulletSpeed;
        ResetTimer();
        currentStatus = ShooterTrapStatus.RELOADING;
        yield return new WaitForSeconds(0.25f);
        _bullet.AddComponent<DestroyOnImpact>();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
