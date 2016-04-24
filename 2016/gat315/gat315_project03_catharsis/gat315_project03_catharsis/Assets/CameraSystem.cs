/*////////////////////////////////////////////////////////////////////////
//SCRIPT: CameraSystem.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour
{

    #region PROPERTIES

    //camera final position offset
    [SerializeField]
    private Vector3 CameraOffset = new Vector3(0f, -5f, -15f);

    [SerializeField]
    private GameObject Target;

    Camera TrackingCamera;

    float CurrentLerpTime;
    Vector3 CameraPos;
    Vector3 TargetPos;
    float MinX;
    float MaxX;
    float MinY;

    [SerializeField]
    private float LeftOffsetX = 4;
    [SerializeField]
    private float RightOffsetX = 1;
    [SerializeField]
    private float CameraSpeed = 3f;
    private Vector3 CameraLeftRotationDegrees = new Vector3(20f, 15f, 0f);
    private Vector3 CameraRightRotationDegrees = new Vector3(20f, -15f, 0f);

    [SerializeField]
    RectTransform CamBox;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure we have a player in the future
        if (this.Target == null)
        {
            Debug.LogWarning(this.gameObject + " has no target assigned, searching for Player (please pick a target next time)");
            this.Target = GameObject.FindWithTag("Player").gameObject;
            if (this.Target != null)
            {
                Debug.Log("Target found = " + this.Target);
            }
            else
            {
                Debug.LogError("UNABLE TO FIND TARGET!");
            }
        }

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
        if (this.Target != null)
        {
            //update the camera box corners
            this.UpdateCameraBoxCorners();


            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(this.Target.transform.position);
            //print(targetScreenPos);

            //update camera and target
            this.CameraPos = this.UpdatePos(this.gameObject);
            this.TargetPos = this.UpdatePos(this.Target);

            Vector3 temp = Camera.main.WorldToScreenPoint(this.TargetPos);

            if(temp.x > this.MaxX)
            {
                //adjust the cameraOffset based on which way the player has went
                if (this.CameraOffset.x != this.RightOffsetX)
                {
                    this.CameraOffset.x = this.RightOffsetX;
                }
                Vector3 endPos = this.TargetPos + this.CameraOffset;

                //update the position of the camera
                this.gameObject.transform.position = Vector3.Lerp(this.CameraPos, endPos, Time.deltaTime * this.CameraSpeed);
                //update rotation of camera
                //Quaternion newRotation = Quaternion.Euler(this.CameraRightRotationDegrees);
                //print("right rotation = " + newRotation);
                //this.gameObject.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * this.CameraSpeed);
            }
            else if (temp.x < this.MinX)
            {
                //adjust the cameraOffset based on which way the player has went
                if(this.CameraOffset.x != this.LeftOffsetX)
                {
                    this.CameraOffset.x = this.LeftOffsetX;
                }
                Vector3 endPos = this.TargetPos + this.CameraOffset;

                //update the position of the camera
                this.gameObject.transform.position = Vector3.Lerp(this.CameraPos, endPos, Time.deltaTime * this.CameraSpeed);
                //update rotation of camera
                //Quaternion newRotation = Quaternion.Euler(this.CameraLeftRotationDegrees);
                //print("left rotation = " + newRotation);
                //this.gameObject.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * this.CameraSpeed);
            }
            else if( temp.y < this.MinY)
            {
                Vector3 endPos = new Vector3(this.CameraPos.x, this.TargetPos.y, this.CameraPos.z) + this.CameraOffset;
                this.gameObject.transform.position = Vector3.Lerp(this.CameraPos, endPos, Time.deltaTime * this.CameraSpeed);
            }



            //rotate the camera to look at this midpoint
            //this.gameObject.transform.LookAt(this.Target.transform);
        }
    }

    void UpdateCameraBoxCorners()
    {
        Vector3[] corners = new Vector3[4];
        this.CamBox.GetWorldCorners(corners);
        this.MinX = corners[0].x;
        this.MaxX = corners[3].x;
        this.MinY = corners[0].y;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdatePos()
    ////////////////////////////////////////////////////////////////////*/
    Vector3 UpdatePos(GameObject obj_)
    {
        return obj_.transform.position;
    }

    #endregion UPDATE CAMERA POSITION FUNCTIONS
}
