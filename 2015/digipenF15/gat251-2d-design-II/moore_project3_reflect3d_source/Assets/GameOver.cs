/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - GameOver.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    //PROPERTIES
    Camera Player1Camera;
    Camera Player2Camera;
    GameObject Winner;
    SoundManager AudioController;
    GameObject LevelSettings;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get cameras
        this.Player1Camera = GameObject.Find("Player1Camera").GetComponent<Camera>();
        this.Player2Camera = GameObject.Find("Player2Camera").GetComponent<Camera>();

        //get winner HUD object
        this.Winner = GameObject.FindWithTag("Winner").gameObject;
        this.Winner.SetActive(false);

        this.LevelSettings = GameObject.FindWithTag("LevelSettings").gameObject;
        this.AudioController = GameObject.Find("AudioController").gameObject.GetComponent<SoundManager>();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DetermineWinner(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    public void DetermineWinner(GameObject player_)
    {
        string winner = "";
        //opposite day here
        if(player_.name == "Player1")
        {
            winner = "Player2";
            this.Player2Camera.rect = new Rect(0, 0, 1, 1);
            this.Player1Camera.enabled = false;
            GameObject.Find("Player1Canvas").gameObject.SetActive(false);
        }
        else
        {
            winner = "Player1";
            this.Player1Camera.rect = new Rect(0, 0, 1, 1);
            this.Player2Camera.enabled = false;
            GameObject.Find("Player2Canvas").gameObject.SetActive(false);
        }

        //declare winner
        this.Winner.SetActive(true);
        this.Winner.GetComponent<Text>().text = winner + " WINS!";
        this.AudioController.PlaySingle((AudioClip)Resources.Load("sfx_victory", typeof(AudioClip)));
        this.LevelSettings.GetComponent<LevelTimer>().DisableLevelTimer();

        StartCoroutine(RestartLevel());
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DetermineWinnerTimeUp()
    /////////////////////////////////////////////////////////////////////////*/
    public void DetermineWinnerTimeUp()
    {
        string winner = "";

        int player1Lives = GameObject.FindWithTag("Player1Lives").GetComponent<PlayerStatus>().Lives;
        int player2Lives = GameObject.FindWithTag("Player2Lives").GetComponent<PlayerStatus>().Lives;
        //opposite day here
        if (player2Lives > player1Lives)
        {
            winner = "Player2";
            this.Player2Camera.rect = new Rect(0, 0, 1, 1);
            this.Player1Camera.enabled = false;
            GameObject.Find("Player1Canvas").gameObject.SetActive(false);
            this.AudioController.PlaySingle((AudioClip)Resources.Load("sfx_victory", typeof(AudioClip)));
        }
        if(player1Lives > player2Lives)
        {
            winner = "Player1";
            this.Player1Camera.rect = new Rect(0, 0, 1, 1);
            this.Player2Camera.enabled = false;
            GameObject.Find("Player2Canvas").gameObject.SetActive(false);
            this.AudioController.PlaySingle((AudioClip)Resources.Load("sfx_victory", typeof(AudioClip)));
        }
        else
        {
            winner = "NO ONE";
            this.AudioController.PlaySingle((AudioClip)Resources.Load("sfx_loser", typeof(AudioClip)));
        }

        //declare winner
        this.Winner.SetActive(true);
        this.Winner.GetComponent<Text>().text = winner + " WINS!";
        this.LevelSettings.GetComponent<LevelTimer>().DisableLevelTimer();

        //disable both players
        GameObject.Find("Player1").SetActive(false);
        GameObject.Find("Player2").SetActive(false);
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        print("restaring level soon");
        yield return new WaitForSeconds(5);
        print("restaring level now");
        Application.LoadLevel("startmenu");
    }
}