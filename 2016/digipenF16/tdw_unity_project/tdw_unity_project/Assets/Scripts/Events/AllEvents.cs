///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — AllEvents.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region EVENTS

public class EVENT_Round_Start : GameEvent
{
    public EVENT_Round_Start() { }
}

#region UI
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Initialize_Teleports : GameEvent
{
    public int playerNumber;
    public int teleportCharges;
    public EVENT_UI_Initialize_Teleports(int _playerNumber, int _teleportCharges)
    {
        playerNumber = _playerNumber;
        teleportCharges = _teleportCharges;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Teleport_Used : GameEvent
{
    public int playerNumber;
    public int teleportCharges;
    public float teleportRechargeTime;
    public EVENT_UI_Teleport_Used(int _playerNumber, int _teleportCharges, float _teleportRechargeTime)
    {
        playerNumber = _playerNumber;
        teleportCharges = _teleportCharges;
        teleportRechargeTime = _teleportRechargeTime;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Teleport_Restored : GameEvent
{
    public int playerNumber;
    public int teleportCharges;
    public EVENT_UI_Teleport_Restored(int _playerNumber, int _teleportCharges)
    {
        playerNumber = _playerNumber;
        teleportCharges = _teleportCharges;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Initialize_HUD : GameEvent
{
    public int numberOfPlayers;
    public EVENT_UI_Initialize_HUD(int _numberOfPlayers)
    {
        numberOfPlayers = _numberOfPlayers;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Initialize_Player : GameEvent
{
    public int playerNumber;
    public int lives;
    public Color color;
    public EVENT_UI_Initialize_Player(int _playerNumber, int _lives, Color _color)
    {
        playerNumber = _playerNumber;
        lives = _lives;
        color = _color;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Life_Lost : GameEvent
{
    public int playerNumber;
    public int livesRemaining;
    public Color playerColor;
    public Vector3 position;
    public EVENT_UI_Life_Lost(int _playerNumber, int _livesRemaining, Color _playerColor, Vector3 _position)
    {
        playerNumber = _playerNumber;
        livesRemaining = _livesRemaining;
        playerColor = _playerColor;
        position = _position;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Player_Eliminated : GameEvent
{
    public int playerNumber;
    public EVENT_UI_Player_Eliminated(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
#endregion

#region PLAYER INITIALIZE/DEATH/ELIMINATED
/// <summary>
/// 
/// </summary>
public class EVENT_Player_Initialized : GameEvent
{
    public PlayerData playerData;
    public EVENT_Player_Initialized(PlayerData _playerData)
    {
        playerData = _playerData;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_Player_Spawned : GameEvent
{
    public int playerNumber;
    public PlayerData playerData;
    public EVENT_Player_Spawned(PlayerData _playerData)
    {
        playerNumber = _playerData.PlayerNumber;
        playerData = _playerData;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_Player_Died : GameEvent
{
    public int playerNumber;
    public PlayerData playerData;
    public Vector3 deathPosition;
    public EVENT_Player_Died(PlayerData _playerData, Vector3 _deathPosition)
    {
        playerNumber = _playerData.PlayerNumber;
        playerData = _playerData;
        deathPosition = _deathPosition;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_Player_Eliminated : GameEvent
{
    public PlayerData playerData;
    public EVENT_Player_Eliminated(PlayerData _playerData)
    {
        playerData = _playerData;
    }
}
#endregion

#region PLAYER ABILITIES
/// <summary>
/// 
/// </summary>
public class EVENT_Teleport_Used : GameEvent
{
    public PlayerData playerData;
    public EVENT_Teleport_Used(PlayerData _playerData)
    {
        playerData = _playerData;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_Teleport_Restored : GameEvent
{
    public PlayerData playerData;
    public EVENT_Teleport_Restored(PlayerData _playerData)
    {
        playerData = _playerData;
    }
}
#endregion

#endregion

public class AllEvents : MonoBehaviour{ }
