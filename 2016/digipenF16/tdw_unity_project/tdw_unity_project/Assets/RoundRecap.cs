///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — RoundRecap.cs
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

public class RoundRecap : MonoBehaviour
{
    #region FIELDS

    [Header("P1")]
    [SerializeField]
    Color p1Color;
    [SerializeField]
    GameObject p1Group;
    [SerializeField]
    RectTransform p1Slider;
    Image p1Image;

    [Header("P2")]
    [SerializeField]
    Color p2Color;
    [SerializeField]
    GameObject p2Group;
    [SerializeField]
    RectTransform p2Slider;
    Image p2Image;

    [Header("P3")]
    [SerializeField]
    Color p3Color;
    [SerializeField]
    GameObject p3Group;
    [SerializeField]
    RectTransform p3Slider;
    Image p3Image;

    [Header("P4")]
    [SerializeField]
    Color p4Color;
    [SerializeField]
    GameObject p4Group;
    [SerializeField]
    RectTransform p4Slider;
    Image p4Image;

    [Header("ANIMATION / DETAILS")]
    [SerializeField]
    string level;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    RectTransform results;
    [SerializeField]
    float totalWinsUntilVictory = 6f;
    [SerializeField]
    LeanTweenType ease;
    [SerializeField]
    float time;
    [SerializeField]
    GameObject nextGroup;
    Text nextText;
    [SerializeField]
    string nextRoundText = " - PRESS START FOR NEXT ROUND -";
    [SerializeField]
    string gameOverText = " - PRESS START TO RETURN TO MAIN MENU -";
    [SerializeField]
    GameObject winnerGroup;
    Text winnerText;


    int winner = 0;
    Color winnerColor;
    bool readyForInput = false;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Events.instance.AddListener<EVENT_Start_Round_Recap>(SetPlayerWinRound);

        winner = 0;
        readyForInput = false;
        //ensure slider values are correct
        p1Image = p1Slider.GetComponent<Image>();
        p1Image.fillAmount = RoundData.p1Wins;

        p2Image = p2Slider.GetComponent<Image>();
        p2Image.fillAmount = RoundData.p2Wins;

        p3Image = p3Slider.GetComponent<Image>();
        p3Image.fillAmount = RoundData.p3Wins;

        p4Image = p4Slider.GetComponent<Image>();
        p4Image.fillAmount = RoundData.p4Wins;

        nextText = nextGroup.transform.GetChild(0).gameObject.GetComponent<Text>();
        winnerText = winnerGroup.transform.GetChild(0).gameObject.GetComponent<Text>();
        canvas = this.gameObject.GetComponent<Canvas>();

