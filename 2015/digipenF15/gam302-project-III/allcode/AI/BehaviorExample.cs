/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - BehaviorExample.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class BehaviorExample : AI_Base {

  private enum STATES
  {
    Init,   //0
    Idle,   //1
    Wander  //2
  }

  [Header ("Behavior Example Parameters")]
  [SerializeField] float MaxTurn = 45f;
  [SerializeField] float MinIdleTime = 5f;
  [SerializeField] float MaxIdleTime = 15f;

  Rigidbody MyRigidbody;
  float Timer;

  //synonymous to Start()
  public override void OnInit()
  {
    //print("Initializing");
    MyRigidbody = this.gameObject.GetComponent<Rigidbody>();
    MyRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
  }

  public override void OnEnter()
  {
    //non-state-dependent actions
    //print(gameObject.name + "'s state is " + (STATES)GetState());
    //print("OnEnter");

    //state-dependent actions
    switch ((STATES)GetState()) {

      case STATES.Init:
        SetState((int)STATES.Idle);
        break;

      case STATES.Idle:
        //stop moving
        SetGoal(transform.position);
        //set random time in state
        Timer = Random.Range(MinIdleTime, MaxIdleTime);
        break;

      case STATES.Wander:
        //go randomly forward
        SetGoal(RandomDirection(MaxTurn));
        //set random time in state
        Timer = Random.Range(MinIdleTime, MaxIdleTime);
        break;
    }
  }

  public override void OnUpdate()
  {
    //non-state-dependent actions
    //print("OnUpdate");

    //state-dependent actions
    switch ((STATES)GetState()) {

      case STATES.Init:
        //nothing
        break;

      case STATES.Idle:
        //LookAt(RandomDirection(MaxTurn * Time.deltaTime));
        if (GetTimeInState() >= Timer) {
          SetState((int)STATES.Wander);
          //print("Changing state to Wander");
        }          
        break;

      case STATES.Wander:
        if (GetTimeInState() >= Timer) {
          SetState((int)STATES.Idle);
          //print("Changing state to Idle");
        }
        break;
    }
  }

  public override void OnExit()
  {
    //non-state-dependent actions
    //print("OnExit");

    //state-dependent actions
    //NOTE: GetLASTState instead of GetState
    switch ((STATES)GetLastState()) {

      case STATES.Init:
        //nothing
        break;

      case STATES.Idle:
        //nothing
        break;

      case STATES.Wander:
        //nothing
        break;
    }
  }

  public override void OnMessage(string msg, GameObject sender)
  {
    //non-state-dependent actions
    //print("OnMsg: " + msg);

    //state-dependent actions
    switch ((STATES)GetState()) {

      case STATES.Init:
        //nothing
        break;

      case STATES.Idle:
        //nothing
        break;

      case STATES.Wander:
        if (msg == "Arrived") {
          SetGoal(RandomDirection(MaxTurn));
          //print("setting new goal");
        }
        break;
    }
  }

  //synonymous to OnDestroy()
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
