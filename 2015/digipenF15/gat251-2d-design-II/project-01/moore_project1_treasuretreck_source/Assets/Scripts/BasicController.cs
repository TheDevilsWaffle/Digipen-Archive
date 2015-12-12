using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour
{
	[SerializeField] float JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float GravityMultiplier = 2f;
	[SerializeField] float RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float MoveSpeedMultiplier = 1f;
	[SerializeField] float AirControl = 0.1f;
	[SerializeField] float RotateSpeedMuliplier = 1f;
	[SerializeField] float AnimSpeedMultiplier = 1f;
	[SerializeField] float GroundCheckDistance = 0.1f;
	[SerializeField] float GroundDrag = 0.8f;
	[SerializeField] float AirDrag = 0.1f;

	private Transform PlayerCam; // A reference to the main camera in the scenes transform
//	private CameraController CameraControl;

	Vector3 LastFrameMovement;

	Rigidbody Rigidbody;
	Animator Animator;
	bool IsGrounded;

	float OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	//float TurnAmount;
	float ForwardAmount;
	Vector3 GroundNormal;

	float CapsuleHeight;
	Vector3 CapsuleCenter;
	CapsuleCollider Capsule;

	bool Crouching;

	void Start ()
	{
		Animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody>();
		Capsule = GetComponent<CapsuleCollider>();
		CapsuleHeight = Capsule.height;
		CapsuleCenter = Capsule.center;

//		get the transform of the main camera
		if (Camera.main != null)
		{
			PlayerCam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning(
				"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}
//		CameraControl = PlayerCam.GetComponent<CameraController>();
			
		Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		OrigGroundCheckDistance = GroundCheckDistance;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Tells us if we are on the ground or not
		Debug.Log (IsGrounded);

		//Get movement input in the form of a vector
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		Vector3 moveVec = new Vector3 (moveX, 0f, moveY);
		if (moveVec.magnitude > 1f) moveVec.Normalize();

		//Get the movement vector relative to the camera
		moveVec = PlayerCam.transform.TransformDirection(moveVec);

		//Special case when moving under the camera
		if(Vector3.Angle(PlayerCam.forward, Vector3.down) < 10f && moveY < 0f)
		{
			Rigidbody.AddForce(LastFrameMovement * 10f * MoveSpeedMultiplier);
		}

		//Check our ground status and project our movement vector onto the ground plane
		CheckGroundStatus();
		moveVec = Vector3.ProjectOnPlane(moveVec, GroundNormal);

		//Apply our movement forces
		if(IsGrounded)
		{
			Rigidbody.AddForce(moveVec * 10f * MoveSpeedMultiplier);
			LastFrameMovement = moveVec;

			//Applying jump forces
			Rigidbody.velocity += new Vector3(0,Input.GetAxis ("Jump") * JumpPower,0);
		}
		else
		{
			Rigidbody.AddForce(moveVec * 10f * MoveSpeedMultiplier * AirControl);
		}
		Rigidbody.AddForce(new Vector3(0,-10 * GravityMultiplier, 0));
		ApplyDrag();

		//Handle rotational movement
		float maxRotate = RotateSpeedMuliplier * 10f * Time.fixedDeltaTime;
		transform.forward = Vector3.RotateTowards (transform.forward, moveVec, maxRotate, 0f);
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
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.down * 0.9f), Vector3.down, out hitInfo, GroundCheckDistance))
		{
			GroundNormal = hitInfo.normal;
			IsGrounded = true;
			//Animator.applyRootMotion = true;
		}
		else
		{
			IsGrounded = false;
			GroundNormal = Vector3.up;
			//Animator.applyRootMotion = false;
		}
	}
}
