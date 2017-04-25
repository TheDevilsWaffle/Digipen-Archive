///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_FadeOut.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class UI_FadeOut : UI_Button_Animation
{
    #region FIELDS
    [SerializeField]
    bool playOnAwake = false;
    [Header("FADE OUT SETTINGS")]
    [SerializeField]
    [Range(0f, 1f)]
    float fadeOutValue = 0f;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInQuad;
    [SerializeField]
    float time = 0.5f;
    [SerializeField]
    float delay = 0f;

    float opaque = 1f;
    float transparent = 0f;
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        if(playOnAwake)
        {
            GetComponent<Image>().enabled = true;
            LeanTween.alpha(this.gameObject.GetComponent<RectTransform>(), fadeOutValue, time)
            .setIgnoreTimeScale(true)
            .setEase(ease)
            .setDelay(delay);
        }
    }

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Animate()
    {
        LeanTween.alpha(rt, fadeOutValue, time)
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
