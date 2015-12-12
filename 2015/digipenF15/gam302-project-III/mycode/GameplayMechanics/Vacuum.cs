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
    [Header("Functionality")]
    [HideInInspector]
    bool IsSuctioning = false;
    [SerializeField]
    GameObject VacuumCollider;
    [HideInInspector]
    GameObject VacuumObject;
    [HideInInspector]
    GameObject CollectionCollider;
    
    [Header("Suction")]
    [SerializeField]
    float SuctionThreshold = 5f;
    [SerializeField]
    float BaseSuctionForce = 0.1f;
    [SerializeField]
    float IncrementalSuctionForce = 0.1f;
    [HideInInspector]
    float SuctionTimer = 0f;

    [Header("Cooldown")]
    [SerializeField]
    public float RechargeThreshold = 2f;
    [HideInInspector]
    float RechargeTimer = 0f;

    //OTHER PROPERTIES
    [HideInInspector]
    GameObject[] SuctionTargets;
    [HideInInspector]
    Vector3 PlayerPos;
    [HideInInspector]
    GameObject Gun;
    GameObject HUDObj;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start ()
    {
        //get and set the gun and collection collider
        this.Gun = GameObject.Find("GunObject");
        this.CollectionCollider = GameObject.Find("CollectionCollider");
        //initialize timers
        this.RechargeTimer = 0f;
        this.SuctionTimer  = 0f;

        this.HUDObj = GameObject.FindWithTag("HUD");
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update ()
    {
        //if we're not already suctioning
        if (!this.IsSuctioning)
        {
            //and we're pressin the suction button
            if (Input.GetAxis("Vacuum") > 0 && this.CanPlayerUseSuction(this.RechargeTimer))
            {
                //set IsSuctioning to true so we only do this once
                this.IsSuctioning = true;

                //create the VacuumObject
                this.CreateVacuumObject();
            }
            //we're not suctioning, update RechargeTimer
            if(this.RechargeTimer <= this.RechargeThreshold)
            {
                this.RechargeTimer += Time.deltaTime;

                
                //call HUD.cs to update the vacuum slider
                GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("vacuumRecharge", this.RechargeTimer);
            }
            //reset SuctionTimer
            this.SuctionTimer = 0f;
        }
        //we are suctioning
        else
        {
            //we're holding down the suction button
            if (Input.GetAxis("Vacuum") > 0)
            {
                //reset RechargeTimer
                this.RechargeTimer = 0f;

                //call HUD.cs to update the vacuum slider
                GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("vacuum", this.SuctionTimer);

                //if we've surpassed our suction limit
                if (SuctionTimer >= this.SuctionThreshold)
                {
                    this.IsSuctioning = false;
                    this.SuctionTimer = 0f;
                    this.DestroyObject(this.VacuumObject);
                }
            }
            //we've stopped pressing the suction button
            else
            {
                this.IsSuctioning = false;
                this.SuctionTimer = 0f;
                this.DestroyObject(this.VacuumObject);
            }

            //update SuctionTimer
            this.SuctionTimer += Time.deltaTime;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CanPlayerUseSuction(float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    bool CanPlayerUseSuction(float timer_)
    {
        if(timer_ >= this.RechargeThreshold)
        {
            //reset the RechargeTimer
            this.RechargeTimer = 0f;

            return true;
        }
        else
        {
            return false;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CreateVacuumObject()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void CreateVacuumObject()
    {
        if (this.VacuumObject == null)
            return;
        //Debug.Log("CREATING VACUUM COLLIDER");
        this.VacuumObject = (GameObject)Instantiate(this.VacuumCollider, this.gameObject.transform.position + this.transform.TransformDirection(new Vector3(0,0,4f)), this.Gun.transform.rotation );
        this.VacuumObject.transform.LookAt(this.VacuumObject.transform.position + this.Gun.transform.up, this.Gun.transform.forward);
        this.VacuumObject.transform.parent = this.Gun.transform;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FindDirectionToPlayer()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    Vector3 FindDirectionToPlayer(Vector3 playerPos_, Vector3 targetPos_)
    {
        //variable to store the found direction needed
        Vector3 suctionDirection = playerPos_ - targetPos_;

        return suctionDirection;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyInitialForceToSuctionTarget(GameObject, Vector3)
    //NOTE: calls FindDirectionToPlayer()
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyInitialForceToSuctionTarget(GameObject obj_, Vector3 objDir_)
    {
        var suctionDirection = this.FindDirectionToPlayer(this.Gun.transform.position, objDir_);

        //apply force on obect torwards player
        obj_.GetComponent<Rigidbody>().AddForce(suctionDirection * (this.BaseSuctionForce * 1/Time.smoothDeltaTime), ForceMode.Impulse);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyIncrementalForceToSuctionTarget(GameObject, Vector3)
    //NOTE: calls FindDirectionToPlayer()
    /////////////////////////////////////////////////////////////////////////*/
    public void ApplyIncrementalForceToSuctionTarget(GameObject obj_, Vector3 objDir_)
    {
        var suctionDirection = this.FindDirectionToPlayer(this.Gun.transform.position, objDir_);

        //apply force on obect torwards player
        obj_.GetComponent<Rigidbody>().AddForce(suctionDirection * (this.IncrementalSuctionForce * 1/Time.smoothDeltaTime), ForceMode.Impulse);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyObject(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void DestroyObject(GameObject obj_)
    {
        if(obj_ != null)
        {
            Destroy(obj_);
        }
    }
}
