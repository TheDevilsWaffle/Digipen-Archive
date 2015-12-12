/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SuctionTarget.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class SuctionTarget : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    public bool IsTargetSuckable = true;
    [HideInInspector]
    public bool IsBeingSuctioned = false;
    [HideInInspector]
    GameObject Gun;
    [SerializeField]
    float AdditionalMass;
    [SerializeField]
    int AmmoWorth = 1;
    [SerializeField]
    int LootWorth = 10;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.Gun = GameObject.Find("Gun");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //if this object is actively being suctioned, call Vacuum.cs's ApplyIncrementalForceToSuctionTarget()
        if (this.IsBeingSuctioned == true)
        {
            this.Gun.GetComponent<Vacuum>().ApplyIncrementalForceToSuctionTarget(this.gameObject, this.gameObject.transform.position);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider obj_)
    {
        //if we've hit the player's colletion collider
        if(obj_.gameObject == GameObject.Find("CollectionCollider"))
        {
            Debug.Log("HIT THE PLAYER, COLLECTED");

            this.AwardPlayer(obj_.gameObject);

            //destroy this object
            this.DestroyObject(this.gameObject);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AwardPlayer(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void AwardPlayer(GameObject player_)
    {
        //update the loot and ammo of the this player
        Player1Data.Player1DataObject.Loot += this.LootWorth;
        Player1Data.Player1DataObject.Ammo += this.AmmoWorth;

        print("PLAYER HAS " + Player1Data.Player1DataObject.Loot + " LOOT and " + Player1Data.Player1DataObject.Ammo + " AMMO");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyObject(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void DestroyObject(GameObject obj_)
    {
        if (obj_ != null)
        {
            Destroy(obj_);
        }
    }
}
