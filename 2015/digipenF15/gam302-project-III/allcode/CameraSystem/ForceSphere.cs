/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ForceSphere.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceSphere : ForceObject
{
    [SerializeField]
    public float Radius = 1;

    private float InfluenceRadius
    {
        get { return this.Radius * Mathf.Max(transform.localScale.x, transform.lossyScale.y, transform.localScale.z) * 0.5f; }
        set { this.Radius = value / Mathf.Max(transform.localScale.x, transform.lossyScale.y, transform.localScale.z) * 0.5f; }
    }
    private GameObject Camera
    {
        get
        {
            if (this.CameraSystem == null)
                return null;
            return this.CameraSystem.Camera;
        }
        set { this.CameraSystem.Camera = value; }
    }

    //private float DampingMultiplier { get { return this.CameraDistance * Mathf.Pow(this.CameraDistance, 2f); } }
    private Vector3 VecToCamera { get { return this.Camera.transform.position - this.transform.position; } }
    private float CameraDistance { get { return Vector3.Distance(this.transform.position, this.Camera.transform.position); } }
    private float DistanceFromSurface { get { return Mathf.Abs(this.CameraDistance - this.InfluenceRadius); } }
    private bool InsideInfluence { get { return this.CameraDistance < this.InfluenceRadius; } }
    private float DistanceFromTarget;

    [Header("Draw Options")]
    [SerializeField]
    public DrawTypes DrawType;
    [SerializeField]
    public Mesh WireFrame = null;

    // Use this for initialization
    void Start ()
    {
        this.CameraSystem.ForceObjects.Add(this);
    }

    void FixedUpdate()
    {
        if(this.AttractionType == AttractionTypes.TowardsSurface)
            this.DistanceFromTarget = this.DistanceFromSurface;
        else
            this.DistanceFromTarget = this.CameraDistance;
    }

    internal override void GetForce ()
    {
        switch(this.InfluenceType)
        {
            case InfluenceTypes.Always:
                this.CalculateForce();
                break;

            case InfluenceTypes.ObjectInside:
                if (this.CameraDistance < this.InfluenceRadius)
                {
                    this.CalculateForce();
                }
                break;

            case InfluenceTypes.ObjectOutside:
                if (this.CameraDistance > this.InfluenceRadius)
                {
                    this.CalculateForce();
                }
                break;
        }
    }

    //Called once every fixed update on each force object. It is responcible
    //for calculating and adding a force to the camera system.
    private void CalculateForce()
    {
        Vector3 forceDirection = -this.VecToCamera.normalized;
        //Vector3 force = forceDirection * (ForceStrength / DampingMultiplier);

        //To perform a horizontal compression or stretch on a graph, instead of solving your
        //equation for f(x), you solve it for f(c*x) for stretching or f(x/c) for compressing, where c is the stretch factor.
        //Vector3 force = forceDirection * (this.ForceStrength * (Mathf.Pow(this.DistanceFromTarget, 2)));
        //Vector3 force = forceDirection * ((this.ForceStrength * (Mathf.Pow((1/this.Damping) * this.DistanceFromTarget, 0.5f))) + ((1 / this.Damping) * this.DistanceFromTarget));

        if (this.UpperMaxDistance == 0 || this.DistanceFromTarget == 0)
            return;

        float evaluation = DampingCurve.Evaluate(this.DistanceFromTarget / this.UpperMaxDistance);
        Vector3 force = forceDirection * this.ForceStrength * DampingCurve.Evaluate(this.DistanceFromTarget / this.UpperMaxDistance);

        if (this.IgnoreMass == false)
            force /= this.CameraSystem.CameraMass;

        //If we are inside the area, we should no longer draw the object in, but instead, push it out
        if ((this.AttractionType == AttractionTypes.TowardsSurface) && (this.InsideInfluence))
        {
            force *= -1;
        }

        this.CameraSystem.TotalCameraForces += force;
    }

    /// <summary>
    /// Debug Information:
    /// Draws gizmos in the editor for visualization and modification of the cylinder.
    /// </summary>

    void OnDrawGizmos()
    {
        if (DrawType != DrawTypes.Always) { return; }
        Draw();
    }
    void OnDrawGizmosSelected()
    {
        if (DrawType != DrawTypes.Selected) { return; }
        Draw();
    }
    void Draw()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireMesh(this.WireFrame, transform.position, Quaternion.Euler(0, 0, 0), this.Radius * transform.lossyScale * 113);
    }
}


