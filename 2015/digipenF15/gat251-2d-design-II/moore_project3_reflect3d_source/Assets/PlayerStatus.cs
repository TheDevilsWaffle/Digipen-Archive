/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PlayerStatus.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    public enum Status { ACTIVE, STUNNED };
    public Status CurrentStatus = Status.ACTIVE;
    [SerializeField]
    float StunThreshold = 3f;
    float StunTimer = 0f;
    [SerializeField]
    GameObject EnemyPlayer;
    [SerializeField]
    public int Lives = 3;
    GameObject LevelSettings;
    GameObject AudioController;
    [SerializeField]
    AudioClip StunnedSFX;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //start the player off as Status = ACTIVE
        this.CurrentStatus = Status.ACTIVE;

        //ensure that the StunTimer is set to 0
        this.StunTimer = 0f;

        //null check and assignment
        if (EnemyPlayer == null)
        {
            if (this.gameObject.transform.root.name == "Player1")
            {
                this.EnemyPlayer = GameObject.Find("Player2").gameObject;
            }
            else
            {
                this.EnemyPlayer = GameObject.Find("Player1").gameObject;
            }
        }

        //assign variables
        this.LevelSettings = GameObject.FindWithTag("LevelSettings").gameObject;
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetPlayerToStunned()
    /////////////////////////////////////////////////////////////////////////*/
    public void SetPlayerToStunned()
    {
        this.CurrentStatus = Status.STUNNED;

        //play the stun sfx
        this.AudioController.GetComponent<SoundManager>().PlaySingle(this.StunnedSFX);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter(Collider)
    /////////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider obj_)
    {

        if (this.CurrentStatus == Status.STUNNED)
        {
            print(obj_.gameObject.transform.root.gameObject);
            if (obj_.gameObject.transform.root.gameObject == this.EnemyPlayer)
            {
                
                //Debug.Log(obj_.gameObject + " has run into " + this.gameObject.transform.root.gameObject);
                this.LevelSettings.GetComponent<RespawnPlayer>().PlayerDied(this.gameObject.transform.root.gameObject);
            }
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (this.CurrentStatus == Status.STUNNED)
        {
            this.StunTimer += Time.deltaTime;

            //DEBUG
            //Debug.Log(this.gameObject.name + " is " + this.CurrentStatus);

            if (this.StunTimer >= this.StunThreshold)
            {
                this.CurrentStatus = Status.ACTIVE;
            }
        }

        else
        {
            this.StunTimer = 0f;
        }
    }

}