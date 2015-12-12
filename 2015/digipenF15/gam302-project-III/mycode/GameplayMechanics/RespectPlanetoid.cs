/*/////////////////////////////////////////////////////////////////////////////
//SCRIPT - RespectPlanetoid.cs
//AUTHOR - Travis Moore & Aughstone Lindsey
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class RespectPlanetoid : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    public GameObject Planetoid;
    [HideInInspector]
    private Vector3 PlanetoidPos
    {
        get { return this.Planetoid.transform.position; }
    }
    [HideInInspector]
    public float DistanceFromPlanetoidCenter
    {
        get { return this.VecToPlanetoid.magnitude; }
        set { this.transform.position = this.PlanetoidPos - (this.VecToPlanetoid.normalized * value); }
    }
    [HideInInspector]
    private Vector3 DirectionFromPlanetoidCenter;
    [SerializeField]
    private float HeightOffset = 0.1f;

    [SerializeField]
    [Tooltip("Whether or not the object floats up and down by modifying its height value")]
    bool Float;

    [SerializeField]
    [Tooltip("The value that will be added to the object to make it float")]
    float FloatHeightOffset;

    [SerializeField]
    [Tooltip("The curve that will be used to modify the height value")]
    AnimationCurve FloatCurve = AnimationCurve.EaseInOut(0,0,1,1);

    [SerializeField]
    [Tooltip("The curve that will be used to modify the height value")]
    float FloatSpeed = 1;

    public Vector3 VecToPlanetoid
    {
        get
        {
            return (this.Planetoid.transform.position - this.transform.position);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //ensure there is an assigned Planetoid
        if(this.Planetoid == null)
        {
            //Debug.LogError(this.gameObject + " does not have an assigned Planetoid, please assign planetoid in RespectPlanetoid.cs");
            this.Planetoid = GameObject.FindGameObjectWithTag("Planet");
        }
        else
        {
            //set DistanceFromPlanetoidCenter
            this.DistanceFromPlanetoidCenter = this.HeightOffset;

            //set this gameobjects orientation to that of the parent planetoid
            this.OrientToPlanetoidSurface();
        }

        this.FloatCurve.preWrapMode = WrapMode.PingPong;
        this.FloatCurve.postWrapMode = WrapMode.PingPong;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //this.DebugDrawing();
        if (!this.Float)
            return;

        this.DistanceFromPlanetoidCenter = this.HeightOffset + (this.FloatHeightOffset * this.FloatCurve.Evaluate(Time.time * this.FloatSpeed));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DebugDrawing()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void DebugDrawing()
    {
        Debug.DrawLine(this.PlanetoidPos, this.gameObject.transform.position, Color.blue);
    }

    void OnDrawGizmosSelected()
    {
        if(this.Planetoid != null)
        {
            this.transform.LookAt(this.transform.position + Vector3.ProjectOnPlane(this.transform.forward, this.VecToPlanetoid), -this.VecToPlanetoid);
            this.DistanceFromPlanetoidCenter = this.HeightOffset;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetObjectOrientationEqualToPlanetoid()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void OrientToPlanetoidSurface()
    {
        this.transform.up = this.transform.position - this.Planetoid.transform.position;
        this.transform.LookAt(this.transform.position + Vector3.ProjectOnPlane(this.transform.forward, this.VecToPlanetoid), -this.VecToPlanetoid);
    }
}
