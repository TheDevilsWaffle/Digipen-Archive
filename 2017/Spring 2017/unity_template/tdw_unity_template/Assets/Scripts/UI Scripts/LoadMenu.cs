///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LoadMenu.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
public enum MenuToLoad
{
    UNASSIGNED,
    START,
    MAIN,
    CONTROLS,
    CREDITS,
    CODA_MAINMENU_QUITGAME,
    PAUSE,
    CODA_PAUSE_MAINMENU,
    CODA_PAUSE_QUITGAME
};
#endregion

#region EVENTS
public class EVENT_LOAD_NEW_MENU : GameEvent
{
    public Canvas canvasToLoad;
    public MenuToLoad menuToLoad;
    public EVENT_LOAD_NEW_MENU(Canvas _canvasToLoad, MenuToLoad _menuToLoad)
    {
        canvasToLoad = _canvasToLoad;
        menuToLoad = _menuToLoad;
    }
}
#endregion

public class LoadMenu : ButtonActionBase
{
    #region FIELDS
    [SerializeField]
    MenuToLoad menuToLoad;
    [SerializeField]
    Canvas canvasToLoad;
    [SerializeField]
    public bool backToPreviousMenu = false;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
	}
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public override void Activate()
    {
        base.Activate();
        if (backToPreviousMenu)
        {
            Events.instance.Raise(new EVENT_LOAD_PREVIOUS_MENU(canvasToLoad, menuToLoad));
        }
        else
        {
            Events.instance.Raise(new EVENT_LOAD_NEW_MENU(canvasToLoad, menuToLoad));
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
		//remove listeners
	}
    #endregion
}
