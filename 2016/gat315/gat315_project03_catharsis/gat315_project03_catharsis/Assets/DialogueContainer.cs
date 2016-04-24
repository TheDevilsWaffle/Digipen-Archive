/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DialogueContainer.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    #region PROPERTIES

    public DialoguePanelController DPC;
    public MyPlatformerController s_MyPlatformerController;
    private Wander s_Wander;
    public Sprite PortraitToSend;

    public string[] BeginningSentences;

    public string[] ResponseSentences;
    private int ResponseNumber;
    public string PlayerRespondedWith;

    public string[] X_EndingSentences;
    public string[] B_EndingSentences;
    public bool HasBeenSpokenTo;

    [Header("NPC THINGS")]
    public bool thisIsLargeNPC = false;
    public bool thisIsSmallNPC = false;
    public bool thisIsLargeNPC_Capturing = false;
    public bool thisIsSmallNPC_Ending = false;
    public GameObject ExitNode;
    public Sprite Arrow;

    private float Timer = 0f;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get self references
        this.s_Wander = GetComponent<Wander>();
        this.s_MyPlatformerController = GameObject.Find("Player").GetComponent<MyPlatformerController>();
        //set bool/ints
        this.HasBeenSpokenTo = false;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SendDialogue()
    ////////////////////////////////////////////////////////////////////*/
    public void SendDialogue()
    {
        //if player has not yet talked to this person
        if (!this.HasBeenSpokenTo)
        {
            //print("hasn't been spoken to, sending info");
            this.DPC.StartDialogue(this.gameObject,
                                   this.BeginningSentences,
                                   this.ResponseSentences,
                                   this.X_EndingSentences,
                                   this.B_EndingSentences,
                                   this.PortraitToSend);
            //stop wandering
            if(this.s_Wander != null)
                s_Wander.StopWandering();

            //stop the player from moving
            this.s_MyPlatformerController.PlayerIsSpeaking = true;
        }
        else
        {
            //print("you've already talked to this NPC");
            this.s_MyPlatformerController.PlayerIsSpeaking = false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SetResponseNumber()
    ////////////////////////////////////////////////////////////////////*/
    public void SetResponseNumber(int responseNumber_)
    {
        this.ResponseNumber = responseNumber_;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SetResponseTo()
    ////////////////////////////////////////////////////////////////////*/
    public void SetResponseTo(string response_)
    {
        this.PlayerRespondedWith = response_;

        if (this.thisIsLargeNPC_Capturing)
            this.gameObject.GetComponent<LargeNPCScript>().LargeNPCAction();

        if (this.thisIsSmallNPC)
            this.gameObject.GetComponent<SmallNPCScript>().SmallNPCAction();
        if (this.thisIsSmallNPC_Ending)
            StartCoroutine(this.WalkAway());

        if (this.thisIsLargeNPC)
            this.OpenDoor();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: SetHasBeenSpokenToTrue()
    ////////////////////////////////////////////////////////////////////*/
    public void SetHasBeenSpokenToTrue()
    {
        //print(this.gameObject.name + " has been spoken to now");
        this.HasBeenSpokenTo = true;
    }

    public void SetHasBeenSpokenToFalse()
    {
        this.HasBeenSpokenTo = false;
    }

    void OpenDoor()
    {
        GameObject door = GameObject.FindWithTag("Door").gameObject;
        door.GetComponent<BoxCollider2D>().enabled = false;
        door.GetComponent<SpriteRenderer>().flipY = false;
        door.GetComponent<SpriteRenderer>().sprite = this.Arrow;
    }

    IEnumerator WalkAway()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        iTween.MoveTo(this.gameObject,
                          iTween.Hash("name", "animateLargeNPC",
                                      "position", this.ExitNode.transform.position,
                                      "time", 5f,
                                      "delay", 10f,
                                      "easetype", EaseType.easeInOutQuad.ToString(),
                                      "looptype", "none"));

        yield return new WaitForSeconds(16f);

        StartCoroutine(GameObject.FindWithTag("LevelSettings").gameObject.GetComponent<SceneManagementSystem>().FadeOutToNextScene());
    }

    #endregion
}