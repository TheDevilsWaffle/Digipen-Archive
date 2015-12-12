/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Vacuum.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Vacuum : MonoBehaviour
{
    //SHOOTING PROPERTIES
    [Header("Shooting Properties")]
    [SerializeField]
    GameObject BulletModel;
    [SerializeField]
    Vector3 BulletSpawnDistance = new Vector3(0, 0, 1);
    [SerializeField]
    public int BulletSpeed = 10;
    [SerializeField]
    float ShotDelay = 1f;
    [SerializeField]
    float ShotVariance = 0f;
    [SerializeField]
    KeyCode FireButton = KeyCode.F;

    //VACUUM PROPERTIES
    [Header("Vacuum Properties")]
    [SerializeField]
    GameObject VacuumCone;
    [SerializeField]
    Quaternion VacuumConeRotation = new Quaternion(0, 0, 0, 90);
    [SerializeField]
    float SuctionPower;
    [SerializeField]
    KeyCode VacuumButton = KeyCode.G;
    [HideInInspector]
    bool IsVacuumFiring = false;

    //OTHER PROPERTIES
    [HideInInspector]
    bool IsFiring = false;
    [HideInInspector]
    float ShotTimer = 0f;
    [HideInInspector]
    Vector3 VacGunPos;

    [SerializeField]
    GameObject Player;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //PURPOSE: Initialize
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //PURPOSE: logical frame update
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if player pressed FireButton
        if (Input.GetKeyDown(this.FireButton))
        {
            //check to see if the ShotDelay threshold has been met
            if (this.CanPlayerShoot(this.ShotTimer, this.ShotDelay))
            {
                this.ShootWeapon();
            }
        }
        if (Input.GetKey(this.VacuumButton) && !this.IsVacuumFiring)
        {
            this.StartSuction();
        }

        //increment timer based on Dt
        this.ShotTimer += Time.deltaTime;

    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CanPlayerShoot()
    //PURPOSE: Checks a timer against a delay threshold
    /////////////////////////////////////////////////////////////////////////*/
    bool CanPlayerShoot(float timer, float delay)
    {
        if (timer >= delay)
        {
            Debug.Log("Player Can Shoot!");
            //reset ShotTimer
            this.ShotTimer = 0f;
            return true;
        }
        else
        {
            Debug.Log("Cannot fire yet (ShotDelay)");
            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ShootWeapon()
    //PURPOSE: create bullet
    /////////////////////////////////////////////////////////////////////////*/
    void ShootWeapon()
    {
        //create the bullet (bullet will apply velocity on it's own script)
        GameObject bullet = (GameObject)Instantiate(this.BulletModel,
                                                    (transform.position + transform.TransformDirection(this.BulletSpawnDistance)),
                                                    transform.rotation);
        bullet.GetComponent<Bullet>().Planet = this.Player.GetComponent<PlayerController>().Planet;
        Debug.Log("pew!");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: StartSuction()
    //PURPOSE: suck near objects
    /////////////////////////////////////////////////////////////////////////*/
    void StartSuction()
    {
        //let's raycast
        Debug.DrawRay(this.transform.position, (this.transform.forward * 10), Color.green, 100f);
        Debug.Log("VACUUM TIME, BABY!");

    }
}