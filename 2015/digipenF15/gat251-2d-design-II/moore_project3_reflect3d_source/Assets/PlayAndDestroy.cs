using UnityEngine;
using System.Collections;

public class PlayAndDestroy : MonoBehaviour
{
    void Start()
    {

    }
	// Update is called once per frame
	void Update () {
	    if(this.gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(this.gameObject);
        }
	}
}
