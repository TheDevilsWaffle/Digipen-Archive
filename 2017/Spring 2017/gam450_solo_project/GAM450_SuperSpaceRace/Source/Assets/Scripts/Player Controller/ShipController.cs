///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ShipController.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;

#region ENUMS
public enum ThrustType
{
    BRAKING,
    CRUISE,
    MAX_SPEED,
    BOOST_SPEED
};
#endregion

#region EVENTS

#endregion

public class ShipController : MonoBehaviour
{
    #region FIELDS
    [Header("SHIP MODEL")]
    [SerializeField]
    GameObject ship;
    Transform ship_tr;

    [Header("CAMERA")]
    [SerializeField]
    Camera camera;
    Transform camera_tr;
    [SerializeField]
    public static Vector3 cameraOffset = new Vector3(0f, 3f, -12f);

    [Header("ACCELERATION")]
    [SerializeField]
    AnimationCurve accelerationCurve;
    [SerializeField]
    float accelerationSpeed = 150f;

    [Header("DECELERATION")]
    [SerializeField]
    AnimationCurve decelerationCurve;
    [SerializeField]
    float decelerationSpeed = 100f;

    [Header("SPEED")]
    [SerializeField]
    float cruiseSpeed = 75f;
    [SerializeField]
    float minSpeed = 50f;
    [SerializeField]
    float maxSpeed = 300f;
    float originalMaxSpeed;
    [SerializeField]
    float boostAccelerationFactor = 5f;
    [SerializeField]
    float boostSpeedFactor = 2f;

    [Header("TURNING")]
    [SerializeField]
    int[] sensitivities;
    int sensitivity = 1;
    [SerializeField]
    Vector3 angVel = new Vector3(0f, 0f, 0f);   //turning stuff

    float boostMaxSpeed;
    bool isBoosting = false;
    bool isBraking = false;
    float speed;
    float deltaSpeed;                           //(speed - cruisespeed)
    float acceleration;
    float deceleration;

