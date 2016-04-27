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

    [Header("Cooldown")]
    [SerializeField]
    float ShieldCooldownThreshold = 3f;


    //HIDDEN PROPERTIES
    [HideInInspector]
    public GameObject ShotBy = null;
    [HideInInspector]
    bool IsCoolingDown = false;
    [HideInInspector]
    float CooldownTimer = 0f;
    [HideInInspector]
    SphereCollider ShieldCollider;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get and assign ShieldCollider
        this.ShieldCollider = this.gameObject.GetComponent<SphereCollider>();
        //set the starting scale size of the Shield;
        this.gameObject.transform.localScale = this.ShieldStartSize;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if player pressed FireButton
        if (Input.GetMouseButtonDown(this.ShieldButton))
        {
            //check to see if the ShotDelay threshold has been met
            if (this.CanPlayerUseShield(this.CooldownTimer, this.ShieldCooldownThreshold))
            {
                this.RaiseShield();
            }
        }
        //increment timer based on Dt
        this.CooldownTimer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CanPlayerUseShield()
    /////////////////////////////////////////////////////////////////////////*/
    bool CanPlayerUseShield(float timer, float delay)
    {
        if (timer >= delay)
        {
            //DEBUG
            Debug.Log(this.gameObject.transform.parent + " can use shield!");

            //reset ShotTimer
            this.CooldownTimer = 0f;
            return true;
        }
        else
        {
            Debug.Log(this.gameObject.transform.parent + "cannot use shield yet (ShieldCooldownThreshold not yet met)");
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

        iTween.ScaleFrom(this.gameObject, iTween.Hash("name", "ShieldAnimation",
                                                      "scale", this.ShieldSizeMultiplier,
                                                      "time", this.ShieldAnimationTime,
                                                      "easetype", "easeInElastic"));
    }
}
