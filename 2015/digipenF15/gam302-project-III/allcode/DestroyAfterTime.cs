/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - DestroyAfterTime.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float TimeBeforeDestroy;

    // Use this for initialization
    void Start ()
    {
        Destroy(this.gameObject, this.TimeBeforeDestroy);
	}
}
