/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - MenuSwitcher.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuSwitcher : MonoBehaviour
{
    GameObject CurrentMenu;
    int CurrentMenuNumber;
    int ArrayLength;
    GameObject[] PowerpointArray;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Powerpoint");
        //sort by alphabetical order


        PowerpointArray[0].gameObject.SetActive(true);
        this.CurrentMenu = PowerpointArray[0];
        this.CurrentMenuNumber = 0;
        this.ArrayLength = PowerpointArray.Length;
        Debug.Log("ARRAY LENGTH = " + this.ArrayLength);
        Debug.Log(this.CurrentMenu);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SortArrayByName(GameObject, GameObject)
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    int SortArrayByName(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            this.TraverseForwardInArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            this.TraverseBackwardInArray();
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TraverseForwardInArray()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void TraverseForwardInArray()
    {
        if (this.CurrentMenuNumber + 1 <= this.ArrayLength - 1)
        {
            this.CurrentMenu.gameObject.SetActive(false);

            this.CurrentMenuNumber += 1;
            Debug.Log(this.CurrentMenuNumber);
            PowerpointArray[this.CurrentMenuNumber].gameObject.SetActive(true);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: TraverseBackwardInArray()
    //NOTE: 
    /////////////////////////////////////////////////////////////////////////*/
    void TraverseBackwardInArray()
    {
        if (this.CurrentMenuNumber - 1 >= 0)
        {
            this.CurrentMenu.gameObject.SetActive(false);

            this.CurrentMenuNumber -= 1;
            Debug.Log(this.CurrentMenuNumber);
            PowerpointArray[this.CurrentMenuNumber].gameObject.SetActive(true);
        }
    }
}
