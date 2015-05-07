/***************************************************************************************/
/*
	filename   	MainMenu.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014 
	
	brief:
	This class contains MainMenu's gameplay. Mainly handles the user's choice (starting
	or exiting the game).
*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.ObjectManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import GamePlay.Level1.Level1;
	import flash.ui.Keyboard;
	import flash.display.MovieClip;

	public class MainMenu extends State
	{
		public var goSelect:GameObject;
		public var iIndex:int;
		/*******************************************************************************/
		/*
			Description:
				Constructor used to set attributes based on passed parameters.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function MainMenu():void
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
			//create start game, give it a starting x, y, name, and define it as a static object
			ObjectManager.AddObject(new StartGameMC(380,200), "the_game_start", ObjectManager.OM_STATICOBJECT);
			
			//create exit game, give it a starting x, y, name, and define it as a static object
			ObjectManager.AddObject(new ExitGameMC(380,300), "the_game_exit", ObjectManager.OM_STATICOBJECT);
			
			//create select button, give it a starting x, y, name, and define it as a static object
			ObjectManager.AddObject(new SelectionButtonMC(200,200), "the_game_select", ObjectManager.OM_STATICOBJECT);
		}
		/*******************************************************************************/
		/*
			Description:
				Initialize() sets a variable standin for "the_game_select" and also sets
				iIndex initially to 0
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			goSelect = ObjectManager.GetObjectByName("the_game_select", ObjectManager.OM_STATICOBJECT);
			iIndex = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update() applies actions based on what keys are pressed. In this case
				it primarily tracks the goSelect and updates its position, as well as
				updating iIndex so that the proper menu choice is activated.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void 
		{
			if(InputManager.IsTriggered(Keyboard.DOWN) && iIndex == 0)
			{
				goSelect.displayobject.y = 200;
				iIndex = 1;
			}
			
			if(InputManager.IsTriggered(Keyboard.UP) && iIndex == 0)
			{
				goSelect.displayobject.y = 200;
				iIndex = 0;
			}
			if(InputManager.IsTriggered(Keyboard.DOWN) && iIndex == 1)
			{
				goSelect.displayobject.y = 300;
				iIndex = 1;
			}
			
			if(InputManager.IsTriggered(Keyboard.UP) && iIndex == 1)
			{
				goSelect.displayobject.y = 200;
				iIndex = 0;
			}
			
			if(InputManager.IsTriggered(Keyboard.SPACE))
			{
				if(iIndex == 0)
				{
					GameStateManager.GotoState(new Level1());
				}
				else if(iIndex == 1)
				{
					GameStateManager.Quit();
				}
			}
			
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
		}
	}
}
