/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ShotProperties.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class ShotProperties : MonoBehaviour
{
    //PROPERTIES
    public Vector3 ShotSize;
    public bool CanBreakBlocks;
    public GameObject ShotBy;
    public Vector3 CurrentVelocity;
    public float Speed;
    [SerializeField]
    float SpeedModifier = 1.5f;
    [SerializeField]
    AudioClip shotDestroyed;
    GameObject AudioController;
    GameObject Enemy;
    Vector3 PointOfContact;
    GameObject NewOwner;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.gameObject.transform.localScale = this.ShotSize;
        //Debug.Log("SHOT FIRED, SIZE = " + this.gameObject.transform.localScale);
        this.CurrentVelocity = this.gameObject.GetComponent<Rigidbody>().velocity;
        //Debug.Log("CURRENT VELOCITY AT CREATION " + this.CurrentVelocity);
        this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;

        //find the AudioController
        this.AudioController = GameObject.FindWithTag("AudioController");

        this.Enemy = this.DetermineEnemy(this.ShotBy);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DetermineEnemy(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    GameObject DetermineEnemy(GameObject shotBy_)
    {
        if (shotBy_.name == "Player1")
        {
            this.NewOwner = GameObject.Find("Player2").gameObject;
            return GameObject.Find("Player2").gameObject;
        }
        else if(shotBy_.name == "Player2")
        {
            this.NewOwner = GameObject.Find("Player1").gameObject;
            return GameObject.Find("Player1").gameObject;
        }
        else
        {
            return null;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnCollisionEnter(Collider)
    /////////////////////////////////////////////////////////////////////////*/
    void OnCollisionEnter(Collision obj_)
    {
        //Debug.Log("COLLISION DETECTED WITH = " + obj_.gameObject);

        if (this.CanBreakBlocks)
        {
            if (obj_.gameObject.tag == "BreakableBlock")
            {
                obj_.gameObject.GetComponent<BreakableBlock>().ExplodeIntoDebris(obj_.contacts[0].point);
            }
        }

        if (obj_.gameObject.transform.root.gameObject == GameObject.Find("Player1").gameObject && obj_.gameObject.tag != "Shield")
        {
            //DEBUG
            //Debug.Log("SHOT HIT = " + obj_.gameObject.transform.root.name);

            GameObject playerHit = GameObject.Find("Player1").gameObject;
            if (playerHit.GetComponent<PlayerInputController>().IsStunned == false)
            {
                playerHit.GetComponent<PlayerInputController>().IsStunned = true;

                playerHit.transform.FindChild("StunParticles").GetComponent<ParticleController>().Play();
                playerHit.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().SetPlayerToStunned();
            }
            else
            {
                //DEBUG
                //Debug.Log("PLAYER IS ALREADY STUNNED");
            }
        }

        if (obj_.gameObject.transform.root.gameObject == GameObject.Find("Player2").gameObject && obj_.gameObject.tag != "Shield")
        {
            //DEBUG
            //Debug.Log("SHOT HIT = " + obj_.gameObject.transform.root.name);

            GameObject playerHit = GameObject.Find("Player2").gameObject;
            if (playerHit.GetComponent<PlayerInputController>().IsStunned == false)
            {
                playerHit.GetComponent<PlayerInputController>().IsStunned = true;

                playerHit.transform.FindChild("StunParticles").GetComponent<ParticleController>().Play();
                playerHit.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().SetPlayerToStunned();
            }
            else
            {
                //DEBUG
                //Debug.Log("PLAYER IS ALREADY STUNNED");
            }
        }

        if (obj_.gameObject.tag == "Shield")
        {
            //print("OBJECT HIT IS = " + obj_.gameObject);
            //print(obj_.gameObject + " POS = " + obj_.gameObject.transform.position);
            //print("I WAS SHOT BY = " + this.ShotBy + " at POS = " + this.ShotBy.transform.position);
            Vector3 targetVector = (this.ShotBy.transform.FindChild("Art").transform.position - this.gameObject.transform.position).normalized;
            this.gameObject.GetComponent<Rigidbody>().velocity = targetVector * (this.Speed * this.SpeedModifier);
            //print("MY NEW VELOCITY IS = " + this.gameObject.GetComponent<Rigidbody>().velocity);

            //save point of contact
            this.PointOfContact = obj_.contacts[0].point;
        }
        else
        {
            //shot destroyed particles
            this.CreateAndPlayParticles("ProjectileDestroyedParticles", obj_.contacts[0].point, this.ShotBy);
            //this.AudioController.GetComponent<SoundManager>().PlaySingle(this.shotDestroyed);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionExit(Collision obj_)
    {
        if (obj_.gameObject.tag == "Shield")
        {
            this.CreateAndPlayParticles("ShieldReflectParticles", this.PointOfContact, this.ShotBy);
        }
    }


    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CreateAndPlayParticles(string Vector3, GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void CreateAndPlayParticles(string particles_, Vector3 pos_, GameObject owner_)
    {
        GameObject reflectParticles = GameObject.Instantiate((GameObject)Resources.Load(particles_, typeof(GameObject)),
                                                             pos_,
                                                             Quaternion.identity) as GameObject;
        if(particles_ == "ShieldReflectParticles")
        {
            this.Enemy = this.NewOwner;
            //this.ShotBy = this.NewOwner;
        }
        print(this.ShotBy + " = NEW REFLECTED OWNER");
        this.gameObject.GetComponent<Renderer>().material = this.ShotBy.GetComponent<PlayerShotMaterial>().ShotColor;
        if (this.ShotBy.name == "Player1")
        {
            reflectParticles.GetComponent<ParticleSystem>().startColor = new Color(0f, 110f / 255f, 210f / 255f, 1f);
        }
        if (this.ShotBy.name == "Player2")
        {
            reflectParticles.GetComponent<ParticleSystem>().startColor = new Color(1f, 150f / 255f, 0f, 1f);
        }
    }
}