///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDTimer.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
public enum TimerMode
{
    COUNT_UP,
    COUNT_DOWN
};
#endregion

#region EVENTS
public class EVENT_TOGGLE_HUD_TIMER : GameEvent
{
    public bool isOn;
    public EVENT_TOGGLE_HUD_TIMER(bool _isOn)
    {
        isOn = _isOn;
    }
}
public class EVENT_TIME_UP : GameEvent
{
    public float timeLimit;
    public EVENT_TIME_UP(float _timeLimit)
    {
        timeLimit = _timeLimit;
    }
}
#endregion

public class HUDTimer : MonoBehaviour
{
    #region FIELDS
    [Header("REFERENCES")]
    RectTransform rt;
    CanvasGroup cg;
    HUDAnimator_Translate ha_trans;

    [SerializeField]
    RectTransform milliseconds;
    Text milliseconds_txt;
    HUDAnimator_Alpha milliseconds_ha_alpha;

    [SerializeField]
    RectTransform minutesSeconds;
    Text minutesSeconds_txt;
    HUDAnimator_Alpha minutesSeconds_ha_alpha;
    Outline minutesSeconds_outline;
    float outlineNormal = 0f;

    [Header("START ON/OFF")]
    [SerializeField]
    bool isOn = false;
    [Header("MODE")]
    [SerializeField]
    TimerMode mode = TimerMode.COUNT_UP;
    
    [Header("TIME LIMIT")]
    [SerializeField]
    int timeLimit_minutes = 1;
    [SerializeField]
    int timeLimit_seconds = 0;

    [Header("PULSE")]
    [SerializeField]
    float pulseThreshold = 11f;
    bool isPulsing = false;

    [Header("ANIMATION-TURN ON")]
    [SerializeField]
    LeanTweenType onEase;
    [SerializeField]
    float onAlpha;
    [SerializeField]
    float onTime;
    [SerializeField]
    float onDelay;

    [Header("ANIMATION-TURN OFF")]
    [SerializeField]
    LeanTweenType offEase;
    [SerializeField]
    float offAlpha;
    [SerializeField]
    float offTime;
    [SerializeField]
    float offDelay;

