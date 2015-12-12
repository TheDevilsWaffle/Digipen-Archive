/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CameraSystem.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DrawTypes { Always, Selected };
public enum InfluenceTypes { Always, ObjectInside, ObjectOutside };
public enum AttractionTypes { TowardsCenter, TowardsSurface };

public class ForceObject : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField]
    internal CameraSystem CameraSystem = null;

    [SerializeField]
    public float ForceStrength = 1;
    [SerializeField]
    public float UpperMaxDistance = 30;
    [SerializeField]
    public AnimationCurve DampingCurve = AnimationCurve.EaseInOut(0.1f, 0.1f,0.1f,0.1f);

    [SerializeField]
    public bool IgnoreMass = false;

    [SerializeField]
    //Whether the force will affect objects when they are inside the area of influence, outside, or just always.
    public InfluenceTypes InfluenceType = InfluenceTypes.ObjectInside;
    [SerializeField]
    //Whether the force will affect objects by drawing them towards their center or towards the outer surface of the area of influence.
    public AttractionTypes AttractionType = AttractionTypes.TowardsCenter;

    //Called once every fixed update on each force object. It is responcible
    //for calculating and adding a force to the camera system.
    internal virtual void GetForce()
    {
    }
}

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject Target = null;
    [SerializeField]
    public GameObject Planetoid = null;
    [SerializeField]
    public Vector3 Offset = new Vector3();
    [SerializeField]
    public float CameraMass = 10;
    public GameObject Camera;
    [HideInInspector]
    public Vector3 TotalCameraForces = new Vector3();
    [HideInInspector]
    public List<ForceObject> ForceObjects = new List<ForceObject>();

    void FixedUpdate()
    {
        foreach (ForceObject forceObject in this.ForceObjects)
        {
            forceObject.GetForce();
        }

        this.Camera.transform.position += this.TotalCameraForces;
        this.TotalCameraForces = new Vector3(0, 0, 0);

        if (this.Target == null)
            return;

        this.Camera.transform.LookAt(Target.transform.position + Target.transform.TransformDirection(this.Offset), Target.transform.up);
    }
}