/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CamRodTest.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class CamRodTest : MonoBehaviour
{
    [SerializeField]
    internal CameraSystem CameraSystem = null;
    Vector3 FacingVector = new Vector3();
    Quaternion Rotation = new Quaternion();

    //private float VerticalRotation, HorizontalRotation = 0;
    public float RotationSpeed = 100;

    // Use this for initialization
    void Start()
    {
        Vector3 awayFromPlanetoid = this.CameraSystem.Planetoid.transform.position - this.transform.position;
        this.FacingVector = this.CameraSystem.Target.transform.forward;
        this.Rotation = Quaternion.LookRotation(CameraSystem.Target.transform.position + awayFromPlanetoid, this.FacingVector);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CameraSystem.Target == null)
            return;
        //Vector3 awayFromPlanetoid = this.CameraSystem.Planetoid.transform.position - this.transform.position;
        //this.FacingVector = this.CameraSystem.Target.transform.forward;
        //this.Rotation = Quaternion.LookRotation(awayFromPlanetoid, this.FacingVector);

        this.Rotation = Quaternion.AngleAxis(this.RotationSpeed * Input.GetAxis("LookHorizontal") * Time.deltaTime, CameraSystem.Camera.transform.up) * this.Rotation;
        this.Rotation = Quaternion.AngleAxis(this.RotationSpeed * -Input.GetAxis("LookVertical") * Time.deltaTime, CameraSystem.Camera.transform.right) * this.Rotation;

        Vector3 towardPlanet = this.CameraSystem.Target.GetComponent<PlayerController>().ToPlanetCenter.normalized;
        Vector3 awayFromPlanet = -towardPlanet;


        //print("Away From Planet: " + awayFromPlanet);
        
        float angleAwayFromPlanet = Vector3.Angle(this.Rotation * Vector3.up, awayFromPlanet);
        //print("Angle Away From Planet: " + angleAwayFromPlanet);
        if (angleAwayFromPlanet < 140)
        {
            //print("Angle To Low! Stop looking so high!");
            float modifier = (140 - angleAwayFromPlanet) / 140;
            this.Rotation = Quaternion.AngleAxis(modifier * 1000 * Time.deltaTime, CameraSystem.Camera.transform.right) * this.Rotation;
        }
        float angleTowardPlanet = Vector3.Angle(this.Rotation * Vector3.up, towardPlanet);
        if (angleTowardPlanet < 40)
        {
            //print("Angle To Low! Stop looking so low!" + (40 - angleTowardPlanet));
            float modifier = (40 - angleTowardPlanet) / 40;
            this.Rotation = Quaternion.AngleAxis(modifier * -1000 * Time.deltaTime, CameraSystem.Camera.transform.right) * this.Rotation;
        }

        //this.CameraSystem.Target.GetComponent<PlayerController>()
        //print(this.transform.InverseTransformDirection(this.Rotation.eulerAngles));

        transform.position = CameraSystem.Target.transform.position + CameraSystem.Target.transform.TransformDirection(CameraSystem.Offset);
        transform.rotation = this.Rotation;

        //transform.LookAt(this.transform.position + awayFromPlanetoid, this.FacingVector);
        //transform.rotation = this.Rotation;
        //this.VacuumObject.transform.LookAt(this.VacuumObject.transform.position + this.Gun.transform.up, this.Gun.transform.forward);
        //this.transform.LookAt(this.transform.position + awayFromPlanetoid, this.FacingVector);
    }
}
