/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - LevelTimer.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    //PROPERTIES
    float Timer = 180f;
    bool IsTimerStarted = false;
    GameObject HUDLevelTimer;
    bool IsGameOver = false;
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.IsGameOver = false;
        this.Timer = 180f;
        this.IsTimerStarted = false;
        this.HUDLevelTimer = GameObject.Find("HUDLevelTimer").gameObject;
        this.HUDLevelTimer.SetActive(false);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.IsTimerStarted)
        {
            this.Timer -= Time.deltaTime;
            this.HUDLevelTimer.transform.FindChild("Text").GetComponent<Text>().text = "TIME LEFT: " + ((int)this.Timer).ToString();
        }
        if(this.Timer <= 0 && !this.IsGameOver)
        {
            this.IsGameOver = true;
            GameObject.FindWithTag("LevelSettings").gameObject.GetComponent<GameOver>().DetermineWinnerTimeUp();
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    public void StartLevelTimer()
    {
        this.IsTimerStarted = true;
        this.HUDLevelTimer.SetActive(true);
    }

    public void DisableLevelTimer()
    {
        this.HUDLevelTimer.SetActive(false);
    }
}