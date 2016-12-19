///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Countdown.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    #region FIELDS

    [Header("Objects")]
    [SerializeField]
    RectTransform background;
    Image background_image;
    [SerializeField]
    RectTransform countdown;
    Text countdown_text;
    int index = 0;
    Vector3 originalScale;
    Vector3 originalPosition;
    [SerializeField]
    string[] countdownStrings;

    [Header("Animation Start")]
    [SerializeField]
    float delayBeforeStart;
    [SerializeField]
    LeanTweenType startingEase;
    [SerializeField]
    float startingTime;
    [SerializeField]
    float startingDuration;
    [SerializeField]
    float startingScaleFactor;
    Vector3 startingScale;
    [SerializeField]
    [Range(0f,1f)]
    float startingAlpha;

    [Header("Animation End")]
    [SerializeField]
    LeanTweenType endingEase;
    [SerializeField]
    float endingTime;
    [SerializeField]
    float endingDuration;
    [SerializeField]
    float endingScaleFactor;
    Vector3 endingScale;
    [SerializeField]
    [Range(0f, 1f)]
    float endingAlpha;

    float instant = 0.001f;
    float transparent = 0f;
    float opaque = 1f;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        if (countdown == null && transform.FindChild("Text").gameObject.GetComponent<RectTransform>())
            countdown = transform.FindChild("Text").gameObject.GetComponent<RectTransform>();
        background_image = background.GetComponent<Image>();
        countdown_text = countdown.gameObject.GetComponent<Text>();
        index = 0;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        this.gameObject.GetComponent<Canvas>().enabled = true;
        originalScale = countdown.transform.localScale;
        originalPosition = countdown.transform.position;
        startingScale = Vector3.one * startingScaleFactor;
        endingScale = Vector3.one * endingScaleFactor;
        ResetCountdown(countdownStrings[index]);
    }
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_string"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ResetCountdown(string _string)
    {
        //increment index
        ++index;
        countdown_text.text = _string;
        if (countdown == null && transform.FindChild("Text").gameObject.GetComponent<RectTransform>())
            countdown = transform.FindChild("Text").gameObject.GetComponent<RectTransform>();
        countdown.transform.localScale = startingScale;
        LeanTween.alphaText(countdown, transparent, instant);
        LeanTween.delayedCall(delayBeforeStart, AnimateCountdownStart);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateCountdownStart()
    {
        LeanTween.alphaText(countdown, startingAlpha, startingTime).setEase(startingEase);
        LeanTween.scale(countdown, originalScale, startingTime).setEase(startingEase);
        LeanTween.delayedCall((startingTime + startingDuration), AnimateCountdownEnd);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateCountdownEnd()
    {
        if (countdown != null)
        {
            LeanTween.alphaText(countdown, endingAlpha, endingTime).setEase(endingEase);
            LeanTween.scale(countdown, endingScale, endingTime).setEase(endingEase);
            LeanTween.delayedCall((endingTime + endingDuration), DetermineNext);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineNext()
    {
        //print("index = " + index + " and coundownStrings.Length = " + countdownStrings.Length);
        if(index < countdownStrings.Length)
        {
            //Debug.Log("COUNTDOWN: going to next string");
            ResetCountdown(countdownStrings[index]);
        }
        else
        {
            //Debug.Log("COUNTDOWN IS OVER!");
            CountdownOver();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CountdownOver()
    {
        LeanTween.alpha(background, transparent, endingTime).setEase(endingEase);
        LeanTween.textAlpha(countdown, transparent, endingTime).setEase(endingEase);
        LeanTween.delayedCall(endingTime, DestroyCountdown);

        Events.instance.Raise(new EVENT_Round_Start());

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DestroyCountdown()
    {
        if (this.gameObject != null)
            this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    #endregion
}
