///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Base_AnimateUI.cs
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

public class Base_AnimateUI : MonoBehaviour
{
    #region FIELDS
    protected RectTransform rt;

    protected Vector3 rtWorldPosition;
    protected Vector3 rtLocalPosition;

    protected Vector3 rtLocalScale;

    protected Quaternion rtLocalRotation;
    protected Vector3 rtLocalRotationDegrees;
    float opaque = 1f;
    float transparent = 0.5f;
    float instant = 0.001f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Awake()
    {
        rt = GetComponent<RectTransform>();

        rtWorldPosition = rt.position;
        rtLocalPosition = rt.localPosition;

        rtLocalScale = rt.localScale;
        rtLocalRotation = rt.localRotation;
        rtLocalRotationDegrees = rt.localEulerAngles;

        opaque = 1f;
        transparent = 0.5f;
        instant = 0.001f;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected virtual void Start()
    {
	
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
