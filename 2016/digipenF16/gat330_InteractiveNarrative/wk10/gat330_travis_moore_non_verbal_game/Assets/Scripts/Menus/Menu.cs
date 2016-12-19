///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Menu.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
public enum MenuName
{
    START,
    HOWTOPLAY,
    MAINMENU,
    OPTIONS,
    CREDITS,
    QUITGAME,
    CODA_QUITGAME,
    CODA_MAINMENU
}
#endregion

public class Menu : MonoBehaviour
{
    #region FIELDS
    [Header("MENU")]
    public MenuName menuName;
    [Header("FIRST BUTTON")]
    public ButtonBase firstButtonToHover;
    Canvas refCanvas;
    
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        refCanvas = GetComponent<Canvas>();
        if (firstButtonToHover == null)
            Debug.LogError("firstButtonToHover is not assigned, please assign a button to start as highlighted!");
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //start firstButtonToSelect to ButtonState.HOVER
        firstButtonToHover.Hover();
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ActivateMenu()
    {
        refCanvas.enabled = true;
    }

    public void DeactivateMenu()
    {
        refCanvas.enabled = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
