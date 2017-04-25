///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDShieldSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_UPDATE_HUD_SHIELD_INCREASED : GameEvent
{
    public int index;
    public float energy;
    public EVENT_UPDATE_HUD_SHIELD_INCREASED(float _energy, int _index)
    {
        energy = _energy;
        index = _index;
    }
}
public class EVENT_UPDATE_HUD_SHIELD_DECREASED : GameEvent
{
    public int index;
    public float energy;
    public EVENT_UPDATE_HUD_SHIELD_DECREASED(float _energy, int _index)
    {
        energy = _energy;
        index = _index;
    }
}
public class EVENT_UPDATE_HUD_SHIELD_ENERGY : GameEvent
{
    public int index;
    public float energy;
    public float perShield;
    public EVENT_UPDATE_HUD_SHIELD_ENERGY(float _energy, int _index, float _perShield)
    {
        energy = _energy;
        index = _index;
        perShield = _perShield;
    }
}
#endregion

public class HUDShieldSystem : MonoBehaviour
{
    RectTransform rt;
    [SerializeField]
    CanvasGroup cg;
    Vector3 position_original;
    Vector3 scale_original;

    #region FIELDS
    [Header("TEXT")]
    [SerializeField]
    RectTransform value;
    Text value_txt;
    [SerializeField]
    RectTransform status;
    Text status_txt;

    [Header("TICKS")]
    [SerializeField]
    TickData[] ticks;
    int tickCount;
    float previousEnergy;
    TickData currentTick;

    [Header("DIVIDER")]
    [SerializeField]
    RectTransform divider;
    Image divider_img;

    [Header("SYSTEM STATES")]
    [SerializeField]
    [Range(0f, 1f)]
    float alpha_highlight = 1f;
    [SerializeField]
    [Range(0f, 1f)]
    float alpha_normal = 0.75f;
    [SerializeField]
    [Range(0f, 1f)]
    float alpha_idle = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    float alpha_empty = 0.25f;

    [Header("INITIALIZE ANIMATION")]
    [SerializeField]
    LeanTweenType initialize_ease = LeanTweenType.easeInBack;
    [SerializeField]
    float initialize_time = 0.5f;
    [SerializeField]
    float initialize_delay = 0.25f;

    [Header("OPERATIONAL ANIMATION")]
    [SerializeField]
    Color op_color = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    string op_string = "SHIELDS";
    [SerializeField]
    LeanTweenType op_ease = LeanTweenType.easeOutBack;
    [SerializeField]
    float op_time = 0.5f;

    [Header("EMPTY ANIMATION")]
    [SerializeField]
    Color empty_color = new Color(1f, 0f, 0f, 1f);
    [SerializeField]
    string empty_string = "WARNING!\nSHIELDS NO CHARGE";
    [SerializeField]
    LeanTweenType empty_ease;
    [SerializeField]
    float empty_time = 0.5f;
    [SerializeField]
    float emptyScaleFactor = 1.1f;
    Vector3 empty_scale;

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

        divider_img = divider.GetComponent<Image>();

        value_txt = value.GetComponent<Text>();
        status_txt = status.GetComponent<Text>();

        //initial values
        position_original = rt.anchoredPosition;
        scale_original = rt.localScale;

        tickCount = ticks.Length;
        empty_scale = status.localScale * emptyScaleFactor;

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        InitializeSystem();

        int _energy = (int)(ShipData.ShieldsEnergy * 100.0f);
        int _hundredths = _energy % 100;
        string _value = (string.Format("{0:00}", "100", _energy, _hundredths)) + "%";
        value_txt.text = _value;

        for (int i = 0; i < tickCount; ++i)
        {
            ticks[i].ToggleAll(true);
        }

