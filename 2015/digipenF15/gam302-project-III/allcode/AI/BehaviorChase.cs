/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - BehaviorChase.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/


using UnityEngine;
using System.Collections;

public class BehaviorChase : AI_Base {

  private enum STATES
  {
    Init,
    Wander,
    Chase
  }

  [Header("Behavior Chase Parameters")]
  [SerializeField] float MaxTurn = 30f;
  [SerializeField] float AggroDistance = 15f;
  [SerializeField] float AggroCone = 45f;
  [SerializeField] float ChaseTime = 10f;
  [SerializeField] bool Shoots = true;
  [SerializeField] float ShootAttemptRange = 10f;
  [SerializeField] float WanderSpeed = 10f;
  [SerializeField] float ChaseSpeed = 15f;

  Rigidbody MyRigidbody;
  float Timer;
  GameObject Target;
  EnemyShoot MyGun;
  Vector3 TargetLast;

  public override void OnInit()
  {
    Target = null;
    MyRigidbody = this.gameObject.GetComponent<Rigidbody>();
    MyRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    MyGun = GetComponent<EnemyShoot>();
  }

  public override void OnEnter()
  {
    //print(gameObject.name + "'s state is " + (STATES)GetState());

    switch ((STATES)GetState()) {
      case STATES.Init:
        SetState((int)STATES.Wander);
        break;

      case STATES.Wander:
        MoveSpeed = WanderSpeed;
        SetGoal(RandomDirection(MaxTurn));
        break;

      case STATES.Chase:
        MoveSpeed = ChaseSpeed;
        Timer = ChaseTime;
        break;
    }
  }

  public override void OnUpdate()
  {
    switch ((STATES)GetState()) {
      case STATES.Wander:
        Target = FindNearestWithTag("Player", AggroCone);
        if (Target != null)
          SetState((int)STATES.Chase);
        break;

      case STATES.Chase:
        //chase player
        if (LineOfSight(Target, AggroCone)) {
          //be at-range from the player
          Vector3 oppDir = transform.position - Target.transform.position;
          oppDir.Normalize();
          SetGoal(Target.transform.position + oppDir * ShootAttemptRange * 0.9f);
          OrientTowardsVelocity = true;

          //if in range, shoot
          if (Shoots && (Target.transform.position - transform.position).magnitude < ShootAttemptRange) {
            LookAt(Target.transform.position);
            MyGun.Shoot();
            OrientTowardsVelocity = false;
          }

          //record last position
          TargetLast = Target.transform.position;
        }
        else {
          //go to last known position
          SetGoal(TargetLast);
          OrientTowardsVelocity = true;

          //find a new target?
          GameObject alternate = FindNearestWithTag("Player", AggroCone);
          if (alternate != null) {
            Target = alternate;
            Timer = ChaseTime;
          }
          //pan the horizon
          else {
            //LookAt(RandomDirection(MaxTurn * Time.deltaTime));
            Timer -= Time.deltaTime;
          }
        }
        //lost'em, wander
        if (Timer < 0f)
          SetState((int)STATES.Wander);
        break;
    }
  }

  public override void OnExit()
  {
    switch ((STATES)GetLastState()) {
      case STATES.Chase:
        Target = null;
        break;
    }
  }

  public override void OnMessage(string msg, GameObject sender)
  {
    switch ((STATES)GetState()) {
      case STATES.Wander:
        if (msg == "Arrived")
          SetGoal(RandomDirection(MaxTurn));
        break;
    }
  }

  public override void JustDestroyed()
  {
    BroadcastMessage("Death");
  }

  Vector3 RandomDirection(float turn)
  {
    int counter = 0;

    Vector3 result = transform.forward * MoveSpeed / 5f;
    Vector3 tempResult = result;
    RaycastHit hit;
    LayerMask notEnemies = 1 << LayerMask.NameToLayer("Enemy");
    notEnemies = ~notEnemies;
    turn -= 2f;
    do {
      ++counter;
      if (counter > 360) {
        //print("infinite loop");
        break;
      }

      turn += 2f;
      Quaternion rotTurn = Quaternion.AngleAxis(Random.Range(-turn, turn), transform.up);
      tempResult = rotTurn * result;
    } while (Physics.SphereCast(GetAdjustedHeight(), AvoidDetectWidth, tempResult, out hit, tempResult.magnitude, notEnemies));

    return GetAdjustedHeight() + tempResult;
  }
}
