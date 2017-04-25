///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ValueBounce.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class ValueBounce : MonoBehaviour
{
    #region FIELDS
    [Header("RANDOMNESS")]
    [SerializeField]
    bool isRandom;
    [SerializeField]
    bool flipPositiveNegative;
    bool isPositive;
    [Header("VALUE (if not random use min)")]
    [SerializeField]
    float valueMin;
    [SerializeField]
    float valueMax;
    float value;
    [Header("TIME (if not random use min)")]
    [SerializeField]
    float timeMin;
    [SerializeField]
    float timeMax;
    float time;
    [Header("DELAY")]
    [SerializeField]
    float delayMin;
    [SerializeField]
    float delayMax;
    float delay;
    [Header("EASING")]
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInOutBounce;
    [SerializeField]
    VignetteAndChromaticAberration vca;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {

    }
	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        StartValueBounce();
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
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void StartValueBounce()
    {
        DetermineValues();
        ValueBounceAnimation();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineValues()
    {
        if (isRandom)
        {
            SetRandomValues();
        }
        else
        {
            value = valueMin;
            time = timeMin;
            delay = delayMin;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetRandomValues()
    {
        //Debug.Log("flipped! now we are " + isPositive);

        value = RandomFloat(valueMin, valueMax);
        time = RandomFloat(timeMin, timeMax);
        delay = RandomFloat(delayMin, delayMax);

        if(flipPositiveNegative)
        {
            if(isPositive)
            {
                Mathf.Abs(value);
                isPositive = false;
            }
            else
            {
                isPositive = true;
                if (value <= 0)
                {
                    return;
                }
                else
                {
                    value *= -1f;
                }
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    float RandomFloat(float _min, float _max)
    {
        return Random.Range(_min, _max);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ValueBounceAnimation()
    {
        float _currentValue = vca.chromaticAberration;
        LeanTween.value(this.gameObject, UpdateChromaticAberration, _currentValue, value, time)
                 .setDelay(delay).setEase(ease)
                 .setOnComplete(StartValueBounce);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateChromaticAberration(float _value)
    {
        //Debug.Log("value = " + _value);

        vca.chromaticAberration = _value;
    }
    #endregion

    #region ONDESTROY
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
