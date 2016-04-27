/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Pause.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Pause:MonoBehaviour
{
    //PROPERTIES
    [Header("Pause Settings")]
    [SerializeField]
    KeyCode KBPauseButton = KeyCode.P;
    [SerializeField]
    KeyCode GPPauseButton = KeyCode.JoystickButton7;
    [HideInInspector]
    bool IsGamePaused = false;
    GameObject AudioController;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure that IsGamePaused is set to false at the start of script
        this.IsGamePaused = false;

        //get the audio controller
        this.AudioController = GameObject.FindWithTag("AudioController");
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
	    if(Input.GetKeyDown(this.KBPauseButton) || Input.GetKeyDown(this.GPPauseButton))
        {
            Debug.Log("PLAYER HAS PAUSED THE GAME");
            Debug.Log("TIMESCALE = " + Time.timeScale);
            //toggle pausing
            Time.timeScale = this.TogglePause();
            //toggle music pausing
            this.ToggleAudioMuting(this.IsGamePaused);

        }
	}

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePause()
    /////////////////////////////////////////////////////////////////////////*/
    int TogglePause()
    {
        //if game is currently paused, unpause it
        if(this.IsGamePaused)
        {
            Debug.Log("UNPAUSING THE GAME");
            //set IsGamePaused to false
            this.IsGamePaused = false;

            return 1;
        }
        //else game is not paused, pause it.
        else
        {
            Debug.Log("PAUSING THE GAME");
            //set IsGamePaused to true
            this.IsGamePaused = true;

            return 0;
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ToggleAudioMuting(int)
    /////////////////////////////////////////////////////////////////////////*/
    void ToggleAudioMuting(bool pause_)
    {
        if(pause_)
        {
            this.AudioController.transform.FindChild("MusicAudioSource").GetComponent<AudioSource>().Pause();
        }
        else
        {
            this.AudioController.transform.FindChild("MusicAudioSource").GetComponent<AudioSource>().UnPause();
        }
    }
}
