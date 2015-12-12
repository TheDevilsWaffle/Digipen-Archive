/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - AI_Base.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////
//NOTES:
//Make sure to inherit from AI_Base instead of Monobehaviour
//Do NOT use Start() or Update()
//InternalMessage("Arrived") is sent when the gameObject reaches the set goal
//Do not have multiple behaviors on a single object
//
//DEFINED BY USER
//OnInit();
//OnEnter(int stateEnum);
//OnUpdate(int stateEnum);
//OnExit(int stateEnum);
//OnMessage(int stateEnum, string msg);
//JustDestroyed();
//
//API FUNCTIONS
//SetState(int stateEnum)   //must cast stateEnum to int
//int GetState()            //must cast return to State
//int GetLastState()        //must cast return to State
//InternalMessage(string msg)
//BroadcastMessage(string msg)
//SetGoal(Vector3 target)
//Vector3 GetGoal()
//Vector3 GetAdjustedHeight()
//float GetTimeInState()
//Vector3 GetToPlanetCenter()
//Jump()
//Vector3 GetLastPosition()
//bool GetGroundStatus()
//bool LineOfSight(GameObject target, float inCone = 180f)            //180 is 360 awareness
//GameObject FindNearestWithTag(string tag, float inSightCone = 180f) //180 is 360 awareness
//LookAt(Vector3 target)
////////////////////////////////////////////////////////////////////////////////

public class AI_Base : MonoBehaviour {

  [Header("Movement Parameters")]
  public GameObject Planet = null;
  public float MoveSpeed = 10f;
  public float Drag = 0.85f;
  public float JumpPower = 10f;
  public float GravityFactor = 10f;

  [Header("Movement AI Settings")]
  public float GroundCheckDistance = 0.85f;
  public float GroundCheckWidth = 0.85f;
  public float SlopeTolerance = 180f;
  public bool OrientTowardsVelocity = true;
  public float GoalToleranceDist = 1f;
  public bool UseAStar = false;
  public float AvoidDetectDistance = 5f;
  public float AvoidDetectWidth = 0.5f;
  public float AvoidEffectiveness = 2f;
  public float HeightOffset = 0f;
  public float JumpCooldown = 0.5f;
  public float StuckCooldown = 5f;
  
  public int CurrState_;
  private int LastState_;
  private float TimeInState_;
  private Queue<Vector3> Waypoints_;
  private Vector3 CurrentGoal_;
  private Vector3 EndGoal_;
  private Vector3 LastPosition_;
  private MessageManager Messenger_;
  private StressManager StressMgr_;
  private Rigidbody MyRigidbody_;
  private bool Jumping_ = false;
  private bool GroundStatus_ = false;
  private float JumpTimer_;
  private float StuckTimer_;

  public void SetState(int stateEnum)
  {
    LastState_ = CurrState_;
    CurrState_ = stateEnum;
    TimeInState_ = 0;

    if(LastState_ != -1)
      OnExit();
    OnEnter();
  }
  public int GetState() {
    return CurrState_;
  }
  public int GetLastState() {
    return LastState_;
  }

  void Start()
  {
    Waypoints_ = new Queue<Vector3>();

    Messenger_ = GameObject.FindGameObjectWithTag("Manager").GetComponent<MessageManager>();
    Messenger_.Register(OnMessage);
    StressMgr_ = GameObject.FindGameObjectWithTag("Manager").GetComponent<StressManager>();
    StressMgr_.IncreasePop();

    MyRigidbody_ = GetComponent<Rigidbody>();
    
    OnInit();

    CurrState_ = -1;
    EndGoal_ = Vector3.one * 9999f;
    CurrentGoal_ = Vector3.one * 9999f;
    StuckTimer_ = StuckCooldown;
    JumpTimer_ = 0f;
    SetState(0);
  }

  public virtual void OnInit() { }
  public virtual void OnEnter() { }
  public virtual void OnUpdate() { }
  public virtual void OnExit() { }
  public virtual void OnMessage(string msg, GameObject sender) { }
  public virtual void JustDestroyed() { }

  public void InternalMessage(string msg) {
    OnMessage(msg, gameObject);
  }
  public void BroadcastMessage(string msg) {
    if(Messenger_ != null)
      Messenger_.BroadcastMessage(msg, gameObject);
  }

