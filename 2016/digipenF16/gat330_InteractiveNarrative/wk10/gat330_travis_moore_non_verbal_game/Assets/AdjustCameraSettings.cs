///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — AdjustCameraSettings.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class AdjustCameraSettings : MonoBehaviour
{
    #region FIELDS
    public Camera camera;
    ColorCorrectionCurves ccc;
    public float saturation;
    public float followers;
    float stepping;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        ccc = camera.GetComponent<ColorCorrectionCurves>();
        Events.instance.AddListener<EVENT_FollowerDied>(UpdateColorSaturation);
    }
	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        ccc.saturation = saturation;
        ccc.UpdateParameters();

        stepping = saturation / followers;
	}
    #endregion

    #region METHODS
    void UpdateColorSaturation(EVENT_FollowerDied _event)
    {
        ccc.saturation -= stepping;
        ccc.UpdateParameters();
        //print("follower died, saturation is now at: " + ccc.saturation);
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_FollowerDied>(UpdateColorSaturation);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
