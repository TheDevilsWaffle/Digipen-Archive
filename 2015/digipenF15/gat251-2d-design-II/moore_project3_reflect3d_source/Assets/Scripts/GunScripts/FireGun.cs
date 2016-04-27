/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - FireGun.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using XInputDotNetPure;
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
    public float Speed = 10f;
    [SerializeField]
    Vector3 ShotOffset = new Vector3(0f, 0f, 1f);
    [SerializeField]
    GameObject ShotObject;
    [SerializeField]
    int NormalShotParticleCount = 50;
    [SerializeField]
    int LargeShotParticleCount = 200;

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

    //SFX PROPERTIES
    [Header("SFX")]
    public AudioClip[] GunSFXArray;
    GameObject AudioController;

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
    [HideInInspector]
    public GamePadState Gamepad;
    GamePadState state;
    GamePadState prevState;
    public PlayerIndex playerIndex;
    bool IsOverheated;
    GameObject GunShotParticles;
    GameObject OverheatParticles;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure variables are set
        this.IsCharging = false;
        this.CanBreakBlocks = false;
        this.ChargeTimer = 0f;
        this.IsOverheated = false;

        //find particles needed
        this.GunShotParticles = this.gameObject.transform.FindChild("GunShotParticles").gameObject;
        this.OverheatParticles = this.gameObject.transform.FindChild("OverheatParticles").gameObject;

        //set CooldownTimer to ShotCooldownThreshold so we can use it right away.
        this.CooldownTimer = this.ShotCooldownThreshold;

        //get the the AudioController
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        //check to see if player pressed FireButton and was not already charging
        /*NOTE: 
                using GetMouseButton instead of GetMouseButtonDown
                so that we can update every frame.
        */
        if (Input.GetMouseButton(this.ShootButton) || prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Pressed)
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
                //slow down the player's movement
                this.gameObject.transform.parent.transform.parent.GetComponent<PlayerMachine>().WalkSpeed = 2f;
                //check to see if we can keep charging the shot
                this.EvaluateChargeTimer(this.ChargeTimer, Time.deltaTime);
            }
        }

        //player was charging weapon, but has released FireButton
        if(this.IsCharging && Input.GetMouseButtonUp(this.ShootButton) || this.IsCharging && prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released)
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

            this.gameObject.transform.parent.transform.parent.GetComponent<PlayerMachine>().WalkSpeed = 10f;
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
    bool CanPlayerShoot(float timer_, float delay_)
    {
        if (timer_ >= delay_)
        {
            //DEBUG
            //Debug.Log(this.gameObject.transform.parent + " can shoot!");

            //reset ShotTimer
            this.CooldownTimer = 0f;
            this.IsOverheated = false;

            return true;
        }
        if(timer_ <= delay_ && !this.IsOverheated)
        {
            //play the cannot fire shot sfx
            this.PlaySFX("sfx_laserInactive");

            //Debug.Log(this.gameObject.transform.parent + "cannot fire yet (ShotCooldownThreshold not yet met)");
            return false;
        }
        else
        {
            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateChargeTimer()
    /////////////////////////////////////////////////////////////////////////*/
    void EvaluateChargeTimer(float timer_, float updateTimer_)
    {
        //evaluate timer_
        if (timer_ <= this.OverheatThreshold)
        {
            //increment ChargeTimer
            this.ChargeTimer += updateTimer_;

            //play the charge noise
            this.PlaySFX("sfx_laserCharge");

            //DEBUG
            //Debug.Log("SHOT IS CHARGING = " + this.ChargeTimer);
        }
        //player has charged too long, break out of charging
        else
        {
            this.OverheatState();

            //play overheat particles
            this.OverheatParticles.GetComponent<ParticleSystem>().Play();

            //play the overheat noise
            this.PlaySFXOneShot("sfx_overheat");
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

        this.IsOverheated = true;

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
        shot.GetComponent<ShotProperties>().ShotBy = this.gameObject.transform.root.gameObject;
        shot.GetComponent<ShotProperties>().ShotSize = this.ShotSize;
        shot.GetComponent<ShotProperties>().Speed = this.Speed;
        shot.GetComponent<ShotProperties>().CanBreakBlocks = this.CanBreakBlocks;
        //assign the shot the correct color
        shot.GetComponent<Renderer>().material = this.gameObject.transform.root.GetComponent<PlayerShotMaterial>().ShotColor;

        //play shoot particles
        this.GunShotParticles.GetComponent<ParticleSystem>().Play();

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

            //play the standard shot sfx
            this.PlaySFXOneShot("sfx_laserBolt");

            //set the amount of particles to fire
            this.GunShotParticles.GetComponent<ParticleSystem>().emissionRate = this.NormalShotParticleCount;

            //DEBUG
            //Debug.Log("SHOT = STANDARD SIZE " + this.ShotSize + " / NO BREAK BLOCKS");
        }
        else
        {
            this.ShotSize = this.StandardShotSize * Mathf.Pow(this.ChargeTimer, this.OverChargeShotSizeMultiplier);
            this.CanBreakBlocks = true;

            //set the CoolDownThreshold timer for a longer time
            this.ShotCooldownThreshold = this.OverchargeShotCooldownThreshold;

            //play the standard shot sfx
            this.PlaySFXOneShot("sfx_laserBlast");

            //set the amount of particles to fire
            this.GunShotParticles.GetComponent<ParticleSystem>().emissionRate = this.LargeShotParticleCount;

            //DEBUG
            //Debug.Log("SHOT = OVERCHARGE SIZE " + this.ShotSize + " / YES BREAK BLOCKS");
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: PlaySFX(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void PlaySFX(string sfxName_)
    {
        foreach (AudioClip sfx in this.GunSFXArray)
        {
            if (sfx != null && sfx.name == sfxName_)
            {
                //Debug.Log("SENDING " + sfx.name + " to SOUNDMANAGER");
                this.AudioController.GetComponent<SoundManager>().PlayPaitiently(sfx);
            }
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: PlaySFXOneShot(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void PlaySFXOneShot(string sfxName_)
    {
        foreach (AudioClip sfx in this.GunSFXArray)
        {
            if (sfx != null && sfx.name == sfxName_)
            {
                //Debug.Log("SENDING " + sfx.name + " to SOUNDMANAGER");
                this.AudioController.GetComponent<SoundManager>().PlaySingle(sfx);
            }
        }
    }

}
