///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_FadeIn.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class UI_FadeIn : UI_Button_Animation
{
    #region FIELDS
    [Header("FADE IN SETTINGS")]
    [SerializeField]
    float fadeInValue = 0.5f;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeOutQuad;
    [SerializeField]
    float time = 0.5f;
    [SerializeField]
    float delay = 0f;

    float opaque = 1f;
    float transparent = 0f;
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Animate()
    {
        LeanTween.alpha(rt, fadeInValue, time)
            .setIgnoreTimeScale(true)
            .setEase(ease)
            .setDelay(delay);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
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
	}
    #endregion
}
