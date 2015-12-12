using UnityEngine;
using System.Collections;

public class EventRaiders : MonoBehaviour {

  public int Difficulty;
  public float LevelEdge;
  public float Speed = 20f;
  public float DistanceFromSurface = 10f;
  public float AvoidanceDistance = 10f;
  public float AvoidanceEffectiveness = 1f;
  public float RaiderDistFromShip = 8.5f;
  public float RaiderDistFromSurface = 1f;
  public AudioClip shipArrivesClip;

  private AudioSource shipArrives; 

  float LifeTime;
  float Timer;
  float Frequency;

  public GameObject Raider;
  Vector3 MySpawnPoint;
  Vector3 Direction;
  Vector3 Destination;
  GameObject Target;
  int RaiderCount;
  StressManager StressMgr;

	void Start () {
    StressMgr = GameObject.FindGameObjectWithTag("Manager").GetComponent<StressManager>();

    //set initial position
    MySpawnPoint = Randomize();
    MySpawnPoint *= LevelEdge * 0.95f;
    transform.position = MySpawnPoint;

    //choose random planet
    GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
    int result = Random.Range(0, planets.Length - 1);
    Target = planets[result];

    //choose location to park
    Destination = ChooseDestination();

    //set lifetime
    LifeTime = 10f * Mathf.Pow(0.95f, Difficulty);
    //difficulty
    RaiderCount = Difficulty;
    Frequency = LifeTime / (RaiderCount - 1);
    Timer = 0;
    shipArrivesClip = new AudioClip();
    shipArrivesClip = (AudioClip)Resources.Load("Sounds/sfx/raider/dark_ship", typeof(AudioClip));
    shipArrives = gameObject.AddComponent<AudioSource>();
        shipArrives.clip = shipArrivesClip;
	}
	
	void Update () {
    Vector3 velocity = Vector3.zero;
    Vector3 myPosition = transform.position;

	  //move to destination planet
    Vector3 distanceVector = Destination - myPosition;
    if (distanceVector.magnitude > Speed * Time.deltaTime) {
      velocity = distanceVector;
      velocity.Normalize();
    }

    //park and spawn raiders
    if (Speed * Time.deltaTime > distanceVector.magnitude) {
      transform.position += distanceVector;

      //spawn raiders based on difficulty on arrival;
		AudioSource.PlayClipAtPoint(shipArrivesClip, transform.position, .4f);
      Timer -= Time.deltaTime;
      if (RaiderCount > 0 && Timer <= 0f) {
        --RaiderCount;
        Timer = Frequency;
        SpawnRaider();
      }

      //stay for lifetime, then leave
      LifeTime -= Time.deltaTime;
      if (LifeTime <= 0) {
        Destination = myPosition + (myPosition - MySpawnPoint) * 2f;
      }
    }

    //avoid obstacles
    RaycastHit result;
    if (velocity != Vector3.zero && Physics.SphereCast(myPosition, transform.localScale.x / 2f,  velocity, out result, AvoidanceDistance)) {
      //Debug.DrawLine(myPosition, myPosition + velocity * result.distance, Color.red, 0f, true);
      velocity += result.normal * AvoidanceEffectiveness / result.distance;
      velocity.Normalize();
    }

    //orient & move
    if (velocity != Vector3.zero) {
      transform.position += velocity * Speed * Time.deltaTime;
      transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
    }

    CheckBounds();
	}

  Vector3 Randomize()
  {
    Vector3 initialAxis = new Vector3(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f));
    initialAxis.Normalize();
    Vector3 perpendicular = new Vector3(-initialAxis.y, initialAxis.x, 0);
    Quaternion rotDistance = Quaternion.AngleAxis(Random.Range(-60f, 60f), perpendicular);
    Vector3 result = rotDistance * initialAxis;
    Quaternion rotSpiral = Quaternion.AngleAxis(Random.Range(0f, 180f), initialAxis);
    result = rotSpiral * result;

    return result;
  }

  Vector3 ChooseDestination()
  {
    Vector3 initialAxis = Target.transform.position - transform.position;
    Vector3 perpendicular = new Vector3(-initialAxis.y, initialAxis.x, 0);
    perpendicular.Normalize();
    Vector3 result = Target.transform.position + perpendicular * (Target.transform.localScale.x / 2f + DistanceFromSurface);
    Direction = result - transform.position;
    Quaternion rotSpiral = Quaternion.AngleAxis(Random.Range(0f, 360f), initialAxis);
    Direction = rotSpiral * Direction;
    result = transform.position + Direction;

    //Debug.DrawLine(result, Target.transform.position, Color.grey, 60f);

    return result;
  }

  void CheckBounds()
  {
    if (transform.position.sqrMagnitude > LevelEdge * LevelEdge) {
      Destroy(gameObject);
    }
  }

  void SpawnRaider()
  {
    if (StressMgr.AtCap())
      return;

    GameObject spawn = Instantiate(Raider, NearShip(), Quaternion.identity) as GameObject;
    spawn.GetComponent<AI_Base>().Planet = Target;
  }

  Vector3 NearShip()
  {
    //random location near ship
    Vector3 toPlanet = Target.transform.position - transform.position;
    float distFromShip = Random.Range(0, RaiderDistFromShip);
    float roughAngle = Mathf.Atan(distFromShip / toPlanet.magnitude);
    Vector3 perpendicular = new Vector3(-toPlanet.y, toPlanet.x, 0);
    Quaternion rotDistance = Quaternion.AngleAxis(Mathf.Rad2Deg * roughAngle, perpendicular);
    Vector3 position = rotDistance * -toPlanet;
    Quaternion rotSpiral = Quaternion.AngleAxis(Random.Range(0f, 360f), toPlanet);
    position = rotSpiral * position;

    //project onto surface
    RaycastHit result;
    Physics.Raycast(Target.transform.position + position, toPlanet, out result);

    return result.point + -toPlanet.normalized * RaiderDistFromSurface;
  }
}
