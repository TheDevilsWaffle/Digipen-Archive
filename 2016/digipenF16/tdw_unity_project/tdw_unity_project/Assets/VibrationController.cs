///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — VibrationController.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
using XInputDotNetPure; // Required in C#
using System;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class VibrationController : MonoBehaviour
{
    #region FIELDS
    PlayerData pd;
    int player;
    float endingIntensity;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        pd = GetComponent<PlayerData>();
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        player = pd.PlayerNumber;
        IncreaseVibrationOverTime(0.0f, 0.9f, 5f, LeanTweenType.easeInCirc);
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
    void IncreaseVibrationOverTime(float _startingIntensity, float _endingIntensity, float _time, LeanTweenType _ease)
    {
        endingIntensity = _endingIntensity;
        LeanTween.value(this.gameObject,
                        SetVibration,
                        _startingIntensity,
                        _endingIntensity,
                        _time)
                  .setEase(_ease);
        LeanTween.delayedCall(_time, SetEndingIntensity).setOnCompleteParam(_endingIntensity);
    }

    private void SetEndingIntensity()
    {
        //print("whalkashflkasdf");
        GamePad.SetVibration((PlayerIndex)player, 1, 1);
    }

    void SetVibration(float _intensity)
    {
        //DEBUG
        //Debug.Log("GamePad#" + player + "'s intensity level is currently = " + _intensity);
        GamePad.SetVibration((PlayerIndex)player, _intensity, _intensity);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
