///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MyFader.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class MyFader : MonoBehaviour
{
    #region FIELDS
    public RectTransform fader;
    public float time;
    LeanTweenType ease;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Events.instance.AddListener<EVENT_AllFollowersDead>(FadeBlack);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        LeanTween.alpha(fader, 1f, 0.001f).setEase(ease);
        LeanTween.alpha(fader, 0f, time).setEase(ease);
    }
    #endregion

    #region METHODS
    void FadeBlack(EVENT_AllFollowersDead _event)
    {
        LeanTween.alpha(fader, 1f, time).setEase(ease);
        LeanTween.delayedCall(time * 2, LoadMenu);
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("sce_mainMenu");
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_AllFollowersDead>(FadeBlack);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
