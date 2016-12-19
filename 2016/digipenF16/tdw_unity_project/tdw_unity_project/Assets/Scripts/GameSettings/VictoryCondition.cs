///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — VictoryCondition.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class VictoryCondition : MonoBehaviour
{
    #region FIELDS
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    string player;
    Color playerColor = new Color(0f, 0.7f, 0f, 1f);
    public Text victoryText;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //victoryText.color = new Color(1f, 1f, 1f, 0f);
        Events.instance.AddListener<EVENT_Player_Died>(CheckRemainingPlayers);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        FindPlayers();
        

    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {

    }
    #endregion

    #region METHODS
    void FindPlayers()
    {
        if (GameObject.FindWithTag("Player 1") != null)
        {
            p1 = GameObject.FindWithTag("Player 1").gameObject;
        }
        if (GameObject.FindWithTag("Player 2") != null)
        {
            p2 = GameObject.FindWithTag("Player 2").gameObject;
        }
    }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        void CheckRemainingPlayers(EVENT_Player_Died _event)
    {
        if (_event.playerData.Lives == 0)
        {
            Destroy(_event.playerData.gameObject);
            if(_event.playerData.PlayerNumber == 0)
            {
                p1 = null;
            }
            if (_event.playerData.PlayerNumber == 1)
            {
                p2 = null;
            }

            int _playersRemaining = 0;
            if (p1 != null)
            {
                print("p1 is still alive");
                ++_playersRemaining;
                player = "Player 1";
            }
            if (p2 != null)
            {
                print("p2 is still alive");
                ++_playersRemaining;
                player = "Player 2";
            }
            if (p3 != null)
            {
                ++_playersRemaining;
                player = "Player 3";
            }
            if (p4 != null)
            {
                ++_playersRemaining;
                player = "Player 4";
            }
            print("Players Remaining = " + _playersRemaining);
            if (_playersRemaining == 1)
                StartCoroutine(DeclareVictory());
        }
    }

    IEnumerator DeclareVictory()
    {
        victoryText.text = player + "WINS!";
        LeanTween.colorText(victoryText.GetComponent<RectTransform>(), playerColor, 0.5f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Player_Died>(CheckRemainingPlayers);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
