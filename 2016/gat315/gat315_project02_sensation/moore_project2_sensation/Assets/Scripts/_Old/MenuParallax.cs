/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MenuParallax.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class MenuParallax : MonoBehaviour
{
    //PROPERTIES
    Vector2 MousePos;
    Vector2 PreviousMousePos;
    float MouseDist;
    GameObject MenuCanvas;
    GameObject MenuBackground;

    float XRotation;
    float YRotation;
    float MaxAngle = 2f;

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get menu canvas
        this.MenuCanvas = GameObject.Find("MenuCanvas");
        this.MenuBackground = GameObject.Find("MenuBackground");
        this.PreviousMousePos = new Vector2(0f, 0f);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //calculate mouse positions from center of the screen
        this.MousePos = this.CalculateMouseDistanceFromCenter();

        //DEBUG - mouse position as percentage away from center
        Debug.Log(this.MousePos);

        if (this.MousePos != this.PreviousMousePos)
        {
            //adjust the background image rotation based on mouse position
            this.RotateMenuBackground(this.MousePos.x, this.MousePos.y);
        }

        //update PreviousMousePos
        this.PreviousMousePos = this.MousePos;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CalculateMouseDistanceFromCenter()
    ////////////////////////////////////////////////////////////////////*/
    Vector2 CalculateMouseDistanceFromCenter()
    {
        //get the initial mouse position
        Vector2 mousePos = Input.mousePosition;

        /*determine which side of the screen the mouse is on then
          divide width/height by 2 to get percentage away from center */
        mousePos.x = ((mousePos.x - (Screen.width / 2)) /
                     (Screen.width / 2));
        mousePos.y = ((mousePos.y - (Screen.height / 2)) /
                      (Screen.height / 2));

        return mousePos;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: RotateMenuBackground(float, float)
    ////////////////////////////////////////////////////////////////////*/
    void RotateMenuBackground(float x_, float y_)
    {
        Quaternion slightRotation = Quaternion.Euler(y_ * this.MaxAngle, -x_ * this.MaxAngle, 0);
        this.MenuBackground.transform.rotation = slightRotation;
    }
}
