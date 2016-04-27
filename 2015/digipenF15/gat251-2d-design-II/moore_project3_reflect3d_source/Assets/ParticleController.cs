using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    if(this.gameObject.transform.root.name == "Player2")
        {
            this.gameObject.GetComponent<ParticleSystem>().startColor = new Color(0f, 110f, 210f, 1f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        this.gameObject.GetComponent<ParticleSystem>().Play();
    }
}
