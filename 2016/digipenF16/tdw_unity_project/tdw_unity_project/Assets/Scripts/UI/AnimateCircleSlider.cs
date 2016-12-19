///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — AnimateCircleSlider.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
public enum Status_CircleSlider
{
    READY,
    ANIMATING,
};
#endregion

#region EVENTS

#endregion

public class AnimateCircleSlider : Base_AnimateUI
{
    #region FIELDS
    Image image;
    float currentValue;
    float full = 1f;
    float empty = 0f;
    Status_CircleSlider status;
    LeanTweenType ease;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void Start()
    {
        base.Start();
        currentValue = image.fillAmount;
        full = 1f;
        empty = 0f;
        status = Status_CircleSlider.READY;
	}
    #endregion

    #region METHODS
    void SetStatusRecharged()
    {
        status = Status_CircleSlider.READY;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_value"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetValue(float _value)
    {
        currentValue = Mathf.Clamp01(_value);
        image.fillAmount = currentValue;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_time"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RechargeToFull(float _time)
    {
        status = Status_CircleSlider.ANIMATING;
        LeanTween.value(this.gameObject, SetValue, image.fillAmount, full, _time).setEase(ease);
        LeanTween.delayedCall(_time, SetStatusRecharged);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rechargeTime"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EmptyThenRechargeToFull(float _rechargeTime)
    {
        SetValue(0f);
        RechargeToFull(_rechargeTime);
    }
    #endregion
}
