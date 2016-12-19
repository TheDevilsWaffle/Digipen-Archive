///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GameInitialize.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;

#region EVENTS
#endregion

[RequireComponent(typeof(SpawnSystem))]
public class GameInitialize : MonoBehaviour
{
    #region FIELDS
    [Header("PLAYER CREATION")]
    [Range(1, 4)]
    public int numberOfPlayers;
    public GameObject playerPrefab;
    public List<Color> playerColors;

    [Header("GAME SPECIFIC")]
    public int initialLives;
    public int maxTeleportCharges;
    public float teleportRechargeTime;
    public float cooldownBetweenTeleports;
    
    public static List<GameObject> activePlayers = new List<GameObject> { };

    SpawnSystem ss;
    Quaternion spawnPointRotationOffset;
    public static int numberOfPlayersCreated;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Events.instance.AddListener<EVENT_HowToPlay_Complete>(StartGame);
        activePlayers = new List<GameObject> { };
        numberOfPlayersCreated = numberOfPlayers;
        ss = GetComponent<SpawnSystem>();
        spawnPointRotationOffset = Quaternion.Euler(0f, 0f, 0f);
    }
	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //print("number of players = " + GamePadInput.numberOfPlayers);
        EvaluatePlayersToColors(numberOfPlayers, playerColors.Count);
        Events.instance.Raise(new EVENT_UI_Initialize_HUD(numberOfPlayers));
	}
    #endregion

    #region METHODS
    void StartGame(EVENT_HowToPlay_Complete _event)
    {
        InitializePlayers(numberOfPlayers);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_players"></param>
    /// <param name="_colors"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void EvaluatePlayersToColors(int _players, int _colors)
    {
        if(_colors < _players)
            Debug.LogError("WARNING! Not enough colors("+ _colors +") defined for " + _players + " players! Assign more colors in PlayerSystem.cs!");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_players"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializePlayers(int _players)
    {
        //loop through starting players
        for (int i = 0; i < _players; ++i)
        {
            //get a spawn point
            Transform _spawnPoint = ss.AssignStartingSpawnPoint();

            //create the player at the spawn point we retrieved
            GameObject _player = (GameObject)Instantiate(playerPrefab, _spawnPoint.position, spawnPointRotationOffset);

            //correct the player's number and name them
            int _correctPlayerNumber = i + 1;
            _player.name = "Player " + _correctPlayerNumber;

            //add this player to active players list
            activePlayers.Add(_player);

            //set info in this player's PlayerData
            PlayerData _playerData = _player.GetComponent<PlayerData>();
            _playerData.SetSpawnPoint(_spawnPoint);

            _playerData.Set_PlayerNumber(i);
            

            _playerData.Set_PlayerColor(playerColors[i]);
            if (_spawnPoint.parent.GetComponent<PlayerPlatform>() != null)
            {
                _spawnPoint.parent.GetComponent<PlayerPlatform>().SetPlayerColor(playerColors[i]);
                _spawnPoint.parent.GetComponent<PlayerPlatform>().SetPlayerNumber(_playerData.PlayerNumber);
            }
            _playerData.SetParticleSystemColor(playerColors[i]);
            _playerData.Set_Lives(initialLives);
            _playerData.Set_MaxTeleportCharges(maxTeleportCharges);
            _playerData.Set_TeleportRechargeTime(teleportRechargeTime);
            _playerData.Set_CoolDownBetweenTeleports(cooldownBetweenTeleports);

            //start the player
            _player.gameObject.SetActive(true);
            _player.GetComponent<Movement>().movementEnabled = false;
            _player.GetComponent<RightStickTeleport>().isTeleportEnabled = false;
            Events.instance.Raise(new EVENT_Player_Spawned(_player.GetComponent<PlayerData>()));
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_HowToPlay_Complete>(StartGame);
    }
    #endregion
}
