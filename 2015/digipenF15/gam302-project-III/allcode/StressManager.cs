/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - StressManager.cs
//AUTHOR - 
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class StressManager : MonoBehaviour {

  public int PopulationLimit = 500;
  public int CurrentPopulation = 0;

	//void Start () {	}
	
	void Update ()
  {
    //GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    //CurrentPopulation = allEnemies.Length;
  }

  public int GetPop()
  {
    return CurrentPopulation;
  }

  public void IncreasePop()
  {
    ++CurrentPopulation;
  }

  public void DecreasePop()
  {
    --CurrentPopulation;
  }

  public bool AtCap() {
    if (CurrentPopulation < PopulationLimit)
      return false;

    return true;
  }
}
