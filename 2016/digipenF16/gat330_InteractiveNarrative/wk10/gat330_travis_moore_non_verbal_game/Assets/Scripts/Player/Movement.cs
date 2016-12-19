///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Movement.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    #region FIELDS
    int playerNumber;

    [Header("MOVEMENT TYPE")]
    public bool isMovement2D;
    public bool isCameraRelative;
    public Transform targetCameraTransform;

    [Header("SPEED")]
    [Range(0f,1f)]
    public float slowAmount = 0.1f;
    public float moveSpeed = 10f;
    public float acceleration = 10f;
    float speed = 0f;
    public float maxSlopeAngle = 45f;

    [Header("ROTATION")]
    public float rotationSpeed;
    [SerializeField]
    AnimationCurve rotationCurve;
    Quaternion inputRotation;
    
    [Header("GROUND")]
    [SerializeField]
    Vector3 baseCornerOffset;
    [SerializeField]
    float groundCheckDistance;
    Vector3 averageGroundNormal;
    int groundContacts;
    [HideInInspector]
    public GameObject ground;

    [Header("GRAVITY")]
    public bool additionalGravity;
    public float gravity = 10f;
    public float groundGravityRatio = 0.1f;

    //refs
    GamePadInputData player;
    Rigidbody rb;
    Transform tr;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        Events.instance.AddListener<EVENT_FollowerDied>(SlowPlayer);
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        playerNumber = 0;
        speed = 0f;
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// FixedUpdate() used for moving with physics
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FixedUpdate ()
    {
        //only run if we exist and have input from a player
        if(this.gameObject != null && GamePadInput.players[playerNumber] != null)
        {
            //get ground status
            UpdateGroundContacts();

            //apply the movement
            ApplyMovement(GamePadInput.players[playerNumber].LeftAnalogStick,
                          GamePadInput.players[playerNumber].LeftAnalogStick_Status);

            //apply rotation (need to run movement first to update inputRotation
            ApplyRotation();

            //apply additional gravity if needed
            if (additionalGravity)
            {
                ApplyAdditionalGravity();
            }
        }
	}
    #endregion

    #region METHODS
    void SlowPlayer(EVENT_FollowerDied _event)
    {
        moveSpeed -= moveSpeed * slowAmount;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Check the ground by casting rays from the player base to the ground
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateGroundContacts()
    {
        //reset
        groundContacts = 0;
        averageGroundNormal = Vector3.zero;

        //create 9 rays in a cube shape
        CastRayWithOffset(0, 1, 0);
        CastRayWithOffset(1, 1, 0);
        CastRayWithOffset(0, 1, 1);
        CastRayWithOffset(-1, 1, 0);
        CastRayWithOffset(0, 1, -1);
        CastRayWithOffset(1, 1, 1);
        CastRayWithOffset(-1, 1, 1);
        CastRayWithOffset(-1, 1, -1);
        CastRayWithOffset(1, 1, -1);

        //get the averageGroundNormal based on # of groundContacts
        averageGroundNormal /= groundContacts;

        //if there are no groundContacts, we're jumping, set ground to null
        if (groundContacts == 0)
        {
            averageGroundNormal = Vector3.up;
            ground = null;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// casts a ray whose base is set by passed parameters
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    /// <param name="_z"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CastRayWithOffset(int _x, int _y, int _z)
    {
        //apply the offset
        Vector3 _cornerOffset = baseCornerOffset;
        _cornerOffset.x *= _x;
        _cornerOffset.y *= _y;
        _cornerOffset.z *= _z;

        //setup the pos to draw a line, then draw it
        Vector3 _lineStart = this.transform.TransformPoint(_cornerOffset);
        Debug.DrawLine(_lineStart, _lineStart - (Vector3.up * groundCheckDistance), Color.green, 0, false);

        foreach (RaycastHit hitInfo in Physics.RaycastAll(_lineStart, -Vector3.up, groundCheckDistance))
        {
            if (hitInfo.collider.transform.root == this.transform)
                continue;

            if (hitInfo.collider != null)
                ground = hitInfo.collider.transform.root.gameObject;

            //print(Vector3.Angle(hitInfo.normal, Vector3.up));
            if (Vector3.Angle(hitInfo.normal, Vector3.up) < maxSlopeAngle)
            {
                ++groundContacts;
                averageGroundNormal += hitInfo.normal;
                //print("Ground Normal from hitInfo normal: " + hitInfo.normal);
                break;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Applies velocity and updates inputRotation based on passed input
    /// </summary>
    /// <param name="_input"></param>
    /// <param name="_status"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyMovement(Vector3 _input, GamePadButtonState _status)
    {
        //if we're 2D movement, adjust _input to match 2D gameplay (switch out z for y, zero out y)
        if (isMovement2D)
        {
            
            _input = new Vector3(_input.x, 0f, _input.y);
        }

        //new variable to store this input, don't want to change raw input
        Vector3 _movement = _input;
        //print("START! movement is set to initial input = " + _input);

        //starting magnitude
        float _inputMagnitude = 1f;

        //movement should be relative to the camera
        if (isCameraRelative && targetCameraTransform != null)
        {
            _inputMagnitude = Mathf.Clamp01(_movement.magnitude);
            _movement = targetCameraTransform.TransformDirection(_movement);

            //print("movement is set relative to camera = " + _movement);
        }

        _movement = Vector3.ProjectOnPlane(_input, Vector3.up);
        //print("movement is projected on a plane = " + _movement);

        //update inputRotation
        if (_movement != Vector3.zero)
            this.inputRotation = Quaternion.LookRotation(_movement, Vector3.up);


        _movement.Normalize();
        //print("movement is now normalized = " + _movement);
        _movement *= moveSpeed * _inputMagnitude;
        //print("movement is * moveSpeed("+ moveSpeed +") and * inputMagnitude("+ _inputMagnitude +") = " + _movement);

        //Inherit velocity from the thing under us
        if (ground != null)
        {
            //print (this.GroundObject);

            Rigidbody _groundRigidbody = ground.GetComponent<Rigidbody>();
            if (_groundRigidbody != null)
            {
                _movement += _groundRigidbody.velocity;
                //print("movement is inheriting from ground = " + _movement);
            }
        }

        Vector3 newVelocity = new Vector3(_movement.x, rb.velocity.y, _movement.z) * Time.deltaTime * 60;
        rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, acceleration * Time.deltaTime);

        //print("final velocity = " + rb.velocity);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyRotation()
    {
        //get angle between where we face now and where input wants us to face
        float _angle = Quaternion.Angle(tr.rotation, inputRotation);
        //use the rotationCurve to get updatedAngle
        float _rotationCurveAngle = rotationCurve.Evaluate(_angle / 180);
        //apply rotation based on where we are now, where we want to go, at a speed that is determined
        //by the rotation speed, rotationCurve angle, and time
        tr.rotation = Quaternion.RotateTowards(tr.rotation, 
                                               inputRotation, 
                                               (rotationSpeed * 180 * _rotationCurveAngle * Time.deltaTime));
        //apply gravity
        if (groundContacts > 0)
            rb.AddForce(new Vector3(0, -gravity * 10 * groundGravityRatio, 0) * Time.deltaTime * 60);
        else
            rb.AddForce(new Vector3(0, -gravity * 10, 0) * Time.deltaTime * 60);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// applies additional gravity upon the player
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyAdditionalGravity()
    {
        //on ground gravity
        if (groundContacts > 0)
        {
            rb.AddForce(new Vector3(0, -gravity * 10 * groundGravityRatio, 0) * Time.deltaTime * 60);
        }
        //in air gravity
        else
        {
            rb.AddForce(new Vector3(0, -gravity * 10, 0) * Time.deltaTime * 60);
        }
    }

    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_FollowerDied>(SlowPlayer);
    }

    #endregion
}
