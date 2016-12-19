/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - CreateObjectsOnDestroy.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections;

public class CreateObjectsOnDestroy : MonoBehaviour
{
    private enum SpawnTypes { SpawnAll, SpawnInOrder, SpawnRandom };

    [SerializeField]
    private SpawnTypes SpawnType;
    [SerializeField]
    private Object[] ObjectsToSpawn;
    [SerializeField][Range(0,100)]
    private SpawnTypes PercentChanceToSpawn;
    [SerializeField]
    private Vector3 SpawnOffset = Vector3.zero;
    private bool IsShuttingDown;

    [SerializeField]
    private Transform ParentTo;

    private int CurrentIndex;

    void OnApplicationQuit()
    {
        this.IsShuttingDown = true;
    }

    public void OnLoadingScene()
    {
        this.IsShuttingDown = true;
    }

    void OnDestroy()
    {
        if (!IsShuttingDown)
            this.SpawnObjects();
    }

    Object GetRandomObject()
    {
        int randomIndex = Random.Range(0, this.ObjectsToSpawn.Length);
        return this.ObjectsToSpawn[randomIndex];
    }

    void SpawnObjects()
    {
        Vector3 spawnLocation = this.transform.position + this.transform.InverseTransformDirection(this.SpawnOffset);
        GameObject spawnedObject;

        switch(this.SpawnType)
        {
            case SpawnTypes.SpawnAll:
                foreach (Object spawnObject in this.ObjectsToSpawn)
                {
                    spawnedObject = (GameObject)Instantiate(spawnObject, spawnLocation, this.transform.rotation);
                    if (this.ParentTo != null)
                        spawnedObject.transform.parent = this.ParentTo;
                }
                break;
            case SpawnTypes.SpawnInOrder:
                spawnedObject = (GameObject)Instantiate(this.ObjectsToSpawn[this.CurrentIndex], spawnLocation, this.transform.rotation);
                if (this.ParentTo != null)
                    spawnedObject.transform.parent = this.ParentTo;
                break;
            case SpawnTypes.SpawnRandom:
                spawnedObject = (GameObject)Instantiate(this.GetRandomObject(), spawnLocation, this.transform.rotation);
                if (this.ParentTo != null)
                    spawnedObject.transform.parent = this.ParentTo;
                break;

        }
    }
}
