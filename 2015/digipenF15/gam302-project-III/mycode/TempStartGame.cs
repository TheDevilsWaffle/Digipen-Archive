/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - TempStartGame.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class TempStartGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("CameraTestScene");
        }
    }
}
