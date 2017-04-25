///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ShakeObject.cs
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
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class ShakeObject : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    GameObject objectToShake;
    float timer = 0f;
    [SerializeField]
    float shakeAmount = 0.7f;
    [SerializeField]
    float upDownAmount = 0.25f;
    [SerializeField]
    float shakeTime = 0.5f;
    [SerializeField]
    float dropOffTime = 1.5f;
    [SerializeField]
    LeanTweenType ease = LeanTweenType.easeInBack;

    Transform tr;
    Vector3 pos;
    Quaternion rot;

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
        //initial values
        timer = 0f;
        pos = tr.localPosition;
        rot = tr.localRotation;
        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {

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
        if (timer > 0f)
        {
            tr.localPosition = tr.localPosition + (Random.insideUnitSphere * shakeAmount * timer);
            timer -= Time.deltaTime;
        }

#if true
        UpdateTesting();
#endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Shake()
    {
        if (LeanTween.isTweening(objectToShake))
        {
            LeanTween.cancel(objectToShake);
        }
        timer = shakeTime;
        //start up/down rotate
        LTDescr shakeUDTween = LeanTween.rotateAroundLocal(objectToShake, Vector3.right, upDownAmount, shakeTime)
                                      .setEase(ease)
                                      .setLoopClamp()
                                      .setRepeat(-1);
        //slow down shake to zero
        LeanTween.value(objectToShake, upDownAmount, 0f, dropOffTime)
                 .setOnUpdate((float _value) => { shakeUDTween.setTo(Vector3.right * _value); })
                 .setEase(LeanTweenType.easeInOutQuad);

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CustomShake(float _amount, float _time, float _dropOffTime)
    {
        if (LeanTween.isTweening(objectToShake))
        {
            LeanTween.cancel(objectToShake);
        }
        timer = shakeTime;
        //start up/down rotate
        LTDescr shakeUDTween = LeanTween.rotateAroundLocal(objectToShake, Vector3.right, _amount, _time)
                                      .setEase(ease)
                                      .setLoopClamp()
                                      .setRepeat(-1);
        //slow down shake to zero
        LeanTween.value(objectToShake, _amount, 0f, _dropOffTime)
                 .setOnUpdate((float _value) => { shakeUDTween.setTo(Vector3.right * _value); })
                 .setEase(LeanTweenType.easeInOutQuad);

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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Shake();
        }
        //Keypad 1
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {

        }
        //Keypad 2
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {

        }
        //Keypad 3
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        //Keypad 4
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
        //Keypad 5
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {

        }
        //Keypad 6
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
    }
    #endregion
}