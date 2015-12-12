/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - MessageManager.cs
//AUTHOR - 
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

  public delegate void MessageFunction(string msg, GameObject sender);
  MessageFunction AllMessageFunctions;

	//void Start () {	}
	//void Update () { }

  public void Register(MessageFunction function)
  {
    AllMessageFunctions += function;
  }

  public void BroadcastMessage(string msg, GameObject sender)
  {
    if(AllMessageFunctions != null)
      AllMessageFunctions(msg, sender);
  }

  public void Unregister(MessageFunction function)
  {
    AllMessageFunctions -= function;
  }

  void OnApplicationQuit()
  {
    AllMessageFunctions = null;
  }

  void OnDestroy()
  {
    AllMessageFunctions = null;
  }
}
