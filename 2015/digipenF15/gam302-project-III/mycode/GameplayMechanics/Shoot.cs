/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Shoot.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System;


public class Shoot:MonoBehaviour
{
    private AudioClip shootSfx;
    AudioSource audio; 
    //Properties
    [SerializeField]
    float ShotVariance = 0f;
    [SerializeField]
    Vector3 SpawnDistance = new Vector3 (0,0,0);
    [HideInInspector]
    bool IsFiring = false;
    [SerializeField]
    float ShotDelay = 1f;
    [HideInInspector]
    float ShotTimer = 0f;
    [HideInInspector]
    Vector3 VacGunPos;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    GameObject Player;

    [SerializeField]
    private GameObject Particles;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //PURPOSE: Initialize
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        if (this.Player == null)
            return;

        this.Player = GameObject.FindWithTag("Player").gameObject;
        shootSfx = new AudioClip();
        shootSfx = (AudioClip)Resources.Load("Sounds/sfx/player/laser");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //PURPOSE: logical frame update
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if player pressed FireButton
        if (Convert.ToBoolean(Input.GetAxis("Fire1")))
        {
            //check to see if the ShotDelay threshold has been met
            if (this.CanPlayerShoot(this.ShotTimer, this.ShotDelay))
            {
                this.ShootWeapon();
            }
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
        if (timer >= delay && Player1Data.Player1DataObject.Ammo > 0)
        {
            //Debug.Log("Player Can Shoot!");
            //reset ShotTimer
            this.ShotTimer = 0f;
            return true;
        }
        else
        {
            //Debug.Log("Cannot fire yet (ShotDelay)");
            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ShootWeapon()
    //PURPOSE: create bullet
    /////////////////////////////////////////////////////////////////////////*/
    void ShootWeapon()
    {
        if (this.Player == null)
            return;

        GameObject planet = this.Player.GetComponent<PlayerController>().Planet;
        GameObject playerCam = this.Player.GetComponent<PlayerController>().PlayerCam;
        
        Vector3 awayFromPlanetoid = planet.transform.position - this.transform.position;

        Vector3 shootDirection = Vector3.ProjectOnPlane(playerCam.transform.forward, awayFromPlanetoid);
        this.Player.transform.LookAt(this.Player.transform.position + shootDirection, -awayFromPlanetoid);

        //create the bullet (bullet will apply velocity on it's own script)
        Vector3 spawnPos = transform.position + transform.TransformDirection(this.SpawnDistance);
        GameObject bullet = (GameObject)Instantiate(this.Bullet, spawnPos, transform.rotation);
        bullet.GetComponent<Bullet>().Planet = planet; //This will be a separate component later
        bullet.GetComponent<Bullet>().HeightFromCenter = (planet.transform.position - spawnPos).magnitude * 0.9f;
        bullet.transform.parent = planet.transform;
        bullet.GetComponent<Bullet>().ShotBy = this.gameObject;
        audio = transform.GetComponentInParent<AudioSource>();
        transform.GetComponentInParent<AudioSource>().clip = shootSfx;
        audio.PlayOneShot(shootSfx, .65f);

        this.Particles.GetComponent<ParticleSystem>().Play();

        //update the hud
        Player1Data.Player1DataObject.UpdateAmmo(-1);
        GameObject.FindWithTag("HUD").GetComponent<HUD>().UpdateHUDElementFromData("ammo", Player1Data.Player1DataObject.Ammo);

    }
}