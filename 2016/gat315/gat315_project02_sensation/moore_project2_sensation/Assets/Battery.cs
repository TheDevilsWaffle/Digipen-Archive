/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Battery.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    #region PROPERTIES

    public SoundManager SFXController;
    public AudioClip GotBatterySFX;

    //references
    GameObject Player;
    FlashLightController PlayerFlashlight;
    [SerializeField]
    LightTrigger CaveEntrance;
    BossController Boss;

    //attributes



    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //CONTENT HERE
        this.Player = GameObject.FindWithTag("Player").gameObject;
        this.PlayerFlashlight = this.Player.GetComponent<FlashLightController>();
        this.Boss = GameObject.Find("Boss").GetComponent<BossController>();
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_01()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider2D_)
    {
        if (collider2D_.gameObject == this.Player)
        {
            this.SFXController.PlaySingle(this.GotBatterySFX);

            this.CaveEntrance.HasBattery = true;
            this.PlayerFlashlight.CurrentBatteryLife = 100;
            this.PlayerFlashlight.ObtainedBattery = true;

            this.PlayerFlashlight.RateOfBatteryDrain = 0f;
            this.PlayerFlashlight.RateOfBatteryDrainNormal = 0f;


            //start boss
            this.Boss.AwakenBoss();
        }

        Destroy(this.gameObject);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_02()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_02()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_03()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_03()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_04()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_04()
    {
        //CONTENT HERE
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}