        previousEnergy = ShipData.ShieldsEnergy;
        currentTick = ticks[(ShipData.ShieldTicks - 1)];
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_SHIELD_EMPTY>(ShieldsEmpty);
        Events.instance.AddListener<EVENT_SHIELD_STATUS_OPERATIONAL>(ShieldsNormal);
        Events.instance.AddListener<EVENT_UPDATE_HUD_SHIELD_DECREASED>(DecreaseShield);
        Events.instance.AddListener<EVENT_UPDATE_HUD_SHIELD_INCREASED>(IncreaseShield);
        Events.instance.AddListener<EVENT_UPDATE_HUD_SHIELD_ENERGY>(UpdateShieldEnergy);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeSystem()
    {
        if (cg != null)
        {
            //cg.alpha = alpha_empty;
        }
        rt.anchoredPosition = new Vector3(position_original.x + 750f,
                                          position_original.y,
                                          position_original.z);

        LeanTween.move(rt, position_original, initialize_time)
                 .setEase(initialize_ease)
                 .setDelay(initialize_delay);

        //LeanTween.alphaCanvas(cg, alpha_highlight, initialize_time)
        //         .setEase(initialize_ease)
        //         .setDelay(initialize_delay);

        //LeanTween.alphaCanvas(cg, alpha_normal, initialize_time)
        //         .setEase(initialize_ease)
        //         .setDelay(initialize_delay + initialize_time);
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
        int _energy = (int)(ShieldSystem.energy * 100.0f);
        int _hundredths = _energy % 100;
        string _value = (string.Format("{0:00}", _energy, _hundredths)) + "%";
        value_txt.text = _value;
#if false
        UpdateTesting();
#endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateShieldEnergy(EVENT_UPDATE_HUD_SHIELD_ENERGY _event)
    {
        //Debug.Log("UpdateShieldEnergy(" + _event.energy + ", " + _event.index + ", " + _event.perShield + ")");

        //correct energy level for each tick of shield
        float _value = 0f;
        if(_event.index == 1)
        {
            _value = ((_event.energy - _event.perShield) * 2);
        }
        else if(_event.index == 0)
        {
            _value = _event.energy * 2;
        }
        //perform the image fill
        ticks[_event.index].UpdateGlowImageFill(_value);
        //divider_img.fillAmount = _value;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseShield(EVENT_UPDATE_HUD_SHIELD_DECREASED _event)
    {
        //ticks[_event.index].ToggleAll(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void IncreaseShield(EVENT_UPDATE_HUD_SHIELD_INCREASED _event)
    {
        //ticks[_event.index].ToggleAll(true);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ShieldsEmpty(EVENT_SHIELD_EMPTY _event)
    {
        LeanTween.cancel(status);

        status_txt.text = empty_string;

        LeanTween.textColor(status, empty_color, empty_time)
                 .setEase(empty_ease)
                 .setLoopType(LeanTweenType.pingPong);

        LeanTween.scale(status, empty_scale, empty_time)
                 .setEase(empty_ease)
                 .setLoopType(LeanTweenType.pingPong);
    }
    void ShieldsNormal(EVENT_SHIELD_STATUS_OPERATIONAL _event)
    {
        LeanTween.cancel(status);

        status_txt.text = op_string;

        LeanTween.textColor(status, op_color, op_time)
                 .setEase(op_ease)
                 .setLoopType(LeanTweenType.once);

        LeanTween.scale(status, scale_original, op_time)
                 .setEase(op_ease)
                 .setLoopType(LeanTweenType.once);
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
        Events.instance.RemoveListener<EVENT_SHIELD_STATUS_OPERATIONAL>(ShieldsNormal);
        Events.instance.RemoveListener<EVENT_SHIELD_EMPTY>(ShieldsEmpty);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_SHIELD_DECREASED>(DecreaseShield);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_SHIELD_INCREASED>(IncreaseShield);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_SHIELD_ENERGY>(UpdateShieldEnergy);
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
        //Keypad 0
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {

        }
        //Keypad 2
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {

        }
        //Keypad 3
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        //Keypad 4
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
        //Keypad 5
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {

        }
        //Keypad 6
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
    }
    #endregion
}