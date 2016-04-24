/*////////////////////////////////////////////////////////////////////////
//SCRIPT: ButtonSFX.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField]
    AudioClip ButtonHover;
    [SerializeField]
    AudioClip ButtonDown;

    SoundManager AudioController;
    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        this.AudioController = GameObject.FindWithTag("AudioSystem").gameObject.GetComponent<SoundManager>();
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Update()
    ////////////////////////////////////////////////////////////////////*/
    void Update()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_01()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_01()
    {
        //CONTENT HERE
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("this happened");
        this.AudioController.PlaySingle(this.ButtonHover);
        throw new NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.AudioController.PlaySingle(this.ButtonDown);
        throw new NotImplementedException();
    }
}