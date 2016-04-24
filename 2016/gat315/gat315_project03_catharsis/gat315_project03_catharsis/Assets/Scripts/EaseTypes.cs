/*////////////////////////////////////////////////////////////////////////
//SCRIPT: EaseTypes.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public enum EaseType { easeInQuad, easeOutQuad, easeInOutQuad,
                easeInCubic, easeOutCubic, easeInOutCubic,
                easeInSine, easeOutSine, easeInOutSine,
                easeInCirc, easeOutCirc, easeInOutCirc,
                easeInBounce, easeOutBounce, easeInOutBounce,
                easeInBack, easeOutBack, easeInOutBack,
                easeInElastic, easeOutElastic, easeInOutElastic,
                linear, spring,};

public class EaseTypes : MonoBehaviour
{
}