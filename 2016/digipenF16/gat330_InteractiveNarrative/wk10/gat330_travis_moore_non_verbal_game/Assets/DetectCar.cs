///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — DetectCar.cs
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

public class DetectCar : MonoBehaviour
{
    #region FIELDS
    Transform tr;
    SphereCollider sc;
    ExplosiveDeath ed;
    public LayerMask layerToDetect;
    public float delayBeforeDeath = 0.75f;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        sc = GetComponent<SphereCollider>();
        ed = GetComponent<ExplosiveDeath>();
        tr = transform;
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
    void OnCollisionEnter(Collision _c)
    {
        //print(_c.collider.gameObject.layer);
        if ((layerToDetect.value & 1<<_c.gameObject.layer) != 0)
        {
            StartCoroutine(FlyBeforeDeath());
        }
    }
    IEnumerator FlyBeforeDeath()
    {
        yield return new WaitForSeconds(delayBeforeDeath);
        ed.Explode(tr.position);
        Destroy(this.gameObject);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
