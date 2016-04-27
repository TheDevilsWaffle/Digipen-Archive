/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - LivesCounter.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesCounter : MonoBehaviour
{
    //PROPERTIES
    Text HUDPlayerLives;
    [SerializeField]
    GameObject CurrentPlayer;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.HUDPlayerLives = this.gameObject.GetComponent<Text>();
        this.HUDPlayerLives.text = "LIVES: " + this.CurrentPlayer.GetComponent<PlayerStatus>().Lives.ToString();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetPlayerLives(int)
    /////////////////////////////////////////////////////////////////////////*/
    public void SetPlayerLives(int lives_)
    {
        this.HUDPlayerLives.text = "LIVES: " + lives_.ToString();
    }
}