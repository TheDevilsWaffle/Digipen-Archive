/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Move.cs
//AUTHOR - 
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    public Vector3 Movement = new Vector3(0,0,0);
    public Vector3 RotationalMovement = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position += Movement;
        transform.Rotate(RotationalMovement);
    }
}
