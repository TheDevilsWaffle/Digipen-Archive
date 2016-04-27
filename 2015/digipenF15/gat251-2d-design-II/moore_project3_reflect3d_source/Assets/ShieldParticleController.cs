using UnityEngine;
using System.Collections;

public class ShieldParticleController : MonoBehaviour {
    public void PlayShieldCharge()
    {
        
        this.gameObject.GetComponent<ParticleSystem>().Play();
    }
}
