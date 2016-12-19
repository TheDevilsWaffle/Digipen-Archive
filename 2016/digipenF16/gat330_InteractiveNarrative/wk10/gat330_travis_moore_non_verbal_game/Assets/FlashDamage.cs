///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — FlashDamage.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class FlashDamage : MonoBehaviour
{
    #region FIELDS
    public RectTransform damage;
    public RectTransform fade;
    public LeanTweenType ease;
    public float speed = 0.5f;
    public float duration = 0.5f;
    public float durationModifier = 0.1f;
    float deaths = 0f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        deaths = 0f;
        Events.instance.AddListener<EVENT_FollowerDied>(Flash);
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
    void Flash(EVENT_FollowerDied _event)
    {
        ++deaths;
        if (LeanTween.isTweening(damage.gameObject))
            LeanTween.cancel(damage.gameObject);
        if (LeanTween.isTweening(fade.gameObject))
            LeanTween.cancel(fade.gameObject);
        LeanTween.alpha(damage, 1f, speed).setEase(ease);
        LeanTween.alpha(fade, 0.6f, speed).setEase(ease);
        LeanTween.alpha(fade, 0f, speed + (durationModifier * deaths)).setEase(ease).setDelay(duration + durationModifier);
        LeanTween.alpha(damage, 0f, speed + (durationModifier * deaths)).setEase(ease).setDelay(duration + durationModifier);
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_FollowerDied>(Flash);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
