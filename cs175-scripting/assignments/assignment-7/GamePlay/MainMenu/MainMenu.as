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
	import GamePlay.Help.HelpMenu;
	
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
			ObjectManager.AddObject(new MainBg(320, 240), "MainBG", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new Title(320, 60), "Title", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new StartGameMC(320, 150), "StartGame", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new ExitGameMC(320, 250), "ExitGame", ObjectManager.OM_STATICOBJECT);
			ObjectManager.AddObject(new HelpButton(320, 350), "Help", ObjectManager.OM_STATICOBJECT);
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
			var goHelp:GameObject;
			var menuBeep = new beep;
			
			if(InputManager.IsTriggered(Keyboard.DOWN) && iIndex == 0)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				goHelp = ObjectManager.GetObjectByName("Help", ObjectManager.OM_STATICOBJECT);
				MovieClip(goStartGame.displayobject).gotoAndStop(1);
				MovieClip(goExitGame.displayobject).gotoAndStop(2);
				MovieClip(goHelp.displayobject).gotoAndStop(1);
				iIndex = 1;
				//play menu sound
				menuBeep = new beep;
				menuBeep.play();
			}
			else if(InputManager.IsTriggered(Keyboard.UP) && iIndex == 1)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				goHelp = ObjectManager.GetObjectByName("Help", ObjectManager.OM_STATICOBJECT);
				MovieClip(goStartGame.displayobject).gotoAndStop(2);
				MovieClip(goExitGame.displayobject).gotoAndStop(1);
				MovieClip(goHelp.displayobject).gotoAndStop(1);
				iIndex = 0;
				//play menu sound
				menuBeep = new beep;
				menuBeep.play();
			}
			else if(InputManager.IsTriggered(Keyboard.DOWN) && iIndex == 1)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				goHelp = ObjectManager.GetObjectByName("Help", ObjectManager.OM_STATICOBJECT);
				MovieClip(goStartGame.displayobject).gotoAndStop(1);
				MovieClip(goExitGame.displayobject).gotoAndStop(1);
				MovieClip(goHelp.displayobject).gotoAndStop(2);
				//play menu sound
				menuBeep = new beep;
				menuBeep.play();
				iIndex = 2;
			}
			else if(InputManager.IsTriggered(Keyboard.UP) && iIndex == 2)
			{
				goStartGame = ObjectManager.GetObjectByName("StartGame", ObjectManager.OM_STATICOBJECT);
				goExitGame = ObjectManager.GetObjectByName("ExitGame", ObjectManager.OM_STATICOBJECT);
				goHelp = ObjectManager.GetObjectByName("Help", ObjectManager.OM_STATICOBJECT);
				MovieClip(goStartGame.displayobject).gotoAndStop(1);
				MovieClip(goExitGame.displayobject).gotoAndStop(2);
				MovieClip(goHelp.displayobject).gotoAndStop(1);
				//play menu sound
				menuBeep = new beep;
				menuBeep.play();
				iIndex = 1;
			}
			
			if(InputManager.IsTriggered(Keyboard.SPACE))
			{
				//set menu select sound
				var menuSelect = new select;
				if(iIndex == 0)
				{
					//play menu select sound
					menuSelect = new select;
					menuSelect.play();
					GameStateManager.GotoState(new Level1());
				}
				else if(iIndex == 1)
				{
					//play menu select sound
					menuSelect = new select;
					menuSelect.play();
					GameStateManager.Quit();
				}
				else if(iIndex == 2)
				{
					//play menu select sound
					menuSelect = new select;
					menuSelect.play();
					GameStateManager.GotoState(new HelpMenu());
				}
			}
			
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
		}
	}
}
