/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Bullet.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Bullet:MonoBehaviour
{
    //Properties
    [SerializeField] float Speed = GameObject.Find("Gun").GetComponent<Vacuum>().BulletSpeed;
    
    public GameObject Planet = null;
    private Vector3 ToPlanetCenter = new Vector3(0,0,0);
    private float GroundCheckDistance = 1;
    private Vector3 GroundNormal = new Vector3(0,0,0);
    private bool IsGrounded = false;
    private float DistanceFromCenter = 0;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //PURPOSE: Initialize
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //fire the bullet upon creation
        //GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, this.Speed));
        DistanceFromCenter = (Planet.transform.position - this.transform.position).magnitude * 0.9f;
    }

    void FixedUpdate()
    {
        ToPlanetCenter = (Planet.transform.position - this.transform.position).normalized;
        CheckGroundStatus();
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (-transform.up * 2f), -transform.up, out hitInfo, GroundCheckDistance))
        {
            GroundNormal = hitInfo.normal;
            IsGrounded = true;
            Planet = hitInfo.transform.gameObject;
            //Animator.applyRootMotion = true;
        }
        else
        {
            IsGrounded = false;
            GroundNormal = -ToPlanetCenter;
            //Animator.applyRootMotion = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //raycast shit will go here
        Vector3 velocity = transform.forward * Speed;
        //float planetRadius = Planet.GetComponent<SphereCollider>().radius;
        //transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity, -ToPlanetCenter);


        Vector3 projectedVelocity = Vector3.ProjectOnPlane(velocity, ToPlanetCenter).normalized * Speed; //Get the projectedVelocity
        Vector3 pointOnPlanet = -ToPlanetCenter * DistanceFromCenter;         //Move the vector towards the center of the planet
        Vector3 nextVelocityPoint = (pointOnPlanet + Planet.transform.position) + projectedVelocity;
        Vector3 newVelocity = nextVelocityPoint - transform.position;        //Get another vector that points to the end of that

        //Update the tragectory to account for extra height:






        GetComponent<Rigidbody>().velocity = newVelocity.normalized * Speed;
        transform.LookAt(transform.position + newVelocity, ToPlanetCenter);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(this.gameObject);
    }
}
