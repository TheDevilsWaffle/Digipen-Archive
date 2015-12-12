/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ScriptTester_HP.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class ScriptTester_HP : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    KeyCode DecreaseHPKey = KeyCode.KeypadMinus;
    [SerializeField]
    KeyCode IncreaseHPKey = KeyCode.KeypadPlus;
    [SerializeField]
    KeyCode EvaluateHPKey = KeyCode.KeypadDivide;
    [SerializeField]
    KeyCode SetHPKey = KeyCode.KeypadPeriod;
    [SerializeField]
    float SetHPValue = 17.6f;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    public void Update()
    {
        if(Input.GetKeyDown(this.DecreaseHPKey))
        {
            print("DOIN IT!");
            this.gameObject.GetComponent<Health>().DecreaseHealth(5f);
        }

        if (Input.GetKeyDown(this.IncreaseHPKey))
        {
            this.gameObject.GetComponent<Health>().IncreaseHealth(2.5f);
        }

        if (Input.GetKeyDown(this.EvaluateHPKey))
        {
            Debug.Log(this.gameObject.GetComponent<Health>().EvaluateHealth());
        }

        if (Input.GetKeyDown(this.SetHPKey))
        {
            this.gameObject.GetComponent<Health>().SetHealthToValue(this.SetHPValue);
        }
    }
}
