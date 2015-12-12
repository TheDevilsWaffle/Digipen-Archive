/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Cheats.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cheats:MonoBehaviour
{
    [Header("LocationCheats")]
    [SerializeField]
    KeyCode Mountains = KeyCode.Keypad1;
    [SerializeField]
    Vector3 MountainsPos = new Vector3(48, 70, 37);
    [HideInInspector]
    Quaternion MountainsRotation = new Quaternion(0, 0.4f, 0, 0.9f);
    [SerializeField]
    KeyCode Forest = KeyCode.Keypad2;
    [SerializeField]
    Vector3 ForestPos = new Vector3(73, 29, 48);
    [HideInInspector]
    Quaternion ForestRotation = new Quaternion(0, 0.9f, 0, 0.5f);
    [SerializeField]
    KeyCode Town = KeyCode.Keypad3;
    [SerializeField]
    Vector3 TownPos = new Vector3(117, 32, 110);
    [HideInInspector]
    Quaternion TownRotation = new Quaternion(0, 0.4f, 0, 0.9f);



    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {

    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //Debug.Log(this.gameObject.transform.rotation);
        if(Input.GetKeyDown(this.Mountains))
        {
            this.gameObject.transform.position = this.MountainsPos;
            this.gameObject.transform.rotation = this.MountainsRotation;
        }
        if (Input.GetKeyDown(this.Forest))
        {
            this.gameObject.transform.position = this.ForestPos;
            this.gameObject.transform.rotation = this.ForestRotation;
        }
        if (Input.GetKeyDown(this.Town))
        {
            this.gameObject.transform.position = this.TownPos;
            this.gameObject.transform.rotation = this.TownRotation;
        }
    }
}