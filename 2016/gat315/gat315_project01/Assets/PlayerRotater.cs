using UnityEngine;
using System.Collections;

public class PlayerRotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void RotatePlayer()
    {
        this.gameObject.transform.Find("PlayerArt").transform.Rotate(0f, 180f, 0f);
	}
}
