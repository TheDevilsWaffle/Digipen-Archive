///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MenuCameraZoom.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_CAMERA_ZOOM_COMPLETE : GameEvent
{
    public EVENT_CAMERA_ZOOM_COMPLETE() { }
}
#endregion

public class MenuCameraZoom : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    GameObject menuCamera;
    [SerializeField]
    CameraMenuTilt cameraMenuTilt;
    [SerializeField]
    LeanTweenPath bezierPath;
    Vector3[] bezierPathReverse;
    [SerializeField]
    float time = 1.5f;
    [SerializeField]
    float delay = 0.2f;
    [SerializeField]
    bool alignToPath = false;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInOutQuint;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        bezierPathReverse = new Vector3[4];
        ExtractPathNodes(bezierPath.vec3);
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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            TranslateCameraForwards();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TranslateCameraBackwards();
        }
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void TranslateCameraForwards()
    {
        //menuCamera.transform.position = startOfPath;
        LeanTween.move(menuCamera, bezierPath.vec3, time)
                 .setOnComplete(CameraZoomComplete)
                 .setOrientToPath(alignToPath)
                 .setDelay(delay)
                 .setEase(ease);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void TranslateCameraBackwards()
    {
        //menuCamera.transform.position = endOfPath;
        LeanTween.move(menuCamera, bezierPathReverse, time)
                 .setOnComplete(CameraZoomComplete)
                 .setOrientToPath(alignToPath)
                 .setDelay(delay)
                 .setEase(ease);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_nodes"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ExtractPathNodes(Vector3[] _nodes)
    {
        int _index = 3;
        foreach(Vector3 _node in bezierPath.vec3)
        {
            bezierPathReverse[_index] = _node;
            --_index;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CameraZoomComplete()
    {
        Events.instance.Raise(new EVENT_CAMERA_ZOOM_COMPLETE());
    }
    #endregion

    #region OnDestroy
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
