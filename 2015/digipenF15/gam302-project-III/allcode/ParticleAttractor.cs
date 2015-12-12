/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ParticleAttactor.cs
//AUTHOR - Auston Lindsay
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour
{

    [SerializeField] public GameObject ParticleObject;
    [HideInInspector] public ParticleSystem ParticleSystem;
    [HideInInspector] public Particle[] Particles;
    
    void Start()
    {
        //this.ParticleSystem = (ParticleSystem)(this.ParticleObject.GetComponent(typeof(ParticleEmitter)));
        //this.Particles = this.ParticleSystem.;
    }


    void Update()
    {
        //for (int i = 0; i < this.Particles.GetUpperBound(0);++i)
        //{
        //    this.Particles[i].position = Vector3.Lerp(this.Particles[i].position, this.transform.position, Time.deltaTime / 2.0f);
        //}
        //this.ParticleSystem.particles = this.Particles;
    }
}
