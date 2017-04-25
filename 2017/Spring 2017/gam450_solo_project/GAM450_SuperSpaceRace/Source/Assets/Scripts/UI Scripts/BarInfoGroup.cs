///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — BarInfoGroup.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class BarInfoGroup : MonoBehaviour
{
    #region FIELDS
    Transform tr;
    [SerializeField]
    GameObject icon;
    Image icon_image;
    Sprite gamepadSprite;
    Sprite keyboardMouseSprite;
    [SerializeField]
    GameObject instruction;
    Text instruction_text;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = transform;

        if (icon == null && tr.FindChild("Icon").gameObject != null)
            icon = tr.FindChild("Icon").gameObject;
        icon_image = icon.GetComponent<Image>();

        if (instruction == null && tr.FindChild("Text").gameObject != null)
            instruction = tr.FindChild("Text").gameObject;
        instruction_text = instruction.GetComponent<Text>();

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE>(SwitchToKeyboardMouseIcons);
        Events.instance.AddListener<EVENT_UPDATE_UI_TO_GAMEPAD>(SwitchToGamePadIcons);
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

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisableIconAndInstructions()
    {
        icon.SetActive(false);
        instruction.SetActive(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_sprite"></param>
    /// <param name="_string"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetBarInfoGroup(Sprite _gamepadSprite, Sprite _keyboardMouseSprite, string _string)
    {
        //first set the sprites so we have them stored for later
        gamepadSprite = _gamepadSprite;
        keyboardMouseSprite = _keyboardMouseSprite;

        //set sprite based on last input we've used
        if(DetermineLastInputMethod())
        {
            SetIconSprite(keyboardMouseSprite);
        }
        else
        {
            SetIconSprite(gamepadSprite);
        }
        SetInstructionString(_string);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <returns>true = keyboard/mouse input, false = gamepad input</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool DetermineLastInputMethod()
    {
        switch (InputTypeDetection.currentlyActiveInputType)
        {
            case ActiveInputType.KEYBOARD:
            case ActiveInputType.MOUSE:
                return true;
            case ActiveInputType.GAMEPAD:
                return false;
            default:
                return true;
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_sprite"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetIconSprite(Sprite _sprite)
    {
        icon.SetActive(true);
        icon_image.sprite = _sprite;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_string"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetInstructionString(string _string)
    {
        instruction.SetActive(true);
        instruction_text.text = _string;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SwitchToKeyboardMouseIcons(EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE _event)
    {
        //Debug.Log("SwitchToKeyboardMouseIcons");
        if (keyboardMouseSprite != null && icon.activeSelf)
        {
            SetIconSprite(keyboardMouseSprite);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SwitchToGamePadIcons(EVENT_UPDATE_UI_TO_GAMEPAD _event)
    {
        //Debug.Log("SwitchToGamePadIcons");
        if (gamepadSprite != null && icon.activeSelf)
        {
            SetIconSprite(gamepadSprite);
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_UPDATE_UI_TO_KEYBOARD_MOUSE>(SwitchToKeyboardMouseIcons);
        Events.instance.RemoveListener<EVENT_UPDATE_UI_TO_GAMEPAD>(SwitchToGamePadIcons);
    }
    #endregion
}
