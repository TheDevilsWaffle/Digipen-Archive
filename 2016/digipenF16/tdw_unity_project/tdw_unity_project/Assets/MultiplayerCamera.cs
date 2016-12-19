///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MultiplayerCamera.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class MultiplayerCamera : MonoBehaviour
{
    #region FIELDS

    public Vector3 cameraOffset;
    public float dampTime;
    private const float DISTANCE_MARGIN = 1.0f;

    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenPlayers;
    private float cameraDistance;
    private float aspectRatio;
    private float fov;
    private float tanFov;

    Vector3 pos1;
    Vector3 pos2;
    Vector3 pos3;
    Vector3 pos4;

    Transform tr;
    Vector3 moveVelocity;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = this.gameObject.transform;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void LateUpdate()
    {
        CheckPlayers();
        // Position the camera in the center.
        Vector3 newCameraPos = Camera.main.transform.position;
        newCameraPos.x = middlePoint.x;
        Camera.main.transform.position = newCameraPos;

        // Find the middle point between players.
        middlePoint = FindMiddlePointBetweenAllPlayers();

        // Calculate the new distance.
        distanceBetweenPlayers = middlePoint.magnitude;
        cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

        // Set camera to new position.
        Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
        Vector3 _desiredPosition = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN) + cameraOffset;

        tr.position = Vector3.SmoothDamp(tr.position, _desiredPosition, ref moveVelocity, dampTime);


    }
    #endregion

    #region METHODS

    void CheckPlayers()
    {
        if (GameInitialize.activePlayers.Count > 0)
        {
            if (GameInitialize.activePlayers[0] != null)
            {
                pos1 = GameInitialize.activePlayers[0].transform.position;
            }
            else
            {
                pos1 = Vector3.zero;
            }

            if (GameInitialize.activePlayers.Count > 1)
            {
                pos2 = GameInitialize.activePlayers[1].transform.position;
            }
            else
            {
                pos2 = Vector3.zero;
            }

            if (GameInitialize.activePlayers.Count > 2)
            {
                pos3 = GameInitialize.activePlayers[2].transform.position;
            }
            else
            {
                pos3 = Vector3.zero;
            }

            if (GameInitialize.activePlayers.Count > 3)
            {
                pos4 = GameInitialize.activePlayers[3].transform.position;
            }
            else
            {
                pos4 = Vector3.zero;
            }
        }
    }

    Vector3 FindMiddlePointBetweenAllPlayers()
    {
        Vector3 _pos1_average = Vector3.zero;
        Vector3 _pos2_average = Vector3.zero;
        Vector3 _pos3_average = Vector3.zero;
        Vector3 _pos4_average = Vector3.zero;

        if (pos1 != Vector3.zero)
        {
            _pos1_average = pos1 * 0.25f;
        }
        if (pos2 != Vector3.zero)
        {
            _pos2_average = pos2 * 0.25f;
        }
        if (pos3 != Vector3.zero)
        {
            _pos3_average = pos3 * 0.25f;
        }
        if (pos4 != Vector3.zero)
        {
            _pos4_average = pos4 * 0.25f;
        }

        return (_pos1_average + _pos2_average + _pos3_average + _pos4_average);

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

}
