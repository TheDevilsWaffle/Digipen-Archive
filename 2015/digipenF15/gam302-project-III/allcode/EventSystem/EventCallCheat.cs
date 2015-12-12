/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EventCallCheat.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class EventCallCheat : MonoBehaviour {

  public GameObject MeteorShowerEvent;
  public GameObject RaiderEvent;

  GameObject MostRecentEventObject_;
  int Counter = 0;

	//void Start () {	}
	
	void Update () {
    if (Input.GetButtonDown("Fire3")) { //LEFT SHIFT
      if (Counter == 0) {
        MostRecentEventObject_ = Instantiate(MeteorShowerEvent, new Vector3(0, 30, 0), Quaternion.identity) as GameObject;
        MostRecentEventObject_.GetComponent<EventMeteorShower>().Difficulty = 10;
      }
      else if (Counter == 1) {
        MostRecentEventObject_ = Instantiate(RaiderEvent, new Vector3(-30, 0, 0), Quaternion.identity) as GameObject;
        MostRecentEventObject_.GetComponent<EventRaiders>().Difficulty = 10;
      }
      else if (Counter % 2 == 0) {
        MostRecentEventObject_ = Instantiate(MeteorShowerEvent, new Vector3(-30, 0, 0), Quaternion.identity) as GameObject;
        MostRecentEventObject_.GetComponent<EventMeteorShower>().Difficulty = Counter * 10 / 2; ;
      }
      else if (Counter % 2 == 1) {
        MostRecentEventObject_ = Instantiate(RaiderEvent, new Vector3(-30, 0, 0), Quaternion.identity) as GameObject;
        MostRecentEventObject_.GetComponent<EventRaiders>().Difficulty = Counter * 10 / 2;
      }
      ++Counter;
    }
	}
}
