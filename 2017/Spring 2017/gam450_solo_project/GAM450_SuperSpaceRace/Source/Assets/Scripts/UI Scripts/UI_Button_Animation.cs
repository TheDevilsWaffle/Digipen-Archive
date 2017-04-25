///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_Button_Animation.cs
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

public class UI_Button_Animation : MonoBehaviour
{
    #region FIELDS
    float alpha_transparent = 0f;
    float alpha_opaque = 1f;

    [Header("PULSE IMAGE/TEXT")]
    [SerializeField]
    Color pulseColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    float pulseTime;
    [SerializeField]
    LeanTweenType pulseAC;
    [Header("PULSE SCALE IMAGE/TEXT")]
    [SerializeField]
    Vector3 pulseScale = new Vector3(1f, 1f, 1f);

    Vector3 localPosition_original;
    Vector3 localScale_original;
    Quaternion localRotation_original;

    protected Transform tr;
    protected RectTransform rt;
    protected Text txt;

    int id_pulseText;
    int id_pulseScale;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = transform;
        rt = GetComponent<RectTransform>();
        if (GetComponent<Text>())
            txt = GetComponent<Text>();

        localPosition_original = tr.localPosition;
        localScale_original = tr.localScale;
        localRotation_original = tr.localRotation;
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
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Animate_PulseImage()
    {
        LeanTween.color(rt, pulseColor, pulseTime)
                 .setLoopType(LeanTweenType.pingPong)
                 .setEase(pulseAC);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Animate_PulseText()
    {
        id_pulseText = LeanTween.colorText(rt, pulseColor, pulseTime)
                                .setLoopType(LeanTweenType.pingPong)
                                .setEase(pulseAC)
                                .id;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_color"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Cancel_Animate_PulseText(Color _color)
    {
        LeanTween.cancel(id_pulseText);
        txt.color = _color;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Cancel_Animate_PulseScale()
    {
        LeanTween.cancel(id_pulseScale);
        rt.localScale = localScale_original;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Animate_PulseScale()
    {
        id_pulseScale = LeanTween.scale(rt, pulseScale, pulseTime)
                                 .setLoopType(LeanTweenType.pingPong)
                                 .setEase(pulseAC)
                                 .id;
    }
    #endregion
}
