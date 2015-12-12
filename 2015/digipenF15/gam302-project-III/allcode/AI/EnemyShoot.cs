/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EnemyShoot.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
  //[SerializeField] public float Inaccuracy = 0.25f; //Depends on how Travis implements it
  [SerializeField] Vector3 OffSet = Vector3.zero;
  [SerializeField] public float TimeBetweenShots = 1f;
  [SerializeField] public GameObject Bullet;
  [SerializeField] public GameObject Gun;

    float CoolDown = 0f;
  Rigidbody myRigidBody;

	void Start ()
  {
    myRigidBody = GetComponent<Rigidbody>();
  }
	
	void Update () {
    CoolDown -= Time.deltaTime;
	}

  //Enemy calls this to attempt to shoot
  public void Shoot()
  {
    if (CoolDown > 0)
      return;

    //shoot bullet
    Vector3 position = this.Gun.transform.position + this.Gun.transform.TransformDirection(this.OffSet) + myRigidBody.velocity * Time.deltaTime;
    GameObject spawn = Instantiate(Bullet, position, transform.rotation) as GameObject;
    GameObject planet = gameObject.GetComponent<AI_Base>().Planet;
    spawn.GetComponent<Bullet>().Planet = planet; //will need to be modified depending on how Travis modifies Bullet
    spawn.GetComponent<Bullet>().HeightFromCenter = (planet.transform.position - position).magnitude * 0.9f;
    spawn.transform.parent = planet.transform;
    spawn.GetComponent<Bullet>().ShotBy = gameObject;

    //set cooldown
    CoolDown = TimeBetweenShots;
  }
}
