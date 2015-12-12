/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EventMeteorShower.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class EventMeteorShower : MonoBehaviour {

  public int Difficulty;
  public float LevelEdge;
  public float TimeBeforeFirst = 5f;
  public float MeteorSpeed = 50f;
  public int IrrelevantFactor = 3;

  float LifeTime;
  float Frequency;
  float Timer = 0;
  int Count = 0;

  public GameObject Meteor;
  Vector3 MySpawnPoint;
  Vector3 MeteorDirection;
  GameObject Target;

	void Start () {
    //set starting positions
    MySpawnPoint = new Vector3(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f));
    MySpawnPoint.Normalize();
    MeteorDirection = MySpawnPoint * -1;
    MySpawnPoint *= LevelEdge;

    //choose random planet
    GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
    int result = Random.Range(0, planets.Length - 1);
    Target = planets[result];

    //set difficulty
    TimeBeforeFirst *= Mathf.Pow(0.9f, Difficulty);
    LifeTime = 10f * Mathf.Pow(0.95f, Difficulty) + TimeBeforeFirst;
    Timer = TimeBeforeFirst;
    if (IrrelevantFactor != 0)
      Frequency = LifeTime / 10f / IrrelevantFactor;
    else
      Frequency = LifeTime / 10f;
	}
	
	void Update () {
    Timer -= Time.deltaTime;
    if (Timer <= 0f) {
      GameObject spawn;
      if(Count == IrrelevantFactor) {
        spawn = Instantiate(Meteor, Aimed(), Quaternion.identity) as GameObject;
        Count = 0;
      }
      else {
        spawn = Instantiate(Meteor, Randomize(), Quaternion.identity) as GameObject;
        ++Count;
      }
        
      spawn.GetComponent<Rigidbody>().AddForce(MeteorDirection * MeteorSpeed, ForceMode.Impulse);
      Timer = Frequency;
    }

    LifeTime -= Time.deltaTime;
    if (LifeTime <= 0f)
      Destroy(gameObject);
	}

  Vector3 Randomize()
  {
    Vector3 perpendicular = new Vector3(-MySpawnPoint.y, MySpawnPoint.x, 0);
    Quaternion rotDistance = Quaternion.AngleAxis(Random.Range(-605f, 60f), perpendicular);
    Vector3 result = rotDistance * MySpawnPoint;
    Quaternion rotSpiral = Quaternion.AngleAxis(Random.Range(0f, 180f), MySpawnPoint);
    result = rotSpiral * result;

    return result;
  }

  Vector3 Aimed()
  {
    //random point on planet
    Vector3 surfacePoint = MySpawnPoint.normalized * (Target.transform.localScale.x / 2f + 10f);
    Vector3 perpendicular = new Vector3(-MySpawnPoint.y, MySpawnPoint.x, 0);
    Quaternion rotDistance = Quaternion.AngleAxis(Random.Range(-85f, 85f), perpendicular);
    surfacePoint = rotDistance * surfacePoint;
    Quaternion rotSpiral = Quaternion.AngleAxis(Random.Range(0f, 180f), MySpawnPoint);
    surfacePoint = rotSpiral * surfacePoint;

    //project onto planet
    RaycastHit result;
    Physics.Raycast(Target.transform.position + surfacePoint, -surfacePoint, out result);
    surfacePoint = result.point;

    //determine spawn
    float a = surfacePoint.magnitude;
    float b = MySpawnPoint.magnitude;
    float c = Vector3.Angle(surfacePoint, MySpawnPoint);
    float lawOfCos = Mathf.Sqrt(a * a + b * b - 2 * a * b * Mathf.Cos(Mathf.Deg2Rad * c));
    return surfacePoint + lawOfCos * MySpawnPoint.normalized * 0.98f;
  }
}
