/*/////////////////////////////////////////////////////////////////////////////
//SCRIPT - StartGameCountdown.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGameCountdown : MonoBehaviour
{
    //PROPERTIES
    GameObject MessageCanvas;
    GameObject Countdown;
    Vector3 CountdownPos;
    Vector3 CountdownScale;
    [SerializeField]
    Vector3 Offset = new Vector3(0f, -100f, 0f);
    [SerializeField]
    Vector3 ScaleDownSize = new Vector3(0.05f, 0.05f, 1f);
    [SerializeField]
    Color CountdownColor = new Color(1f, 1f, 1f, 1.0f);
    float DelayTime = 1f;
    float ScaleTime = 0.25f;
    string EaseType = "easeOutQuad";

    GameObject Player1;
    GameObject Player2;

    GameObject AudioController;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //find the things we need and assign values
        this.MessageCanvas = GameObject.Find("MessageCanvas");
        this.Countdown = GameObject.Find("Countdown");
        this.CountdownPos = this.Countdown.transform.position;
        this.CountdownScale = this.Countdown.transform.localScale;
        this.Countdown.GetComponent<Text>().color = this.CountdownColor;

        //start the countdown at 3
        this.AnimateCountdown("3");

        //get the audio controller
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;

        //say "3"
        this.AudioController.GetComponent<SoundManager>().PlaySingle((AudioClip)Resources.Load("sfx_3", typeof(AudioClip)));

        //get the players and disable them
        this.Player1 = GameObject.Find("Player1").gameObject;
        this.Player2 = GameObject.Find("Player2").gameObject;

        this.TogglePlayerControl(this.Player1);
        this.TogglePlayerControl(this.Player2);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateCountdown(string)
    /////////////////////////////////////////////////////////////////////////*/
    void TogglePlayerControl(GameObject player_)
    {
        if (player_.GetComponent<PlayerInputController>().enabled)
        {
            player_.transform.FindChild("Art").transform.FindChild("Gun").GetComponent<FireGun>().enabled = false;
            player_.GetComponent<PlayerInputController>().enabled = false;
        }
        else
        {
            player_.transform.FindChild("Art").transform.FindChild("Gun").GetComponent<FireGun>().enabled = true;
            player_.GetComponent<PlayerInputController>().enabled = true;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateCountdown(string)
    /////////////////////////////////////////////////////////////////////////*/
        void AnimateCountdown(string textNumber_)
    {
        //assign new text
        this.Countdown.GetComponent<Text>().text = textNumber_;

        //place this in the right spot, right scale, right color
        this.Countdown.transform.position = this.CountdownPos;
        this.Countdown.transform.localScale = this.CountdownScale;
        this.Countdown.GetComponent<Text>().color = this.CountdownColor;

        //animate this
        this.AnimateScale(this.Countdown, this.ScaleDownSize, this.ScaleTime, this.EaseType);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuScale(GameObject, Vector3, string)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateScale(GameObject obj_, Vector3 scale_, float time_, string easeType_)
    {
        //offset needed to compensate for new scale size
        iTween.MoveTo(obj_, iTween.Hash("name", "AnimateCountdownMoveTo",
                                        "delay", this.DelayTime,
                                        "time", time_,
                                        "easetype", easeType_,
                                        "position", this.CountdownPos + this.Offset));
        //scale down for epicness!
        iTween.ScaleTo(obj_, iTween.Hash("name", "AnimateCountdownScaleTo",
                                         "delay", this.DelayTime,
                                         "time", time_,
                                         "easetype", easeType_,
                                         "scale", scale_,
                                         "oncompletetarget", this.gameObject,
                                         "oncomplete", "EvaluateCountdown"));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateCountdown()
    /////////////////////////////////////////////////////////////////////////*/
    void EvaluateCountdown()
    {
        switch(this.Countdown.GetComponent<Text>().text)
        {
            case "3":
                //say "2"
                this.AudioController.GetComponent<SoundManager>().PlaySingle((AudioClip)Resources.Load("sfx_2", typeof(AudioClip)));
                this.AnimateCountdown("2");
                break;
            case "2":
                //say "1"
                this.AudioController.GetComponent<SoundManager>().PlaySingle((AudioClip)Resources.Load("sfx_1", typeof(AudioClip)));
                this.AnimateCountdown("1");
                break;
            case "1":
                //say "GO"
                this.AudioController.GetComponent<SoundManager>().PlaySingle((AudioClip)Resources.Load("sfx_go", typeof(AudioClip)));
                this.AnimateCountdown("GO!");
                break;
            case "GO!":
                this.TogglePlayerControl(this.Player1);
                this.TogglePlayerControl(this.Player2);
                //start the game timer
                this.gameObject.GetComponent<LevelTimer>().StartLevelTimer();
                Destroy(this.Countdown);
                break;
            default:
                break;
        }
    }
}
