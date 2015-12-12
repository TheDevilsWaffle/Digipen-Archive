/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PopulatePlanet.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class PopulatePlanet : MonoBehaviour
{
    SphereCollider Collider;

    [SerializeField]
    [Tooltip("The objects representing the creatures that should populate this planet.")]
    private Object[] Creatures;
    [SerializeField]
    [Tooltip("A range which determines the delay between each population")]
    private Vector2 CreaturePopulationDelayRange = new Vector2(5, 10);
    //The time that remains before the next creature population
    private float TimeUntilCreaturePopulate;
    [SerializeField]
    [Tooltip("The number of creatures we spawn after each population delay")]
    private float CreatureSpawnCount = 1;
    [SerializeField]
    [Tooltip("The maximum number of creatures we can have in the world before we stop populating.")]
    private float MaxCreaturePopulation = 1;

    [SerializeField]
    private Object[] DetailObjects;
    [SerializeField]
    [Tooltip("The number of detail objects we spawn when the planet is generated")]
    private float DetailObjectSpawnCount = 10;

    [SerializeField]
    [Tooltip("Objects will spawn with their default size plus or minus (+-) this value")]
    private float DetailObjectSizeOffset = 25;

    [SerializeField]
    [Tooltip("Objects will spawn with their default size plus or minus (+-) this value")]
    private float DetailObjectColorOffset = 100;

    // Use this for initialization
    void Start ()
    {
        if (this.CreaturePopulationDelayRange.y < this.CreaturePopulationDelayRange.x)
            Debug.LogError("Population range maximum cannot be less than the minimum", this);

        this.Collider = this.GetComponent<SphereCollider>();

        //this.DetailObjectColorOffset /= 255;
        this.PopulateDetailObjects();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.TimeUntilCreaturePopulate <= 0)
        {
            this.PopulateCreatures();
            this.TimeUntilCreaturePopulate = Random.Range(this.CreaturePopulationDelayRange.x, this.CreaturePopulationDelayRange.y);
        }

        this.TimeUntilCreaturePopulate -= Time.deltaTime;
    }

    //Populates the planets with creatures
    void PopulateCreatures()
    {
        for(int i = 0; i < this.CreatureSpawnCount; ++i)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= this.MaxCreaturePopulation)
                return;

            Vector3 spawnPosition = this.transform.position + (Random.onUnitSphere * this.Collider.radius * 4.5f);
            Object creatureToSpawn = this.Creatures[Random.Range(0, this.Creatures.Length)];
            GameObject creature = (GameObject) Instantiate(creatureToSpawn, spawnPosition, Quaternion.identity);
            creature.GetComponent<BehaviorFlee>().Planet = this.gameObject;
        }
    }
    void PopulateDetailObjects()
    {
        for (int i = 0; i < this.DetailObjectSpawnCount; ++i)
        {
            //We don't worry about the "height" that the object is spawned at because respect planetoid should take care of it
            Vector3 spawnPosition = this.transform.position + Random.onUnitSphere;
            Object objectToSpawn = this.DetailObjects[Random.Range(0, this.DetailObjects.Length)];
            GameObject detailObject = (GameObject)Instantiate(objectToSpawn, spawnPosition, Random.rotationUniform);
            detailObject.GetComponent<RespectPlanetoid>().Planetoid = this.gameObject;

            Vector3 objectScale = detailObject.transform.localScale;
            Vector3 scaleMin = new Vector3(objectScale.x - this.DetailObjectSizeOffset, objectScale.x - this.DetailObjectSizeOffset, objectScale.x - this.DetailObjectSizeOffset);
            Vector3 scaleMax = new Vector3(objectScale.x + this.DetailObjectSizeOffset, objectScale.x + this.DetailObjectSizeOffset, objectScale.x + this.DetailObjectSizeOffset);
            float randomScale = Random.Range(scaleMin.x, scaleMax.x);
            ///detailObject.transform.localScale = new Vector3(Random.Range(scaleMin.x, scaleMax.x), Random.Range(scaleMin.y, scaleMax.y), Random.Range(scaleMin.z, scaleMax.z));
            detailObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            //print("Offset: " + this.DetailObjectColorOffset);
            Color objectColor = detailObject.GetComponent<Renderer>().material.color * 255;
            Color colorMin = new Color(objectColor.r - this.DetailObjectSizeOffset, objectColor.g - this.DetailObjectSizeOffset, objectColor.b - this.DetailObjectSizeOffset);
            //print("Color Min: " + colorMin);
            Color colorMax = new Color(objectColor.r + this.DetailObjectSizeOffset, objectColor.g + this.DetailObjectSizeOffset, objectColor.b + this.DetailObjectSizeOffset);
            //print("Color Max: " + colorMax);
            detailObject.GetComponent<Renderer>().material.color = new Color(Random.Range(colorMin.r, colorMax.r), Random.Range(colorMin.g, colorMax.g), Random.Range(colorMin.b, colorMax.b)) / 255;

            //Make sure it isn't in an awkward position
            //if (Physics.CheckSphere(detailObject.transform.position, 0.5f))
            //    Destroy(detailObject);
        }
    }
}
