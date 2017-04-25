///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — FreeCursorSystem.cs
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

public class FreeCursorSystem : MonoBehaviour
{
    #region FIELDS
    public GameObject cursor;
    RectTransform cursor_rt;
    Vector3 cursorPos;
    Vector3 auxVec3 = new Vector3(0, 0, 0);
    [Header("MOVEMENT")]
    float initialVelocity = 0f;
    float finalVelocity = 9f;
    float currentVelocity = 0f;
    float accelerationRate = 6f;
    float decelerationRate = 6f;
    public AnimationCurve accelerationCurve;
    public AnimationCurve decelerationCurve;
    float accelerationTimer = 0f;
    float decelerationTimer = 0f;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        currentVelocity = 0f;
        cursor_rt = cursor.GetComponent<RectTransform>();
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
        //Vector3 _screenPos = Camera.main.WorldToScreenPoint(cursor.transform.position);
        if(GamePadInput.players[0].LeftAnalogStick_Status == GamePadButtonState.HELD)
        {
            Mathf.Clamp01(accelerationTimer += Time.deltaTime);
            decelerationTimer = 0f;
            AccelerateCursor();
        }
        else
        {
            Mathf.Clamp01(decelerationTimer += Time.deltaTime);
            accelerationTimer = 0f;
            DecelerateCursor();
        }

        UpdateCursorPosition(GamePadInput.players[0].LeftAnalogStick);

        


	}
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AccelerateCursor()
    {
        currentVelocity = currentVelocity + (accelerationCurve.Evaluate(accelerationTimer) * accelerationRate);
        //Debug.Log("AccelerateCursor() currentVelocity = " + currentVelocity);
        //Debug.Log("AccelerationCurve = " + accelerationCurve.Evaluate(Time.deltaTime) + " * " + accelerationRate);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DecelerateCursor()
    {
        currentVelocity = currentVelocity - (decelerationCurve.Evaluate(decelerationTimer) * decelerationRate);
        //Debug.Log("DecelerateCursor() currentVelocity = " + currentVelocity);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_stick"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateCursorPosition(Vector3 _stick)
    {
        currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, finalVelocity);
        cursor_rt.Translate((_stick.x * currentVelocity), (_stick.y * currentVelocity), 0f);
        //Vector3 pos = Camera.main.WorldToViewportPoint(cursor_rt.position);
        //pos.x = Mathf.Clamp(pos.x, 0f, Screen.width);
        //print("screen.width = " + Screen.width + ", and pos.x = " + pos.x);
        //pos.y = Mathf.Clamp(pos.y, 0f, Screen.height);
        //print("screen.height = " + Screen.height + ", and pos.y = " + pos.y);
        //cursor_rt.localPosition = new Vector3(pos.x, pos.y, cursor_rt.localPosition.z);


        //WHERE I LEFT OFF

        /*
         * need to keep cursor within screen bounds. Right now it has free reign. Acceleration is working
         just fine, needs value tweaks, but is working as intended. Need to detect its position over other
         objects. Abandonning this right now for camera tilt.
         * */
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
