/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - BehaviorFlee.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class BehaviorFlee : AI_Base {

  private enum STATES
  {
    Init,
    Idle,
    Wander,
    Flee
  }

  [Header("Behavior Flee Parameters")]
  [SerializeField] float MaxTurn = 45f;
  [SerializeField] float MinIdleTime = 10f;
  [SerializeField] float MaxIdleTime = 30f;
  [SerializeField] float MinWanderTime = 5f;
  [SerializeField] float MaxWanderTime = 15f;
  [SerializeField] float FearDistance = 5f;
  [SerializeField] float FleeTime = 10f;
  [SerializeField] float WanderSpeed = 10f;
  [SerializeField] float FleeSpeed = 15f;

  Rigidbody MyRigidbody;
  float Timer;
  GameObject Target;

  public override void OnInit()
  {
    Target = null;
    MyRigidbody = this.gameObject.GetComponent<Rigidbody>();
    MyRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
  }

  public override void OnEnter()
  {
    //print(gameObject.name + "'s state is " + (STATES)GetState());

    switch ((STATES)GetState()) {
      case STATES.Init:
        SetState((int)STATES.Wander);
        break;

      case STATES.Idle:
        SetGoal(transform.position);
        Timer = Random.Range(MinIdleTime, MaxIdleTime);
        break;

      case STATES.Wander:
        SetGoal(transform.position);
        Timer = Random.Range(MinWanderTime, MaxWanderTime);
        MoveSpeed = WanderSpeed;
        break;

      case STATES.Flee:
        Timer = FleeTime;
        MoveSpeed = FleeSpeed;
        break;
    }
  }

  public override void OnUpdate()
  {
    switch ((STATES)GetState()) {
      case STATES.Idle:
        //LookAt(RandomDirection(MaxTurn * Time.deltaTime * 5f));
        Target = FindNearestWithTag("Player");
        if (Target != null && (Target.transform.position - transform.position).magnitude < FearDistance) {
          SetState((int)STATES.Flee);
        }
          
        else if (GetTimeInState() >= Timer)
          SetState((int)STATES.Wander);
        break;

      case STATES.Wander:
        Target = FindNearestWithTag("Player");
        if (Target != null && (Target.transform.position - transform.position).magnitude < FearDistance) {
          SetState((int)STATES.Flee);
        }
          
        else if (GetTimeInState() >= Timer)
          SetState((int)STATES.Idle);
        break;

      case STATES.Flee:
        if (Target != null && LineOfSight(Target))
          SetGoal(RunFromPosition(Target.transform.position));
        else {
          //LookAt(RandomDirection(MaxTurn * Time.deltaTime));
          Timer -= Time.deltaTime;
        }
        if (Timer < 0f)
          SetState((int)STATES.Idle);
        break;
    }
  }

  public override void OnExit()
  {
    switch ((STATES)GetLastState()) {
      case STATES.Flee:
        Target = null;
        break;
    }
  }

  public override void OnMessage(string msg, GameObject sender)
  {
    if (sender == null)
        return;
    if (msg == "Death") {
      if ((transform.position - sender.transform.position).magnitude > FearDistance)
        return;

      SetGoal(RunFromPosition(sender.transform.position));
      SetState((int)STATES.Flee);
    }
      
    switch ((STATES)GetState()) {
      case STATES.Wander:
        if (msg == "Arrived")
          SetGoal(RandomDirection(MaxTurn));
        break;
      case STATES.Flee:
        if (msg == "Arrived")
          LookAt(GetLastPosition());
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

    Vector3 result = transform.forward * MoveSpeed / 3f;
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

  Vector3 RunFromPosition(Vector3 pos)
  {
    Vector3 oppDir = transform.position - pos;
    oppDir = Vector3.ProjectOnPlane(oppDir, GetToPlanetCenter());
    oppDir.Normalize();

    return transform.position + oppDir * MoveSpeed / 2f;
  }
}