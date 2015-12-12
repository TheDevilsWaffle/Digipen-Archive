/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EnemyMovement.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

  //a dumb AI with toggle-able features
  //will likely break down into multiple parts that can be attached to a base AI for simpler behavior combining

  [SerializeField] float JumpPower = 12f;
  [Range(1f, 4f)] [SerializeField] float GravityMultiplier = 2f;
  [SerializeField] float MoveSpeed = 1.5f;
  [SerializeField] float GroundCheckDistance = 0.1f;
  [SerializeField] public GameObject Planet = null;
  [SerializeField] float GroundDrag = 0.85f;
  [SerializeField] float AirDrag = 0.85f;
  [SerializeField] float StallJumpDistance = 1f;
  [SerializeField] float WanderRange = 270f;
  [SerializeField] float AvoidanceDetectionDistance = 1f;
  [SerializeField] float AvoidanceEffectiveness = 1f;
  [SerializeField] float AggroRadius = 30f;
  [SerializeField] float AggroTimeOut = 10f;

  public bool Chase = false;
  public bool ShootsIfClose = false;
  public float ShootAttemptRange = 15f;
  public bool Flee = false;
  public GameObject Target = null;
  float ClosestPlayer = 9999f;

  Rigidbody Rigidbody;
  public Vector3 ToPlanetCenter = new Vector3();
  Vector3 GroundNormal;
  bool IsGrounded;
  Vector3 LastPosition;
  float AggroTimer = 0;

  EnemyShoot MyGun;

  void Start()
  {
    Rigidbody = GetComponent<Rigidbody>();
    MyGun = GetComponent<EnemyShoot>();

    Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    //planet normal
    ToPlanetCenter = Planet.transform.position - transform.position;
    ToPlanetCenter.Normalize();

    //move forward
    Vector3 moveVec = gameObject.transform.forward;
    moveVec = Vector3.ProjectOnPlane(moveVec, ToPlanetCenter);
    moveVec.Normalize();

    AggroPlayer();

    if (Chase && Target != null) {
      //chase
      Vector3 toPlayer = Target.transform.position - transform.position;
      moveVec = Vector3.ProjectOnPlane(toPlayer, ToPlanetCenter);
      moveVec.Normalize();
    }
    else if (Flee && Target != null) {
      //flee
      Vector3 toPlayer = Target.transform.position - transform.position;
      moveVec = Vector3.ProjectOnPlane(toPlayer, ToPlanetCenter);
      moveVec *= -1;
      moveVec.Normalize();
    }
    else {
      //random wander
      Quaternion rotAngle = Quaternion.AngleAxis(Random.RandomRange(-WanderRange * Time.deltaTime, WanderRange * Time.deltaTime), ToPlanetCenter);
      moveVec = rotAngle * moveVec;
    }

    //obstacle avoidance
    RaycastHit result;
    Vector3 myPosition = transform.position;
    if (Physics.Raycast(myPosition, moveVec, out result, AvoidanceDetectionDistance)) {
      //Debug.DrawLine(myPosition, myPosition + moveVec * result.distance, Color.red, 1f, true);
      Vector3 repulsion = Vector3.ProjectOnPlane(result.normal, ToPlanetCenter);
      repulsion.Normalize();
      moveVec += repulsion * AvoidanceEffectiveness / result.distance;
      moveVec.Normalize();
    }

    CheckGroundStatus();

    //if stuck, try jumping
    Vector3 distance = LastPosition - transform.position;
    if (IsGrounded && distance.sqrMagnitude < StallJumpDistance * StallJumpDistance * Time.deltaTime) {
      Rigidbody.velocity += GroundNormal * this.JumpPower;
    }

    //Apply our movement forces
    Rigidbody.AddForce(moveVec * 10f * MoveSpeed);
    Rigidbody.AddForce(this.ToPlanetCenter * 10 * GravityMultiplier);
    ApplyDrag();

    //Orient towards velocity
    if (moveVec.sqrMagnitude >= 1) {
      transform.LookAt(transform.position + moveVec, -ToPlanetCenter);
    }

    //if close to player & see player, shoot
    if (ShootsIfClose && Target != null && LineOfSight(Target)) {
      Vector3 distFromPlayer = transform.position - Target.transform.position;
      if (distFromPlayer.sqrMagnitude < ShootAttemptRange * ShootAttemptRange) {
        MyGun.Shoot();
      }
    }

    //Record last position
    LastPosition = transform.position;

    //check if target is in line of sight
    if (Target != null)
      if (LineOfSight(Target))
        AggroTimer = AggroTimeOut;
      else
        AggroTimer -= Time.deltaTime;

    //de-aggro if timed out
    if (AggroTimer < 0)
      Target = null;

  }

  bool LineOfSight(GameObject target)
  {
    RaycastHit result;
    Vector3 myPosition = transform.position;
    Vector3 direction = target.transform.position - myPosition;
    if (Physics.Raycast(myPosition, direction, out result, AvoidanceDetectionDistance)) {
      //Debug.DrawLine(myPosition, myPosition + direction * result.distance, Color.red, 1f, true);
      if (result.collider.gameObject.tag == "Player") {
        return true;
      }
    }
    return false;
  }

  void ApplyDrag()
  {
    float drag = 0f;
    if (IsGrounded)
      drag = GroundDrag;
    else
      drag = AirDrag;

    Rigidbody.AddForce(0.5f * Rigidbody.velocity * -drag);
  }

  void CheckGroundStatus()
  {
    RaycastHit hitInfo;
    if (Physics.Raycast(transform.position + (ToPlanetCenter * 0.9f), ToPlanetCenter, out hitInfo, GroundCheckDistance)) {
      GroundNormal = hitInfo.normal;
      IsGrounded = true;
    }
    else {
      IsGrounded = false;
      GroundNormal = -ToPlanetCenter;
    }
  }

  void AggroPlayer()
  {
    GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
    for (int i = 0; i < Players.Length; ++i) {
      Vector3 distance = Players[i].transform.position - transform.position;
      if (distance.sqrMagnitude < AggroRadius * AggroRadius)
        if(LineOfSight(Players[i]))
          if (distance.magnitude < ClosestPlayer) {
            Target = Players[i];
            ClosestPlayer = distance.magnitude;
          }
    }
    ClosestPlayer = 9999f;
  }
}
