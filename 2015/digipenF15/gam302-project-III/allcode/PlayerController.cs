/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - PlayerController.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float GravityMultiplier = 2f;
	[SerializeField] float RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float MoveSpeedMultiplier = 1f;
	[SerializeField] float AirControl = 0.1f;
	[SerializeField] float RotateSpeedMuliplier = 1f;
	[SerializeField] float AnimSpeedMultiplier = 1f;
	[SerializeField] float GroundCheckDistance = 0.1f;

    [SerializeField]
    public GameObject PlayerCam = null;

    //Max velocity should equal the squareroot of 2 times mass times acceleration over drag, or: sqrt( 2(mass)(acceleration) / drag) )?

    [Header("Drag Settings")]
    [SerializeField] float GroundDrag = 0.8f;
    [SerializeField] float AirDrag = 0.1f;
	[SerializeField] public GameObject Planet = null;
    [SerializeField]
    public GameObject PlanetNumoroTwo = null;

    //[HideInInspector] public GameObject PlayerCam; // A reference to the main camera in the scenes transform
//	private CameraController CameraControl;

	Vector3 LastFrameMovement;

	Rigidbody Rigidbody;
	//Animator Animator;
	bool IsGrounded;

	public Vector3 ToPlanetCenter = new Vector3 ();

	float OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	//float TurnAmount;
	float ForwardAmount;
	Vector3 GroundNormal;

    public bool CanMove = true;

    float CapsuleHeight;
	Vector3 CapsuleCenter;
	CapsuleCollider Capsule;

	bool Crouching;
    private AudioClip[] jumpClips;
    private AudioClip[] hurtClips;
    private int jumpIndex;
    private int hurtIndex;
    private AudioSource jumps;
    private AudioSource hurts;
    private float audioPlayCoolDown; 
    private void CycleThroughJumps()
    {
        if(audioPlayCoolDown < 0.0f)
        {
            AudioClip clip = jumpClips[jumpIndex];
            jumpIndex = (jumpIndex + 1) % 3;
            jumps.clip = clip;
            jumps.PlayOneShot(clip, 0.04f);
            audioPlayCoolDown = .5f;
        }
    }
 

     private void CycleThroughHurt()
     {
         hurtIndex = Random.Range(0, 3);
         AudioClip clip = hurtClips[hurtIndex];
         hurts.clip = clip;
         hurts.PlayOneShot(clip, .2f);

     }

	void Start ()
	{
        audioPlayCoolDown = .5f;
		//Animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody>();
		Capsule = GetComponent<CapsuleCollider>();
		CapsuleHeight = Capsule.height;
		CapsuleCenter = Capsule.center;

		// Get the transform of the main camera
		if (Camera.main.gameObject != null)
		{
			PlayerCam = Camera.main.gameObject;
		}
		else
		{
			//Debug.LogWarning(
				//"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}
		// CameraControl = PlayerCam.GetComponent<CameraController>();
			
		Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		OrigGroundCheckDistance = GroundCheckDistance;


        jumpClips = new AudioClip[] { (AudioClip)Resources.Load("Sounds/sfx/player/jump01", typeof(AudioClip)),
                                      (AudioClip)Resources.Load("Sounds/sfx/player/jump02", typeof(AudioClip)),
                                      (AudioClip)Resources.Load("Sounds/sfx/player/jump03", typeof(AudioClip))};

        hurtClips = new AudioClip[]{  (AudioClip)Resources.Load("Sounds/sfx/player/hurt01", typeof(AudioClip)),
                                      (AudioClip)Resources.Load("Sounds/sfx/player/hurt02", typeof(AudioClip)),
                                      (AudioClip)Resources.Load("Sounds/sfx/player/hurt03", typeof(AudioClip)),
                                      (AudioClip)Resources.Load("Sounds/sfx/player/hurt04", typeof(AudioClip))};

        jumps = gameObject.AddComponent<AudioSource>();
        hurts = gameObject.AddComponent<AudioSource>();

        jumpIndex = 0;
        hurtIndex = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        //this.IsGrounded = false;

		//Tells us if we are on the ground or not
		//Debug.Log (IsGrounded);

		//Get movement input in the form of a vector
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");

		ToPlanetCenter = this.Planet.transform.position - this.transform.position;
		ToPlanetCenter.Normalize();

        Vector3 vecToCamera = PlayerCam.transform.position - this.transform.position;
        float angle = Vector3.Angle(vecToCamera, -ToPlanetCenter);

        Vector3 cameraUp = PlayerCam.transform.up;
        Vector3 cameraRight = PlayerCam.transform.right;
        Vector3 cameraForward = PlayerCam.transform.right;

            //if (Input.GetKey(KeyCode.Space))
            //{
            //    this.Planet = this.PlanetNumoroTwo;
            //}

        /////////////////////////////////////

        //Use world vector components instead:
        //how much does the camera's positive y relate to the player's up (difference)
        //how much does the camera's positive x relate to the player's up (difference)

        float AngleDiffX = (PlayerCam.transform.TransformDirection(-ToPlanetCenter).x) * 90;
        float AngleDiffY = (PlayerCam.transform.TransformDirection(-ToPlanetCenter).y) * 90;
        if (angle > 90)
        {
            AngleDiffX = 180 - AngleDiffX;
            AngleDiffY = 180 - AngleDiffY;
        }

        //print(AngleDiffY);


        //If angle is between 0 and 45, use positive y axis
        //If between 45 and 90, use positive z
        //if between 90 and 135, use negative z
        //if between 135 and 180, use negative y
        Vector3 moveVec = Vector3.zero;
        if (this.CanMove)
            moveVec = new Vector3(moveX, 0f, moveY);

        //if (angle >= 0 && angle < 45)
        //{
        //    moveVec = new Vector3(moveX, moveY, 0f);
        //}
        //else if (angle >= 45 && angle < 90)
        //{
        //    moveVec = new Vector3(moveX, 0f, moveY);
        //}
        //else if (angle >= 90 && angle < 135)
        //{
        //    moveVec = new Vector3(moveX, 0f, moveY);
        //}
        //else if (angle >= 135 && angle <= 180)
        //{
        //    moveVec = new Vector3(moveX, moveY, 0f);
        //}
        //Vector3 moveVec = new Vector3(moveX, moveY, 0f);
        //if (AngleDiffX >= 0 && AngleDiffX < 45)
        //{
        //    moveVec = new Vector3(moveX, moveY, 0f);
        //}
        //else if (AngleDiffX >= 45 && AngleDiffX < 90)
        //{
        //    moveVec = new Vector3(moveX, 0f, moveY);
        //}
        //else if (AngleDiffX >= 90 && AngleDiffX < 135)
        //{
        //    moveVec = new Vector3(0f, moveY, moveX);
        //}
        //else if (AngleDiffX >= 135 && AngleDiffX <= 180)
        //{
        //    moveVec = new Vector3(0f, moveY, moveX);
        //}

        //if (AngleDiffY >= 0 && AngleDiffY < 45)
        //{
        //    moveVec = new Vector3(moveX, moveY, 0f);
        //}
        //else if (AngleDiffY >= 45 && AngleDiffY < 90)
        //{
        //    moveVec = new Vector3(moveX, 0f, moveY);
        //}
        //else if (AngleDiffY >= 90 && AngleDiffY < 135)
        //{
        //    moveVec = new Vector3(moveX, 0f, moveY);
        //}
        //else if (AngleDiffY >= 135 && AngleDiffY <= 180)
        //{
        //    moveVec = new Vector3(moveX, moveY, 0f);
        //}

        //moveVec = Vector3.zero;
        if (moveVec.magnitude > 1f)
            moveVec.Normalize();

        //print(angle);

        //Get the movement vector relative to the camera
        moveVec = PlayerCam.transform.TransformDirection(moveVec);
        //moveVec *= -vecToCamera;
        
        //Rotate right around the forward vector
        //moveVec = Quaternion.AngleAxis(AngleDiffX, cameraUp) * moveVec;

        //print(AngleDiffX);
        //print(moveVec);

        //Rotate up around right vector
        //moveVec = Quaternion.AngleAxis(AngleDiffY, cameraRight) * moveVec;

        //Check our ground status and project our movement vector onto the ground plane
        CheckGroundStatus();
		moveVec = Vector3.ProjectOnPlane (moveVec, ToPlanetCenter);
		moveVec.Normalize ();

        //print(moveVec);

        //Debug.Log (GroundNormal);
        //Debug.Log (ToPlanetCenter);

        //Apply our movement forces
        audioPlayCoolDown -= Time.deltaTime; 
        if (IsGrounded)
		{
			Rigidbody.AddForce(moveVec * 10f * MoveSpeedMultiplier);
			LastFrameMovement = moveVec;

			//Applying jump forces
			Rigidbody.velocity += GroundNormal * this.JumpPower * Input.GetAxis("Jump");;
            if(Input.GetAxis("Jump") != 0.0f)
                    CycleThroughJumps();
                
		}
		else
		{
			Rigidbody.AddForce(moveVec * 10f * MoveSpeedMultiplier * AirControl);
		}

		Rigidbody.AddForce(this.ToPlanetCenter * 10 * GravityMultiplier);
		ApplyDrag();

		//Handle rotational movement
		float maxRotate = RotateSpeedMuliplier * 10f * Time.fixedDeltaTime;
        //Vector3 faceDirection = Vector3.ProjectOnPlane(moveVec, ToPlanetCenter).normalized;
        
        if (moveVec.sqrMagnitude != 0)
        {
            //transform.LookAt(transform.position + Vector3.RotateTowards(transform.forward, moveVec, maxRotate, 10f), -ToPlanetCenter);
            transform.LookAt(transform.position + moveVec, -ToPlanetCenter);
        }
        else
        {
            transform.LookAt(transform.position + Vector3.ProjectOnPlane(transform.forward, ToPlanetCenter), -ToPlanetCenter);
        }
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

    void OnCollisionStay(Collision collisionInfo_)
    {
        Vector3 normals = Vector3.zero;
        foreach (ContactPoint contactPoint in collisionInfo_.contacts)
        {
            //this.IsGrounded = true;
            //if (Vector3.Angle(contactPoint.normal, this.transform.up) < 90)
            //    this.IsGrounded = true;

            normals += contactPoint.normal;
        }

        this.IsGrounded = Vector3.Angle(normals.normalized, -this.ToPlanetCenter.normalized) < 90;
    }

    void OnCollisionEnter(Collision collisionInfo_)
    {
        //this.IsGrounded = true;
        //Vector3 normals = Vector3.zero;
        //foreach(ContactPoint contactPoint in collisionInfo_.contacts)
        //{
        //    this.IsGrounded = true;
        //    if (Vector3.Angle(contactPoint.normal, this.transform.up) < 90)
        //        this.IsGrounded = true;

        //    //normals += contactPoint.normal;
        //}
        //if (Vector3.Angle(normals.normalized, this.ToPlanetCenter.normalized) < 90)
        //{
            
        //}
        if(collisionInfo_.gameObject.tag == "Bullet")
        {
            CycleThroughHurt();
        }

    }
    void OnCollisionExit(Collision collisionInfo_)
    {
        //this.IsGrounded = false;
    }

    void CheckGroundStatus()
	{
        RaycastHit hitInfo;
        int ignorePlayerMask = 1 << LayerMask.NameToLayer("Player");
        ignorePlayerMask = ~ignorePlayerMask;

        Vector3 rayStart = transform.position + (ToPlanetCenter * 0.1f);
        Debug.DrawLine(rayStart, rayStart + (ToPlanetCenter * this.GroundCheckDistance));
        if (Physics.Raycast(rayStart, ToPlanetCenter, out hitInfo, this.GroundCheckDistance))
        {
            this.GroundNormal = hitInfo.normal;
            IsGrounded = true;
            return;
        }
        rayStart = transform.position + (-ToPlanetCenter * 0.3f) + (this.transform.forward * (this.transform.lossyScale.z / 2));
        Debug.DrawLine(rayStart, rayStart + (ToPlanetCenter * this.GroundCheckDistance));
        if (Physics.Raycast(rayStart, ToPlanetCenter, out hitInfo, this.GroundCheckDistance, ignorePlayerMask))
        {
            this.GroundNormal = hitInfo.normal;
            IsGrounded = true;
            return;
        }
        rayStart = transform.position + (-ToPlanetCenter * 0.3f) - (this.transform.forward * (this.transform.lossyScale.z / 2));
        Debug.DrawLine(rayStart, rayStart + (ToPlanetCenter * this.GroundCheckDistance));
        if (Physics.Raycast(rayStart, ToPlanetCenter, out hitInfo, this.GroundCheckDistance, ignorePlayerMask))
        {
            this.GroundNormal = hitInfo.normal;
            IsGrounded = true;
            return;
        }
        rayStart = transform.position + (-ToPlanetCenter * 0.3f) + (this.transform.right * (this.transform.lossyScale.x / 2));
        Debug.DrawLine(rayStart, rayStart + (ToPlanetCenter * this.GroundCheckDistance));
        if (Physics.Raycast(rayStart, ToPlanetCenter, out hitInfo, this.GroundCheckDistance, ignorePlayerMask))
        {
            this.GroundNormal = hitInfo.normal;
            IsGrounded = true;
            return;
        }
        rayStart = transform.position + (-ToPlanetCenter * 0.3f) - (this.transform.right * (this.transform.lossyScale.x / 2));
        Debug.DrawLine(rayStart, rayStart + (ToPlanetCenter * this.GroundCheckDistance));
        if (Physics.Raycast(rayStart, ToPlanetCenter, out hitInfo, this.GroundCheckDistance, ignorePlayerMask))
        {
            this.GroundNormal = hitInfo.normal;
            IsGrounded = true;
            return;
        }
        this.IsGrounded = false;
    }
}
