/*////////////////////////////////////////////////////////////////////////
//SCRIPT: cs_HealthSlider.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cs_HealthSlider : MonoBehaviour
{
    #region PROPERTIES

    //references
    [Header("FOREGROUND SLIDER")]
    public Slider FSlider;
    public float FSlider_AnimateTime;

    [Header("BACKGROUND SLIDER")]
    public Slider BSlider;
    public float BSlider_AnimateDelay;
    public float BSlider_AnimateTime;

    //attributes
    private float MaximumValue;
    private float MinimumValue;
    private float CurrentValue;


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize slider attributes
        MaximumValue = FSlider.maxValue;
        MinimumValue = FSlider.minValue;

        BSlider.maxValue = MaximumValue;
        BSlider.minValue = MinimumValue;

        CurrentValue = FSlider.value;
        BSlider.value = CurrentValue;
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ChangeSliderValue(-25f);
    }

    #endregion

    #region METHODS

    /*////////////////////////////////////////////////////////////////////
    //ChangeSliderValue()
    ////////////////////////////////////////////////////////////////////*/
    void ChangeSliderValue(float newValue_)
    {
        CurrentValue = (int)(CurrentValue + newValue_);

        AnimateForegroundSlider();
        AnimateBackgroundSlider();
    }

    /*////////////////////////////////////////////////////////////////////
    //SetSliderValue()
    ////////////////////////////////////////////////////////////////////*/
    void SetSliderValue(float setValue_)
    {
        CurrentValue = (int)setValue_;
        FSlider.value = CurrentValue;
    }

    /*////////////////////////////////////////////////////////////////////
    //AnimateForegroundSlider()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateForegroundSlider()
    {
        LeanTween.value(FSlider.gameObject, 
                        SetForegroundSliderValue, 
                        FSlider.value, 
                        CurrentValue, 
                        FSlider_AnimateTime);
    }

    /*////////////////////////////////////////////////////////////////////
    //SetForegroundSliderValue()
    ////////////////////////////////////////////////////////////////////*/
    void SetForegroundSliderValue(float value_)
    {
        FSlider.value = value_;
    }

    /*////////////////////////////////////////////////////////////////////
    //AnimateBackgroundSlider()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateBackgroundSlider()
    {
        LeanTween.value(BSlider.gameObject, 
                        SetBackgroundSliderValue, 
                        BSlider.value, 
                        CurrentValue, 
                        BSlider_AnimateTime).setDelay(BSlider_AnimateDelay); 
    }

    /*////////////////////////////////////////////////////////////////////
    //SetBackgroundSliderValue()
    ////////////////////////////////////////////////////////////////////*/
    void SetBackgroundSliderValue(float value_)
    {
        BSlider.value = value_;
    }

    #endregion
}