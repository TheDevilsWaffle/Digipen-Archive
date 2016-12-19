///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — SpawnSystem.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_Player_Final_Life : GameEvent
{
    public PlayerData pd;
    public EVENT_Player_Final_Life(PlayerData _pd)
    {
        pd = _pd;
    }
}
public class EVENT_Start_Round_Recap : GameEvent
{
    public int playerNumber;
    public EVENT_Start_Round_Recap(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
#endregion

public class SpawnSystem : MonoBehaviour
{
    #region FIELDS
    public List<Transform> spawnPoints;
    int startingSpawnIndex = 0;
    public AudioClip explodeSFX;
    AudioSource sfx;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Events.instance.AddListener<EVENT_Player_Died>(EvaluatePlayerLives);
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        startingSpawnIndex = 0;
	}
    #endregion

    #region METHODS
    public Transform GetStartingSpawnPoint()
    {
        int _temp = startingSpawnIndex++;
        //print(spawnPoints[_temp].gameObject.name);
        return spawnPoints[_temp];
    }
    public Transform AssignStartingSpawnPoint()
    {
        int _temp = startingSpawnIndex;
        ++startingSpawnIndex;
        return spawnPoints[_temp];
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public Transform GetRandomSpawnPoint()
    {
        //get random number
        int _index = Random.Range(0, (spawnPoints.Count - 1));
        return spawnPoints[_index];
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    void EvaluatePlayerLives(EVENT_Player_Died _event)
    {
        sfx.PlayOneShot(explodeSFX);
        //turn off this player
        _event.playerData.gameObject.SetActive(false);
        //print("EvaluateLivesLeft(" + _event.playerData.gameObject.name + ")");
        if (_event.playerData.Lives > 0)
        {
            //_event.playerData.SubtractALife();
            //print(_event.playerData.gameObject.name + " now has " + _event.playerData.Lives + " left!");
            Respawn(_event.playerData);
        }
        else
        {
            //take this player out of the list of activePlayers
            GameObject temp = GameInitialize.activePlayers.Where(obj => obj.name == _event.playerData.gameObject.name).SingleOrDefault();
            GameInitialize.activePlayers.Remove(temp);
            CheckForLastPlayerStanding();
        }
    }

    void CheckForLastPlayerStanding()
    {
        if(GameInitialize.activePlayers.Count == 1)
        {
            foreach (GameObject player in GameInitialize.activePlayers)
            {
                Events.instance.Raise(new EVENT_Start_Round_Recap(player.GetComponent<PlayerData>().PlayerNumber));
            }    
        }
    }

    void Respawn(PlayerData _pd)
    {
        _pd.gameObject.transform.position = _pd.SpawnPoint.position;
        _pd.gameObject.SetActive(true);
        Events.instance.Raise(new EVENT_Player_Spawned(_pd));
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Player_Died>(EvaluatePlayerLives);
    }
    #endregion

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Keypad1))
    //    {
    //        Debug.Log("KEYPAD1 pressed!");
    //        Events.instance.Raise(new EVENT_Start_Round_Recap(0));
    //    }
    //}
}