  private void CalculatePath(Vector3 target, GameObject Planet)
  {
    //do Astar and store path to Waypoints
  }
  public void SetGoal(Vector3 target)
  {
    Waypoints_.Clear();

    if (UseAStar)
      CalculatePath(target, Planet);
    else
      Waypoints_.Enqueue(target);

    CurrentGoal_ = Vector3.one * 9999f;
    EndGoal_ = target;
  }
  public Vector3 GetGoal()
  {
    return EndGoal_;
  }
  public Vector3 GetAdjustedHeight()
  {
    return transform.position + transform.up * HeightOffset;
  }
  private bool AtGoal(Vector3 goal)
  {
    //adjustment to get top-most surface
    float tempHeight = 2.5f;

    RaycastHit[] results = Physics.RaycastAll(goal + -GetToPlanetCenter() * tempHeight, GetToPlanetCenter(), (Planet.transform.position - goal).magnitude);
    //self exception
    Vector3 goalOnSurface = Vector3.zero;
    for (int i = 0; i < results.Length; ++i) {
      if(results[i].transform == transform)
        continue;
      goalOnSurface = results[i].point;
    }

    results = Physics.RaycastAll(GetAdjustedHeight() + -GetToPlanetCenter() * tempHeight, GetToPlanetCenter(), (Planet.transform.position - GetAdjustedHeight()).magnitude);
    //self exception
    Vector3 posOnSurface = Vector3.zero;
    for (int i = 0; i < results.Length; ++i) {
      if (results[i].transform == transform)
        continue;
      posOnSurface = results[i].point;
    }

    //Debug.DrawLine(goal + -GetToPlanetCenter() * tempHeight, goalOnSurface, Color.blue);
    //Debug.DrawLine(GetAdjustedHeight() + -GetToPlanetCenter() * tempHeight, posOnSurface, Color.magenta);

    if ((posOnSurface - goalOnSurface).magnitude > GoalToleranceDist)
      return false;
    return true;
  }
  public float GetTimeInState()
  {
    return TimeInState_;
  }

  public Vector3 GetToPlanetCenter()
  {
    if (this.Planet == null)
        return Vector3.zero;
    Vector3 result = Planet.transform.position - GetAdjustedHeight();
    result.Normalize();
    return result;
  }
  private Vector3 MoveToGoal() {
    if (EndGoal_ == Vector3.one * 9999f)
      return Vector3.zero;

    if (CurrentGoal_ == Vector3.one * 9999f)
      CurrentGoal_ = Waypoints_.Peek();
    
    if (AtGoal(CurrentGoal_)) {
      Waypoints_.Dequeue();
      if (Waypoints_.Count == 0) {
        EndGoal_ = Vector3.one * 9999f; 
        CurrentGoal_ = Vector3.one * 9999f;
        InternalMessage("Arrived");
        return Vector3.zero;
      }
      else
        CurrentGoal_ = Waypoints_.Peek();
    }

    Vector3 dir = CurrentGoal_ - transform.position;
    dir = Vector3.ProjectOnPlane(dir, GetToPlanetCenter());
    dir.Normalize();
    return dir;
  }
  public void Jump()
  {
    MyRigidbody_.AddForce(-GetToPlanetCenter() * JumpPower, ForceMode.Impulse);
    Jumping_ = true;
    JumpTimer_ = JumpCooldown;
  }
  private bool HurdleObstacles(Vector3 moveVec)
  {
    if (!CheckGroundStatus())
      return false;

    if (moveVec == Vector3.zero || JumpPower == 0)
      return false;

    if (Jumping_)
      return false;

    //Debug.DrawRay(GetAdjustedHeight(), moveVec * MoveSpeed / 5f, Color.red);

    //check in front for obstacle
    RaycastHit result;
    LayerMask notBullets = 1 << LayerMask.NameToLayer("Bullet");
    notBullets = ~notBullets;
    if (!Physics.SphereCast(GetAdjustedHeight(), AvoidDetectWidth, moveVec, out result, MoveSpeed / 5f, notBullets))
      return false;

    //Debug.DrawRay(GetAdjustedHeight() + -GetToPlanetCenter() * JumpPower * 0.35f, moveVec * result.distance, Color.yellow);

    //check above if able to jump over
    Vector3 dir = result.point - GetAdjustedHeight();
    if (Physics.SphereCast(GetAdjustedHeight() + -GetToPlanetCenter() * JumpPower * 0.35f, AvoidDetectWidth, dir, out result, dir.magnitude, notBullets))
      return false;

    //Jump
    Jump();
    return true;
  }
  private Vector3 AvoidObstacles(Vector3 moveVec)
  {
    if (moveVec == Vector3.zero)
      return Vector3.zero;

    RaycastHit result;
    if (Physics.SphereCast(GetAdjustedHeight(), AvoidDetectWidth, moveVec, out result, AvoidDetectDistance)) {
      //Debug.DrawLine(GetAdjustedHeight(), result.point, Color.red);
      Vector3 toObject = result.point - GetAdjustedHeight();
      Vector3 repulsion = Vector3.ProjectOnPlane(result.normal, toObject);
      repulsion.Normalize();
      repulsion = Vector3.ProjectOnPlane(repulsion, GetToPlanetCenter());
      repulsion.Normalize();
      moveVec += repulsion * AvoidEffectiveness / result.distance;
      moveVec.Normalize();
    }

    return moveVec;
  }
  private void ApplyForces(Vector3 moveVec)
  {
    //Movement
    if(moveVec != Vector3.zero)
      MyRigidbody_.AddForce(moveVec * MoveSpeed);
    //Drag
    Vector3 surfaceVel = Vector3.ProjectOnPlane(MyRigidbody_.velocity, GetToPlanetCenter());
    MyRigidbody_.AddForce(-surfaceVel * Drag);
    //Gravity
    MyRigidbody_.AddForce(GetToPlanetCenter() * GravityFactor);
  }
  private void OrientToVelocity(Vector3 moveVector)
  {
    if (!OrientTowardsVelocity || moveVector == Vector3.zero)
      return;

    transform.LookAt(transform.position + moveVector, -GetToPlanetCenter());
  }

