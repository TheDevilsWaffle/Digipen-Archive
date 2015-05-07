/****************************************************************************************
FILENAME   PauseButtonSP.as 
AUTHOR     Travis Moore
EMAIL      travis.moore@digipen.edu
DATE       12/09/2014 
/***************************************************************************************/
package GamePlay.PlatformLevel
{
	//IMPORT
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import GamePlay.PlatformLevel.Level;
	import GamePlay.MainMenu.MainMenu;
	import flash.ui.Keyboard;

	//CLASS PAUSEBUTTONSP
	public class PauseButtonSP extends GameObject
	{
		//VARIABLES
		private var iSelection:int;			//keep track of which button is selected
		public var iBtnDistance:int;		//how far apart are the buttons
		
		/********************************************************************************
		FUNCTION- Constructor()
		/*******************************************************************************/
		public function PauseButtonSP(nPosX_:Number, 
									  nPosY_:Number):void
		{
			super(new PauseButton(), 
				  nPosX_, 
				  nPosY_);
			
			//set iSelection based on internal class
			this.iSelection = BUTTONSELECTED.RESUMEGAME;
			this.iBtnDistance = 90;
		}
		
		/********************************************************************************
		FUNCTION- Update()
		/*******************************************************************************/
		override public function Update():void
		{
			//variables to hook to buttons
			var btnResume = ObjectManager.GetObjectByName("ResumeGame", 
														  ObjectManager.OM_STATICOBJECT);
			var btnRestart = ObjectManager.GetObjectByName("RestartGame", 
														   ObjectManager.OM_STATICOBJECT);
			var btnExit = ObjectManager.GetObjectByName("ExitToMainMenu", 
														ObjectManager.OM_STATICOBJECT);
			
			//moving up through buttons
			if(InputManager.IsReleased(Keyboard.UP))
			{
				displayobject.y -= this.iBtnDistance;
			}
			//moving down through the buttons
			else if(InputManager.IsReleased(Keyboard.DOWN))
			{
				displayobject.y += this.iBtnDistance;
			}
			//if we have overshot the button, set to what it is supposed to be
			if(displayobject.y < btnResume.displayobject.y)
			{
				displayobject.y = btnResume.displayobject.y;
			}
			else if(displayobject.y > btnExit.displayobject.y)
			{
				displayobject.y = btnExit.displayobject.y
			}
			
			//select a button using spacebar
			if(InputManager.IsPressed(Keyboard.SPACE))
			{
				//BtnResume
				if(displayobject.y == btnResume.displayobject.y)
				{
					//first, destroy the pause menu buttons
					ObjectManager.RemoveObjectByName("PauseButton", ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("ResumeGame", ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("RestartGame", ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("ExitToMainMenu", ObjectManager.OM_STATICOBJECT);
					
					//next, stop the background music
					SoundManager.RemoveSoundObjectByName("soPauseMenuBG");
					
					//now, set bIsPaused to false
					GamePlay.PlatformLevel.Level.bIsPaused = false;
					
					//now, restart the regular game music
					SoundManager.ResumeSoundObject(GamePlay.PlatformLevel.Level.bIsPaused)
				}
				//BtnRestart
				if(displayobject.y == btnRestart.displayobject.y)
				{
					//restart the level
					GameStateManager.GotoState(new Level());
					//remove all the sounds
					SoundManager.RemoveAllSoundObjects();
				}
				//BtnExit
				if(displayobject.y == btnExit.displayobject.y)
				{
					//go to menu
					GameStateManager.GotoState(new MainMenu());
					//next, stop the background music
					SoundManager.RemoveSoundObjectByName("soPauseMenuBG");
					//remove all the sounds
					SoundManager.RemoveAllSoundObjects();
				}
			}
		}
	}
}

//CLASS SELECTED
internal class BUTTONSELECTED
{
	public static const RESTARTGAME:Number = 0;
	public static const RESUMEGAME:Number = 1;
	public static const EXITGAME:Number = 2;
}