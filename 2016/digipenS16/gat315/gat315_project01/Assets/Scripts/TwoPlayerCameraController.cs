/*////////////////////////////////////////////////////////////////////////
//SCRIPT: TwoPlayerCameraController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class TwoPlayerCameraController : MonoBehaviour {

    #region PROPERTIES

    //camera final position offset
    public float CamOffsetX;
    public float CamOffsetY;
    public float CamOffsetZ; 

    //players to track
    GameObject Player1;
    GameObject Player2;

    Camera TrackingCamera;

    //mid point between
    Vector3 VectorBetweenP1AndP2;

    //used as ref
    Vector3 velocity = Vector3.zero;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //find the players
        this.Player1 = GameObject.FindWithTag("Player1").gameObject;
        this.Player2 = GameObject.FindWithTag("Player2").gameObject;

        //get the camera component
        this.TrackingCamera = this.gameObject.GetComponent<Camera>();
    }

    #endregion INITIALIZATION

    #region UPDATE CAMERA POSITION FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: LateUpdate()
    ////////////////////////////////////////////////////////////////////*/
    void LateUpdate()
   {
        if (this.Player1 != null && this.Player2 != null)
        {
            //get the midpoint vector between player 1 and player 2
            this.VectorBetweenP1AndP2 = this.MidPoint(this.Player1.transform.position,
                                                      this.Player2.transform.position);

            //black magicks
            Vector3 point = this.TrackingCamera.WorldToViewportPoint(this.VectorBetweenP1AndP2);
            Vector3 delta = this.VectorBetweenP1AndP2 - this.TrackingCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

            //new camera destination based on where the midpoint vector is
            Vector3 destination = transform.position + delta;

            //offset camera destination
            Vector3 offsetDestination = destination + new Vector3(this.CamOffsetX,
                                                                  this.CamOffsetY,
                                                                  this.CamOffsetZ);

            //updating the position of the camera
            this.gameObject.transform.position = Vector3.SmoothDamp(transform.position,
                                                                    offsetDestination,
                                                                    ref velocity,
                                                                    0.25f);

            //rotate the camera to look at this midpoint
            this.gameObject.transform.LookAt(this.VectorBetweenP1AndP2);
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MidPoint(Vector3, Vector3)
    ////////////////////////////////////////////////////////////////////*/
    Vector3 MidPoint(Vector3 p1Pos_, Vector3 p2Pos_)
    {
        //get mid points for x, y, and z
        float midX = ((p1Pos_.x + p2Pos_.x) / 2);
        float midy = ((p1Pos_.y + p2Pos_.y) / 2);
        float midz = ((p1Pos_.z + p2Pos_.z) / 2);
        
        //return the midpoint vector
        return new Vector3(midX, midy, midz);
    }

    #endregion UPDATE CAMERA POSITION FUNCTIONS
}
