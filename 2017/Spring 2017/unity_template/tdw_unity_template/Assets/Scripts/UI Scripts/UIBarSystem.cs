///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UIBarSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using System;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class UIBarSystem : MonoBehaviour
{
    #region FIELDS
    [Header("BAR")]
    [SerializeField]
    bool overrideBarInInspector = true;
    [SerializeField]
    GameObject bar;
    CanvasGroup bar_cg;
    Image bar_img;
    [SerializeField]
    [Range(0f,1f)]
    float barOpacity;
    [SerializeField]
    Color barColor;
    
    [Header("BAR BACKGROUND")]
    [SerializeField]
    bool enableBarBackground = false;
    [SerializeField]
    GameObject barBackground;

    [Header("INFO")]
    [SerializeField]
    GameObject barInfoGroupContainer;
    BarInfoContainerController barInfoContainerController;

    [SerializeField]
    float infoOpacity;

    int sizeOfBarInfoContainer;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        if(barInfoGroupContainer == null && GameObject.Find("UI Bar/Bar Info Container").gameObject != null)
            barInfoGroupContainer = GameObject.Find("UI Bar/Bar Info Container").gameObject;

        barInfoContainerController = barInfoGroupContainer.GetComponent<BarInfoContainerController>();

        if (bar != null)
        {
            bar_cg = bar.GetComponent<CanvasGroup>();
            bar_img = bar.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("bar has not been assigned, please assign a gameobject with a CanvasGroup and Image component!");
        }

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_UI_BAR_UPDATE>(UpdateUIBar);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        InitializeBarSettings();
        SetBarBackground(enableBarBackground);
        sizeOfBarInfoContainer = barInfoContainerController.sizeOfBarInfoContainer;
        //Debug.Log("sizeOfBarInfoContainer is set to = " + sizeOfBarInfoContainer);
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
    
    }
    #endregion

    #region PUBLIC METHODS
    
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateUIBar(EVENT_UI_BAR_UPDATE _event)
    {
        //Debug.Log("UpdateUIBar()");
        
        int _index = sizeOfBarInfoContainer - 1;
        if (_index <= 0)
            return;
        //Debug.Log("_index is being set to sizeOfBarInfoContainer - 1, which = " + _index);

        //event has a list of uiBarInfos that we can apply to our UI bar
        foreach (UIBarInfo _uiBarInfo in _event.uiBarInfos)
        {
            //Debug.Log("_index is now " + _index);
            if (barInfoContainerController != null)
            {
                StartCoroutine(barInfoContainerController.SetUIBarGroupInfo(_uiBarInfo, _index));
                --_index;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeBarSettings()
    {
        if (overrideBarInInspector)
        {
            SetBarColor(barColor);
            SetBarOpacity(barOpacity);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_enable"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetBarBackground(bool _enable)
    {
        if (_enable)
        {
            barBackground.SetActive(true);
        }
        else
        {
            barBackground.SetActive(false);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_value"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetBarOpacity(float _value)
    {
        //ensure clamped value between 0 and 1
        bar_cg.alpha = Mathf.Clamp01(_value);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_color"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetBarColor(Color _color)
    {
        //ensure no transparency in the color
        if(_color.a < 1)
        {
            _color = new Color(_color.r, _color.g, _color.b, 1f);
        }

        bar_img.color = _color;
    }
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
