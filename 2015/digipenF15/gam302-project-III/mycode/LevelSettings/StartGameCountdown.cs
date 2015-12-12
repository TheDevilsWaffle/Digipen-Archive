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
    GameObject[] ObjectsToHide;
    [SerializeField]
    Vector3 Offset = new Vector3(0f, -100f, 0f);
    [SerializeField]
    Vector3 ScaleDownSize = new Vector3(0.05f, 0.05f, 1f);
    [SerializeField]
    Color CountdownColor = new Color(1f, 1f, 1f, 1.0f);
    float DelayTime = 1f;
    float ScaleTime = 0.25f;
    string EaseType = "easeOutQuad";

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

        GameObject.Find("Player").GetComponent<PlayerController>().CanMove = false;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateCountdown(string)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateCountdown(string textNumber_)
    {
        foreach(GameObject objectToHide in this.ObjectsToHide)
        {
            objectToHide.SetActive(false);
        }

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
                this.AnimateCountdown("2");
                break;
            case "2":
                this.AnimateCountdown("1");
                break;
            case "1":
                this.AnimateCountdown("G O !");
                break;
            case "G O !":
                Destroy(this.Countdown);
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                if (playerController != null)
                    playerController.CanMove = true;
                foreach (GameObject objectToHide in this.ObjectsToHide)
                {
                    objectToHide.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
