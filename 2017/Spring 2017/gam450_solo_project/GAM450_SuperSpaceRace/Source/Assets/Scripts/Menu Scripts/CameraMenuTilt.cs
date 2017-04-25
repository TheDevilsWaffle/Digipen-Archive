///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — CameraMenuTilt.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class CameraMenuTilt : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    Camera menuCamera;
    Transform menuCamera_tr;
    [Header("POSITION")]
    //Vector3 originalPosition;
    //Vector3 updatedPosition;
    //[SerializeField]
    //float xShift;
    //float maxXShift;
    //float minXShift;
    //[SerializeField]
    //float yShift;
    //float maxYShift;
    //float minYShift;
    //float zPosition;
    [Header("ROTATION")]
    Quaternion originalRotation;
    Vector3 updatedRotation;
    [SerializeField]
    float xTilt;
    float maxXTilt;
    float minXTilt;
    [SerializeField]
    float yTilt;
    float maxYTilt;
    float minYTilt;
    float zRotation;
    [SerializeField]
    float time = 1f;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInOutQuad;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        menuCamera_tr = menuCamera.transform;
        //originalPosition = menuCamera_tr.position;
        originalRotation = menuCamera_tr.rotation;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        InitializeValues();
    }
    void InitializeValues()
    {
        //x shifts
        //minXShift = originalPosition.x - xShift;
        //maxXShift = originalPosition.x + xShift;

        //y shifts
        //minYShift = originalPosition.y - yShift;
        //maxYShift = originalPosition.y + yShift;

        //zPosition = originalPosition.z;

        //x tilts
        maxXTilt = originalRotation.x + xTilt;
        minXTilt = originalRotation.x - xTilt;

        //y tilts
        maxYTilt = originalRotation.y + yTilt;
        minYTilt = originalRotation.y - yTilt;

        zRotation = originalRotation.z;
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
        
        //ShiftCameraPosition(MouseInput.PercentagePositionCorrected);
        TiltCamera(MouseInput.PercentagePositionCorrected);
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //void ShiftCameraPosition(Vector3 _mouse)
    //{
    //    float _xShift = Mathf.Clamp(originalPosition.x + (_mouse.x * xShift), minXShift, maxXShift);
    //    float _yShift = Mathf.Clamp(originalPosition.y + (_mouse.y * yShift), minYShift, maxYShift);
    //    updatedPosition = new Vector3(_xShift, _yShift, zPosition);
    //    //Debug.Log("_xShift = " + _xShift + " and _yShift = " + _yShift);

    //    LeanTween.move(menuCamera.gameObject, updatedPosition, time).setEase(ease);
    //}
    void TiltCamera(Vector3 _mouse)
    {
        float _xTilt = Mathf.Clamp(originalRotation.x + (_mouse.x * xTilt), minXTilt, maxXTilt);
        float _yTilt = Mathf.Clamp(originalRotation.y + (_mouse.y * yTilt), minYTilt, maxYTilt);
        updatedRotation = new Vector3(originalRotation.y + _yTilt, originalRotation.x + _xTilt, originalRotation.z);

        LeanTween.rotate(menuCamera.gameObject, updatedRotation, (time * Time.deltaTime)).setEase(ease);
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
