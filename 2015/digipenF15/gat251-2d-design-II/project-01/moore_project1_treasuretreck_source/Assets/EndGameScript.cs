/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - EndGame.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript:MonoBehaviour
{
    [Header("GameStartEndFadeSettings")]
    [SerializeField]
    GameObject ScreenFader;
    [SerializeField]
    float FadeOutTime = 2.0f;
    [SerializeField]
    float FadeInTime = 3.0f;
    [HideInInspector]
    float GameTimer;
    [SerializeField]
    float GameStartDelayThreshold = 1.0f;


    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //check to see if GameStartDelayThreshold is met
        if(this.GameTimer > this.GameStartDelayThreshold)
        {
            this.FadeOutScreenFader();
        }

        //check to see if player touched owner
    

        //update timer
        this.GameTimer += Time.deltaTime;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutScreenFader()
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutScreenFader()
    {
        this.ScreenFader.GetComponent<Image>().CrossFadeAlpha(0.0f, 
                                                              this.FadeOutTime, 
                                                              true);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: OnCollisionEnter(collision)
    /////////////////////////////////////////////////////////////////////////*/
    void OnCollisionEnter(Collision obj_)
    {
        Debug.Log(obj_.collider.name);
        if(obj_.collider.name == "Player")
        {
            Debug.Log("PLAYER HIT ME");
        }
    }
}
