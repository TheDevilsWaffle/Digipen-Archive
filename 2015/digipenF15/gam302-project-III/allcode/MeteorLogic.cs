/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - MeteorLogic.cs
//AUTHOR - Enrique Rodriguez
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MeteorLogic : MonoBehaviour {
  public AudioClip explosionSfx; 

	public float LevelEdge;

	//void Start () {	}
	
	void Update () {
    CheckBounds();

    //raycast for collision warning
    Vector3 direction = gameObject.GetComponent<Rigidbody>().velocity;
    direction.Normalize();
    Vector3 myPosition = transform.position;
    RaycastHit result;
    if (Physics.Raycast(myPosition, direction, out result)) {
      //place warning on point of incoming meteor
      //Debug.DrawLine(myPosition, myPosition + direction * result.distance, Color.red, 0f, true);
    }
	}

  void OnCollisionEnter(Collision colEvent)
  {
    //meteor impact
    AudioSource.PlayClipAtPoint(explosionSfx, transform.position, 1.0f);
    
    Destroy(gameObject);
  }

  void CheckBounds()
  {
    if (transform.position.sqrMagnitude > LevelEdge * LevelEdge)
      Destroy(gameObject);
  }
}
