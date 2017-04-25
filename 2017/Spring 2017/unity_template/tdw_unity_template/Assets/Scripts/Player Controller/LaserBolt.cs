///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LaserBolt.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

#region ENUMS
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
public class EVENT_LASER_HIT_OBJECT : GameEvent
{
    public GameObject objectHit;
    public Vector3 contactPoint;
    public Vector3 contactNormal;
    public EVENT_LASER_HIT_OBJECT(GameObject _objectHit, Vector3 _contactPoint, Vector3 _contactNormal)
    {
        objectHit = _objectHit;
        contactPoint = _contactPoint;
        contactNormal = _contactNormal;
    }
}
#endregion

public class LaserBolt : MonoBehaviour
{
    #region FIELDS
    [Header("Explosion Prefab")]
    [SerializeField]
    GameObject explosionPrefab;
    [SerializeField]
    AudioClip sfx_laser_explosion;

    Transform tr;
    BoxCollider bc;
    Rigidbody rb;
    bool destroyUponImpact = true;
    float lifetime = 5f;
    float speed = 50f;
    Vector3 direction = Vector3.zero;
    float timer = 0f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //refs
        tr = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        //initial values
        if (direction == Vector3.zero)
        {
            direction = tr.forward.normalized;
        }
        timer = 0f;

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Fire();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        //Events.instance.AddListener<>();
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifetime)
        {
            Destroy(this.gameObject);
        }

    #if false
        UpdateTesting();
    #endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Initialize(float _speed, float _lifetime, Vector3 _direction, bool _destroyUponImpact = true)
    {
        speed = _speed;
        lifetime = _lifetime;
        destroyUponImpact = _destroyUponImpact;
        direction = _direction;
        //Debug.Log("direction sent = " + direction);
        if(direction == Vector3.zero || direction == null)
        {
            direction = tr.forward.normalized;
            //Debug.Log("direction after normalized = " + direction);
        }
    }
    public void Fire()
    {
        //Debug.Log("AddForce(" + direction + ", " + speed + ")");
        rb.AddForce((direction * speed), ForceMode.Impulse);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    private void OnCollisionEnter(Collision _collision)
    {
        if (destroyUponImpact)
        {
            if (_collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                GameObject _explosion = GameObject.Instantiate(explosionPrefab, _collision.contacts[0].point, Quaternion.identity);

                Events.instance.Raise(new EVENT_LASER_HIT_OBJECT(_collision.gameObject, _collision.contacts[0].point, _collision.contacts[0].normal));

                AudioSystem.Instance.MakeAudioSource(sfx_laser_explosion.name);

                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        //Events.instance.RemoveListener<>();
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}