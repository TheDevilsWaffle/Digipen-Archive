/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - RespawnPlayer.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class RespawnPlayer : MonoBehaviour
{
    //PROPERTIES
    LivesCounter Player1LivesHUD;
    LivesCounter Player2LivesHUD;
    [SerializeField]
    GameObject[] Player1RespawnPos;
    [SerializeField]
    GameObject[] Player2RespawnPos;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    public void Start()
    {
        this.Player1LivesHUD = GameObject.Find("Player1Lives").gameObject.GetComponent<LivesCounter>();
        this.Player2LivesHUD = GameObject.Find("Player2Lives").gameObject.GetComponent<LivesCounter>();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: PlayerDied(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    public void PlayerDied(GameObject player_)
    {
        //reduce lives and respawn the player
        if (player_.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().Lives > 0)
        {
            player_.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().Lives -= 1;

            if (player_ == GameObject.Find("Player1").gameObject)
            {
                this.RespawnPlayerInNewPos(player_);
                this.Player1LivesHUD.SetPlayerLives(player_.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().Lives);
            }
            if (player_ == GameObject.Find("Player2").gameObject)
            {
                this.RespawnPlayerInNewPos(player_);
                this.Player2LivesHUD.SetPlayerLives(player_.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().Lives);
            }
            //DEBUG
            //Debug.Log("dead");
            StartCoroutine(Respawn(player_));
        }

        if (player_.transform.FindChild("PlayerCollider").GetComponent<PlayerStatus>().Lives <= 0)
        {
            //Debug.Log("PLAYER HAS RUN OUT OF LIVES");
            //kill the player
            //Destroy(this.gameObject.transform.root.gameObject);
            this.gameObject.GetComponent<GameOver>().DetermineWinner(player_);
        }
    }

    void RespawnPlayerInNewPos(GameObject player_)
    {
        if(player_.name == "Player1")
        {
            int i = Random.Range(0, 2);
            GameObject.Find("Player1").transform.position = this.Player1RespawnPos[i].transform.position;
            player_.SetActive(false);
        }
        else
        {
            int i = Random.Range(0, 2);
            GameObject.Find("Player2").transform.position = this.Player2RespawnPos[i].transform.position;
            player_.SetActive(false);
        }
    }

    IEnumerator Respawn(GameObject player_)
    {
        player_.SetActive(false);
        yield return new WaitForSeconds(3);
        player_.SetActive(true);
        //player_.transform.FindChild("Art").gameObject.transform.FindChild("PlayerCollider").gameObject.GetComponent<PlayerStatus>().CurrentStatus = PlayerStatus.Status.ACTIVE;
    }
}