/*/////////////////////////////////////////////////////////////////////////////
//SCRIPT - InGameMessageSystem.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMessageSystem : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    GameObject InGameMessageCanvas;
    GameObject InGameMessage;
    //TOP PRIORITY
    Color TopPriorityColor = new Color(1f, 0f, 55 / 255f, 1.0f);
    Vector3 TopPriorityPos = new Vector3(0f, 0f, 1f);
    Vector3 TopPriorityScale = new Vector3(1f, 1f, 1f);
    //NORMAL
    Color NormalColor = new Color(0f, 125 / 255f, 125 / 255f, 1.0f);
    Vector3 NormalPos = new Vector3(-1f, 0f, 1f);
    Vector3 NormalScale = new Vector3(0.5f, 0.5f, 0.5f);

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CreateMessage(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void CreateMessage(GameObject message_)
    {
    }
}
