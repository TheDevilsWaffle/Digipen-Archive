/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - ObjectivesController.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Collections;

public class ObjectivesController : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    int PlayerNumber = 0;
    GameObject ObjectivesImage;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    [SerializeField]
    Sprite[] ObjectivesImagesArray;
    int Index = 0;
    float Timer;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //connecting the gamepad
        this.playerIndex = (PlayerIndex)this.PlayerNumber;
        GamePad.GetState(this.playerIndex);
        prevState = state;
        state = GamePad.GetState(playerIndex);
        this.ObjectivesImage = GameObject.Find("ObjectivesImage").gameObject;
        this.Timer = 1f;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
        if (this.Timer >= 1f)
        {
            if (state.Buttons.A == ButtonState.Pressed)
            {
                print("here");
                if (this.Index < 3)
                {
                    this.Index += 1;
                    this.ObjectivesImage.GetComponent<Image>().overrideSprite = this.ObjectivesImagesArray[this.Index];
                    print(this.ObjectivesImage.GetComponent<Image>().sprite);
                    this.Timer = 0f;
                }
                else
                {
                    Application.LoadLevel("TestZone");
                }
            }
        }
        this.Timer += Time.deltaTime;
        //print(this.Timer);
    }
}