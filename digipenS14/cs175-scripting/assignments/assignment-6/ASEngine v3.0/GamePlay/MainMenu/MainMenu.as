/***************************************************************************************/
/*
	filename   	MainMenu.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

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
		private var iIndex:int;

		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{			
			ObjectManager.AddObject(new StartGameMC(275, 150), "StartGame", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new ExitGameMC(275, 250), "ExitGame", ObjectManager.OM_STATICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			iIndex = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			var goStartGame:GameObject;
			var goExitGame:GameObject;
			
			if(InputManager.IsTriggered(Keyboard.DOWN) && iIndex == 0)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				MovieClip(goStartGame.displayobject).gotoAndStop(1);
				++iIndex;
				MovieClip(goExitGame.displayobject).gotoAndStop(2);
			}
			
			if(InputManager.IsTriggered(Keyboard.UP) && iIndex == 1)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				MovieClip(goExitGame.displayobject).gotoAndStop(1);
				--iIndex;
				MovieClip(goStartGame.displayobject).gotoAndStop(2);
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
