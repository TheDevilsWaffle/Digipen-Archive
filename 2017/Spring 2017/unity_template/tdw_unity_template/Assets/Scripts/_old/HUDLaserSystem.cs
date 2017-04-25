///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HUDLaserSystem.cs
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
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
public class EVENT_UPDATE_HUD_LASER_SHOT_INCREASED : GameEvent
{
    public int lasersTotal;
    public int index;
    public EVENT_UPDATE_HUD_LASER_SHOT_INCREASED(int _lasersTotal, int _index)
    {
        lasersTotal = _lasersTotal;
        index = _index;
    }
}
public class EVENT_UPDATE_HUD_LASER_SHOT_DECREASED : GameEvent
{
    public int lasersTotal;
    public int index;
    public EVENT_UPDATE_HUD_LASER_SHOT_DECREASED(int _lasersTotal, int _index)
    {
        lasersTotal = _lasersTotal;
        index = _index;
    }
}
public class EVENT_UPDATE_HUD_LASER_ENERGY : GameEvent
{
    public int index;
    public float energy;
    public float perLaser;
    public EVENT_UPDATE_HUD_LASER_ENERGY(int _index, float _energy, float _perLaser)
    {
        index = _index;
        energy = _energy;
        perLaser = _perLaser;
    }
}
#endregion

public class HUDLaserSystem : MonoBehaviour
{
    #region FIELDS
    RectTransform rt;
    [SerializeField]
    CanvasGroup cg;
    Vector3 position_original;
    Vector3 scale_original;

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
    int currentTick;
    int tickCount;

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
    string op_string = "LASERS";
    [SerializeField]
    LeanTweenType op_ease = LeanTweenType.easeOutBack;
    [SerializeField]
    float op_time = 0.5f;

    [Header("EMPTY ANIMATION")]
    [SerializeField]
    Color empty_color = new Color(1f, 0f, 0f, 1f);
    string empty_string = "WARNING!\nLASERS EMPTY";
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

        value_txt = value.GetComponent<Text>();
        status_txt = status.GetComponent<Text>();

        divider_img = divider.GetComponent<Image>();

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
        value_txt.text = "0" + ShipData.WeaponTicks.ToString();
        for (int i = 0; i < tickCount; ++i)
        {
            ticks[i].ToggleAll(true);
        }
        currentTick = tickCount - 1;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_LASER_STATUS_OPERATIONAL>(LasersNormal);
        Events.instance.AddListener<EVENT_UPDATE_HUD_LASER_SHOT_DECREASED>(DecreaseWeaponTicks);
        Events.instance.AddListener<EVENT_UPDATE_HUD_LASER_SHOT_INCREASED>(IncreaseWeaponTicks);
        Events.instance.AddListener<EVENT_UPDATE_HUD_LASER_ENERGY>(UpdateLaserEnergy);
        Events.instance.AddListener<EVENT_LASER_EMPTY>(LasersEmpty);
    }
    void InitializeSystem()
    {
        if (cg != null)
        {
            //cg.alpha = alpha_empty;
        }
        rt.anchoredPosition = new Vector3(position_original.x - 750f, 
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
    void UpdateLaserEnergy(EVENT_UPDATE_HUD_LASER_ENERGY _event)
    {
        //Debug.Log("UpdateLaserEnergy(" + _event.energy + ", " + _event.index + ", " + _event.perLaser + ")");

        //correct energy level for each tick of laser
        float _value = 0f;
        if (_event.index == 6)
        {
            _value = ((_event.energy - (_event.perLaser * 6)) * 7);
            //print("7, percentage = "+_value);
        }
        else if (_event.index == 5)
        {
            _value = ((_event.energy - (_event.perLaser * 5)) * 7);
            //print("6, percentage = " + _value);
        }
        else if (_event.index == 4)
        {
            _value = ((_event.energy - (_event.perLaser * 4)) * 7);
            //print("5, percentage = " + _value);
        }
        else if (_event.index == 3)
        {
            _value = ((_event.energy - (_event.perLaser * 3)) * 7);
            //print("4, percentage = " + _value);
        }
        else if (_event.index == 2)
        {
            _value = ((_event.energy - (_event.perLaser * 2)) * 7);
            //print("3, percentage = " + _value);
        }
        else if (_event.index == 1)
        {
            _value = ((_event.energy - _event.perLaser * 1) * 7);
            //print("2, percentage = " + _value);
        }
        else if (_event.index == 0)
        {
            _value = (_event.energy * 7);
            //print("1, percentage = " + _value);
        }
        //ensure that the ticks above it are empty
        if((_event.index + 1) <= 6)
        {
            //print(_event.index + 1);
            ticks[_event.index + 1].UpdateGlowImageFill(0);
        }
        //perform the image fill
        ticks[_event.index].UpdateGlowImageFill(_value);
        //divider_img.fillAmount = _value;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateLaserStatus(EVENT_LASER_STATUS_OPERATIONAL _event)
    {
        LeanTween.cancel(this.gameObject);

        status_txt.text = op_string;

        LeanTween.textColor(status, op_color, op_time)
                 .setEase(op_ease)
                 .setLoopType(LeanTweenType.once);

        LeanTween.scale(status, scale_original, op_time)
                 .setEase(op_ease)
                 .setLoopType(LeanTweenType.once);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LasersEmpty(EVENT_LASER_EMPTY _event)
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
    void LasersNormal(EVENT_LASER_STATUS_OPERATIONAL _event)
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
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecreaseWeaponTicks(EVENT_UPDATE_HUD_LASER_SHOT_DECREASED _event)
    {
        //Debug.Log("DecreaseWeaponTicks("+_event.lasersTotal+")");

        //ticks[(_event.index)].ToggleAll(false);
        value_txt.text = "0" + _event.lasersTotal.ToString();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void IncreaseWeaponTicks(EVENT_UPDATE_HUD_LASER_SHOT_INCREASED _event)
    {
        //Debug.Log("IncreaseWeaponTicks(" + _event.lasersTotal + ")");

        //ticks[(_event.index)].ToggleAll(true);
        value_txt.text = "0" + _event.lasersTotal.ToString();
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
        Events.instance.RemoveListener<EVENT_LASER_STATUS_OPERATIONAL>(LasersNormal);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_LASER_SHOT_DECREASED>(DecreaseWeaponTicks);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_LASER_SHOT_INCREASED>(IncreaseWeaponTicks);
        Events.instance.RemoveListener<EVENT_UPDATE_HUD_LASER_ENERGY>(UpdateLaserEnergy);
        Events.instance.RemoveListener<EVENT_LASER_EMPTY>(LasersEmpty);
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
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}