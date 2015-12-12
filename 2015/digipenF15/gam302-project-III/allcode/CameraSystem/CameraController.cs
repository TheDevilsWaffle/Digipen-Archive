/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CameraController.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject Target = null;
    Vector3 StartingOffset = new Vector3();
    Vector3 BoomVec = new Vector3();
    [SerializeField]
    float FollowSpeedMulitplier = 1f;
    [SerializeField]
    float RotateSpeedMulitplier = 1f;

    private float HorizontalAngle = Mathf.PI;
    private float VerticalAngle = -Mathf.PI / 2;

    private Vector3 SphericalUp = new Vector3(0, 0, 0);
    private Vector3 SphericalForward = new Vector3(0, 0, 0);
    private Vector3 SphericalRight = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        StartingOffset = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SphericalUp = -Target.GetComponent<PlayerController>().ToPlanetCenter;
        //SphericalForward = Vector3.Cross(new Vector3(1, 0, 0), SphericalUp);
        //SphericalRight = Vector3.Cross(new Vector3(0, 0, 1), SphericalUp);

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //		Vector3 moveVec = new Vector3 (moveX, 0f, moveY);
        //		if (moveVec.magnitude > 1f) moveVec.Normalize();

        //BoomVec = -moveVec + new Vector3(0,StartingOffset.y, 0);
        //transform.forward = -moveVec;
        //BoomVec = Target.transform.TransformDirection(StartingOffset);
        
        Vector3 forwardProjected = Vector3.ProjectOnPlane(transform.forward, Target.transform.up).normalized;

        Vector3 vecToTarg = Target.transform.position - (transform.position);
        Vector3 vecToTargProjected = Vector3.ProjectOnPlane(vecToTarg, Target.transform.up).normalized;

        Vector3 playerUp = -Target.GetComponent<PlayerController>().ToPlanetCenter;

        Vector3 rotationVec = Vector3.Cross(vecToTarg.normalized, SphericalUp);

        //float horizontalAngle = 180;//Vector3.Angle(Target.transform.forward, vecToTargProjected);
        //float verticalAngle = Vector3.Angle(vecToTargProjected, vecToTarg);
        float boomLength = 10;

        //BoomVec = (Target.transform.up * 10) + (-Target.transform.forward * 10);
        BoomVec = Target.transform.TransformDirection(new Vector3(Mathf.Sin(HorizontalAngle), 0, Mathf.Cos(HorizontalAngle)));
        BoomVec = Quaternion.AngleAxis(VerticalAngle, rotationVec) * BoomVec * boomLength;
        //BoomVec = Quaternion.AngleAxis(HorizontalAngle, -Target.GetComponent<PlayerController>().ToPlanetCenter) * SphericalForward * boomLength;

        //VerticalAngle -= Time.deltaTime * Input.GetAxis("Mouse Y") * (float)20;
        //HorizontalAngle += Time.deltaTime * Input.GetAxis("Mouse X") * (float)3;

        //transform.RotateAround(Target.transform.position, SphericalUp, HorizontalAngle);
        //transform.RotateAround(Target.transform.position, rotationVec, VerticalAngle);

        Vector3 newPosition = Target.transform.position + BoomVec + (SphericalUp * 10);
        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeedMulitplier * Time.fixedDeltaTime);

        transform.LookAt(Target.transform.position, Target.transform.up);
        //transform.Rotate(Quaternion.AngleAxis(HorizontalAngle, Target.transform.up) * Target.transform.up);

        //transform.up = -Target.GetComponent<PlayerController> ().ToPlanetCenter;
        //Vector3.Slerp(transform.forward vecToTarg, 5 * Time.deltaTime);



        /*//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        NOTE:
        If we are looking at the surface of the planet from top down, the "vertical" input axis should relate to the local Y axis of the camera, 
        but, if we are looking at an angle more perpendicular to the surface, then it shoudl be projected on the z-axis of the camera.

        We should get the camera's forward vector and compare it against the surface normal at the player position.
        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    }
}
