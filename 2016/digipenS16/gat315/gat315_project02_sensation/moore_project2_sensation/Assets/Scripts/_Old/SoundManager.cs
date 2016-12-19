/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SoundManager.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: PlayMusic()
    ////////////////////////////////////////////////////////////////////*/
    public void PlayMusic()
    {
        this.musicSource.Play();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TogglePauseMusic()
    ////////////////////////////////////////////////////////////////////*/
    public void TogglePauseMusic()
    {
        if (this.musicSource.isPlaying)
            this.musicSource.Pause();
        else
            this.musicSource.UnPause();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: PlaySingle()
    ////////////////////////////////////////////////////////////////////*/
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.PlayOneShot(clip);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: PlayPaitiently()
    ////////////////////////////////////////////////////////////////////*/
    public void PlayPaitiently(AudioClip clip, bool isRandom = false, float lowPitch = 0.9f, float highPitch = 1.1f)
    {
        if (!isRandom)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
            if (!efxSource.isPlaying)
            {
                //Play the clip.
                efxSource.Play();
            }
        }

        else
        {
            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitch, highPitch);
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
            //assign pitch to clip
            efxSource.pitch = randomPitch;

            if (!efxSource.isPlaying)
            {
                //Play the clip.
                efxSource.Play();
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: RandomizeSfx()
    ////////////////////////////////////////////////////////////////////*/
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }
}