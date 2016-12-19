///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — DetectTarget.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class DetectTarget : MonoBehaviour
{
    #region FIELDS
    public bool isOn = true;
    public float delayBeforeDeath = 0.75f;
    public bool stayAliveAfterDetection = false;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //print("LAYER TO DETECT = " + layerToDetect.value);
    }
    #endregion

    #region METHODS
    //void OnCollisionEnter(Collision _c)
    //{
    //    //print(_c.collider.gameObject.layer);
    //    if (_c.gameObject.GetComponent<PlayerData>() != null && _c.gameObject.GetComponent<PlayerData>().Status == PlayerStatus.VULNERABLE)
    //    {
    //        _c.gameObject.GetComponent<TelefragDetector>().KillPlayer();
    //    }
    //    if (!stayAliveAfterDetection)
    //        Destroy(this.gameObject);
    //}
    void OnTriggerEnter(Collider _c)
    {
        if (isOn)
        {
            //print(_c.collider.gameObject.layer);
            if (_c.gameObject.GetComponent<PlayerData>() != null && _c.gameObject.GetComponent<PlayerData>().Status == PlayerStatus.VULNERABLE)
            {
                _c.gameObject.GetComponent<TelefragDetector>().KillPlayer();
            }
            if (!stayAliveAfterDetection)
                Destroy(this.gameObject);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}