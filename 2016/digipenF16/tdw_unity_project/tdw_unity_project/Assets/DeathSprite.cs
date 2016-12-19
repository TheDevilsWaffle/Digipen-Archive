///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — DeathSprite.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class DeathSprite : MonoBehaviour
{
    #region FIELDS
    RectTransform rt;
    Outline ol;
    Image im;

    [Header("SCALE")]
    [SerializeField]
    float scaleFactor;
    Vector3 scaleEnd;
    Vector3 scaleStart;
    [SerializeField]
    LeanTweenType scaleEase;
    [SerializeField]
    float scaleTime;

    [Header("ALPHA")]
    [SerializeField]
    float startingAlpha = 0.5f;
    [SerializeField]
    LeanTweenType alphaOpaqueEase;
    [SerializeField]
    float alphaOpaqueTime;
    [SerializeField]
    float delayBeforeTransparent;
    [SerializeField]
    LeanTweenType alphaTransparentEase;
    [SerializeField]
    float alphaTransparentTime;
    float opaque = 1f;
    float transparent = 0f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        rt.localScale = Vector3.one;
        ol = GetComponent<Outline>();
        im = GetComponent<Image>();
        im.color = new Color(1f, 1f, 1f, 0f);
        scaleEnd = rt.localScale * scaleFactor;
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

    #region METHODS
    public void AnimateDeathSprite(Color _color)
    {
        im.color = _color;
        ol.effectColor = Color.white;
        AnimateScale();
        AnimateAlpha();
    }

    void AnimateScale()
    {
        LeanTween.scale(rt, scaleEnd, scaleTime).setEase(scaleEase);
    }
    void AnimateAlpha()
    {
        LeanTween.alpha(rt, startingAlpha, alphaOpaqueTime).setEase(alphaOpaqueEase);
        LeanTween.alpha(rt, transparent, alphaTransparentTime).setEase(alphaTransparentEase).setDelay(alphaOpaqueTime + delayBeforeTransparent);
        LeanTween.delayedCall(alphaOpaqueTime + delayBeforeTransparent + alphaTransparentTime, DestroyDeathSprite);
    }

    void DestroyDeathSprite()
    {
        Destroy(this.gameObject);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
