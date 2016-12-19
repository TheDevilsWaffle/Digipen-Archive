///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LoadScreenController.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region ENUMS
#endregion

#region EVENTS
public class EVENT_Load_Screen : GameEvent
{
    public EVENT_Load_Screen(){}
}
#endregion

public class LoadScreenController : MonoBehaviour
{
    #region FIELDS

    [Header("LOAD SCREEN PARTS")]
    [SerializeField]
    RectTransform topLeft;
    Vector3 tl_originalPosition;
    [SerializeField]
    RectTransform topRight;
    Vector3 tr_originalPosition;
    [SerializeField]
    RectTransform bottomLeft;
    Vector3 bl_originalPosition;
    [SerializeField]
    RectTransform bottomRight;
    Vector3 br_originalPosition;

    [Header("ANIMATION")]
    [SerializeField]
    LeanTweenType ease;
    [SerializeField]
    LeanTweenType easeOut;
    [SerializeField]
    float time;
    [SerializeField]
    float delay;

    float transparent = 0f;
    float opaque = 1f;
    float instant = 0.001f;

    string scene;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tl_originalPosition = topLeft.GetComponent<RectTransform>().localPosition;
        tr_originalPosition = topRight.GetComponent<RectTransform>().localPosition;
        bl_originalPosition = bottomLeft.GetComponent<RectTransform>().localPosition;
        br_originalPosition = bottomRight.GetComponent<RectTransform>().localPosition;

        Events.instance.AddListener< EVENT_Load_Screen>(StartLoadScreenAnimation);
        transparent = 0f;
        opaque = 1f;
        instant = 0.001f;
    }
    void Start()
    {
        RevealScreen();
    }

    #endregion

    #region METHODS
    public void RevealScreen()
    {
        LeanTween.move(topLeft, Vector3.zero, instant);
        LeanTween.move(topRight, Vector3.zero, instant);
        LeanTween.move(bottomLeft, Vector3.zero, instant);
        LeanTween.move(bottomRight, Vector3.zero, instant);

        LeanTween.move(topLeft, tl_originalPosition, time).setEase(easeOut).setDelay(0.25f);
        LeanTween.move(topRight, tr_originalPosition, time).setEase(easeOut).setDelay(0.25f);
        LeanTween.move(bottomLeft, bl_originalPosition, time).setEase(easeOut).setDelay(0.25f);
        LeanTween.move(bottomRight, br_originalPosition, time).setEase(easeOut).setDelay(0.25f);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void StartLoadScreenAnimation(EVENT_Load_Screen _event)
    {
        LeanTween.move(topLeft, Vector3.zero, time).setEase(ease);
        LeanTween.move(topRight, Vector3.zero, time).setEase(ease);
        LeanTween.move(bottomLeft, Vector3.zero, time).setEase(ease);
        LeanTween.move(bottomRight, Vector3.zero, time).setEase(ease);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Load_Screen>(StartLoadScreenAnimation);
    }

    #endregion

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.KeypadEnter))
    //    {
    //        StartLoadScreenAnimation();
    //    }
    //}
}
