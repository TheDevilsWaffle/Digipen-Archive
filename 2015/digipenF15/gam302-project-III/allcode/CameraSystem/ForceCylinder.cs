/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ForceCylinder.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceCylinder : ForceObject
{
    [SerializeField]
    public float Radius = 1;
    [SerializeField]
    public float Height = 1;
    public bool IgnoreHeightInfluence = false;
    
    [Header("Vertical Flow Options")]
    [SerializeField]
    private FlowTypes FlowType = FlowTypes.TowardsBottom;
    private enum FlowTypes { TowardsTop, TowardsCenter, TowardsBottom, AlwaysUp, AlwaysDown, NoFlow };
    [SerializeField]
    public float VerticalFlowStrength = 1;
    [SerializeField]
    public float VerticalUpperMaxDistance = 30;
    [SerializeField]
    public AnimationCurve VerticalDampingCurve = AnimationCurve.EaseInOut(0.1f, 0.1f, 0.1f, 0.1f);

    private float InfluenceRadius
    {
        get
        {
            if (this == null)
                return 0;

            return this.Radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z) * 0.5f;
        }
        set { this.Radius = value / Mathf.Max(transform.lossyScale.x, transform.lossyScale.z) * 0.5f; }
    }
    private float InfluenceHeight
    {
        get { return this.Height * this.transform.lossyScale.y; }
        set { this.Height = value / this.transform.lossyScale.y; }
    }
    private GameObject Camera
    {
        get { return this.CameraSystem.Camera; }
        set { this.CameraSystem.Camera = value; }
    }
    
    private Vector3 FromCenterToCam = new Vector3();
    private Vector3 FromCenterToCamVertical = new Vector3();
    private Vector3 FromTopToCamVertical = new Vector3();
    private Vector3 FromBottomToCamVertical = new Vector3();

    private float CameraDistance { get { return FromCenterToCam.magnitude; } }
    private float VerticalDistance { get { return FromCenterToCamVertical.magnitude; } }

    private bool InsideHeightInfluence { get { return this.VerticalDistance < this.InfluenceHeight; } }
    private bool InsideRadialInfluence { get { return this.CameraDistance < this.InfluenceRadius; } }

    private float VerticalDistanceFromTop { get { return Mathf.Abs(this.VerticalDistance - this.InfluenceHeight); } }
    private float VerticalDistanceFromCenter { get { return Mathf.Abs(this.VerticalDistance - this.InfluenceHeight); } }
    private float VerticalDistanceFromBottom { get { return Mathf.Abs(this.VerticalDistance - this.InfluenceHeight); } }
    private float VerticalDistanceFromTarget;

    private float RadialDistanceFromSurface { get { return Mathf.Abs(this.CameraDistance - this.InfluenceRadius); } }
    private float RadialDistanceFromTarget;

    [Header("Draw Options")]
    [SerializeField]
    public DrawTypes DrawType;
    [SerializeField]
    public Mesh WireFrame = null;

    // Use this for initialization
    void Start()
    {
        this.CameraSystem.ForceObjects.Add(this);
    }

    void FixedUpdate()
    {
        if (this.CameraSystem.Target == null)
            return;

        //Getting the vector to the camera
        Vector3 vecToCam = this.Camera.transform.position - this.transform.position; //Getting the vector from this object to the camera
        Vector3 vecToCamFromTop = (this.Camera.transform.position + this.transform.InverseTransformDirection(new Vector3(0, this.Height, 0))) - this.transform.position; //Getting the vector from this object's top to the camera
        Vector3 vecToCamFromBottom = (this.Camera.transform.position - this.transform.InverseTransformDirection(new Vector3(0, this.Height, 0))) - this.transform.position; //Getting the vector from this object bottom to the camera

        //Transforming to local space
        Vector3 localToCam = this.transform.InverseTransformDirection(vecToCam); //Transforming the vector to local space
        Vector3 localToCamFromTop = this.transform.InverseTransformDirection(vecToCamFromTop); //Transforming the vector to local space
        Vector3 localToCamFromBottom = this.transform.InverseTransformDirection(vecToCamFromBottom); //Transforming the vector to local space

        //Getting the radial distance to the centeral axis
        Vector3 localToCamZeroed = new Vector3(localToCam.x, 0, localToCam.z); //Zeroing out the y axis (because we don't care about it)
        this.FromCenterToCam = this.transform.TransformDirection(localToCamZeroed); //Transforming back into world and returning the value

        //Getting the vertical distance to the center
        Vector3 vertLocalToCamZeroed = new Vector3(0, localToCam.y, 0);//Zeroing out the x and z axes (because we don't care about them)
        Vector3 vertLocalToCamFromTopZeroed = new Vector3(0, localToCamFromTop.y, 0);//Zeroing out the x and z axes (because we don't care about them)
        Vector3 vertLocalToCamFromBottomZeroed = new Vector3(0, localToCamFromBottom.y, 0);//Zeroing out the x and z axes (because we don't care about them)

        this.FromCenterToCamVertical = this.transform.TransformDirection(vertLocalToCamZeroed);//Transforming back into world and returning the value
        this.FromTopToCamVertical = this.transform.TransformDirection(vertLocalToCamFromTopZeroed);//Transforming back into world and returning the value
        this.FromBottomToCamVertical = this.transform.TransformDirection(vertLocalToCamFromBottomZeroed);//Transforming back into world and returning the value


        //Setting the radial target distance depending on whether we are looking to target the surface or the center
        switch (this.AttractionType)
        {
            case AttractionTypes.TowardsSurface:
                this.RadialDistanceFromTarget = this.RadialDistanceFromSurface;
                break;

            case AttractionTypes.TowardsCenter:
                this.RadialDistanceFromTarget = this.CameraDistance;
                break;
        }

        //Setting the vertical target distance depending on whether we are looking to target the top, bottom, or center
        switch (this.FlowType)
        {
            case FlowTypes.TowardsTop:
                this.VerticalDistanceFromTarget = this.VerticalDistanceFromTop;
                break;

            case FlowTypes.TowardsCenter:
                this.VerticalDistanceFromTarget = this.VerticalDistanceFromCenter;
                break;

            case FlowTypes.TowardsBottom:
                this.VerticalDistanceFromTarget = this.VerticalDistanceFromBottom;
                break;
        }
    }

    //Called once every fixed update on each force object. It is responcible
    //for calculating and adding a force to the camera system.
    internal override void GetForce()
    {
        switch (this.InfluenceType)
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

    private void CalculateForce()
    {
        //Calculating InwardForce:
        Vector3 forceDirection = -this.FromCenterToCam.normalized;

        if (this.UpperMaxDistance == 0 || this.RadialDistanceFromTarget == 0)
            return;

        float evaluation = DampingCurve.Evaluate(this.RadialDistanceFromTarget / this.UpperMaxDistance);
        Vector3 force = forceDirection * this.ForceStrength * evaluation;

        if (this.IgnoreMass == false)
            force /= this.CameraSystem.CameraMass;

        //If we are inside the area, we should no longer draw the object in, but instead, push it out
        if ((this.AttractionType == AttractionTypes.TowardsSurface) && (this.InsideRadialInfluence))
        {
            force *= -1;
        }

        this.CameraSystem.TotalCameraForces += force;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Calculating flow force:

        if (this.FlowType == FlowTypes.NoFlow)
            return;

        Vector3 verticalForceDirection = new Vector3(0,0,0);
        switch (this.FlowType)
        {
            case FlowTypes.TowardsTop:
                verticalForceDirection = -this.FromTopToCamVertical.normalized;
                break;

            case FlowTypes.TowardsCenter:
                verticalForceDirection = -this.FromCenterToCamVertical.normalized;
                break;

            case FlowTypes.TowardsBottom:
                verticalForceDirection = -this.FromBottomToCamVertical.normalized;
                break;
        }

        //print(this.VerticalDistanceFromTarget);

        if (this.UpperMaxDistance == 0 || this.VerticalDistanceFromTarget == 0)
            return;

        float verticalEvaluation = this.VerticalDampingCurve.Evaluate(this.VerticalDistanceFromTarget / this.VerticalUpperMaxDistance);
        Vector3 verticalForce = verticalForceDirection * this.VerticalFlowStrength * verticalEvaluation;

        if (this.IgnoreMass == false)
            verticalForce /= this.CameraSystem.CameraMass;

        //If we are inside the area, we should no longer draw the object in, but instead, push it out
        if (((this.FlowType == FlowTypes.TowardsTop) || (this.FlowType == FlowTypes.TowardsBottom)) && (this.InsideHeightInfluence))
        {
            verticalForce *= -1;
        }

        this.CameraSystem.TotalCameraForces += verticalForce;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireMesh(this.WireFrame, transform.position, this.transform.rotation, new Vector3(this.InfluenceRadius, this.InfluenceHeight, this.InfluenceRadius));
        Gizmos.DrawLine(this.Camera.transform.position, this.Camera.transform.position - FromCenterToCamVertical);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position - this.transform.TransformDirection(new Vector3(0, this.InfluenceHeight, 0)), this.Radius * transform.lossyScale.x * 0.1f);
    }
}


