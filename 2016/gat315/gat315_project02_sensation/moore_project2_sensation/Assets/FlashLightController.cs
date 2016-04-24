/*////////////////////////////////////////////////////////////////////////
//SCRIPT: FlashLightController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using LOS;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public enum FlashlightStatus { ON, OFF };

public class FlashLightController : MonoBehaviour
{
    #region PROPERTIES

    [Header("REFERENCES")]
    [SerializeField]
    public LOSLightBase Flashlight;
    [SerializeField]
    public HUDController HUD;
    [SerializeField]
    private SFXList SFXList;
    [SerializeField]
    SoundManager SFXSystem;
    MyPlatformerCharacter Player;
    [SerializeField]
    private NoiseAndGrain CameraNG;
    MyInput CurrentInput;
    [SerializeField]
    private Image Darkness;

    //light system for player lighting
    LightController PlayerLight;
    
    
    FlashlightStatus FlashlightActive = FlashlightStatus.OFF;
    [Header("FLASHLIGHT ATTRIBUTES")]
    [Range(0f, 359f)]
    public float OnAngle = 45f;
    private float OffAngle = 0.5f;
    [SerializeField]
    private float MaxBatteryLife = 100f;
    [SerializeField]
    public float RateOfBatteryDrain = 5f;
    [SerializeField]
    public float RateOfBatteryDrainNormal = 1f;
    public float CurrentBatteryLife;

    private int FlashlightFacingOn = -22;
    private int FlashlightFacingOff = 270;

    private bool KillPlayer;
    public bool ObtainedBattery;

    #endregion PROPERTIES

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get references
        this.Flashlight = this.gameObject.transform.Find("Flashlight").GetComponent<LOSFullScreenLight>();
        this.Player = this.GetComponentInParent<MyPlatformerCharacter>();
        this.CurrentInput = this.gameObject.GetComponent<MyPlatformerController>().Current;
        this.HUD = GameObject.FindWithTag("HUD").gameObject.GetComponent<HUDController>();
        this.PlayerLight = GameObject.FindWithTag("LightSystem").gameObject.GetComponent<LightController>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //initialize content
        this.CurrentBatteryLife = this.MaxBatteryLife;
        this.HUD.UpdateFlashlight((int)this.CurrentBatteryLife);

        //start the flashlight being off
        this.Flashlight.coneAngle = this.OffAngle;
        this.Flashlight.faceAngle = this.FlashlightFacingOff;
        this.FlashlightActive = FlashlightStatus.OFF;

        this.Flashlight.enabled = false;
    }

    #endregion INITIALIZATION

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //update flashlight status and current input
        this.UpdateFlashlightInputAndStatus();
        
        //flashlight is on
        if (this.FlashlightActive == FlashlightStatus.ON && this.CurrentBatteryLife > 0f)
        {
            //ensure that it's been turned on initially
            if (Flashlight.coneAngle != this.OnAngle)
            {
                //if not, turn on the flashlight
                this.TurnFlashlightOn();
            }
            //rotate the flashlight at will
            else
            {
                this.Flashlight.faceAngle = this.UpdateFlashlightRotation(this.CurrentInput.FlashlightDegrees, 
                                                                           this.Player.FacingRight);
            }

            //start battery drain
            this.DrainBattery(this.RateOfBatteryDrain);
        }

        //flashlight is not on
        else
        {
            //ensure that the flashlight is off
            if (this.Flashlight.coneAngle != this.OffAngle)
            {
                //if it is not off, turn it off
                this.TurnFlashlightOff();
            }
        }

        //regardless, always drain battery
        this.DrainBatteryNormally(this.RateOfBatteryDrainNormal);

        if(this.CurrentBatteryLife <= 0f && !this.KillPlayer)
        {
            this.KillPlayer = true;
            //player is dead
            this.Player.GetComponent<Health>().DecreaseHealth(99);
        }
    }

    #endregion UPDATE

    #region FLASHLIGHT UPDATE/ON/OFF

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlightInputAndStatus()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateFlashlightInputAndStatus()
    {
        this.CurrentInput = this.gameObject.GetComponent<MyPlatformerController>().Current;
        this.FlashlightActive = this.CurrentInput.FlashlightActive;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TurnFlashlightOff()
    ////////////////////////////////////////////////////////////////////*/
    void TurnFlashlightOff()
    {
        this.Flashlight.coneAngle = this.OffAngle;
        this.Flashlight.faceAngle = this.FlashlightFacingOff;
        this.FlashlightActive = FlashlightStatus.OFF;
        //this.Flashlight.obstacleLayer = LayerMask.NameToLayer("Everything");

        //update HUD
        this.HUD.SwapSprite("flashlight");

        //play sfx
        this.SFXSystem.PlaySingle(this.SFXList.FlashlightOff);

        //turn off Noise and Grain off camera
        this.CameraNG.enabled = true;

        this.PlayerLight.StopUpdatingLight = false;
        this.Player.GetComponent<SpriteRenderer>().sortingOrder = 1;

        this.Darkness.enabled = true;

        this.Flashlight.enabled = false;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TurnFlashlightOn()
    ////////////////////////////////////////////////////////////////////*/
    void TurnFlashlightOn()
    {
        this.Flashlight.coneAngle = this.OnAngle;
        this.Flashlight.faceAngle = this.UpdateFlashlightRotation(this.CurrentInput.FlashlightDegrees, this.Player.FacingRight);
        this.FlashlightActive = FlashlightStatus.ON;
        //this.Flashlight.obstacleLayer = LayerMask.GetMask("Environment");

        //update HUD
        this.HUD.SwapSprite("flashlight");

        //sfx
        this.SFXSystem.PlaySingle(this.SFXList.FlashlightOn);

        this.PlayerLight.StopUpdatingLight = true;
        this.PlayerLight.ResetLightRotation();
        this.Player.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //turn off Noise and Grain on camera
        this.CameraNG.enabled = false;

        this.Darkness.enabled = false;

        this.Flashlight.enabled = true;
    }

    #endregion

    #region FLASHLIGHT ROTATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlightRotation()
    ////////////////////////////////////////////////////////////////////*/
    int UpdateFlashlightRotation(float degrees_, bool facingRight_)
    {
        //correct for LOS default positioning
        degrees_ -= 90;
        degrees_ *= -1;

        //player is facing right
        if (facingRight_)
        {
            if (degrees_ == 90f)
            {
                return 0;
            }
            else if (degrees_ < -90)
                return -90;
            else if (degrees_ > 90)
                return 90;
            else
                return (int)degrees_;
        }
        //player is facing left
        else
        {
            if (degrees_ == 90f)
                return 180;
            else if (degrees_ < 90f)
                return 90;
            else if (degrees_ < -90f)
                return -90;
            else
                return (int)degrees_;
        }
    }

    #endregion

    #region BATTERY

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DrainBattery()
    ////////////////////////////////////////////////////////////////////*/
    void DrainBattery(float drainRate_)
    {
        if(!this.ObtainedBattery)
        {
            //drain the battery
            this.CurrentBatteryLife -= Time.deltaTime / drainRate_;
        }
        

        //update the HUD
        this.HUD.UpdateFlashlight((int)this.CurrentBatteryLife);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: DrainBatteryNormally()
    ////////////////////////////////////////////////////////////////////*/
    void DrainBatteryNormally(float drainRate_)
    {
        if (!this.ObtainedBattery)
        {
            //drain the battery
            this.CurrentBatteryLife -= Time.deltaTime / drainRate_;
        }
        //update the HUD
        this.HUD.UpdateFlashlight((int)this.CurrentBatteryLife);
    }

    #endregion X_FUNCTIONS

}