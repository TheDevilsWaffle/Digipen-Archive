/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CheatCodes.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheatCodes : MonoBehaviour
{
    GameObject LevelSettings;
	// Use this for initialization
	void Start ()
    {
        this.LevelSettings = GameObject.FindWithTag("LevelSettings").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            this.LevelSettings.GetComponent<GameOver>().DetermineWinner(GameObject.Find("Player2"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            this.LevelSettings.GetComponent<GameOver>().DetermineWinner(GameObject.Find("Player1"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            this.LevelSettings.GetComponent<GameOver>().DetermineWinnerTimeUp();
        }
    }
}
