/*////////////////////////////////////////////////////////////////////////
//SCRIPT: LightTrigger.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using LOS;
using UnityEngine.UI;

public class LightTrigger : MonoBehaviour
{
    #region PROPERTIES

    //refernces
    public BoxCollider2D BeginDarknessObject;
    private Vector3 BeginDarknessPos;
    public BoxCollider2D AbsoluteDarknessObject;
    private Vector3 AbsoluteDarknessPos;
    public LOSFullScreenLight DarknessOverlay;
    public Image FlashlightOffDarkness;
    private GameObject Player;
    private GameObject Camera;
    private EffectsController CameraEffects;
    
    //attributes
    private bool PlayerHasCrossedThreshold = false;
    private float Distance;
    public bool HasBattery = false;
    private float DarknessOffsetValue = 20f;

    private float NG_IntensityMultiplier;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get player
        this.Player = GameObject.FindWithTag("Player").gameObject;

        //get camera
        this.Camera = GameObject.FindWithTag("MainCamera").gameObject;
        this.CameraEffects = this.Camera.GetComponent<EffectsController>();

        //set positions
        this.BeginDarknessPos = this.BeginDarknessObject.transform.position;
        this.AbsoluteDarknessPos = this.AbsoluteDarknessObject.transform.position;

        //reset bools
        this.PlayerHasCrossedThreshold = false;
        this.HasBattery = false;

        //get distance
        this.Distance = Vector3.Distance(this.AbsoluteDarknessPos, this.BeginDarknessPos);
    }

    #endregion

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if (this.Player != null)
        {
            if (this.PlayerHasCrossedThreshold)
            {
                //print("Player pos.x = " + this.Player.transform.position.x + ", and Distance = " + this.Distance);
                float newAlphaValue = (this.Player.transform.position.x - this.DarknessOffsetValue);
                newAlphaValue = Mathf.Clamp(newAlphaValue, 70f, 240f);
                //print("value after clamp = " + newAlphaValue);

                Color newColorValue = new Color(0f, 0f, 0f, (newAlphaValue / 255f));
                this.DarknessOverlay.color = new Color(0f, 0f, 0f, 0.5f);
                this.FlashlightOffDarkness.color = newColorValue;

                float percentageComplete = this.Player.transform.position.x / this.AbsoluteDarknessPos.x;
                this.CameraEffects.SetNoiseAndGrain((1 + percentageComplete));
            }
        }
    }

    #endregion

    #region TRIGGERS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_01()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider_)
    {
        //CONTENT HERE
        if (collider_.gameObject == this.Player)
        {
            //print("Player Crossed Threshold");
            this.PlayerHasCrossedThreshold = true;

            if(this.HasBattery)
            {
                //print("Player Crossed Threshold with battery");
                GameObject.FindWithTag("LevelSettings").GetComponent<LevelController>().WinScreen();
            }
        }
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

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_05()
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