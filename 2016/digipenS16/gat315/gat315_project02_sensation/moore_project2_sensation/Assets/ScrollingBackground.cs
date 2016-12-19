using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class ScrollingBackground : MonoBehaviour
{

    public Transform[] Backgrounds;
    private float[] ParallaxScales;
    public float Smoothing = 2f;

    private Vector3 LastPos;

	// Use this for initialization
	void Start ()
    {
        this.LastPos = this.gameObject.transform.position;

        this.ParallaxScales = new float[this.Backgrounds.Length];

        for(int i =0; i < this.ParallaxScales.Length; ++i)
        {
            this.ParallaxScales[i] = this.Backgrounds[i].position.z * 1;
        }

	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        for( int i = 0; i < this.Backgrounds.Length; ++i)
        {
            Vector3 parallax = (this.LastPos - this.gameObject.transform.position) * (ParallaxScales[i] / this.Smoothing);

            this.Backgrounds[i].position = new Vector3(this.Backgrounds[i].position.x + parallax.x, this.Backgrounds[i].position.y + parallax.y, this.Backgrounds[i].position.z);
        }

        this.LastPos = this.gameObject.transform.position;
    }
}
