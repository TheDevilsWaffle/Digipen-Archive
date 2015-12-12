/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - MenuLogic.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MenuLogic:MonoBehaviour
{
    [Header("LevelToLoad")]
    [SerializeField]
    string LevelToLoad;

    [Header("MenuSwitching")]
    [SerializeField]
    GameObject MenuToAnimate;
    [SerializeField]
    GameObject CurrentMenu;
    [SerializeField]
    GameObject NextMenu;
    [HideInInspector]
    Stack<GameObject> MenuStack;

    [Header("Fade Out Object")]
    [SerializeField]
    GameObject ScreenMask;
    [SerializeField]
    float FadeOutTime = 1f;
    [SerializeField]
    float FadeInTime = 1f;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //create the MenuStack from scratch
        this.MenuStack = new Stack<GameObject>();

        //fade out the alpha value of the FadeOutObject
        this.FadeOutScreenMask(this.ScreenMask, this.FadeOutTime);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    /////////////////////////////////////////////////////////////////////////*/
    void Update()
    {
	
	}

    public void LoadMenu(string menu_)
    {
        Debug.Log("Loading " + menu_);

        //animate the old menu out
        this.AnimateMenuOut(this.CurrentMenu);
        //add this menu to a stack for "back" button use


        //animate the new menu selected in
        //this.AnimateMenuIn(this.NextMenu);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuOut(gameobject)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateMenuOut(GameObject menu_)
    {
        iTween.ScaleTo(menu_, iTween.Hash("scale",new Vector3(0,0,0), "time", 0.5f, "oncomplete", "DestroyMenu", "oncompleteparams", this.CurrentMenu ));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuIn(gameobject)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateMenuIn(GameObject menu_)
    {

    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyMenu(gameobject)
    /////////////////////////////////////////////////////////////////////////*/
    void DestroyMenu(GameObject menu_)
    {
        Debug.Log("DESTROYING " + menu_);
        Destroy(menu_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutScreenMask(GameObject, Float)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutScreenMask(GameObject obj_, float time_)
    {
        //Debug.Log("FADING OUT SCREEN MASK NOW");
        iTween.FadeTo(obj_, 0f, time_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInScreenMask(GameObject, Float, String)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeInScreenMask(GameObject obj_, float time_, string level_)
    {
        Debug.Log("FADE IN SCREEN MASK REACHED");
        iTween.ColorTo(obj_, iTween.Hash(
                                        "Color", new Color(22 / 255, 22 / 255, 22 / 255, 255 / 255),
                                        "time", 1f,
                                        "easetype", "easeInQuad",
                                        "oncompletetarget", this.gameObject,
                                        "oncomplete", "LoadNextLevel",
                                        "oncompleteparams", level_));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadNextLevel()
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadNextLevel()
    {
        Application.LoadLevel(this.LevelToLoad);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: LoadLevelByName(string)
    /////////////////////////////////////////////////////////////////////////*/
    public void LoadLevelByName(string level_)
    {
        Application.LoadLevel(level_);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: QuitGame()
    /////////////////////////////////////////////////////////////////////////*/
    public void QuitGame()
    {
        Application.Quit();
    }
}
