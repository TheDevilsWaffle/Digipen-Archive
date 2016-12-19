/*////////////////////////////////////////////////////////////////////////
//SCRIPT: LightController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    #region PROPERTIES

    [SerializeField]
    GameObject PlayerLight;
    [SerializeField]
    GameObject EnvironmentLight;
    [SerializeField]
    GameObject Player;
    Vector3 CurrentPlayerPos;
    Vector3 PreviousPlayerPos;

    [HideInInspector]
    public bool StopUpdatingLight = false;

    float PlayerLightRotationMultiplier = -0.5f;
    float EnvironmentLightRotationMultiplier = -3f;

    #endregion PROPERTIES

    #region UPDATE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        if(this.Player != null && !this.StopUpdatingLight)
        {
            this.CurrentPlayerPos = this.Player.transform.position;

            if (this.CurrentPlayerPos != this.PreviousPlayerPos)
            {
                this.UpdateLightRotation(this.CurrentPlayerPos.y);
            }
        }
    }

    #endregion

    #region LIGHT ROTATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateLightRotation()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateLightRotation(float playerY_)
    {
        //update player light
        Quaternion newPlayerLightRotation = Quaternion.Euler(playerY_ * this.PlayerLightRotationMultiplier, 0f, 0f);
        this.PlayerLight.transform.rotation = newPlayerLightRotation;

        //update environment light
        Quaternion newEnvironmentLightRotation = Quaternion.Euler(playerY_ * this.EnvironmentLightRotationMultiplier, 0f, 0f);
        this.EnvironmentLight.transform.rotation = newEnvironmentLightRotation;
    }

    public void ResetLightRotation()
    {
        //print("RESETTING LIGHT!");
        this.PlayerLight.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    #endregion

}