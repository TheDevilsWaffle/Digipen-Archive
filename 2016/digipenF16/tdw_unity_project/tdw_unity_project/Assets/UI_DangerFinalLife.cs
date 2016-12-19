///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — UI_DangerFinalLife.cs
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

public class UI_DangerFinalLife : MonoBehaviour
{
    #region FIELDS
    Text txt;
    RectTransform rt;
    [SerializeField]
    string warning = " DANGER!\nLAST LIFE!";
    string playerNumber;


    [Header("SCALE")]
    [SerializeField]
    float scaleFactor;
    Vector3 scaleEnd;
    Vector3 scaleStart;
    [SerializeField]
    LeanTweenType scaleEase;
    [SerializeField]
    float scaleTime;

    [Header("POSITION")]
    Vector3 positionEnd;
    [SerializeField]
    LeanTweenType positionEase;
    [SerializeField]
    float positionTime;

    [Header("ALPHA")]
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
        txt = GetComponent<Text>();
        rt = GetComponent<RectTransform>();
        txt.color = new Color(1f, 1f, 1f, 0f);

        scaleStart = rt.localScale;
        scaleEnd = scaleStart * scaleFactor;
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
    public void AnimateDangerText(Color _color, Vector3 _startPosition)
    {
        rt.position = _startPosition;
        //positionEnd = new Vector3(_startPosition.x, _startPosition.y + _yOffset, _startPosition.z);
        //print(_startPosition + " and end position is " + positionEnd);
        txt.color = _color;
        AnimateScale();
        //AnimatePosition();
        AnimateAlpha();
    }
    void AnimateScale()
    {
        LeanTween.scale(rt, scaleEnd, scaleTime).setEase(scaleEase);
    }
    void AnimatePosition()
    {
        LeanTween.move(rt, positionEnd, positionTime).setEase(positionEase);
    }
    void AnimateAlpha()
    {
        LeanTween.alphaText(rt, opaque, alphaOpaqueTime).setEase(alphaOpaqueEase);
        LeanTween.alphaText(rt, transparent, alphaTransparentTime).setEase(alphaTransparentEase).setDelay(alphaOpaqueTime + delayBeforeTransparent);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
