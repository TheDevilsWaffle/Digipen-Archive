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

        if (GamePadInput.players[0].A == GamePadButtonState.PRESSED && p1Joined == false)
        {
            p1Joined = true;
            ++activePlayers;

            characterSelects[0].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p1Color;
            characterSelects[0].transform.Find("Background/Avatar/Insert Coin").gameObject.SetActive(false);
            characterSelects[0].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
            playerModels[0].gameObject.SetActive(true);

            buttons[0].Active();
            buttons[0].transform.Find("Icon").gameObject.SetActive(false);
            SetSoloButtonText(buttons[0], activeText);
            buttons[0].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
            buttons[0].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
        }
        if (GamePadInput.players[1].A == GamePadButtonState.PRESSED && p2Joined == false)
        {
            p2Joined = true;
            ++activePlayers;

            characterSelects[1].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p2Color;
            characterSelects[1].transform.Find("Background/Avatar/Insert Coin").gameObject.SetActive(false);
            characterSelects[1].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
            playerModels[1].gameObject.SetActive(true);

            buttons[1].Active();
            buttons[1].transform.Find("Icon").gameObject.SetActive(false);
            SetSoloButtonText(buttons[1], activeText);
            buttons[1].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
            buttons[1].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
        }
        if (GamePadInput.players[2].A == GamePadButtonState.PRESSED && p3Joined == false)
        {
            p3Joined = true;
            ++activePlayers;

            characterSelects[2].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p3Color;
            characterSelects[2].transform.Find("Background/Avatar/Insert Coin").gameObject.SetActive(false);
            characterSelects[2].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
            playerModels[2].gameObject.SetActive(true);

            buttons[2].Active();
            buttons[2].transform.Find("Icon").gameObject.SetActive(false);
            SetSoloButtonText(buttons[2], activeText);
            buttons[2].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
            buttons[2].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
        }
        if (GamePadInput.players[3].A == GamePadButtonState.PRESSED && p4Joined == false)
        {
            p4Joined = true;
            ++activePlayers;

            characterSelects[3].transform.Find("Background/Avatar").gameObject.GetComponent<Image>().color = p4Color;
            characterSelects[3].transform.Find("Background/Avatar/Insert Coin").gameObject.SetActive(false);
            characterSelects[3].transform.Find("Background/Avatar/Player").gameObject.GetComponent<Text>().color = Color.white;
            playerModels[3].gameObject.SetActive(true);

            buttons[3].Active();
            buttons[3].transform.Find("Icon").gameObject.SetActive(false);
            SetSoloButtonText(buttons[3], activeText);
            buttons[3].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseText(Color.white);
            buttons[3].transform.Find("Text").gameObject.GetComponent<UI_Button_Animation>().Cancel_Animate_PulseScale();
        }
        if (activePlayers >= 2 && GamePadInput.players[0].Start == GamePadButtonState.PRESSED)
        {
            GamePadInput.numberOfPlayers = activePlayers;
            //print("TOTAL NUMBER OF PLAYERS = " + GamePadInput.numberOfPlayers);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
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
