/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PickUp.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    //[RequireComponent (typeof (RespectPlanetoid))]

    public enum PickUpTypeEnum { AMMO, LOOT, HEALTH, VACUUM };
    [Header("PickUp Type")]
    [SerializeField]
    PickUpTypeEnum PickUpType;
    [SerializeField]
    bool IsDestroyable;
    [SerializeField]
    bool IsSuckable;
    [SerializeField]
    int Worth = 100;

    [Header("Planetoid Properties")]
    [SerializeField]
    GameObject Planetoid;

    [Header("Floating Properties")]
    [SerializeField]
    float FloatHeight = 0.75f;
    [SerializeField]
    float FloatTime = 0.75f;
    [HideInInspector]
    Vector3 OriginalPos;
    [HideInInspector]
    Vector3 CurrentPos;
    [HideInInspector]
    bool IsFloatingUp;
    Vector3 DirectionToPlanetoid;
    [HideInInspector]
    Vector3 MaxDistancePos;
    [HideInInspector]
    float MaxDistance;
    [HideInInspector]
    Vector3 MinDistancePos;
    [HideInInspector]
    float MinDistance;

    [Header("Duration Properties")]
    [SerializeField]
    float Lifetime = 100f;
    [SerializeField]
    float CriticalThreshold = 0;
    [SerializeField]
    Material OriginalMaterial;
    [SerializeField]
    Color CriticalColor;
    [HideInInspector]
    Color OriginalColor;
    [SerializeField]
    float BlinkTime = 0.5f;
    [SerializeField]
    int RotationSpeed = 180;
    [HideInInspector]
    float Timer;
    [HideInInspector]
    bool IsCritical;

    private AudioClip[] clips;
    private AudioSource audio; 

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //set original position
        this.OriginalPos = this.gameObject.transform.position;
        //get Planetoid
        this.Planetoid = this.gameObject.GetComponent<RespectPlanetoid>().Planetoid;
        //establish the min and max distance of this object
        this.SetMinMaxDistance(this.gameObject.GetComponent<RespectPlanetoid>().DistanceFromPlanetoidCenter);
        //start the pickup floating up and down
        this.FloatUp();
        //initialize timer
        this.Timer = 0f;
        //set the material
        this.gameObject.GetComponent<Renderer>().material = this.OriginalMaterial;
        //get the original color of the object
        this.OriginalColor = this.gameObject.GetComponent<Renderer>().material.color;

        clips = new AudioClip[]{(AudioClip)Resources.Load("Sounds/sfx/pickups/loot",typeof(AudioClip)),
                                  (AudioClip)Resources.Load("Sounds/sfx/pickups/ammo",typeof(AudioClip)),
                                  (AudioClip)Resources.Load("Sounds/sfx/pickups/health",typeof(AudioClip))};
        audio = gameObject.AddComponent<AudioSource>();
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.Timer < this.Lifetime)
        {
            //set current position
            this.CurrentPos = this.gameObject.transform.position;
            //rotate the pickup
            this.RotatePickUp(this.RotationSpeed, Time.deltaTime);
            //float the pickup up and down

            if (this.Timer >= this.CriticalThreshold && !this.IsCritical)
            {
                //set IsCritical to true so we don't keep doing this,
                this.IsCritical = true;
                //start blinking this object
                this.BlinkCritical();
            }

            //update the timer
            this.Timer += Time.deltaTime;
            //Debug.Log(this.Timer);
        }

        if (this.Timer >= this.Lifetime)
        {
            this.DestroyOwner();
        }


        //Debug.Log("CURRENT POS = " + this.CurrentPos);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyOwner()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void OnTriggerEnter(Collider obj_)
    {
        if (this.IsDestroyable && obj_.gameObject.name == "Bullet(Clone)")
        {
            this.DestroyOwner();
        }
        if (obj_.gameObject.name == "Player")
        {
            if(obj_.gameObject.GetComponent<Player1Data>() != null)
            {
                switch(this.PickUpType)
                {
                    case (PickUpTypeEnum.LOOT):
                        Player1Data.Player1DataObject.UpdateLoot(this.Worth);
                        //update hud
                        GameObject.FindWithTag("HUD").GetComponent<HUD>().UpdateHUDElementFromData("loot", Player1Data.Player1DataObject.Loot);
                        GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("loot", Player1Data.Player1DataObject.Loot);
                        audio.clip = clips[0];
                        AudioSource.PlayClipAtPoint(audio.clip, transform.position, .5f);
                        break;

                    case (PickUpTypeEnum.AMMO):
                        Player1Data.Player1DataObject.UpdateAmmo(this.Worth);
                        //update the hud
                        GameObject.FindWithTag("HUD").GetComponent<HUD>().UpdateHUDElementFromData("ammo", Player1Data.Player1DataObject.Ammo);
                        GameObject.FindWithTag("HUD").GetComponent<HUD>().HUDEffects("ammo", Player1Data.Player1DataObject.Ammo);
                        audio.clip = clips[1];
                        AudioSource.PlayClipAtPoint(audio.clip, transform.position, .5f);
                        break;
                    //case (PickUpTypeEnum.HEALTH):
                    //    Player1Data.Player1DataObject.UpdateAmmo(this.Worth);
                    //    //update the hud
                    //    GameObject.FindWithTag("HUD").GetComponent<HUD>().UpdateHUDElementFromData("health", Player1Data.Player1DataObject);
                    //    break;
                    //case (PickUpTypeEnum.VACUUM):
                    //    Player1Data.Player1DataObject.UpdateAmmo(this.Worth);
                    //    //update the hud
                    //    GameObject.FindWithTag("HUD").GetComponent<HUD>().UpdateHUDElementFromData("ammo", Player1Data.Player1DataObject.Ammo);
                    //    break;

                    default:
                        Debug.LogError("ERROR! Set the PickUpTypeEnum on this pickup!");
                        break;
                }
            }
            this.DestroyOwner();
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyOwner()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void DestroyOwner()
    {
        //kill this game object
        Destroy(this.gameObject);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: RotatePickUp()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void RotatePickUp(float speed_, float time_)
    {
        this.gameObject.transform.Rotate(Vector3.up, (speed_ * time_), Space.Self);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FloatUp()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void FloatUp()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", this.MaxDistancePos,
                                                   "time", this.FloatTime,
                                                   "easetype", "easeInOutQuad",
                                                   "oncomplete", "FloatDown"));
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FloatDown()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void FloatDown()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", this.MinDistancePos,
                                                   "time", this.FloatTime,
                                                   "easetype", "easeInOutQuad",
                                                   "oncomplete", "FloatUp",
                                                   "oncompleteparams", "time_"));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: BlinkCritical(color)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void BlinkCritical()
    {
        iTween.ColorTo(this.gameObject, iTween.Hash("color", this.CriticalColor,
                                                        "includechildren", true,
                                                        "time", this.BlinkTime,
                                                        "oncomplete", "BlinkOriginal"));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: BlinkOriginal(color)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void BlinkOriginal()
    {
        iTween.ColorTo(this.gameObject, iTween.Hash("color", this.OriginalColor,
                                                        "includechildren", true,
                                                        "time", this.BlinkTime,
                                                        "oncomplete", "BlinkCritical"));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetMinMaxDistance()
    //NOTE: requires a property from RespectPlanetoid.cs
    /////////////////////////////////////////////////////////////////////////*/
    void SetMinMaxDistance(float originalDistance_)
    {
        //get direction from original placement of pickup to the planetoid
        this.DirectionToPlanetoid = (this.Planetoid.transform.position - this.OriginalPos).normalized;

        //get the max position
        this.MaxDistancePos = this.OriginalPos +- (this.DirectionToPlanetoid * this.FloatHeight);
        //get the distance from original pos to new max pos
        this.MaxDistance = Vector3.Distance(this.OriginalPos, this.MaxDistancePos);

        //get the min position
        this.MinDistancePos = this.OriginalPos + (this.DirectionToPlanetoid * this.FloatHeight);
        //get the distance from the original pos to new min pos
        this.MinDistance = Vector3.Distance(this.OriginalPos, this.MinDistancePos);

        //DEBUG
        //Debug.Log("ORIGINAL POS = " + this.OriginalPos + " MAX POS = " + this.MaxDistancePos + " MIN POS = " + this.MinDistancePos);
        //Debug.Log("MAXDISTANCE = " + this.MaxDistance + " MINDISTANCE = " + this.MinDistance);
    }
}
