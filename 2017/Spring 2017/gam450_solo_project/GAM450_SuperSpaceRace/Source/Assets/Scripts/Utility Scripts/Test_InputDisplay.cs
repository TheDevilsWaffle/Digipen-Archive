///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Test_InputDisplay.cs
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

public class Test_InputDisplay : MonoBehaviour
{
    #region FIELDS
    [Header("MOUSE")]
    [SerializeField]
    RectTransform mStatus;
    Text mStatus_txt;
    [SerializeField]
    RectTransform mDirection;
    Text mDirection_txt;
    [SerializeField]
    RectTransform mPixelCoordinates;
    Text mPixelCoordinates_txt;
    [SerializeField]
    RectTransform mPercentageCoordinates;
    Text mPercentageCoordinates_txt;
    [SerializeField]
    RectTransform m1Status;
    Text m1Status_txt;
    [SerializeField]
    RectTransform m2Status;
    Text m2Status_txt;

    [Header("GAMEPAD")]
    [SerializeField]
    RectTransform gpStatus;
    Text gpStatus_txt;
    [SerializeField]
    RectTransform gpX;
    Text gpX_txt;
    [SerializeField]
    RectTransform gpY;
    Text gpY_txt;
    [SerializeField]
    RectTransform gpB;
    Text gpB_txt;
    [SerializeField]
    RectTransform gpA;
    Text gpA_txt;
    [SerializeField]
    RectTransform gpView;
    Text gpView_txt;
    [SerializeField]
    RectTransform gpMenu;
    Text gpMenu_txt;
    [SerializeField]
    RectTransform gpLeftAnalog;
    Text gpLeftAnalog_txt;
    [SerializeField]
    RectTransform gpRightAnalog;
    Text gpRightAnalog_txt;
    [SerializeField]
    RectTransform gpDpadLeft;
    Text gpDPadLeft_txt;
    [SerializeField]
    RectTransform gpDPadUp;
    Text gpDPadUp_txt;
    [SerializeField]
    RectTransform gpDpadRight;
    Text gpDPadRight_txt;
    [SerializeField]
    RectTransform gpDpadDown;
    Text gpDPadDown_txt;
    [SerializeField]
    RectTransform gpLeftBumper;
    Text gpLeftBumper_txt;
    [SerializeField]
    RectTransform gpRightBumper;
    Text gpRightBumper_txt;
    [SerializeField]
    RectTransform gpLeftTrigger;
    Text gpLeftTrigger_txt;
    [SerializeField]
    RectTransform gpRightTrigger;
    Text gpRightTrigger_txt;



    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        mPixelCoordinates_txt = mPixelCoordinates.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        mPercentageCoordinates_txt = mPercentageCoordinates.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        m1Status_txt = m1Status.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        m2Status_txt = m2Status.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        mStatus_txt = mStatus.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        mDirection_txt = mDirection.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();

        gpStatus_txt = gpStatus.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
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
        UpdateMouseInput();
	}
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMouseInput()
    {
        mPixelCoordinates_txt.text = MouseInput.PixelPosition.ToString();
        mPercentageCoordinates_txt.text = MouseInput.PercentagePositionCorrected.ToString();
        mStatus_txt.text = MouseInput.IsMouseActive.ToString();
        m1Status_txt.text = MouseInput.Mouse1.ToString();
        m2Status_txt.text = MouseInput.Mouse2.ToString();
        mDirection_txt.text = MouseInput.MouseCurrentDirection.ToString();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
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