        DisableRoundRecap();
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //ensure correct number of players
        DisableInactivePlayerScores(GameInitialize.numberOfPlayersCreated);
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
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        //    ReloadRound();
        if (readyForInput && winner == 0 )
        {
            //print("ready!");
            if(GamePadInput.players[0].A == GamePadButtonState.PRESSED)
            {
                //print("reloading this level");
                CloseOutRoundRecap();
                LeanTween.delayedCall(time, ReloadRound);
            }
        }
        else if( readyForInput && winner != 0)
        {
            if(GamePadInput.players[0].A == GamePadButtonState.PRESSED)
            {
                //print("game over, loading menu");
                CloseOutRoundRecap();
                LeanTween.delayedCall(time, LoadMainMenu);
            }
        }
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ReloadRound()
    {
        SceneManager.LoadScene(level);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LoadMainMenu()
    {
        //reset Round Data
        RoundData.p1Wins = 0;
        RoundData.p2Wins = 0;
        RoundData.p3Wins = 0;
        RoundData.p4Wins = 0;
        RoundData.hasHowToPlayBeenShown = false;

        SceneManager.LoadScene("sce_mainMenu", LoadSceneMode.Single);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisableRoundRecap()
    {
        canvas.enabled = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_players"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DisableInactivePlayerScores(int _players)
    {
        switch (_players)
        {
            case (2):
                p3Group.SetActive(false);
                p4Group.SetActive(false);
                break;
            case (3):
                p4Group.SetActive(false);
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetPlayerWinRound(EVENT_Start_Round_Recap _event)
    {
        //Debug.Log("ROUNDRECAP: SetPlayerWinRound() called");
        switch (_event.playerNumber)
        {
            case (0):
                ++RoundData.p1Wins;
                //Debug.Log("RoundData.p1Wins = " + RoundData.p1Wins);
                break;
            case (1):
                ++RoundData.p2Wins;
                //Debug.Log("RoundData.p2Wins = " + RoundData.p2Wins);
                break;
            case (2):
                ++RoundData.p3Wins;
                //Debug.Log("RoundData.p3Wins = " + RoundData.p3Wins);
                break;
            case (3):
                ++RoundData.p4Wins;
                //Debug.Log("RoundData.p4Wins = " + RoundData.p4Wins);
                break;
            default:
                break;
        }
        EnableRoundRecap();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void EnableRoundRecap()
    {
        //Debug.Log("ROUNDRECAP: EnableRoundRecap() called");
        canvas.enabled = true;
        CalculateScore();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void CalculateScore()
    {
        //Debug.Log("ROUNDRECAP: CalculateScore() called");

        float p1Percentage = RoundData.p1Wins / totalWinsUntilVictory;
        if (p1Percentage >= 1f)
        {
            p1Percentage = 1f;
            winner = 1;
            winnerColor = p1Color;
        }
        LeanTween.value(p1Image.gameObject, UpdateP1Score, 0f, p1Percentage, time).setEase(ease);

        float p2Percentage = RoundData.p2Wins / totalWinsUntilVictory;
        if (p2Percentage >= 1f)
        {
            p2Percentage = 1f;
            winner = 2;
            winnerColor = p2Color;
        }
        LeanTween.value(p2Image.gameObject, UpdateP2Score, 0f, p2Percentage, time).setEase(ease);

        float p3Percentage = RoundData.p3Wins / totalWinsUntilVictory;
        if (p3Percentage >= 1f)
        {
            p3Percentage = 1f;
            winner = 3;
            winnerColor = p3Color;
        }
        LeanTween.value(p3Image.gameObject, UpdateP3Score, 0f, p3Percentage, time).setEase(ease);

        float p4Percentage = RoundData.p4Wins / totalWinsUntilVictory;
        if (p4Percentage >= 1f)
        {
            p4Percentage = 1f;
            winner = 4;
            winnerColor = p4Color;
        }
        LeanTween.value(p4Image.gameObject, UpdateP4Score, 0f, p4Percentage, time).setEase(ease);
        LeanTween.delayedCall(time, DetermineNext);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineNext()
    {
        //Debug.Log("ROUNDRECAP: DetermineNext() called");
        if (winner != 0)
        {
            //Debug.Log("WINNER!");
            nextText.text = gameOverText;
            LeanTween.alphaCanvas(nextGroup.GetComponent<CanvasGroup>(), 1, time).setEase(ease);
            readyForInput = true;
            DeclareWinner();
        }
        else
        {
            //Debug.Log("GOTO NEXT ROUND");
            nextText.text = nextRoundText;
            LeanTween.alphaCanvas(nextGroup.GetComponent<CanvasGroup>(), 1, time).setEase(ease);
            readyForInput = true;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DeclareWinner()
    {
        //Debug.Log("ROUNDRECAP: DecalreWinner() called");

        LeanTween.alphaCanvas(results.GetComponent<CanvasGroup>(), 0f, time).setEase(ease);
        winnerText.color = winnerColor;
        winnerText.text = "PLAYER " + winner + " WINS!";
        LeanTween.alphaCanvas(winnerGroup.GetComponent<CanvasGroup>(), 1, time).setDelay(time).setEase(ease);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CloseOutRoundRecap()
    {
        //Debug.Log("ROUNDRECAP: ClouseOutRoundRecap() called");

        readyForInput = false;
        winnerGroup.GetComponent<CanvasGroup>().alpha = 0f;
        nextGroup.GetComponent<CanvasGroup>().alpha = 0f;
        LeanTween.delayedCall(time, DisableRoundRecap);
        Events.instance.Raise(new EVENT_Load_Screen());
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateP1Score(float _amount)
    {
        p1Image.fillAmount = _amount;
    }
    void UpdateP2Score(float _amount)
    {
        p2Image.fillAmount = _amount;
    }
    void UpdateP3Score(float _amount)
    {
        p3Image.fillAmount = _amount;
    }
    void UpdateP4Score(float _amount)
    {
        p4Image.fillAmount = _amount;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Start_Round_Recap>(SetPlayerWinRound);
    }
    #endregion
}