  public Vector3 GetLastPosition()
  {
    return LastPosition_;
  }
  private bool CheckGroundStatus()
  {
    RaycastHit result;
    if (Physics.SphereCast(GetAdjustedHeight(), GroundCheckWidth, GetToPlanetCenter(), out result, GroundCheckDistance)) {
      if (Vector3.Angle(result.normal, -GetToPlanetCenter()) < SlopeTolerance) {
        if (Jumping_ && JumpTimer_ < 0f)
          Jumping_ = false;
        GroundStatus_ = true;
        return true;
      }
    }
    GroundStatus_ = false;
    return false;
  }
  public bool GetGroundStatus()
  {
    return GroundStatus_;
  }
  private void Unstuck(Vector3 moveVec) {
    if((LastPosition_ - transform.position).magnitude < 0.001f && moveVec != Vector3.zero)
      StuckTimer_ -= Time.deltaTime;

    if (StuckTimer_ < 0f) {
      Jump();
      StuckTimer_ = StuckCooldown;
    }
  }

  void FixedUpdate()
  {
    OnUpdate();

    Vector3 moveVec = MoveToGoal();

    if (!HurdleObstacles(moveVec))
      moveVec = AvoidObstacles(moveVec);

    ApplyForces(moveVec);
    OrientToVelocity(moveVec);

    Unstuck(moveVec);
    LastPosition_ = transform.position;

    JumpTimer_ -= Time.deltaTime;
    TimeInState_ += Time.deltaTime;
  }
  
  public bool LineOfSight(GameObject target, float inCone = 180f)
  {
    if (target == null)
        return false;

    RaycastHit result;
    Vector3 direction = target.transform.position - GetAdjustedHeight();
    if (inCone != 180f && Vector3.Angle(transform.forward, direction) > inCone)
      return false;

    if (Physics.Raycast(GetAdjustedHeight(), direction, out result, direction.magnitude)) {
      //Debug.DrawLine((transform.position + CenterOffset), result.point, Color.red);
      if (result.collider.gameObject.GetInstanceID() == target.GetInstanceID())
        return true;
    }
    return false;
  }
  public GameObject FindNearestWithTag(string tag, float inSightCone = 180f)
  {
    float closest = 999999f;
    GameObject result = null;
    GameObject[] list = GameObject.FindGameObjectsWithTag(tag);
    for (int i = 0; i < list.Length; ++i) {
      float dist = (list[i].transform.position - transform.position).magnitude;
      if (inSightCone != 180f && !LineOfSight(list[i], inSightCone))
        continue;
      if (dist < closest) {
        result = list[i];
        closest = dist;
      }
    }
    return result;
  }
  public void LookAt(Vector3 target)
  {
    if (target == null)
        return;
    transform.LookAt(target, -GetToPlanetCenter());
  }

  void OnDestroy()
  {
    StressMgr_.DecreasePop();
    JustDestroyed();

    if (Messenger_ != null)
      Messenger_.Unregister(OnMessage);
  }
}
