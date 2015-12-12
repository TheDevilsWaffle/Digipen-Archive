/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - pest_audio.cs
//AUTHOR - Fernando Lima
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class pest_audio : MonoBehaviour {

    // define the audio clips
    public AudioClip[] clipsList;
    public float minTime;
    public float maxTime;
    private float timeUntilNextSound;

    private AudioSource moveClip;
    private AudioSource deadClip;
 
private void CycleThroughAudios()
    {
        AudioClip clip = clipsList[Random.Range(5, 9)];
        moveClip.clip = clip;
        AudioSource.PlayClipAtPoint(clip, transform.position, .2f);
        float rrange = Random.Range(5.0f, 8.0f);
        timeUntilNextSound = rrange; 
    }

private void OnCollisionEnter(Collision collisionInfo)
{
    if(collisionInfo.gameObject.tag == "Bullet")
    {
        AudioClip clip = clipsList[Random.Range(0, 4)];
        deadClip.clip = clip;
        AudioSource.PlayClipAtPoint(clip, transform.position, .2f);

    }
}

 public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) 
 { 
     AudioSource newAudio = gameObject.AddComponent<AudioSource>();
     newAudio.clip = clip; 
     newAudio.loop = loop;
     newAudio.playOnAwake = playAwake;
     newAudio.volume = vol; 

     return newAudio; 
 }
 
 public void Awake(){
     timeUntilNextSound = 1;
     } 


	// Use this for initialization
	void Start () {
        clipsList = new AudioClip[]{ (AudioClip)Resources.Load("Sounds/sfx/slug/slug_dies1", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_dies2", typeof(AudioClip)), 
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_dies3", typeof(AudioClip)), 
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_dies4", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_dies5", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_moves1", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_moves2", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_moves3", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_moves4", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/slug/slug_moves5", typeof(AudioClip))};

        
        moveClip = gameObject.AddComponent<AudioSource>();
        deadClip = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        

        timeUntilNextSound -= Time.deltaTime;

        if(timeUntilNextSound < 0.0f)
        {
            CycleThroughAudios();
        }
	}
}