    Vector3 shipRot;                            //turning stuff
    Transform tr;
    GamePadInputData player;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = GetComponent<Transform>();
        camera_tr = camera.GetComponent<Transform>();
        ship_tr = ship.GetComponent<Transform>();
        boostMaxSpeed = maxSpeed * boostSpeedFactor;
        originalMaxSpeed = maxSpeed;
        sensitivity = sensitivities[1];

        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        speed = cruiseSpeed;
        Events.instance.Raise(new EVENT_UPDATE_THRUST(ThrustType.CRUISE));
    }

    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_UPDATE_THRUST>(UpdateThrust);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// FixedUpdate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void FixedUpdate()
    {
        if (GamePadInput.players[0] != null)
        {
            player = GamePadInput.players[0];
            Thrust(player.RT, player.RTValue, player.RB, player.LB);

            //ANGULAR DYNAMICS//

            shipRot = ship_tr.localEulerAngles;

            //since angles are only stored (0,360), convert to +- 180
            if (shipRot.x > 180) shipRot.x -= 360;
            if (shipRot.y > 180) shipRot.y -= 360;
            if (shipRot.z > 180) shipRot.z -= 360;

            //vertical stick adds to the pitch velocity
            //         (*************************** this *******************************) is a nice way to get the square without losing the sign of the value
            angVel.x += player.LeftAnalogStick.y * Mathf.Abs(player.LeftAnalogStick.y) * sensitivity * Time.fixedDeltaTime;
            //print("angVel.x(" + angVel.x + ",) += analog.y(" + player.LeftAnalogStick.y + ") * sensitivity(" + sensitivity + ") * Time.fixedDeltaTime(" + Time.fixedDeltaTime);

            //horizontal stick adds to the roll and yaw velocity... also thanks to the .5 you can't turn as fast/far sideways as you can pull up/down
            float turn = player.LeftAnalogStick.x * Mathf.Abs(player.LeftAnalogStick.x) * sensitivity * Time.fixedDeltaTime;
            angVel.y += turn * .5f;
            angVel.z -= turn * .5f;
            //print("GAMEPAD STICKS\n" + player.LeftAnalogStick);

            RollAndYaw(player.RightAnalogStick);

            //moves camera (make sure you're GetChild()ing the camera's index)
            //I don't mind directly connecting this to the speed of the ship, because that always changes smoothly
            camera_tr.localPosition = cameraOffset + new Vector3(0, 0, -deltaSpeed * .02f);

            float sqrOffset = ship_tr.localPosition.sqrMagnitude;
            Vector3 offsetDir = ship_tr.localPosition.normalized;

            //this takes care of realigning after collisions, where the ship gets displaced due to its rigidbody.
            //I'm pretty sure this is the best way to do it (have the ship and the rig move toward their mutual center)
            ship_tr.Translate(-offsetDir * sqrOffset * 20 * Time.fixedDeltaTime);
            
            //(**************** this ***************) is what actually makes the whole ship move through the world!
            transform.Translate((offsetDir * sqrOffset * 50 + ship_tr.forward * speed) * Time.fixedDeltaTime, Space.World);

            //comment this out for starfox, remove the x and z components for shadows of the empire, and leave the whole thing for free roam
            transform.Rotate(shipRot.x * Time.fixedDeltaTime, (shipRot.y * Mathf.Abs(shipRot.y) * .02f) * Time.fixedDeltaTime, shipRot.z * Time.fixedDeltaTime);
            //print("SHIP Rotation\n" + ship_tr.rotation.eulerAngles);
            UpdateShipData();
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateShipData()
    {
        ShipData.IsBoosting = isBoosting;
        ShipData.IsBraking = isBraking;

        //is normal speed?
        if (!isBoosting)
        {
            if (ShipData.BoostSpeedPercentage > 0)
            {
                ShipData.SpeedPercentage = 1f;
                ShipData.BoostSpeedPercentage = Mathf.Clamp(((speed - originalMaxSpeed) * 3), 0f, boostMaxSpeed) / (boostMaxSpeed);
            }
            else
            {
                ShipData.SpeedPercentage = speed / maxSpeed;
                ShipData.BoostSpeedPercentage = 0f;
            }
        }
        //else boosting
        else if (isBoosting)
        {
            ShipData.SpeedPercentage = 1;
            ShipData.BoostSpeedPercentage = Mathf.Clamp(((speed - originalMaxSpeed) * 3), 0f, boostMaxSpeed) / (boostMaxSpeed);
        }
        ShipData.SpeedValue = (int)speed;
        
    }

    void UpdateThrust(EVENT_UPDATE_THRUST _event)
    {
        switch (_event.thrust)
        {
            case ThrustType.BRAKING:
                sensitivity = sensitivities[0];

                break;
            case ThrustType.CRUISE:
                sensitivity = sensitivities[1];

                break;
            case ThrustType.MAX_SPEED:
                sensitivity = sensitivities[2];

                break;
            case ThrustType.BOOST_SPEED:
                sensitivity = sensitivities[3];

                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_thrust"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateThrustType(ThrustType _thrust)
    {
        //Debug.Log("UpdateThrustType("+_thrust+")");
        
        if (BoostSystem.thrust != _thrust)
        {
            //Debug.Log("new trust("+_thrust+") is different than previous thrust("+BoostSystem.thrust+")");
            Events.instance.Raise(new EVENT_UPDATE_THRUST(_thrust));
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_thrust"></param>
    /// <param name="_thrustValue"></param>
    /// <param name="_brake"></param>
    /// <param name="_brakeValue"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Thrust(GamePadButtonState _thrust, float _thrustValue, GamePadButtonState _boost, GamePadButtonState _brake)
    {
        deltaSpeed = speed - cruiseSpeed;
        //acceleration = maxSpeed - speed;
        acceleration = (_thrustValue * accelerationSpeed);
        //Debug.Log("acceleration base = " + acceleration);
        acceleration *= accelerationCurve.Evaluate(speed / maxSpeed);
        //Debug.Log("acceleration (after anicurve) = " + acceleration);

        deceleration = (1f * decelerationSpeed);
        //Debug.Log("deceleration base = " + deceleration);
        deceleration *= decelerationCurve.Evaluate(speed / maxSpeed);
        //Debug.Log("deceleration (after anicurve) = " + deceleration);




        //throttle held
        if(_thrust == GamePadButtonState.HELD && _brake == GamePadButtonState.INACTIVE)
        {
            isBraking = false;
            maxSpeed = originalMaxSpeed;
            //boosting

            //Debug.Log("SHIELDS = "+ShieldSystem.energy+"\nLASERS = " + LaserSystem.energy);
            if (_thrust == GamePadButtonState.HELD && _boost == GamePadButtonState.HELD && (ShieldSystem.energy > 0 || LaserSystem.energy > 0))
            {
                isBoosting = true;
                maxSpeed = boostMaxSpeed;
                UpdateThrustType(ThrustType.BOOST_SPEED);
                //accelerate
                if (speed < boostMaxSpeed)
                {
                    //Debug.Log("thrust held, accelerating!\nspeed(" + speed + ") > (" + maxSpeed + ")");
                    speed += acceleration * Time.fixedDeltaTime;
                }
                else if (speed > boostMaxSpeed)
                {
                    //Debug.Log("thrust held, decellerating\nspeed(" + speed + ") > (" + maxSpeed + ")");
                    speed -= deceleration * Time.fixedDeltaTime;
                }
            }
            //just throttle
            else if (_boost == GamePadButtonState.INACTIVE || (ShieldSystem.energy <= 0 || LaserSystem.energy <= 0))
            {
                isBoosting = false;
                maxSpeed = originalMaxSpeed;
                UpdateThrustType(ThrustType.MAX_SPEED);
                if(speed < maxSpeed)
                {
                    speed += acceleration * Time.fixedDeltaTime;
                }
                else if (speed > maxSpeed)
                {
                    //Debug.Log("thrust held, decellerating\nspeed(" + speed + ") > (" + maxSpeed + ")");
                    speed -= deceleration * Time.fixedDeltaTime;
                }
            }
        }
        //braking
        if (_thrust == GamePadButtonState.INACTIVE && _brake == GamePadButtonState.HELD || _brake == GamePadButtonState.PRESSED)
        {
            isBraking = true;
            isBoosting = false;
            UpdateThrustType(ThrustType.BRAKING);
            if (speed > minSpeed)
            {
                speed -= deceleration * Time.fixedDeltaTime;
            }
        }
        //crusing
        else if(_thrust == GamePadButtonState.INACTIVE && _brake == GamePadButtonState.INACTIVE)
        {
            isBoosting = false;
            isBraking = false;
            speed -= Mathf.Clamp(deltaSpeed * Mathf.Abs(deltaSpeed), -30, 100) * Time.fixedDeltaTime;
            UpdateThrustType(ThrustType.CRUISE);
            if (speed < cruiseSpeed)
            {
                speed += acceleration * Time.fixedDeltaTime;
            }
            //else if (speed > cruiseSpeed)
            //{
            //    //Debug.Log("thrust held, decellerating\nspeed(" + speed + ") > (" + maxSpeed + ")");
            //    speed -= deceleration * Time.fixedDeltaTime;
            //}
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rsValue"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RollAndYaw(Vector3 _rsValue)
    {
        //rs add to the roll and yaw.  No deltatime here for a quick response
        //angVel.y adds to turning if wanted
        if (_rsValue.x < -0.2f)
        {
            //angVel.y -= 20;
            angVel.z += 50;
            speed -= 5 * Time.fixedDeltaTime;
        }
        if (_rsValue.x > 0.2f)
        {
            //angVel.y += 20;
            angVel.z -= 50;
            speed -= 5 * Time.fixedDeltaTime;
        }

        //angular velocity is higher when going slower, and vice versa.
        angVel /= 1 + deltaSpeed * .001f;

        //this is what limits your angular velocity.  Basically hard limits it at some value due to the square magnitude, you can
        //tweak where that value is based on the coefficient
        angVel -= angVel.normalized * angVel.sqrMagnitude * .08f * Time.fixedDeltaTime;


        //and finally rotate.  
        ship_tr.Rotate(angVel * Time.fixedDeltaTime);

        //this limits your rotation, as well as gradually realigns you.  It's a little convoluted, but it's
        //got the same square magnitude functionality as the angular velocity, plus a constant since x^2
        //is very small when x is small.  Also realigns faster based on speed.  feel free to tweak
        ship_tr.Rotate(-shipRot.normalized * .015f * (shipRot.sqrMagnitude + 250) * (1 + speed / maxSpeed) * Time.fixedDeltaTime);
    }
    #endregion

    #region PUBLIC METHODS

    #endregion

    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_UPDATE_THRUST>(UpdateThrust);
    }
    #endregion
}
