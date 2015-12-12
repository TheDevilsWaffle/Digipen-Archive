/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EventManager.cs
//AUTHOR - Enrique Rodriguez and Fernando Lima
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/
using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

  public float TotalTimeTillNextEvent_ = 15f;
  [SerializeField] float TimeBeforeFirstEvent_ = 15f;
  [SerializeField] float EventFrequencyScaling_ = 0.9f;
  [SerializeField] float MinimumEventTime_ = 5f;
  [SerializeField] int StartingDifficulty_ = 2;
  [SerializeField] int DifficultyScaling_ = 1;
  [SerializeField] int DifficultyFrequencyThreshold_ = 3;

  float Timer_ = 0f;
  float CountDown_;
  int CurrentDifficulty_;
  GameObject MostRecentEventObject_;

  public GameObject EventPreferred;
  [SerializeField] GameObject EventMeteorShower;
  [SerializeField] GameObject EventRaiders;
  //[SerializeField] GameObject EventPassingComet;
  //[SerializeField] GameObject EventSpacefleetWarfront;
  //[SerializeField] GameObject EventTentacleMonster;
  //black hole
  //supernova
  //creation/bigbang
  //cosmic storm
  //orbit nearing sun
  //wormhole
    //hacking in layered audio
  private static int layer;
  public AudioClip []layers;
  private AudioSource layer1;
  private AudioSource layer2;
  private AudioSource layer3;
  private AudioSource layer4;

  private void InitLayeredAudio()
  {
      layer = 1;
      layers = new AudioClip[]{(AudioClip)Resources.Load("Sounds/music/MarioGalaxy Layer1", typeof(AudioClip)),
              (AudioClip)Resources.Load("Sounds/music/MarioGalaxy Layer2", typeof(AudioClip)),
              (AudioClip)Resources.Load("Sounds/music/MarioGalaxy Layer3", typeof(AudioClip)),
              (AudioClip)Resources.Load("Sounds/music/MarioGalaxy Layer4", typeof(AudioClip))};

      layer1 = gameObject.AddComponent<AudioSource>();
      layer2 = gameObject.AddComponent<AudioSource>();
      layer3 = gameObject.AddComponent<AudioSource>();
      layer4 = gameObject.AddComponent<AudioSource>();

      layer1.mute = false;
      layer2.mute = true;
      layer3.mute = true;
      layer4.mute = true;

      layer1.loop = true;
      layer2.loop = true;
      layer3.loop = true;
      layer4.loop = true;

      layer1.clip = layers[0];
      layer2.clip = layers[1];
      layer3.clip = layers[2];
      layer4.clip = layers[3];

   

      layer1.Play();
      layer2.Play();
      layer3.Play();
      layer4.Play();

      layer1.volume = .03f;
      layer2.volume = .03f;
      layer3.volume = .03f;
      layer4.volume = .03f;

  }

  private void AddSoundLayer(int layer)
  {
      if(layer > 13)
          return;
      if(layer >= 5)
      {
          layer2.mute = false;
      }
      if(layer >= 9)
      {
          layer3.mute = false; 
      }
      if(layer == 13)
      {
          layer4.mute = false; 
      }
  }


	void Start () {
    CountDown_ = TimeBeforeFirstEvent_;
    CurrentDifficulty_ = StartingDifficulty_;
    MostRecentEventObject_ = gameObject; //self
    InitLayeredAudio();
	}
	
	void Update () {
    Timer_ += Time.deltaTime;
    CountDown_ -= Time.deltaTime;

    if (CountDown_ <= 0f) {
      RandomEvent();
      CountDown_ = TotalTimeTillNextEvent_;

      //increase scaling
      CurrentDifficulty_ += DifficultyScaling_;
      if (CurrentDifficulty_ % DifficultyFrequencyThreshold_ == 0) {
        TotalTimeTillNextEvent_ = MinimumEventTime_ + (TotalTimeTillNextEvent_ - MinimumEventTime_) * EventFrequencyScaling_;
      }
      AddSoundLayer(++layer);
  
    }

	}

  void RandomEvent()
  {
    int result = Random.Range(2, CurrentDifficulty_);
    //list of primes: 2 3 5 7 11 13 17 19 23 29
    if (result % 13 == 0) {
      //tentacle monster
    }
    else if (result % 11 == 0) {
      //spacefleet warfront
    }
    else if (result % 7 == 0) {
      //passing comet
    }
    else if (result % 5 == 0) {
      //print("RAIDERS");
      MostRecentEventObject_ = Instantiate(EventRaiders);
      MostRecentEventObject_.GetComponent<EventRaiders>().Difficulty = CurrentDifficulty_;
    }
    else if (result % 3 == 0) {
      //print("METEORS");
      MostRecentEventObject_ = Instantiate(EventMeteorShower);
      MostRecentEventObject_.GetComponent<EventMeteorShower>().Difficulty = CurrentDifficulty_;
    }
    else if (result % 2 == 0) {
      //reserved for zone inclined event
      MostRecentEventObject_ = Instantiate(EventPreferred);

      //hard send the difficulty, sadly
      if (MostRecentEventObject_.GetComponent<EventMeteorShower>() != null) 
        MostRecentEventObject_.GetComponent<EventMeteorShower>().Difficulty = CurrentDifficulty_;
      else if (MostRecentEventObject_.GetComponent<EventRaiders>() != null)
        MostRecentEventObject_.GetComponent<EventRaiders>().Difficulty = CurrentDifficulty_;
      //else if (MostRecentEventObject_.GetComponent<EventRaiders>() != null)
      //  MostRecentEventObject_.GetComponent<EventRaiders>().Difficulty = CurrentDifficulty_;
      //etc.
    }
    else {
      //no event?
    }
  }
}
