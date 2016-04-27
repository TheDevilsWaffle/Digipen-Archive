/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ShieldController.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
    //PROPERTIES
    [Header("Shield")]
    [RangeAttribute(0, 3)]
    [SerializeField]
    int ShieldButton = 1;
    [SerializeField]
    float ShieldAnimationTime = 3f;
    [SerializeField]
    Vector3 ShieldStartSize = new Vector3(0.25f, 0.25f, 0.25f);
    [SerializeField]
    Vector3 ShieldSizeMultiplier = new Vector3(1.25f, 1.25f, 1.25f);
    [SerializeField]
    string EaseType = "easeInOutQuad";

    [Header("Cooldown")]
    [SerializeField]
    public float ShieldCooldownThreshold = 3f;

    //SFX
    [Header("SFX")]
    public AudioClip[] SFXArray;
    GameObject AudioController;

    //HIDDEN PROPERTIES
    [HideInInspector]
    public GameObject ShotBy = null;
    [HideInInspector]
    bool IsCoolingDown = false;
    [HideInInspector]
    float CooldownTimer = 3f;
    [HideInInspector]
    SphereCollider ShieldCollider;
    [HideInInspector]
    public bool UseShield = false;

    //PARTICLES
    GameObject ShieldChargeParticles;
    

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get and assign ShieldCollider
        this.ShieldCollider = this.gameObject.GetComponent<SphereCollider>();
        //set the starting scale size of the Shield;
        this.gameObject.transform.localScale = this.ShieldStartSize;
        this.UseShield = false;
        this.IsCoolingDown = false;
        //get the the AudioController
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;

        //get particle systems needed
        this.ShieldChargeParticles = this.gameObject.transform.FindChild("ShieldChargeParticles").gameObject;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if player pressed FireButton
        if (Input.GetMouseButtonDown(this.ShieldButton) || this.UseShield)
        {
            //check to see if the ShotDelay threshold has been met
            if (this.CanPlayerUseShield(this.CooldownTimer, this.ShieldCooldownThreshold))
            {
                this.RaiseShield();
                this.IsCoolingDown = true;
            }
        }
        //increment timer based on Dt
        this.CooldownTimer += Time.deltaTime;

        //set UseShield back to false
        this.UseShield = false;
        if(this.CooldownTimer >= this.ShieldCooldownThreshold && this.IsCoolingDown)
        {
            this.PlaySFX("sfx_shieldActivate");
            this.IsCoolingDown = false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CanPlayerUseShield()
    /////////////////////////////////////////////////////////////////////////*/
    bool CanPlayerUseShield(float timer, float delay)
    {
        if (timer >= delay)
        {
            //DEBUG
            //Debug.Log(this.gameObject.transform.parent + " can use shield!");

            //reset ShotTimer
            this.CooldownTimer = 0f;
            return true;
        }
        else
        {
            //DEBUG
            //Debug.Log(this.gameObject.transform.parent + "cannot use shield yet (ShieldCooldownThreshold not yet met)");

            //play inactive sound
            this.PlaySFX("sfx_shieldInactive");

            //play inactive particles
            this.ShieldChargeParticles.GetComponent<ParticleSystem>().Play();

            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: RaiseShield()
    /////////////////////////////////////////////////////////////////////////*/
    void RaiseShield()
    {
        //DEBUG
        //Debug.Log("RAISING SHIELD");

        //scale the collider slightly bigger
        this.ShieldCollider.radius = 0.6f;

        //animate the the shield scale up
        iTween.ScaleTo(this.gameObject, iTween.Hash("name", "ShieldAnimation",
                                                    "scale", this.ShieldSizeMultiplier,
                                                    "time", this.ShieldAnimationTime,
                                                    "easetype", this.EaseType,
                                                    "onstarttarget", this.gameObject,
                                                    "onstartparams", "sfx_shieldUp",
                                                    "onstart", "PlaySFX"));

        //animate the shield scale down
        iTween.ScaleTo(this.gameObject, iTween.Hash("name", "ShieldRetractAnimation",
                                                    "delay", (this.ShieldAnimationTime * 6f),
                                                    "scale", this.ShieldStartSize,
                                                    "time", this.ShieldAnimationTime,
                                                    "easetype", "easeOutQuad",
                                                    "onstarttarget", this.gameObject,
                                                    "onstartparams", "sfx_shieldDown",
                                                    "onstart", "PlaySFX",
                                                    "oncompletetarget", this.gameObject,
                                                    "oncomplete", "ShieldLowered"));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ShieldLowered()
    /////////////////////////////////////////////////////////////////////////*/
    void ShieldLowered()
    {
        this.ShieldCollider.radius = 0.5f;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: PlaySFX(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void PlaySFX(string sfxName_)
    {
        foreach(AudioClip sfx in this.SFXArray)
        {
            if(sfx != null && sfx.name == sfxName_)
            {
                //Debug.Log("SENDING " + sfx.name + " to SOUNDMANAGER");
                this.AudioController.GetComponent<SoundManager>().PlayPaitiently(sfx);
            }
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnCollisionExit(Collider)
    /////////////////////////////////////////////////////////////////////////*/
    void OnCollisionExit(Collision obj_)
    {
        if(obj_.gameObject.name == "ShotBolt(Clone)")
        {
            this.PlaySFX("sfx_shieldReflect");
        }
    }
}
