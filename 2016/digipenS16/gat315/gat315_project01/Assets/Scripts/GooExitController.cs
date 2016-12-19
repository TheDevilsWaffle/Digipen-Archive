/*////////////////////////////////////////////////////////////////////////
//SCRIPT: GooExitController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

enum ExitType { LARGE, SMALL };

public class GooExitController : MonoBehaviour
{
    [SerializeField]
    ExitType ExitSize = ExitType.LARGE;
    [SerializeField]
    Color WrongSizeColor;
    [SerializeField]
    Color RightSizeColor;
    [SerializeField]
    Color InActiveColor;
    [SerializeField]
    float AnimationTime = 0.5f;
    [SerializeField]
    GameObject OutsideRing;
    [SerializeField]
    GameObject ExitForPlayer;

    LevelController LevelSettings;

    public bool IsReadyForWin;
    public GameObject OtherRingTrigger;
    bool IsStartOfLevel;
    float Timer = 0f;
    float TimerThreshold = 1.0f;

    void Awake()
    {
        this.LevelSettings = GameObject.FindWithTag("LevelSettings").GetComponent<LevelController>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize bool
        this.IsStartOfLevel = true;
        this.IsReadyForWin = false;
        //start off with the exits lit up and then go to inactive color
        this.AnimateExit(this.RightSizeColor);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update timer
        if (this.Timer < this.TimerThreshold)
        {
            this.Timer += Time.deltaTime;
        }
        //turn bools to false after set time
        if (this.IsStartOfLevel && this.Timer > this.TimerThreshold)
        {
            this.IsStartOfLevel = false;
            this.AnimateExitReset();
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter(Collider objCollider_)
    {
        GameObject obj;
        //make sure the object touching this thing 
        if (objCollider_.gameObject.transform.parent.gameObject != null)
        {
            obj = objCollider_.gameObject.transform.parent.gameObject;
            //if this is large exit and this is the large player
            if (obj == this.ExitForPlayer &&
                obj.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.LARGE
                && this.ExitSize == ExitType.LARGE)
            {
                //exit ready feedback
                this.LevelExitReady(obj, this.gameObject);
            }

            //if this is the small exit and this is the small player
            else if (obj == this.ExitForPlayer &&
                     obj.GetComponent<Mass>().PlayerCurrentSize == PlayerSize.SMALL
                     && this.ExitSize == ExitType.SMALL)
            {
                //exit ready feedback
                this.LevelExitReady(obj, this.gameObject);
            }

            //mismatch, not the right player size for this exit
            else
            {
                this.AnimateExit(this.WrongSizeColor);
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerExit(Collider)
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerExit(Collider objCollider_)
    {
        GameObject obj = objCollider_.gameObject.transform.parent.gameObject;
        //make sure it's one of the players leaving the exit
        if (obj != null)
        {
            this.IsReadyForWin = false;

            //make sure we stop flashing
            //iTween.StopByName("AnimateExit");

            //reset the color
            this.AnimateExitReset();
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: LevelExitReady(GameObject, GameObject)
    ////////////////////////////////////////////////////////////////////*/
    void LevelExitReady(GameObject player_, GameObject exitObj_)
    {
        this.IsReadyForWin = true;
        //animate the exit
        this.AnimateExit(this.RightSizeColor);
        //play the sound
        //CODE NEEDED HERE

        //check to make sure both players have reached their exit
        this.CheckWinCondition();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateExit(Color)
    ////////////////////////////////////////////////////////////////////*/
    void AnimateExit(Color color_)
    {
        //animate the exit
        iTween.ColorTo(this.OutsideRing,
                            iTween.Hash("name", "AnimateExit",
                                        "delay", 0.25f,
                                        "color", color_,
                                        "time", this.AnimationTime,
                                        "includechildren", false));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateExitReset()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateExitReset()
    {
        //animate the exit
        iTween.ColorTo(this.OutsideRing,
                            iTween.Hash("name", "AnimateExitReset",
                                        "delay", 0.25f,
                                        "color", this.InActiveColor,
                                        "includechildren", false,
                                        "time", this.AnimationTime));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CheckWinCondition()
    ////////////////////////////////////////////////////////////////////*/
    void CheckWinCondition()
    {
        //get the other exit's bool and check against ours
        if(this.IsReadyForWin && this.OtherRingTrigger.GetComponent<GooExitController>().IsReadyForWin)
        {
            this.LevelSettings.LevelComplete();
        }
    }
}
