/***************************************************************************************/
/*
	filename   	MainMenu.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/12/2013 
	
	brief:
	This class contains MainMenu's gameplay. Mainly handles the user's choice (starting
	or exiting the game).
*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.*;
	import flash.ui.Keyboard;
	
	public class MainMenu extends State
	{
		/*******************************************************************************/
		/*
			Description:
				Constructor that is only responsible for instantiating the level.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function MainMenu() 
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for adding the menu object to the 
				ObjectManager. It will be called first after the level is instantiated
				and by that will populate the main menu.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{
			ObjectManager.AddObject(new GameObject(new StartGame(),380,200,0), "StartGame", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new GameObject(new ExitGame(),380,350,0), "ExitGame", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new SelectionButtonSP(200,200), "SelectionButton", ObjectManager.OM_STATICOBJECT);
		}
	}
}
