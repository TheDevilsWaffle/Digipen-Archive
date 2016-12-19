///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_CharacterSelect.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region EVENTS
public class EVENT_Load_Next_Scene : GameEvent
{
    public string scene;
    public EVENT_Load_Next_Scene(string _scene)
    {
        scene = _scene;
    }
}
#endregion

public class UI_CharacterSelect : MonoBehaviour
{
    #region FIELDS
    bool p1Joined;
    bool p2Joined;
    bool p3Joined;
    bool p4Joined;
    int activePlayers = 0;
    [SerializeField]
    string sceneToLoad;
    [SerializeField]
    List<GameObject> characterSelects;
    [SerializeField]
    List<ButtonBase> buttons;
    [SerializeField]
    List<GameObject> playerModels;
    [SerializeField]
    GameObject startText;
    bool isStartDisplayed = false;

    [Header("COLORS")]
    [SerializeField]
    Color p1Color = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    Color p2Color = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    Color p3Color = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    Color p4Color = new Color(1f, 1f, 1f, 1f);

    [Header("BUTTONS")]
    [SerializeField]
    string inactiveText;
    [SerializeField]
    string activeText;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        activePlayers = 0;
        p1Joined = false;
        p2Joined = false;
        p3Joined = false;
        p4Joined = false;
        isStartDisplayed = false;
    }
	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        SetAllButtonsText(inactiveText);
        StartTextPulse();
        startText.SetActive(false);
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
        if(activePlayers >= 2 && !isStartDisplayed)
            DisplayStartText();
        for (int i = 0; i < GamePadInput.numberOfPlayers; ++i)
        {
            if(i == 0)
            {
                if (GamePadInput.players[0] != null && GamePadInput.players[0].A == GamePadButtonState.PRESSED && p1Joined == false)
                {
                    p1Joined = true;
                    ++activePlayers;

                    characterSelects[0].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p1Color;
                    characterSelects[0].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().text = "READY!";
                    characterSelects[0].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().color = Color.white;
                    characterSelects[0].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
                    //playerModels[0].gameObject.SetActive(true);

                    buttons[0].Active();
                    buttons[0].transform.Find("Icon").gameObject.SetActive(false);
                    SetSoloButtonText(buttons[0], activeText);
                    buttons[0].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
                    buttons[0].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
                }
                if (GamePadInput.players[0] != null && activePlayers >= 2 && GamePadInput.players[0].Start == GamePadButtonState.PRESSED)
                {
                    GameData.totalPlayers = activePlayers;
                    //print("TOTAL NUMBER OF PLAYERS = " + GameData.totalPlayers);
                    Events.instance.Raise(new EVENT_Load_Screen());
                    LeanTween.delayedCall(1.25f, LoadScene);
                    //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
                }
            }
            if(i == 1)
            {
                if (GamePadInput.players[1] != null && GamePadInput.players[1].A == GamePadButtonState.PRESSED && p2Joined == false)
                {
                    p2Joined = true;
                    ++activePlayers;

                    characterSelects[1].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p2Color;
                    characterSelects[1].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().text = "READY!";
                    characterSelects[1].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().color = Color.white;

                    characterSelects[1].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
                    //playerModels[1].gameObject.SetActive(true);

                    buttons[1].Active();
                    buttons[1].transform.Find("Icon").gameObject.SetActive(false);
                    SetSoloButtonText(buttons[1], activeText);
                    buttons[1].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
                    buttons[1].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
                }
            }
            if(i == 2)
            {
                if (GamePadInput.players[2] != null && GamePadInput.players[2].A == GamePadButtonState.PRESSED && p3Joined == false)
                {
                    p3Joined = true;
                    ++activePlayers;

                    characterSelects[2].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p3Color;
                    characterSelects[2].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().text = "READY!";
                    characterSelects[2].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().color = Color.white;
                    characterSelects[2].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
                    playerModels[2].gameObject.SetActive(true);

                    buttons[2].Active();
                    buttons[2].transform.Find("Icon").gameObject.SetActive(false);
                    SetSoloButtonText(buttons[2], activeText);
                    buttons[2].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
                    buttons[2].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
                }
            }
            if(i == 3)
            {
                if (GamePadInput.players[3].A == GamePadButtonState.PRESSED && p4Joined == false)
                {
                    p4Joined = true;
                    ++activePlayers;

                    characterSelects[3].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p4Color;
                    characterSelects[3].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().text = "READY!";
                    characterSelects[3].transform.Find("Background/Avatar/Insert Coin").GetComponent<Text>().color = Color.white;
                    characterSelects[3].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
                    //playerModels[3].gameObject.SetActive(true);

                    buttons[3].Active();
                    buttons[3].transform.Find("Icon").gameObject.SetActive(false);
                    SetSoloButtonText(buttons[3], activeText);
                    buttons[3].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
                    buttons[3].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
                }
            }
            
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_playerCount"></param>
    void LoadScene()
    {
        switch (activePlayers)
        {
            case (2):
                SceneManager.LoadScene("sce_2p", LoadSceneMode.Single);
                break;
            case (3):
                SceneManager.LoadScene("sce_3p", LoadSceneMode.Single);
                break;
            case (4):
                SceneManager.LoadScene("sce_4p", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DisplayStartText()
    {
        isStartDisplayed = true;
        startText.SetActive(true);
        startText.GetComponent<UI_Button_Animation>().Animate_PulseScale();
        startText.GetComponent<UI_Button_Animation>().Animate_PulseText();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_button"></param>
    /// <param name="_text"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSoloButtonText(ButtonBase _button, string _text)
    {
        _button.transform.Find("Text").gameObject.GetComponent<Text>().text = _text;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_text"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetAllButtonsText(string _text)
    {
        foreach (ButtonBase _button in buttons)
        {
            _button.transform.Find("Text").gameObject.GetComponent<Text>().text = _text;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void StartTextPulse()
    {
        foreach (ButtonBase _button in buttons)
        {
            _button.transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Animate_PulseText();
            _button.transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Animate_PulseScale();
            _button.transform.Find("Icon").gameObject.GetComponent<UI_Button_Animation>().Animate_PulseScale();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void StartImagePulse()
    {
        foreach (ButtonBase _button in buttons)
        {
            _button.gameObject.GetComponent<UI_Button_Animation>().Animate_PulseImage();
        }
    }
    #endregion
}
