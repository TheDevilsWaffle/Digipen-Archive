///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — AllEvents.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

/*
#region EVENTS

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
public class EVENT_UI_Initialize_Lives : GameEvent
{
    public int playerNumber;
    public int lives;
    public EVENT_UI_Initialize_Lives(int _playerNumber, int _lives)
    {
        playerNumber = _playerNumber;
        lives = _lives;
    }
}
/// <summary>
/// 
/// </summary>
public class EVENT_UI_Life_Lost : GameEvent
{
    public int playerNumber;
    public int livesRemaining;
    public EVENT_UI_Life_Lost(int _playerNumber, int _livesRemaining)
    {
        playerNumber = _playerNumber;
        livesRemaining = _livesRemaining;
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
public class EVENT_Player_Died : GameEvent
{
    public int playerNumber;
    public PlayerData playerData;
    public EVENT_Player_Died(PlayerData _playerData)
    {
        playerNumber = _playerData.PlayerNumber;
        playerData = _playerData;
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
*/
public class AllEvents : MonoBehaviour{ }
