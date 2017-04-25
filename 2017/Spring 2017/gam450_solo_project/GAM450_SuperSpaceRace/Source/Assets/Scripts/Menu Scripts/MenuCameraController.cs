///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MenuCameraController.cs
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

public class MenuCameraController : MonoBehaviour
{
    #region FIELDS
    [Header("Menu Camera")]
    [SerializeField]
    GameObject menuCamera;
    [SerializeField]
    CameraMenuTilt cmt;
    [Header("Starting Canvas")]
    [SerializeField]
    Transform startingCanvas;
    [Header("Hyperspace Particle System")]
    [SerializeField]
    ParticleSystem hyperspace;
    [Header("SFX")]
    [SerializeField]
    AudioClip sfx_hyperspace;
    [Header("Camera Paths/Nodes")]
    [SerializeField]
    MenuCameraZoom startToMain;
    MenuCameraZoom mainToStart;
    [SerializeField]
    MenuCameraZoom mainToControls;
    [SerializeField]
    MenuCameraZoom mainToCredits;
    [SerializeField]
    MenuCameraZoom mainToOptions;
    [SerializeField]
    MenuCameraZoom mainToQuit;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        SetSubscriptions();
        cmt = GetComponent<CameraMenuTilt>();
        if (hyperspace == null && GameObject.Find("Particle System-Hyperspace").GetComponent<ParticleSystem>() != null)
            hyperspace = GameObject.Find("Particle System-Hyperspace").GetComponent<ParticleSystem>();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        menuCamera.transform.LookAt(startingCanvas);
        ToggleHyperspace(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_CAMERA_ZOOM_START>(StartCameraZoom);
        Events.instance.AddListener<EVENT_CAMERA_ZOOM_COMPLETE>(ReenableCameraMenuTilt);
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
    void StartCameraZoom(EVENT_CAMERA_ZOOM_START _event)
    {
        //Debug.Log("StartCameraZoom("+ _event.menuToLoad + ", backwards = " + _event.backwards +")");
        DisableCameraMenuTilt();
        ToggleHyperspace(true);
        AudioSystem.Instance.MakeAudioSource("sfx_hyperspace");
        switch (_event.menuToLoad)
        {
            case MenuToLoad.UNASSIGNED:
                break;
            case MenuToLoad.START:
                if (startToMain != null && _event.backwards)
                {
                    startToMain.TranslateCameraBackwards();
                }
                break;
            case MenuToLoad.MAIN:
                if (startToMain != null && !_event.backwards)
                {
                    startToMain.TranslateCameraForwards();
                }
                if (mainToQuit != null && _event.backwards)
                {
                    mainToQuit.TranslateCameraBackwards();
                }
                break;
            case MenuToLoad.CONTROLS:
                if (mainToControls != null && !_event.backwards)
                {
                    mainToControls.TranslateCameraForwards();
                }
                if (mainToControls != null && _event.backwards)
                {
                    mainToControls.TranslateCameraBackwards();
                }
                break;
            case MenuToLoad.CREDITS:
                if (mainToCredits != null && !_event.backwards)
                {
                    mainToCredits.TranslateCameraForwards();
                }
                if (mainToCredits != null && _event.backwards)
                {
                    mainToCredits.TranslateCameraBackwards();
                }
                break;
            case MenuToLoad.CODA_MAINMENU_QUITGAME:
                if (mainToQuit != null && !_event.backwards)
                {
                    mainToQuit.TranslateCameraForwards();
                }
                if (mainToQuit != null && _event.backwards)
                {
                    mainToQuit.TranslateCameraBackwards();
                }
                break;
            default:
                break;
        }
    }
    void ToggleHyperspace(bool _enable)
    {
        if (hyperspace != null)
        {
            hyperspace.Play(_enable);
            hyperspace.enableEmission = _enable;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DisableCameraMenuTilt()
    {
        if(cmt != null)
            cmt.enabled = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void EnableCameraMenuTilt()
    {
        cmt.enabled = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_event"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ReenableCameraMenuTilt(EVENT_CAMERA_ZOOM_COMPLETE _event)
    {
        EnableCameraMenuTilt();
        ToggleHyperspace(false);
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_CAMERA_ZOOM_COMPLETE>(ReenableCameraMenuTilt);
    }
    #endregion
}