    [Header("ANIMATION-PULSE")]
    [SerializeField]
    LeanTweenType pulseEase;
    [SerializeField]
    int pulseLoops;
    [SerializeField]
    float pulseScaleFactor = 2.5f;
    Vector3 pulseScale;
    [SerializeField]
    float pulseTime;
    [SerializeField]
    float pulseDelay;
    [SerializeField]
    float pulseAlpha;
    [SerializeField]
    Color pulseColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField]
    float pulseOutline = 0.35f;

    float timeLimit;
    string currentTime;
    public string CurrentTime
    {
        get { return currentTime; }
        private set { currentTime = value; }
    }
    float timer = 0f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //refs
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        ha_trans = GetComponent<HUDAnimator_Translate>();

        minutesSeconds_txt = minutesSeconds.GetComponent<Text>();
        minutesSeconds_ha_alpha = minutesSeconds.GetComponent<HUDAnimator_Alpha>();
        milliseconds_txt = milliseconds.GetComponent<Text>();
        minutesSeconds_ha_alpha = milliseconds.GetComponent<HUDAnimator_Alpha>();
        minutesSeconds_outline = minutesSeconds.GetComponent<Outline>();

        //initial values
        pulseScale = rt.localScale * pulseScaleFactor;
        isPulsing = false;

        //subscriptions
        SetSubscriptions();

        //set mode appropriately
        SetTimerMode(mode);

        //TurnOff();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        TurnOn();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_TOGGLE_HUD_TIMER>(ToggleOnOff);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(isOn)
        {
            //count up
            if (mode == TimerMode.COUNT_UP)
            {
                timer += Time.deltaTime;  
            }
            //else countdown
            else
            {
                timer -= Time.deltaTime;
            }

            //check if time's up, if true stop the timer & send game event
            TimeLimitCheck();

            //update timer
            milliseconds_txt.text = UpdateMilliseconds();
            minutesSeconds_txt.text = UpdateMinutesSeconds();

            //update currentTime
            CurrentTime = minutesSeconds_txt.text + ":" + milliseconds_txt.text;
        }

    #if false
        UpdateTesting();
    #endif

    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    void Pulse()
    {
        //pulse scale
        LeanTween.scale(rt, pulseScale, pulseTime)
                 .setEase(pulseEase)
                 .setLoopCount(pulseLoops)
                 .setLoopType(LeanTweenType.pingPong)
                 .setDelay(pulseDelay);
        LeanTween.alphaCanvas(cg, pulseAlpha, pulseTime)
                 .setEase(pulseEase)
                 .setLoopCount(pulseLoops)
                 .setLoopType(LeanTweenType.pingPong)
                 .setDelay(pulseDelay);
        LeanTween.value(minutesSeconds.gameObject, OutlineValue, 0f, pulseOutline, pulseTime)
                 .setEase(pulseEase)
                 .setLoopCount(pulseLoops)
                 .setLoopType(LeanTweenType.pingPong)
                 .setDelay(pulseDelay);
    }
    void OutlineValue(float _value)
    {
        minutesSeconds_outline.effectColor = new Color(1f, 1f, 1f, _value);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TurnOn()
    {
        LeanTween.alphaCanvas(cg, onAlpha, onTime)
                 .setEase(onEase)
                 .setDelay(onDelay);
        ha_trans.TranslateIn();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TurnOff()
    {
        LeanTween.alphaCanvas(cg, offAlpha, offTime)
                 .setEase(offEase)
                 .setDelay(offDelay);
        ha_trans.TranslateOut();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Turn the HUD timer on/off
    /// </summary>
    /// <param name="_event">on/off event bool</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleOnOff(EVENT_TOGGLE_HUD_TIMER _event)
    {
        isOn = _event.isOn;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Set the timer based on either COUNT_DOWN or COUNT_UP
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetTimerMode(TimerMode _mode)
    {
        //countdown
        if (_mode == TimerMode.COUNT_DOWN)
        {
            timer = (timeLimit_minutes * 60) + timeLimit_seconds;
            int _time = (int)(timer * 100.0f);
            int _minutes = _time / (60 * 100);
            int _seconds = (_time % (60 * 100)) / 100;
            minutesSeconds_txt.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }
        //count up
        else
        {
            timeLimit = (timeLimit_minutes * 60) + timeLimit_seconds;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the milliseconds based on timer
    /// </summary>
    /// <returns>formated string for milliseconds</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    string UpdateMilliseconds()
    {
        int _time = (int)(timer * 100.0f);
        int _hundredths = _time % 100;

        return string.Format("{0:00}", _hundredths);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the minutes/seconds based on timer
    /// </summary>
    /// <returns>formatted string for minutes/seconds</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    string UpdateMinutesSeconds()
    {
        int _time = (int)(timer * 100.0f);
        int _minutes = _time / (60 * 100);
        int _seconds = (_time % (60 * 100)) / 100;

        return string.Format("{0:00}:{1:00}.", _minutes, _seconds);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// checks if time is up based on TimerMode (COUNT_DOWN or COUNT_UP)
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TimeLimitCheck()
    {
        //pulse
        if(!isPulsing && timer <= pulseThreshold)
        {
            isPulsing = true;
            Pulse();
        }

        //countdown
        if (mode == TimerMode.COUNT_DOWN && timer <= 0f)
        {
            isOn = false;
            //Debug.Log(mode + " is OVER!");
            Events.instance.Raise(new EVENT_TIME_UP(timeLimit));

            return;
        }
        //count up
        else if (mode == TimerMode.COUNT_UP && timer >= timeLimit)
        {
            isOn = false;
            //Debug.Log(mode + " is OVER");
            Events.instance.Raise(new EVENT_TIME_UP(timeLimit));
            return;
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_TOGGLE_HUD_TIMER>(ToggleOnOff);
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            TurnOn();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TurnOff();
        }
    }
    #endregion
}
