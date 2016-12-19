///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDSystem.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;
//using UnityEngine.UI;

public class HUDSystem : MonoBehaviour
{
    #region FIELDS
    public GameObject[] huds;
    [SerializeField]
    GameObject finalLifePrefab;
    [SerializeField]
    GameObject deathSpritePrefab;
    [SerializeField]
    Camera mainCamera;

    Transform tr;
    #endregion

    void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    //        Events.instance.Raise(new EVENT_Player_Final_Life(GameObject.Find("Player 3").GetComponent<PlayerData>()));
    //    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    //        Events.instance.Raise(new EVENT_Player_Died(GameObject.Find("Player 3").GetComponent<PlayerData>()));
    }

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = this.gameObject.transform;

        foreach (GameObject hud in huds)
            hud.SetActive(false);
        Events.instance.AddListener<EVENT_UI_Initialize_HUD>(InitializeUI);
        Events.instance.AddListener<EVENT_UI_Initialize_Player>(InitializePlayer);
        Events.instance.AddListener<EVENT_UI_Life_Lost>(UpdatePlayerHUDLives);
        Events.instance.AddListener<EVENT_UI_Player_Eliminated>(UpdatePlayerHUDEliminated);
        Events.instance.AddListener<EVENT_Player_Final_Life>(AlertPlayerOfFinalLife);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {  
	}
    #endregion

    #region METHODS
    void AlertPlayerOfFinalLife(EVENT_Player_Final_Life _event)
    {
        GameObject _finalLife = (GameObject)GameObject.Instantiate(finalLifePrefab, tr, false);

        Vector3 _startingPosition = Vector3.zero;

        switch (_event.pd.PlayerNumber)
        {
            case (0):
                _finalLife.transform.SetParent(huds[0].gameObject.transform);
                _startingPosition = huds[0].GetComponent<RectTransform>().position;
                _startingPosition = new Vector3(_startingPosition.x, _startingPosition.y + -50f, _startingPosition.z);
                break;
            case (1):
                _finalLife.transform.SetParent(huds[1].gameObject.transform);
                _startingPosition = huds[1].GetComponent<RectTransform>().position;
                _startingPosition = new Vector3(_startingPosition.x, _startingPosition.y + -50f, _startingPosition.z);
                break;
            case (2):
                _finalLife.transform.SetParent(huds[2].gameObject.transform);
                _startingPosition = huds[2].GetComponent<RectTransform>().position;
                _startingPosition = new Vector3(_startingPosition.x, _startingPosition.y + 50f, _startingPosition.z);
                break;
            case (3):
                _finalLife.transform.SetParent(huds[3].gameObject.transform);
                _startingPosition = huds[3].GetComponent<RectTransform>().position;
                _startingPosition = new Vector3(_startingPosition.x, _startingPosition.y + 50f, _startingPosition.z);
                break;
            default:
                break;
        }

        _finalLife.GetComponent<UI_DangerFinalLife>().AnimateDangerText(_event.pd.PlayerColor, _startingPosition);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeUI(EVENT_UI_Initialize_HUD _event)
    {
        //print("initializing hud with " + _event.numberOfPlayers + " players!");
        for (int i = 0; i < _event.numberOfPlayers; ++i)
        {
            huds[i].SetActive(true);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializePlayer(EVENT_UI_Initialize_Player _event)
    {
        switch (_event.playerNumber)
        {
            case (0):
                huds[0].GetComponent<LivesSystem>().InitializeHUDProperties(_event.color);
                break;
            case (1):
                huds[1].GetComponent<LivesSystem>().InitializeHUDProperties(_event.color);
                break;
            case (2):
                huds[2].GetComponent<LivesSystem>().InitializeHUDProperties(_event.color);
                break;
            case (3):
                huds[3].GetComponent<LivesSystem>().InitializeHUDProperties(_event.color);
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdatePlayerHUDEliminated(EVENT_UI_Player_Eliminated _event)
    {
        switch(_event.playerNumber)
        {
            case (0):
                huds[0].GetComponent<LivesSystem>().EliminatePlayer();
                break;
            case (1):
                huds[1].GetComponent<LivesSystem>().EliminatePlayer();
                break;
            case (2):
                huds[2].GetComponent<LivesSystem>().EliminatePlayer();
                break;
            case (3):
                huds[3].GetComponent<LivesSystem>().EliminatePlayer();
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdatePlayerHUDLives(EVENT_UI_Life_Lost _event)
    {
        // IMPORTANT! THIS IS HOW YOU GET UI THINGS TO SPAWN BASED UPON LOCATION!
        GameObject _deathSprite = (GameObject)GameObject.Instantiate(deathSpritePrefab, tr, false);
        _deathSprite.transform.SetParent(this.gameObject.transform, false);
        Vector3 _position = mainCamera.WorldToScreenPoint(_event.position);
        _deathSprite.GetComponent<RectTransform>().position = _position;


        _deathSprite.GetComponent<DeathSprite>().AnimateDeathSprite(_event.playerColor);
        switch (_event.playerNumber)
        {
            
            case (0):
                huds[0].GetComponent<LivesSystem>().DestroyLife(_event.livesRemaining);
                break;
            case (1):
                huds[1].GetComponent<LivesSystem>().DestroyLife(_event.livesRemaining);
                break;
            case (2):
                huds[2].GetComponent<LivesSystem>().DestroyLife(_event.livesRemaining);
                break;
            case (3):
                huds[3].GetComponent<LivesSystem>().DestroyLife(_event.livesRemaining);
                break;
        }
    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_UI_Initialize_HUD>(InitializeUI);
        Events.instance.RemoveListener<EVENT_UI_Initialize_Player>(InitializePlayer);
        Events.instance.RemoveListener<EVENT_UI_Life_Lost>(UpdatePlayerHUDLives);
        Events.instance.RemoveListener<EVENT_UI_Player_Eliminated>(UpdatePlayerHUDEliminated);
        Events.instance.RemoveListener<EVENT_Player_Final_Life>(AlertPlayerOfFinalLife);
    }
}
