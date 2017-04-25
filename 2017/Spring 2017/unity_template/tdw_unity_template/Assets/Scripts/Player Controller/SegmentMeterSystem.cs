///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — SegmentMeterSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class SegmentMeterSystem : MonoBehaviour
{
    #region FIELDS
    RectTransform containerRT;

    [Header("SEGMENT")]
    [SerializeField]
    RectTransform segmentPrefab;

    [Header("SEGMENT METER ATTRIBUTES")]
    [SerializeField]
    int maxSegments;
    [SerializeField]
    int index;
    List<RectTransform> segments;

    [Header("ANIMATE IN")]
    [SerializeField]
    float scaleFactorPreIn;
    Vector3 scalePreIn;
    [SerializeField]
    float delayIn;
    [SerializeField]
    float scaleFactorIn;
    Vector3 scaleIn;

    [Header("ANIMATE OUT")]
    [SerializeField]
    float delayOut;
    [SerializeField]
    float scaleFactorOut;
    Vector3 scaleOut;

    float instant = 0.001f;
    float opaque = 1f;
    float transparent = 0f;
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
        containerRT = GetComponent<RectTransform>();

        //initial values
        index = 0;
        segments = new List<RectTransform> { };
        scalePreIn = Vector3.one * scaleFactorPreIn;
        scaleIn = Vector3.one * scaleFactorIn;
        scaleOut = Vector3.one * scaleFactorOut;

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        RebuildSegmentMeter(maxSegments);
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
    /// Update
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {

    #if false
        UpdateTesting();
    #endif
    }

    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// destroys all objects in the segment meter before rebuilding with SetSegmentMeterMax()
    /// </summary>
    /// <param name="_value">the new max limit of the segment meter</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void RebuildSegmentMeter(int _value)
    {
        foreach (RectTransform child in containerRT)
        {
            Destroy(child.gameObject);
        }
        SetSegmentMeterMax(_value);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets the segments in the segment meter to the passed value (cannot exceed max value)
    /// </summary>
    /// <param name="_value">amount to set segment meter to</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetSegmentsTo(int _value)
    {
        //new value is less than current index; Subtract
        if((_value - 1) < index)
        {
            //Debug.Log("new value(" + _value + ") is less than index(" + index + "), Subract(" + (index - _value) + ")");
            SubtractSegments(index - (_value - 1));
        }
        //new value is more than current index; add
        else
        {
            //Debug.Log("value - 1 = " +(_value - 1) + "and index = " + index);
            if ((_value - 1) == index)
            {
                //Debug.Log("new value is the same as index, do nothing");
                return;
            }
            
            else if (_value < maxSegments - 1)
            {
                //Debug.Log("new value(" + _value + ") is less than maxSegments(" + maxSegments + "), Add(" + ((_value - 1) - index) + ")");
                AddSegments(_value - index);
            }
            //just add to max
            else
            {
                //Debug.Log("new value(" + _value + ") is more than maxSegments(" + maxSegments + "), Add(" + ((maxSegments - 1) - index) + ")");
                AddSegments((maxSegments - 1) - index);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// subtracts segments from the segment meter to a limit of zero
    /// </summary>
    /// <param name="_value">amount to subtract from segment meter</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SubtractSegments(int _value)
    {
        StartCoroutine(Subtract(_value));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// adds segments to the segment meter up to its max
    /// </summary>
    /// <param name="_value">amount to add to segment menter</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AddSegments(int _value)
    {
        StartCoroutine(Add(_value));
    }
    #endregion

    #region PRIVATE METHODS
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets new segment meter max, resets segments, and calls coroutine InitializeSegmentMeter()
    /// </summary>
    /// <param name="_value">new max segment meter amount</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSegmentMeterMax(int _value)
    {
        //reset index & segments
        index = _value - 1;
        segments.Clear();

        //reset and add each segment to segments
        for (int i = 0; i < _value; ++i)
        {
            RectTransform _segment = Instantiate(segmentPrefab, containerRT, false);
            _segment.name = "Segment " + i;
            _segment.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            _segment.GetComponent<Image>().enabled = true;
            segments.Add(_segment);
        }

        //animate in the new segment meter
        StartCoroutine(InitializeSegmentMeter());
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// animates in entire segment meter
    /// </summary>
    /// <returns>waits for delayIn seconds</returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator InitializeSegmentMeter()
    {
        foreach (RectTransform _segment in segments)
        {
            AnimateInSegment(_segment);
            yield return new WaitForSeconds(delayIn);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// sets predefined scale before animating scale and alpha of a segment in
    /// </summary>
    /// <param name="_rt">segment to animate</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateInSegment(RectTransform _rt)
    {
        //Debug.Log("AnimateInSegment(" + _rt.name + ")");

        _rt.localScale = scalePreIn;
        _rt.GetComponent<HUDAnimator_Alpha>().AnimateImageAlphaIn();
        _rt.GetComponent<HUDAnimator_Scale>().AnimateScaleIn();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// animates scale and alpha of a segment out
    /// </summary>
    /// <param name="_rt">segment to animate</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void AnimateOutSegment(RectTransform _rt)
    {
        //Debug.Log("AnimateOutSegment(" + _rt.name + ")");

        _rt.GetComponent<HUDAnimator_Alpha>().AnimateImageAlphaOut();
        _rt.GetComponent<HUDAnimator_Scale>().AnimateScaleOut();
    } 
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// performs safety checks before subtracting x segments
    /// </summary>
    /// <param name="_value">amount of segments to subtract</param>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator Subtract(int _value)
    {
        for (int i = 0; i < _value; ++i)
        {
            if (index >= 0)
            {
                AnimateOutSegment(segments[index]);
                segments[index].name = segments[index].name.Replace("_DISABLED", "");
                --index;

                //Debug.Log("index = " + index);
                yield return new WaitForSeconds(delayOut);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// performs safety checks before adding segments
    /// </summary>
    /// <param name="_value">amount of segments to add</param>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator Add(int _value)
    {
        for (int i = 0; i < _value; ++i)
        {
            //Debug.Log("index = " + index + ", and maxSegments = " + maxSegments);

            //don't add if index is already at max
            if (index == maxSegments - 1)
            {
                break;
            }
            else if (index < maxSegments)
            {
                segments[index].name = segments[index].name + "_DISABLED";
                AnimateInSegment(segments[index]);

                //check the index value
                ++index;
                if (index >= maxSegments)
                {
                    index = maxSegments - 1;
                }

                yield return new WaitForSeconds(delayIn);
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
            SubtractSegments(3);
        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddSegments(2);
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetSegmentsTo(8);
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            RebuildSegmentMeter(15);
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