/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Mass.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public enum PlayerSize { SMALL, LARGE };

public class Mass : MonoBehaviour
{
    Vector3 SmallSize = new Vector3(0.2f, 0.2f, 0.2f);
    Vector3 LargeSize = new Vector3(0.5f, 0.5f, 0.5f);

    //jumping
    float SmallJumpAcceleration = 0.5f;
    float SmallJumpHeight = 0.5f;
    float LargeJumpAcceleration = 0.25f;
    float LargeJumpHeight = 0.25f;

    [SerializeField]
    float AnimateTime = 0.5f;

    public PlayerSize PlayerCurrentSize;
    float SCCRadius;
    float SCCRadius_Large = 0.25f;
    float SCCRadius_Small = 0.10f;
    Vector3 PlayerArtOffset;
    Vector3 PlayerArt_LargeOffset = new Vector3(0f, 0.5f, 0f);
    Vector3 PlayerArt_SmallOffset = new Vector3(0f, 0.1f, 0f);
    public AudioClip SFX_TrasferMass;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {

        this.SCCRadius = this.gameObject.GetComponent<SuperCharacterController>().radius;
        this.PlayerArtOffset = this.gameObject.transform.FindChild("PlayerArt").transform.localPosition;

        /*
            LARGE: OFFSET = 0.27, RADIUS = 0.26
            SMALL: OFFSET = 0.125, RADIUS = 0.11
        */

        //evaluate currentsize
        this.EvaluateCurrentSize(this.PlayerCurrentSize);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateCurrentSize(PlayerSize)
    ////////////////////////////////////////////////////////////////////*/
    void EvaluateCurrentSize(PlayerSize size_)
    {
        if(size_ == PlayerSize.SMALL)
        {
            this.SCCRadius = this.SCCRadius_Small;
            this.AnimateSize(this.gameObject, this.SmallSize, this.AnimateTime);
            this.PlayerArtOffset = this.PlayerArt_SmallOffset;

            this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.SmallJumpAcceleration;
            this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.SmallJumpHeight;
        }
        else
        {
            this.SCCRadius = this.SCCRadius_Large;
            this.AnimateSize(this.gameObject, this.LargeSize, this.AnimateTime);
            this.PlayerArtOffset = this.PlayerArt_LargeOffset;

            this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.LargeJumpAcceleration;
            this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.LargeJumpHeight;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: IncreasePlayerSize()
    ////////////////////////////////////////////////////////////////////*/
    public void IncreasePlayerSize()
    {
        
        //make the player now LARGE
        this.PlayerCurrentSize = PlayerSize.LARGE;
        //animate to a bigger size
        this.AnimateSize(this.gameObject, this.LargeSize, this.AnimateTime);
        this.SCCRadius = this.SCCRadius_Large;
        this.PlayerArtOffset = this.PlayerArt_LargeOffset;

        this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.LargeJumpAcceleration;
        this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.LargeJumpHeight;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DecreasePlayerSize()
    ////////////////////////////////////////////////////////////////////*/
    public void DecreasePlayerSize()
    {
        //make the player now SMALL
        this.PlayerCurrentSize = PlayerSize.SMALL;
        //animate to a smaller size
        this.AnimateSize(this.gameObject, this.SmallSize, this.AnimateTime);
        this.SCCRadius = this.SCCRadius_Small;
        this.PlayerArtOffset = this.PlayerArt_SmallOffset;

        this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.SmallJumpAcceleration;
        this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.SmallJumpHeight;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateSize(GameObject, Vector3, float)
    ////////////////////////////////////////////////////////////////////*/
    void AnimateSize(GameObject obj_, Vector3 scale_, float time_)
    {
        iTween.ScaleTo(obj_, iTween.Hash("name", "AnimateSize",
                                         "time", time_,
                                         "scale", scale_,
                                         "easetype", "easeOutElastic"));

        this.gameObject.GetComponent<SoundManager>().RandomizeSfx(this.SFX_TrasferMass);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AdjustPlayerJumpAttributes()
    ////////////////////////////////////////////////////////////////////*/
    void AdjustPlayerJumpAttributes()
    {
        if(this.PlayerCurrentSize == PlayerSize.SMALL)
        {
            this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.SmallJumpAcceleration;
            this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.SmallJumpHeight;
        }
        else
        {
            this.gameObject.GetComponent<PlayerMachine>().JumpAcceleration = this.LargeJumpAcceleration;
            this.gameObject.GetComponent<PlayerMachine>().JumpHeight = this.LargeJumpHeight;
        }
    }
}