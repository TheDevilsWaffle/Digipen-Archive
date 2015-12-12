/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - raider_audio.cs
//AUTHOR - Fernando Lima
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class raider_audio : MonoBehaviour {
    public AudioClip[] clipsList;
    private float timeUntilNextSound;
    private static float noOverlapTime;
    private static bool isTalking;
    private bool imTalking; 

    private AudioSource tauntClip;
    private AudioSource deadClip;
    private void CycleThroughAudios()
    {
        if (!raider_audio.isTalking)
        {
            
            AudioClip clip = clipsList[Random.Range(0, 3)];
            tauntClip.clip = clip;
            AudioSource.PlayClipAtPoint(clip, transform.position, .2f);
            float rrange = Random.Range(8.0f, 12.0f);
            timeUntilNextSound = rrange;
            raider_audio.isTalking = true;
            raider_audio.noOverlapTime = 5.0f;
            imTalking = true; 
        }
        else
            return; 
    }
    private void TalkingTimer()
    {
        if (raider_audio.isTalking)
        {
            raider_audio.noOverlapTime -= Time.deltaTime;
            if (raider_audio.noOverlapTime < 0.0f)
            {
                raider_audio.isTalking = false;
                imTalking = false; 
            }
        }
    }
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Bullet")
        {
            AudioClip clip = clipsList[4];
            deadClip.clip = clip;
            AudioSource.PlayClipAtPoint(clip, transform.position, .2f);
        }

    }
    public void Awake()
    {
        timeUntilNextSound = 1.0f;
        raider_audio.noOverlapTime = 3.0f;
        raider_audio.isTalking = false;
        imTalking = false; 
    }
	// Use this for initialization
	void Start () {
        clipsList = new AudioClip[]{ (AudioClip)Resources.Load("Sounds/sfx/raider/exterminate", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/raider/target_acquired", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/raider/time_of_men", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/raider/you_can_run", typeof(AudioClip)),
                                     (AudioClip)Resources.Load("Sounds/sfx/raider/ouch", typeof(AudioClip))};

        tauntClip = gameObject.AddComponent<AudioSource>();
        deadClip = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        timeUntilNextSound -= Time.deltaTime;
        if(timeUntilNextSound < 0.0f)
        {
            CycleThroughAudios();
        }
        if(imTalking)
            TalkingTimer();
    }
}
