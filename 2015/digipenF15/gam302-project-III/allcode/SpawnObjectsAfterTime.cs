/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SpawnObjectsAfterTime.cs
//AUTHOR - AustonLindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class SpawnObjectsAfterTime : MonoBehaviour
{
    [SerializeField]
    private Object[] ObjectsToSpawn;
    [SerializeField]
    private float TimeBetweenSpawns = 1;
    [SerializeField]
    private Vector3 SpawnOffset = Vector3.zero;
    private float TimeUntilSpawn;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.TimeUntilSpawn -= Time.deltaTime;
        if(this.TimeUntilSpawn <= 0)
        {
            this.SpawnObjects();
            this.TimeUntilSpawn = this.TimeBetweenSpawns;
        }
	}

    void SpawnObjects()
    {
        Vector3 spawnLocation = this.transform.position + this.transform.InverseTransformDirection(this.SpawnOffset);
        foreach(Object spawnObject in this.ObjectsToSpawn)
        {
            Instantiate(spawnObject, spawnLocation, this.transform.rotation);
        }
    }
}
