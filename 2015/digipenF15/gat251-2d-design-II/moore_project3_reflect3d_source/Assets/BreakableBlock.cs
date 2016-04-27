/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - BreakableBlock.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class BreakableBlock : MonoBehaviour
{
    Vector3 Position;
    Quaternion Rotation;
    float ScaleX;
    float ScaleY;
    float ScaleZ;
    float Force = 0f;
    float Radius = 5f; // explosion force decreases to zero at this distance
    int Pieces;
    [SerializeField]
    AudioClip ExplosiveSFX;
    GameObject AudioController;

    Material OriginalMaterial;

    void Start()
    {
        this.Position = this.gameObject.transform.position;
        this.Rotation = this.gameObject.transform.rotation;
        this.Pieces = RandomDebrisAmount();
        this.ScaleX = this.gameObject.transform.localScale.x;
        this.ScaleY = this.gameObject.transform.localScale.y;
        this.ScaleZ = this.gameObject.transform.localScale.z;
        this.OriginalMaterial = this.gameObject.GetComponent<Renderer>().material;

        //get the audio controller
        this.AudioController = GameObject.FindWithTag("AudioController").gameObject;
    }

    public void ExplodeIntoDebris(Vector3 hitPoint_)
    {
        // create replacement pieces:
        for (int i = 0; i < this.Pieces; ++i)
        {
            GameObject brokenBrick = GameObject.Instantiate((GameObject)Resources.Load("Debris", typeof(GameObject)),
                                                                 this.Position,
                                                                 Quaternion.identity) as GameObject;
            brokenBrick.GetComponent<Renderer>().material = this.OriginalMaterial;
            brokenBrick.transform.localScale = RandomScale();
            brokenBrick.GetComponent<Rigidbody>().AddExplosionForce((this.Force), hitPoint_, this.Radius);
        }

        //play explosive sound
        this.AudioController.GetComponent<SoundManager>().PlayPaitiently(this.ExplosiveSFX);
        Destroy(this.gameObject); // destroy original brick
    }

    int RandomDebrisAmount()
    {
        return Random.Range(3, 7);
    }

    Vector3 RandomScale()
    {
        return new Vector3(Random.Range(0.2f, 0.8f) * this.ScaleX, 
                           Random.Range(0.2f, 0.8f) * this.ScaleY, 
                           Random.Range(0.2f, 0.8f) * this.ScaleZ);
    }
}