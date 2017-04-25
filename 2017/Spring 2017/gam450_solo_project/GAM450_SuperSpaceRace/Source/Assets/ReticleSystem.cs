///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ReticleSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using System.Collections;
using System.Collections.Generic;

#region ENUMS
public enum TargetStatus
{
    NO_TARGET,
    LOCKED
};
#endregion

#region EVENTS
/*
public class EVENT_EXAMPLE
{
    public EVENT_EXAMPLE() { }
}
*/ 
#endregion

public class ReticleSystem : MonoBehaviour
{
    #region FIELDS
    RectTransform rt;
    Vector3 rt_scale;
    List<Image> reticleImages;
    [SerializeField]
    float[] alphasNormal;

    [Header("CANNON")]
    [SerializeField]
    RectTransform cannon;
    Image cannon_img;
    [SerializeField]
    RectTransform cannonOutline;
    Image cannonOutline_img;
    Color cannonOutline_color;
    [SerializeField]
    RectTransform cannonGlow;
    Image cannonGlow_img;

    [Header("INNER TRI")]
    [SerializeField]
    RectTransform innerTri;
    Image innerTri_img;
    Vector3 innerTri_scale;
    Color innerTri_color;

    [Header("RING")]
    [SerializeField]
    RectTransform ring;
    Image ring_img;
    AutoMoveAndRotate ring_amr;
    [SerializeField]
    Vector3 ring_normalSpeed;
    [SerializeField]
    Vector3 ring_targetSpeed;
    Color ring_normalColor;

    [Header("ANIMATION - TARGET PULSE")]
    [SerializeField]
    LeanTweenType tpEase;
    [SerializeField]
    float tpTime;
    [SerializeField]
    float fireTime;
    [SerializeField]
    float tpScaleFactor;
    Vector3 tpScale;
    [SerializeField]
    Color targetColor;
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
        cannon_img = cannon.GetComponent<Image>();
        cannonOutline_img = cannonOutline.GetComponent<Image>();
        cannonGlow_img = cannonGlow.GetComponent<Image>();
        innerTri_img = innerTri.GetComponent<Image>();
        ring_img = ring.GetComponent<Image>();
        ring_amr = ring.GetComponent<AutoMoveAndRotate>();

        //initial values
        rt_scale = cannonOutline.localScale;
        innerTri_scale = innerTri.localScale;
        tpScale = innerTri.localScale * tpScaleFactor;
        reticleImages = new List<Image>() { };
        reticleImages.Add(cannon_img);
        reticleImages.Add(cannonOutline_img);
        reticleImages.Add(cannonGlow_img);
        reticleImages.Add(innerTri_img);
        reticleImages.Add(ring_img);

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        SetAlphasNormal();
        ring_normalColor = ring_img.color;
        innerTri_color = innerTri_img.color;
        cannonOutline_color = cannonOutline_img.color;
        ToggleRingRotation(true);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_CANNON_FIRED>(FireCannon);
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
    void SetAlphasNormal()
    {
        for (int i = 0; i < reticleImages.Count; ++i)
        {
            Color _color = reticleImages[i].color;
            reticleImages[i].color = new Color(_color.r, _color.g, _color.b, alphasNormal[i]);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleRingRotation(bool _state)
    {
        ring_amr.enabled = _state;
        ring_amr.rotateDegreesPerSecond.value = ring_normalSpeed;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ToggleTargetStatus(TargetStatus _status)
    {
        if(_status == TargetStatus.LOCKED)
        {
            ring_img.color = targetColor;
            ring_amr.rotateDegreesPerSecond.value = ring_targetSpeed;
            innerTri_img.color = targetColor;
            LeanTween.scale(innerTri, tpScale, tpTime)
                     .setEase(tpEase)
                     .setLoopType(LeanTweenType.pingPong);
            cannonGlow_img.color = Color.white;
        }
        else
        {
            innerTri_img.color = innerTri_color;
            LeanTween.cancel(innerTri.gameObject);
            LeanTween.scale(innerTri, innerTri_scale, tpTime)
                     .setEase(tpEase)
                     .setLoopType(LeanTweenType.once);
            cannonGlow_img.color = new Color(0f, 0f, 0f, 0f);
            ring_img.color = ring_normalColor;
            ring_amr.rotateDegreesPerSecond.value = ring_normalSpeed;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FireCannon(EVENT_CANNON_FIRED _event)
    {
        //LeanTween.cancel(rt.gameObject);
        //LeanTween.scale(rt, tpScale, fireTime)
        //         .setEase(LeanTweenType.easeOutBack);
        //LeanTween.scale(rt, rt_scale, fireTime)
        //         .setEase(LeanTweenType.easeInBack)
        //         .setDelay(fireTime);
        LeanTween.cancel(innerTri.gameObject);
        LeanTween.scale(innerTri, tpScale * 1.25f, fireTime/2)
                 .setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(innerTri, rt_scale, fireTime/2)
                 .setEase(LeanTweenType.easeInBack)
                 .setDelay(fireTime);
        LeanTween.cancel(ring.gameObject);
        LeanTween.color(ring, targetColor, fireTime)
                 .setEase(tpEase);
        LeanTween.color(ring, ring_normalColor, fireTime)
                 .setEase(tpEase)
                 .setDelay(tpTime);
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
        Events.instance.RemoveListener<EVENT_CANNON_FIRED>(FireCannon);
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
            ToggleTargetStatus(TargetStatus.LOCKED);
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ToggleTargetStatus(TargetStatus.NO_TARGET);
        }
        //Keypad 2
        if (Input.GetKeyDown(KeyCode.Keypad2))
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