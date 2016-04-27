/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - FireGun.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class FireGun : MonoBehaviour
{
    //GUN PROPERTIES
    [Header("Gun")]
    [RangeAttribute(0, 3)]
    [SerializeField]
    int ShootButton = 0;

    //SHOT PROPERTIES
    [Header("Shot")]
    [SerializeField]
    Vector3 StandardShotSize = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    float OverChargeShotSizeMultiplier = 1.25f;
    [SerializeField]
    float Speed = 750f;
    [SerializeField]
    Vector3 ShotOffset = new Vector3(0f, 0f, 1f);
    [SerializeField]
    GameObject ShotObject;

    //CHARGE PROPERTIES
    [Header("Charge")]
    [SerializeField]
    float OverheatThreshold = 3f;
    [SerializeField]
    float OverChargeThreshold = 2f;

    //COOLDOWN PROPERTIES
    [Header("Cooldown")]
    [SerializeField]
    float StandardShotCooldownThreshold = 1.5f;
    [SerializeField]
    float OverchargeShotCooldownThreshold = 3f;
    [SerializeField]
    float OverheatCooldownThreshold = 4.5f;
    [HideInInspector]
    float ShotCooldownThreshold = 3f;

    //HIDDEN PROPERTIES
    [HideInInspector]
    public GameObject ShotBy = null;
    [HideInInspector]
    bool IsCoolingDown = false;
    [HideInInspector]
    float CooldownTimer = 0f;
    [HideInInspector]
    bool IsCharging = false;
    [HideInInspector]
    float ChargeTimer = 0f;
    [HideInInspector]
    Vector3 ShotSize = new Vector3(1f, 1f, 1f);
    [HideInInspector]
    bool CanBreakBlocks = false;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure variables are set
        this.IsCharging = false;
        this.CanBreakBlocks = false;
        this.ChargeTimer = 0f;

        //set CooldownTimer to ShotCooldownThreshold so we can use it right away.
        this.CooldownTimer = this.ShotCooldownThreshold;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if player pressed FireButton and was not already charging
        /*NOTE: 
                using GetMouseButton instead of GetMouseButtonDown
                so that we can update every frame.
        */
        if (Input.GetMouseButton(this.ShootButton))
        {
            //check to see if the ShotDelay threshold has been met
            if (!this.IsCharging && this.CanPlayerShoot(this.CooldownTimer, this.ShotCooldownThreshold))
            {
                //update IsCharging to true so we can potentially fire the weapon
                this.IsCharging = true;

                //DEBUG
                //Debug.Log("setting ISCHARGING = " + this.IsCharging);
            }
            if(this.IsCharging)
            {
                //check to see if we can keep charging the shot
                this.EvaluateChargeTimer(this.ChargeTimer, Time.deltaTime);
            }
        }

        //player was charging weapon, but has released FireButton
        if(this.IsCharging && Input.GetMouseButtonUp(this.ShootButton))
        {
            //set the shot properties
            this.SetShotProperties();

            //finally fire the shot
            this.ShootWeapon();

            //reset charge
            this.ResetCharge();
        }
        if (!this.IsCharging)
        {
            //increment CooldownTimer based on Dt
            this.CooldownTimer += Time.deltaTime;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ResetCharge()
    /////////////////////////////////////////////////////////////////////////*/
    void ResetCharge()
    {
        //reset IsCharging back to false
        this.IsCharging = false;
        //reset ChargeTimer
        this.ChargeTimer = 0f;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CanPlayerShoot()
    /////////////////////////////////////////////////////////////////////////*/
    bool CanPlayerShoot(float timer, float delay)
    {
        if (timer >= delay)
        {
            //DEBUG
            //Debug.Log(this.gameObject.transform.parent + " can shoot!");

            //reset ShotTimer
            this.CooldownTimer = 0f;
            return true;
        }
        else
        {
            Debug.Log(this.gameObject.transform.parent + "cannot fire yet (ShotCooldownThreshold not yet met)");
            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateChargeTimer()
    /////////////////////////////////////////////////////////////////////////*/
    void EvaluateChargeTimer(float timer_, float updateTimer_)
    {
        //evaluate timer_
        if(timer_ <= this.OverheatThreshold)
        {
            //increment ChargeTimer
            this.ChargeTimer += updateTimer_;

            //DEBUG
            Debug.Log("SHOT IS CHARGING = " + this.ChargeTimer);
        }
        //player has charged too long, break out of charging
        else
        {
            this.OverheatState();
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OverheatState()
    /////////////////////////////////////////////////////////////////////////*/
    void OverheatState()
    {
        this.ChargeTimer = 0f;
        this.IsCharging = false;

        //set the CoolDownThreshold timer for a MUCH longer time
        this.ShotCooldownThreshold = this.OverheatCooldownThreshold;

        //DEBUG
        //Debug.Log("GUN HAS OVERHEATED!");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ShootWeapon()
    /////////////////////////////////////////////////////////////////////////*/
        void ShootWeapon()
    {
        Vector3 shootDirection = this.gameObject.transform.forward;

        GameObject shot = (GameObject)Instantiate(this.ShotObject, 
                                                  (this.gameObject.transform.position + this.gameObject.transform.forward), 
                                                  Quaternion.identity);

        //fire the newly created blasterBolt
        shot.GetComponent<Rigidbody>().velocity = (shootDirection * this.Speed);

        //assign this player as the one who shot this shot
        shot.GetComponent<ShotProperties>().ShotBy = this.gameObject.transform.parent.gameObject;
        shot.GetComponent<ShotProperties>().ShotSize = this.ShotSize;
        shot.GetComponent<ShotProperties>().CanBreakBlocks = this.CanBreakBlocks;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetShotProperties()
    /////////////////////////////////////////////////////////////////////////*/
    void SetShotProperties()
    {
        //evaluate timer_
        if (this.ChargeTimer <= this.OverChargeThreshold)
        {
            this.ShotSize = this.StandardShotSize;
            this.CanBreakBlocks = false;

            //set the CoolDownThreshold timer to normal time
            this.ShotCooldownThreshold = this.StandardShotCooldownThreshold;

            //DEBUG
            Debug.Log("SHOT = STANDARD SIZE " + this.ShotSize + " / NO BREAK BLOCKS");
        }
        else
        {
            this.ShotSize = this.StandardShotSize * Mathf.Pow(this.ChargeTimer, this.OverChargeShotSizeMultiplier);
            this.CanBreakBlocks = true;

            //set the CoolDownThreshold timer for a longer time
            this.ShotCooldownThreshold = this.OverchargeShotCooldownThreshold;

            //DEBUG
            Debug.Log("SHOT = OVERCHARGE SIZE " + this.ShotSize + " / YES BREAK BLOCKS");
        }
    }
}
